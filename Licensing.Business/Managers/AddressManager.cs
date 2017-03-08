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

        public AddressType GetAddressType(string addressType)
        {
            return _addressWorker.GetAddressType(addressType);
        }

        public Address GetPrimaryAddress(License license)
        {
            //get primary address type
            AddressType primaryAddressType = _addressWorker.GetAddressType("Primary");

            //return address
            return license.Addresses.Where(a => a.AddressTypeId == primaryAddressType.AddressTypeId).FirstOrDefault();
        }

        public Address GetHomeAddress(License license)
        {
            //get home address type
            AddressType homeAddressType = _addressWorker.GetAddressType("Home");

            //return address
            return license.Addresses.Where(a => a.AddressTypeId == homeAddressType.AddressTypeId).FirstOrDefault();
        }

        public Address GetAgentOfServiceAddress(License license)
        {
            //get agent of service address
            AddressType agentOfServiceAddressType = _addressWorker.GetAddressType("Agent Of Service");

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
