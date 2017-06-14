using Licensing.Data.Context;
using Licensing.Data.Workers;
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
    public class OrderManager
    {
        private LicensingContext _context;
        private OrderWorker _orderWorker;

        public OrderManager(LicensingContext context)
        {
            _context = context;
            _orderWorker = new OrderWorker(context);
        }

        public void SetLicenseOrder(License license, int orderNumber)
        {
            if (orderNumber == 0)
            {
                //delete payments
                if (license.LicensingOrder != null) { DeletePayments(license.LicensingOrder); }

                //delete order
                DeleteOrder(license.LicensingOrder);
            }
            else
            {
                if (license.LicensingOrder == null) { license.LicensingOrder = new Order(); }

                license.LicensingOrder.AmsOrderNumber = orderNumber;
                _context.SaveChanges();
            }
        }

        public void SetSectionOrder(License license, int orderNumber)
        {
            if (orderNumber == 0)
            {
                //delete payments
                if (license.SectionOrder != null) { DeletePayments(license.SectionOrder); }

                //delete order
                DeleteOrder(license.SectionOrder);
            }
            else
            {
                if (license.SectionOrder == null) { license.SectionOrder = new Order(); }

                license.SectionOrder.AmsOrderNumber = orderNumber;
                _context.SaveChanges();
            }
        }

        public void DeleteOrder(Order order)
        {
            //delete payments for the order
            DeletePayments(order);

            //delete order
            _orderWorker.DeleteOrder(order);
        }

        public void DeletePayments(Order order)
        {
            //load payments for order
            List<Transaction> transactions = order.Transactions.ToList();

            //loop through payments and delete them
            transactions.ForEach(t => order.Transactions.Remove(t));
            _context.SaveChanges();
        }
    }
}
