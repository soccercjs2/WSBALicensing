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

        public AddressCountry GetAddressCountry(string amsCode)
        {
            return _addressWorker.GetAddressCountry(amsCode);
        }

        public AddressCountry GetAddressCountry(int id)
        {
            return _addressWorker.GetAddressCountry(id);
        }

        public ICollection<AddressCountry> GetAddressCountries()
        {
            return _addressWorker.GetAddressCountries();
        }

        public AddressState GetAddressState(string countryCode, string amsCode)
        {
            return _addressWorker.GetAddressState(countryCode, amsCode);
        }

        public ICollection<AddressState> GetAddressStates()
        {
            return _addressWorker.GetAddressStates();
        }

        public ICollection<AddressState> GetAddressStates(string countryCode)
        {
            return _addressWorker.GetAddressStates(countryCode);
        }

        public Address GetPrimaryAddress(License license)
        {
            //get primary address type
            AddressType primaryAddressType = _addressWorker.GetAddressType("OFFICE");

            if (license.Addresses == null) { return null; }
            
            //return address
            return license.Addresses.Where(a => a.AddressTypeId == primaryAddressType.AddressTypeId).FirstOrDefault();
        }

        public Address GetHomeAddress(License license)
        {
            //get home address type
            AddressType homeAddressType = _addressWorker.GetAddressType("HOME");

            if (license.Addresses == null) { return null; }

            //return address
            return license.Addresses.Where(a => a.AddressTypeId == homeAddressType.AddressTypeId).FirstOrDefault();
        }

        public Address GetAgentOfServiceAddress(License license)
        {
            //get agent of service address
            AddressType agentOfServiceAddressType = _addressWorker.GetAddressType("AGENTOFSERVICE");

            if (license.Addresses == null) { return null; }

            //return address
            return license.Addresses.Where(a => a.AddressTypeId == agentOfServiceAddressType.AddressTypeId).FirstOrDefault();
        }

        public void SetAddress(Address address)
        {
            address.Confirmed = true;
            _addressWorker.SetAddress(address);
        }

        public void DeleteAddress(Address address)
        {
            _addressWorker.DeleteAddress(address);
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

            if (primaryAddress != null && primaryAddress.State != null && primaryAddress.State.AmsCode == "WA") { return false; }
            if (homeAddress != null && homeAddress.State != null && homeAddress.State.AmsCode == "WA") { return false; }

            return true;
        }

        public IList<AddressType> GetAmsAddressTypes()
        {
            IList<AddressType> options = new List<AddressType>();
            var codes = WSBA.AMS.CodeTypesManager.GetAddressTypeCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new AddressType() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetAddressType(AddressType option)
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

            _addressWorker.SetAddressType(option);
        }

        public void DeleteAddressType(AddressType option)
        {
            _addressWorker.DeleteAddressType(option);
        }

        public IList<AddressType> GetAddressTypesToBeAdded(ICollection<AddressType> codes, ICollection<AddressType> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<AddressType> GetAddressTypesToBeActivated(ICollection<AddressType> codes, ICollection<AddressType> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<AddressType> GetAddressTypesToBeChanged(ICollection<AddressType> codes, ICollection<AddressType> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<AddressType> GetAddressTypesToBeDeactivated(ICollection<AddressType> codes, ICollection<AddressType> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<AddressType> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<AddressType> codesToDeactivate = new List<AddressType>();

            foreach (AddressType option in codesToRemove)
            {
                ICollection<Address> responsesWithOption = _addressWorker.GetResponsesWithAddressType(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<AddressType> GetAddressTypesToBeDeleted(ICollection<AddressType> codes, ICollection<AddressType> amsCodes)
        {
            IList<AddressType> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<AddressType> codesToDeleted = new List<AddressType>();

            foreach (AddressType option in codesToRemove)
            {
                ICollection<Address> responsesWithOption = _addressWorker.GetResponsesWithAddressType(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public IList<AddressCountry> GetAmsAddressCountries()
        {
            IList<AddressCountry> options = new List<AddressCountry>();
            var codes = WSBA.AMS.CodeTypesManager.GetCountryList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new AddressCountry() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetAddressCountry(AddressCountry country)
        {
            if (country.AddressCountryId == 0)
            {
                AddressCountry existingCode = _addressWorker.GetAddressCountry(country.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = country.Name;
                    country = existingCode;
                }
            }

            _addressWorker.SetAddressCountry(country);
        }

        public void DeleteAddressCountry(AddressCountry country)
        {
            _addressWorker.DeleteAddressCountry(country);
        }

        public IList<AddressCountry> GetAddressCountriesToBeAdded(ICollection<AddressCountry> codes, ICollection<AddressCountry> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<AddressCountry> GetAddressCountriesToBeActivated(ICollection<AddressCountry> codes, ICollection<AddressCountry> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<AddressCountry> GetAddressCountriesToBeChanged(ICollection<AddressCountry> codes, ICollection<AddressCountry> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<AddressCountry> GetAddressCountriesToBeDeactivated(ICollection<AddressCountry> codes, ICollection<AddressCountry> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<AddressCountry> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<AddressCountry> codesToDeactivate = new List<AddressCountry>();

            foreach (AddressCountry option in codesToRemove)
            {
                ICollection<Address> responsesWithOption = _addressWorker.GetResponsesWithAddressCountry(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<AddressCountry> GetAddressCountriesToBeDeleted(ICollection<AddressCountry> codes, ICollection<AddressCountry> amsCodes)
        {
            IList<AddressCountry> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<AddressCountry> codesToDeleted = new List<AddressCountry>();

            foreach (AddressCountry option in codesToRemove)
            {
                ICollection<Address> responsesWithOption = _addressWorker.GetResponsesWithAddressCountry(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public IList<AddressState> GetAmsAddressStates()
        {
            IList<AddressState> options = new List<AddressState>();
            var codes = WSBA.AMS.CodeTypesManager.GetStatesProvinces().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new AddressState() { Name = code.Description, AmsCountryCode = code.CountryCode, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetAddressState(AddressState state)
        {
            if (state.AddressStateId == 0)
            {
                AddressState existingCode = _addressWorker.GetAddressState(state.AmsCountryCode, state.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = state.Name;
                    state = existingCode;
                }
            }

            _addressWorker.SetAddressState(state);
        }

        public void DeleteAddressState(AddressState state)
        {
            _addressWorker.DeleteAddressState(state);
        }

        public IList<AddressState> GetAddressStatesToBeAdded(ICollection<AddressState> codes, ICollection<AddressState> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode && c.AmsCountryCode == ac.AmsCountryCode)).ToList();
        }

        public IList<AddressState> GetAddressStatesToBeActivated(ICollection<AddressState> codes, ICollection<AddressState> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode && c.AmsCountryCode == ac.AmsCountryCode)).ToList();
        }

        public IList<AddressState> GetAddressStatesToBeChanged(ICollection<AddressState> codes, ICollection<AddressState> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.AmsCountryCode == ac.AmsCountryCode && c.Name != ac.Name)).ToList();
        }

        public IList<AddressState> GetAddressStatesToBeDeactivated(ICollection<AddressState> codes, ICollection<AddressState> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<AddressState> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode && ac.AmsCountryCode == c.AmsCountryCode)).ToList();
            IList<AddressState> codesToDeactivate = new List<AddressState>();

            foreach (AddressState option in codesToRemove)
            {
                ICollection<Address> responsesWithOption = _addressWorker.GetResponsesWithAddressState(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<AddressState> GetAddressStatesToBeDeleted(ICollection<AddressState> codes, ICollection<AddressState> amsCodes)
        {
            IList<AddressState> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode && ac.AmsCountryCode == c.AmsCountryCode)).ToList();
            IList<AddressState> codesToDeleted = new List<AddressState>();

            foreach (AddressState option in codesToRemove)
            {
                ICollection<Address> responsesWithOption = _addressWorker.GetResponsesWithAddressState(option);
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
                requirementType = license.LicenseType.LicenseTypeRequirement.PrimaryAddress;
            }
            else if (addressType == "Home")
            {
                address = GetHomeAddress(license);
                requirementType = license.LicenseType.LicenseTypeRequirement.HomeAddress;
            }
            else if (addressType == "Agent of Service")
            {
                address = GetAgentOfServiceAddress(license);
                if (license.LicenseType.LicenseTypeRequirement.AgentOfServiceAddress == RequirementType.Required && !AgentOfServiceAddressRequired(license))
                {
                    requirementType = RequirementType.Excluded;
                }
                else
                {
                    requirementType = license.LicenseType.LicenseTypeRequirement.AgentOfServiceAddress;
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
