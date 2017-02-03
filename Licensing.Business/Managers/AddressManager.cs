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

        public Address GetPrimaryAddress(License license)
        {
            //get primary address type
            AddressType primaryAddressType = _context.AddressTypes.Where(at => at.Name == "Primary").FirstOrDefault();

            //return address
            return _addressWorker.GetAddress(license, primaryAddressType);
        }

        public Address GetHomeAddress(License license)
        {
            //get home address type
            AddressType homeAddressType = _context.AddressTypes.Where(at => at.Name == "Home").FirstOrDefault();

            //return address
            return _addressWorker.GetAddress(license, homeAddressType);
        }

        public Address GetAgentOfServiceAddress(License license)
        {
            //get agent of service address
            AddressType agentOfServiceAddressType = _context.AddressTypes.Where(at => at.Name == "Agent Of Service").FirstOrDefault();

            //return address
            return _addressWorker.GetAddress(license, agentOfServiceAddressType);
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

        public DashboardContainerVM GetDashboardContainerVM(Address address)
        {
            RouteContainer editRoute = new RouteContainer("Address", "Edit", address.AddressId);
            RouteContainer confirmRoute = new RouteContainer("Address", "Confirm", address.AddressId);

            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(address.LicenseId);

            RequirementType requirementType = RequirementType.Excluded;

            if (address.AddressType.Name == "Primary") { requirementType = license.LicenseType.PrimaryAddress; }
            if (address.AddressType.Name == "Home") { requirementType = license.LicenseType.HomeAddress; }

            if (address.AddressType.Name == "Agent of Service")
            {
                if (license.LicenseType.AgentOfServiceAddress == RequirementType.Required && !AgentOfServiceAddressRequired(license))
                {
                    requirementType = RequirementType.Excluded;
                }
                else
                {
                    requirementType = license.LicenseType.AgentOfServiceAddress;
                }
            }

            return new DashboardContainerVM(
                address.AddressType.Name,
                requirementType,
                IsComplete(address),
                editRoute,
                confirmRoute,
                null,
                "_Address",
                address
            );
        }

        public bool AgentOfServiceAddressRequired(License license)
        {
            Address primaryAddress = GetPrimaryAddress(license);
            Address homeAddress = GetHomeAddress(license);

            if (primaryAddress != null && primaryAddress.State == "WA") { return false; }
            if (homeAddress != null && homeAddress.State == "WA") { return false; }

            return true;
        }
    }
}
