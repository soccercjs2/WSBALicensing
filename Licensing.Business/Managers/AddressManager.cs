using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Addresses;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class AddressManager
    {
        private LicensingContext _context;
        private AddressWorker _addressWorker;

        public AddressManager(LicensingContext context)
        {
            _context = context;
            _addressWorker = new AddressWorker(context);
        }

        public Address GetAddress(int addressId)
        {
            return _addressWorker.GetAddress(addressId);
        }

        public AddressType GetAddressType(string amsCode)
        {
            return _addressWorker.GetAddressType(amsCode);
        }

        public ICollection<AddressType> GetAddressTypes()
        {
            return _addressWorker.GetAddressTypes();
        }

        public Address GetPrimaryAddress(License license)
        {
            //get primary address type
            AddressType primaryAddressType = _addressWorker.GetAddressType("OFFICE");

            //return address
            return license.Addresses.Where(a => a.AddressTypeId == primaryAddressType.AddressTypeId).FirstOrDefault();
        }

        public Address GetHomeAddress(License license)
        {
            //get home address type
            AddressType homeAddressType = _addressWorker.GetAddressType("HOME");

            //return address
            return license.Addresses.Where(a => a.AddressTypeId == homeAddressType.AddressTypeId).FirstOrDefault();
        }

        public Address GetAgentOfServiceAddress(License license)
        {
            //get agent of service address
            AddressType agentOfServiceAddressType = _addressWorker.GetAddressType("AGENTOFSERVICE");

            //return address
            return license.Addresses.Where(a => a.AddressTypeId == agentOfServiceAddressType.AddressTypeId).FirstOrDefault();
        }

        public void SetAddress(Address address)
        {
            address.Confirmed = true;
            _addressWorker.SetAddress(address);
        }

        public void Confirm(Address address)
        {
            address.Confirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(Address address)
        {
            return (address != null && address.Confirmed);
        }

        public bool AgentOfServiceAddressRequired(License license)
        {
            Address primaryAddress = GetPrimaryAddress(license);
            Address homeAddress = GetHomeAddress(license);

            if (primaryAddress != null && primaryAddress.State == "WA") { return false; }
            if (homeAddress != null && homeAddress.State == "WA") { return false; }

            return true;
        }

        public IList<AddressType> GetAmsOptions()
        {
            IList<AddressType> options = new List<AddressType>();
            var codes = WSBA.AMS.CodeTypesManager.GetAddressTypeCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new AddressType() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetOption(AddressType option)
        {
            if (option.AddressTypeId == 0)
            {
                AddressType existingCode = _addressWorker.GetAddressType(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _addressWorker.SetOption(option);
        }

        public void DeleteOption(AddressType option)
        {
            _addressWorker.DeleteOption(option);
        }

        public IList<AddressType> GetCodesToBeAdded(ICollection<AddressType> codes, ICollection<AddressType> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<AddressType> GetCodesToBeActivated(ICollection<AddressType> codes, ICollection<AddressType> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<AddressType> GetCodesToBeChanged(ICollection<AddressType> codes, ICollection<AddressType> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<AddressType> GetCodesToBeDeactivated(ICollection<AddressType> codes, ICollection<AddressType> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<AddressType> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<AddressType> codesToDeactivate = new List<AddressType>();

            foreach (AddressType option in codesToRemove)
            {
                ICollection<Address> responsesWithOption = _addressWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<AddressType> GetCodesToBeDeleted(ICollection<AddressType> codes, ICollection<AddressType> amsCodes)
        {
            IList<AddressType> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<AddressType> codesToDeleted = new List<AddressType>();

            foreach (AddressType option in codesToRemove)
            {
                ICollection<Address> responsesWithOption = _addressWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license, string addressType)
        {
            Address address = null;
            RequirementType requirementType = RequirementType.Excluded;

            if (addressType == "Primary")
            {
                address = GetPrimaryAddress(license);
                requirementType = license.LicenseType.PrimaryAddress;
            }
            else if (addressType == "Home")
            {
                address = GetHomeAddress(license);
                requirementType = license.LicenseType.HomeAddress;
            }
            else if (addressType == "Agent of Service")
            {
                address = GetAgentOfServiceAddress(license);
                if (license.LicenseType.AgentOfServiceAddress == RequirementType.Required && !AgentOfServiceAddressRequired(license))
                {
                    requirementType = RequirementType.Excluded;
                }
                else
                {
                    requirementType = license.LicenseType.AgentOfServiceAddress;
                }
            }

            RouteContainer editRoute = null;
            RouteContainer confirmRoute = null;

            if (address != null)
            {
                editRoute = new RouteContainer("Address", "Edit", address.AddressId);

                if (license.LicenseType.Name == "Judicial")
                {
                    confirmRoute = new RouteContainer("Address", "Confirm", address.AddressId);
                }
            }
            else
            {
                if (addressType == "Primary") { editRoute = new RouteContainer("Address", "CreatePrimary", license.LicenseId); }
                else if (addressType == "Home") { editRoute = new RouteContainer("Address", "CreateHome", license.LicenseId); }
                else if (addressType == "Agent of Service") { editRoute = new RouteContainer("Address", "CreateAgentOfService", license.LicenseId); }
            }

            return new DashboardContainerVM(
                addressType + " Address",
                requirementType,
                IsComplete(address),
                editRoute,
                confirmRoute,
                false,
                "_Address",
                address
            );
        }
    }
}
