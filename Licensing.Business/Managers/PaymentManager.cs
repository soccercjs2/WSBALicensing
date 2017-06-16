using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Donations;
using Licensing.Domain.Licenses;
using Licensing.Domain.Orders;
using Licensing.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class PaymentManager
    {
        private LicensingContext _context;
        private PaymentWorker _paymentWorker;

        public PaymentManager(LicensingContext context)
        {
            _context = context;
            _paymentWorker = new PaymentWorker(context);
        }

        public void SetTransaction(Order order, string amsCode, int amsTransactionId, decimal amount, DateTime transactionDate)
        {
            var transaction =
                (order.Transactions != null) ?
                order.Transactions.Where(p => p.AmsTransactionId == amsTransactionId).FirstOrDefault() :
                null;

            if (transaction == null)
            {
                transaction = new Transaction() { AmsCode = amsCode, AmsTransactionId = amsTransactionId, TransactionDate = transactionDate };

                if (order.Transactions == null) { order.Transactions = new List<Transaction>(); }
                order.Transactions.Add(transaction);
            }

            transaction.Amount = amount;
            _context.SaveChanges();
        }

        public void DeleteTransactions(Order order, string amsCode)
        {
            if (order.Transactions != null)
            {
                var transactions = order.Transactions.Where(p => p.AmsCode == amsCode).ToList();

                if (transactions != null)
                {
                    foreach (var transaction in transactions)
                    {
                        order.Transactions.Remove(transaction);
                        _context.SaveChanges();
                    }
                }
            }
        }

        public void DeleteTransaction(Order order, int amsTransactionId)
        {
            if (order.Transactions != null)
            {
                var transaction = order.Transactions.Where(p => p.AmsTransactionId == amsTransactionId).FirstOrDefault();

                if (transaction != null)
                {
                    order.Transactions.Remove(transaction);
                    _context.SaveChanges();
                }
            }
        }

        public byte[] GenerateInvoice(License license, string basePath, CheckoutVM checkoutVM)
        {
            //determine title
            string title;

            if (license.PreviousLicenseType.LicenseTypeId != license.LicenseType.LicenseTypeId)
            {
                title = license.LicensePeriod.EndDate.Year + " LICENSING INVOICE AND STATUS CHANGE";
            }
            else
            {
                title = license.LicensePeriod.EndDate.Year + " LICENSING INVOICE";
            }

            //create an invoice builder ready for orders
            WSBA_Invoice_Builder.InvoiceBuilder invoiceBuilder = new WSBA_Invoice_Builder.InvoiceBuilder(
                title,
                license.Customer.FirstName + " " + license.Customer.LastName,
                license.Customer.BarNumber,
                basePath + "Images\\WSBA_logo.png",
                basePath + "Fonts\\C39_60HB.ttf"
            );

            //set customer header message
            invoiceBuilder.CustomHeaderMessage = "Payment must be postmarked or delivered by " +
                license.LicensePeriod.LateFeeDate.AddDays(-1).ToShortDateString() + ".";

            //create license order and add license/donation products
            if (checkoutVM.LicenseProducts != null || checkoutVM.DonationProducts != null)
            {
                WSBA_Invoice_Builder.Order licensingOrder = invoiceBuilder.AddOrder();
                BuildLicensingInvoice(ref licensingOrder, license.LicensingOrder, checkoutVM.LicenseProducts, checkoutVM.DonationProducts);
            }

            //create section order and add section products
            if (checkoutVM.SectionProducts != null)
            {
                WSBA_Invoice_Builder.Order sectionsOrder = invoiceBuilder.AddOrder();
                BuildSectionInvoice(ref sectionsOrder, license.SectionOrder, checkoutVM.SectionProducts);
            }

            //build instructions
            BuildInstructions(license, ref invoiceBuilder, checkoutVM);

            return invoiceBuilder.GetInvoice();
        }

        private void BuildLicensingInvoice(ref WSBA_Invoice_Builder.Order invoiceLicensingOrder, Order licensingOrder,
            ICollection<LicenseProductVM> licenseProductVMs, ICollection<DonationProductVM> donationProductVMs)
        {
            //set order titles
            invoiceLicensingOrder.Title = "Licensing Order";

            //set order number if order exists
            if (licensingOrder != null)
            {
                invoiceLicensingOrder.OrderNumber = licensingOrder.AmsOrderNumber.ToString();
            }

            //add license products to license order
            if (licenseProductVMs != null)
            {
                foreach (var licenseProductVM in licenseProductVMs)
                {
                    invoiceLicensingOrder.AddLineItem(licenseProductVM.LicenseProduct.Name, "$" + licenseProductVM.Price);
                }
            }

            //add donation products to license order
            if (donationProductVMs != null)
            {
                foreach (var donationProductVM in donationProductVMs)
                {
                    invoiceLicensingOrder.AddLineItem(donationProductVM.Name, "$" + donationProductVM.Amount);
                }
            }
        }

        private void BuildSectionInvoice(ref WSBA_Invoice_Builder.Order invoiceSectionOrder, Order sectionOrder, ICollection<SectionProductVM> sectionProductVMs)
        {
            //set order titles
            invoiceSectionOrder.Title = "Sections Order";

            //set order number if order exists
            if (sectionOrder != null)
            {
                invoiceSectionOrder.OrderNumber = sectionOrder.AmsOrderNumber.ToString();
            }

            //add section products to section order
            if (sectionProductVMs != null)
            {
                foreach (var sectionProductVM in sectionProductVMs)
                {
                    invoiceSectionOrder.AddLineItem(sectionProductVM.Name, "$" + sectionProductVM.Price);
                }
            }
        }

        private void BuildInstructions(License license, ref WSBA_Invoice_Builder.InvoiceBuilder invoiceBuilder, CheckoutVM checkoutVM)
        {
            BuildLicensingOrderInstructions(license, ref invoiceBuilder, checkoutVM);
            BuildSectionOrderInstructions(license, ref invoiceBuilder, checkoutVM);
            BuildStatusChangeInstructions(license, ref invoiceBuilder);
            BuildOrderCleanupInstructions(license, ref invoiceBuilder);
        }

        private void BuildLicensingOrderInstructions(License license, ref WSBA_Invoice_Builder.InvoiceBuilder invoiceBuilder, CheckoutVM checkoutVM)
        {
            //build instructions for licensing order
            if (license.LicensingOrder == null)
            {
                invoiceBuilder.AddInstruction("Create the above licensing order for the member.");
            }
            else
            {
                //check if licensing order has each licensing product in ams, if it doesn't, add instruction to add product to ams licensing order
                if (checkoutVM.LicenseProducts != null)
                {
                    foreach (var licenseProductVM in checkoutVM.LicenseProducts)
                    {
                        //check if ams licensing order has product
                        bool orderHasProduct = WSBA.AMS.OrderManager.OrderHasProduct(license.LicensingOrder.AmsOrderNumber.ToString(), licenseProductVM.LicenseProduct.AmsCode);

                        //if it doesn't, add instruction to add product to ams licensing order
                        if (!orderHasProduct)
                        {
                            invoiceBuilder.AddInstruction(String.Format("Add {0} product to licensing order ({1}) with an amount of ${2}.",
                                licenseProductVM.LicenseProduct.AmsCode, license.LicensingOrder.AmsOrderNumber, licenseProductVM.Price));
                        }
                    }
                }

                //check if licensing order has each donation product in ams, if it doesn't, add instruction to add product to ams licensing order
                if (checkoutVM.DonationProducts != null)
                {
                    foreach (var donationProductVM in checkoutVM.DonationProducts)
                    {
                        //check if ams licensing order has product
                        bool orderHasProduct = WSBA.AMS.OrderManager.OrderHasProduct(license.LicensingOrder.AmsOrderNumber.ToString(), donationProductVM.AmsCode);

                        //if it doesn't, add instruction to add product to ams licensing order
                        if (!orderHasProduct)
                        {
                            invoiceBuilder.AddInstruction(String.Format("Add {0} product to licensing order ({1}) with an amount of ${2}.",
                                donationProductVM.AmsCode, license.LicensingOrder.AmsOrderNumber, donationProductVM.Amount));
                        }
                    }
                }
            }
        }

        private void BuildSectionOrderInstructions(License license, ref WSBA_Invoice_Builder.InvoiceBuilder invoiceBuilder, CheckoutVM checkoutVM)
        {
            //build instructions for section order
            if (license.SectionOrder == null)
            {
                invoiceBuilder.AddInstruction("Create the above section order for the member.");
            }
            else
            {
                //check if section order has each section product in ams, if it doesn't, add instruction to add product to ams section order
                if (checkoutVM.SectionProducts != null)
                {
                    foreach (var sectionProductVM in checkoutVM.SectionProducts)
                    {
                        //check if ams licensing order has product
                        bool orderHasProduct = WSBA.AMS.OrderManager.OrderHasProduct(license.SectionOrder.AmsOrderNumber.ToString(), sectionProductVM.AmsCode);

                        //if it doesn't, add instruction to add product to ams licensing order
                        if (!orderHasProduct)
                        {
                            invoiceBuilder.AddInstruction(String.Format("Add {0} product to licensing order ({1}) with an amount of ${2}.",
                                sectionProductVM.AmsCode, license.SectionOrder.AmsOrderNumber, sectionProductVM.Price));
                        }
                    }
                }
            }
        }

        private void BuildStatusChangeInstructions(License license, ref WSBA_Invoice_Builder.InvoiceBuilder invoiceBuilder)
        {
            //build instructions for a status change
            if (license.LicenseType.LicenseTypeId != license.PreviousLicenseType.LicenseTypeId)
            {
                invoiceBuilder.AddInstruction(String.Format("The member has elected to change their membership type. Please change their membership type from {0} to {1}",
                    license.PreviousLicenseType.Name, license.LicenseType.Name));
            }
        }

        private void BuildOrderCleanupInstructions(License license, ref WSBA_Invoice_Builder.InvoiceBuilder invoiceBuilder)
        {
            //get cycle end date
            DateTime cycleEndDate = new DateTime(license.LicensePeriod.EndDate.Year, 12, 31);

            //create master customer id
            string masterCustomerId = "000000000000".Substring(0, 12 - license.Customer.BarNumber.Length) + license.Customer.BarNumber;

            //build instructions for cleaning up extra orders with balance
            var ordersNumberToCancel = WSBA.AMS.OrderManager.GetLicensingOrderNumbersToCancel(
                masterCustomerId,
                cycleEndDate,
                (license.LicensingOrder != null) ? license.LicensingOrder.AmsOrderNumber.ToString() : "",
                (license.SectionOrder != null) ? license.SectionOrder.AmsOrderNumber.ToString() : "");

            if (ordersNumberToCancel != null)
            {
                foreach (var orderNumber in ordersNumberToCancel)
                {
                    invoiceBuilder.AddInstruction(String.Format("Cancel order with order number {0}", orderNumber));
                }
            }
        }

        public void PayWithCreditCard(License license, CheckoutVM checkoutVM)
        {
            OrderManager orderManager = new OrderManager(_context);
            
            //create master customer id
            string masterCustomerId = "000000000000".Substring(0, 12 - license.Customer.BarNumber.Length) + license.Customer.BarNumber;

            //has balance flags
            bool hasLicensingBalance = false;
            bool hasSectionBalance = false;

            //create license order and add license/donation products
            if (checkoutVM.LicenseProducts != null || checkoutVM.DonationProducts != null)
            {
                hasLicensingBalance = true;
                
                //initialize order product comments list
                var orderProductComments = new List<WSBA.AMS.OrderProductComment>();

                //create order product comments for licensing products
                if (checkoutVM.LicenseProducts != null)
                {
                    foreach (var licenseProductVM in checkoutVM.LicenseProducts)
                    {
                        var orderProductComment = new WSBA.AMS.OrderProductComment();
                        orderProductComment.ProductId = licenseProductVM.LicenseProduct.AmsProductId;

                        if (licenseProductVM.PrimaryProduct && checkoutVM.HasKellerDeduction)
                        {
                            orderProductComment.ProductDiscountId = license.LicenseType.KellerDiscount.AmsProductDiscountId;
                        }

                        if (licenseProductVM.LateFeeProduct) { orderProductComment.ItemAmount = licenseProductVM.Price; }

                        orderProductComments.Add(orderProductComment);
                    }
                }

                //create order product comments for donation products
                if (checkoutVM.DonationProducts != null)
                {
                    foreach (var donationProductVM in checkoutVM.DonationProducts)
                    {
                        var orderProductComment = new WSBA.AMS.OrderProductComment();
                        orderProductComment.ProductId = donationProductVM.AmsProductId;
                        orderProductComment.ItemAmount = donationProductVM.Amount;

                        orderProductComments.Add(orderProductComment);
                    }
                }

                if (orderProductComments.Count > 0)
                {
                    //instantiate variables for ams methods
                    string orderNumber = null;
                    string errorMessage = null;
                    bool success = true;

                    if (license.LicensingOrder != null)
                    {
                        orderNumber = license.LicensingOrder.AmsOrderNumber.ToString();

                        //if licensing order exists, add products to existing order
                        success = WSBA.AMS.OrderManager.AddAdditionalProducts(orderNumber, orderProductComments, ref errorMessage);
                    }
                    else
                    {
                        //if licensing order does not exist, create new order with products
                        success = WSBA.AMS.OrderManager.CreateOrder(masterCustomerId, orderProductComments, ref orderNumber, ref errorMessage);
                    }

                    if (success)
                    {
                        orderManager.SetLicenseOrder(license, Convert.ToInt32(orderNumber));
                    }
                    else
                    {
                        //do error message stuff
                    }
                }
            }

            //create section order and add section products
            if (checkoutVM.SectionProducts != null)
            {
                hasSectionBalance = true;
                
                //initialize order product comments list
                var orderProductComments = new List<WSBA.AMS.OrderProductComment>();

                //create order with products
                if (checkoutVM.SectionProducts != null)
                {
                    foreach (var sectionProductVM in checkoutVM.SectionProducts)
                    {
                        var orderProductComment = new WSBA.AMS.OrderProductComment();
                        orderProductComment.ProductId = sectionProductVM.AmsProductId;

                        orderProductComments.Add(orderProductComment);
                    }
                }

                //instantiate variables for ams methods
                string orderNumber = null;
                string errorMessage = null;
                bool success = true;

                //update/create order if there are products that needed to be added
                if (orderProductComments.Count > 0)
                {
                    if (license.SectionOrder != null)
                    {
                        orderNumber = license.SectionOrder.AmsOrderNumber.ToString();

                        //if licensing order exists, add products to existing order
                        success = WSBA.AMS.OrderManager.AddAdditionalProducts(orderNumber, orderProductComments, ref errorMessage);
                    }
                    else
                    {
                        //if licensing order does not exist, create new order with products
                        success = WSBA.AMS.OrderManager.CreateOrder(masterCustomerId, orderProductComments, ref orderNumber, ref errorMessage);
                    }

                    if (success)
                    {
                        orderManager.SetSectionOrder(license, Convert.ToInt32(orderNumber));
                    }
                    else
                    {
                        //do error message stuff
                    }
                }
            }

            //track if all payments are paid successfully
            bool paymentsSuccessful = true;

            if (license.LicensingOrder != null && hasLicensingBalance)
            {
                string paymentErrorMessage = null;
                string receiptNumber = null;

                var isPaymentSuccessful = WSBA.AMS.PaymentManager.PayOrder(
                    license.LicensingOrder.AmsOrderNumber.ToString(), 
                    checkoutVM.CreditCardNumber, 
                    checkoutVM.CreditCardType,
                    checkoutVM.ExpirationMonth, 
                    checkoutVM.ExpirationYear,
                    checkoutVM.SecurityCode,
                    checkoutVM.NameOnCard, ref receiptNumber, ref paymentErrorMessage);

                if (isPaymentSuccessful)
                {
                    if (license.PreviousLicenseType.LicenseTypeId == license.LicenseType.LicenseTypeId)
                    {
                        //set new membership type
                        WSBA.AMS.MemberManager.SetMemberStatus(masterCustomerId, license.LicenseType.AmsMemberType);
                    }
                }
                else
                {
                    paymentsSuccessful = false;
                }
            }

            if (license.SectionOrder != null && hasSectionBalance)
            {
                string paymentErrorMessage = null;
                string receiptNumber = null;

                var isPaymentSuccessful = WSBA.AMS.PaymentManager.PayOrder(
                    license.SectionOrder.AmsOrderNumber.ToString(),
                    checkoutVM.CreditCardNumber,
                    checkoutVM.CreditCardType,
                    checkoutVM.ExpirationMonth,
                    checkoutVM.ExpirationYear,
                    checkoutVM.SecurityCode,
                    checkoutVM.NameOnCard, ref receiptNumber, ref paymentErrorMessage);

                if (!isPaymentSuccessful)
                {
                    paymentsSuccessful = false;
                }
            }

            if (paymentsSuccessful)
            {
                //get cycle end date
                DateTime cycleEndDate = new DateTime(license.LicensePeriod.EndDate.Year, 12, 31);

                //cancel all proforma orders
                WSBA.AMS.OrderManager.CancelLicensingOrdersWithBalance(masterCustomerId, cycleEndDate);
            }
        }
    }
}
