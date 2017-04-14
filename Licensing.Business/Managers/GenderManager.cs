using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Genders;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class GenderManager
    {
        private LicensingContext _context;
        private GenderWorker _genderWorker;

        public GenderManager(LicensingContext context)
        {
            _context = context;
            _genderWorker = new GenderWorker(context);
        }

        public ICollection<GenderOption> GetOptions()
        {
            return _genderWorker.GetOptions();
        }

        public GenderOption GetOption(string code)
        {
            return _genderWorker.GetOption(code);
        }

        public void SetGender(License license, int optionId)
        {
            GenderOption option = _genderWorker.GetOption(optionId);

            if (license.Gender == null)
            {
                license.Gender = new Gender();
            }

            license.Gender.Option = option;

            _context.SaveChanges();
        }

        public void SetGender(License license, GenderOption option)
        {
            if (license.Gender == null)
            {
                license.Gender = new Gender();
            }

            license.Gender.Option = option;

            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return license.Gender != null;
        }

        public IList<GenderOption> GetAmsOptions()
        {
            IList<GenderOption> options = new List<GenderOption>();
            var codes = WSBA.AMS.CodeTypesManager.GetGenderCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new GenderOption() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetOption(GenderOption option)
        {
            if (option.GenderOptionId == 0)
            {
                GenderOption existingCode = _genderWorker.GetOption(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _genderWorker.SetOption(option);
        }

        public void DeleteOption(GenderOption option)
        {
            _genderWorker.DeleteOption(option);
        }

        public IList<GenderOption> GetCodesToBeAdded(ICollection<GenderOption> codes, ICollection<GenderOption> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<GenderOption> GetCodesToBeActivated(ICollection<GenderOption> codes, ICollection<GenderOption> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<GenderOption> GetCodesToBeChanged(ICollection<GenderOption> codes, ICollection<GenderOption> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<GenderOption> GetCodesToBeDeactivated(ICollection<GenderOption> codes, ICollection<GenderOption> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<GenderOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<GenderOption> codesToDeactivate = new List<GenderOption>();

            foreach (GenderOption option in codesToRemove)
            {
                ICollection<Gender> responsesWithOption = _genderWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<GenderOption> GetCodesToBeDeleted(ICollection<GenderOption> codes, ICollection<GenderOption> amsCodes)
        {
            IList<GenderOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<GenderOption> codesToDeleted = new List<GenderOption>();

            foreach (GenderOption option in codesToRemove)
            {
                ICollection<Gender> responsesWithOption = _genderWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }
    }
}
