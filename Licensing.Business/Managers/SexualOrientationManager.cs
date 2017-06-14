using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using Licensing.Domain.SexualOrientations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class SexualOrientationManager
    {
        private LicensingContext _context;
        private SexualOrientationWorker _sexualOrientationWorker;

        public SexualOrientationManager(LicensingContext context)
        {
            _context = context;
            _sexualOrientationWorker = new SexualOrientationWorker(context);
        }

        public void DeleteSexualOrientation(License license)
        {
            _sexualOrientationWorker.DeleteSexualOrientation(license.SexualOrientation);
        }

        public ICollection<SexualOrientationOption> GetOptions()
        {
            return _sexualOrientationWorker.GetOptions();
        }

        public SexualOrientationOption GetOption(string code)
        {
            return _sexualOrientationWorker.GetOption(code);
        }

        public void SetSexualOrientation(License license, int optionId)
        {
            SexualOrientationOption option = _sexualOrientationWorker.GetOption(optionId);

            if (license.SexualOrientation == null)
            {
                license.SexualOrientation = new SexualOrientation();
            }

            license.SexualOrientation.Option = option;

            _context.SaveChanges();
        }

        public void SetSexualOrientation(License license, SexualOrientationOption option)
        {
            if (license.SexualOrientation == null)
            {
                license.SexualOrientation = new SexualOrientation();
            }

            license.SexualOrientation.Option = option;

            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return license.SexualOrientation != null;
        }

        public IList<SexualOrientationOption> GetAmsOptions()
        {
            IList<SexualOrientationOption> options = new List<SexualOrientationOption>();
            var codes = WSBA.AMS.CodeTypesManager.GetSexualOrientationCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new SexualOrientationOption() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetOption(SexualOrientationOption option)
        {
            if (option.SexualOrientationOptionId == 0)
            {
                SexualOrientationOption existingCode = _sexualOrientationWorker.GetOption(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _sexualOrientationWorker.SetOption(option);
        }

        public void DeleteOption(SexualOrientationOption option)
        {
            _sexualOrientationWorker.DeleteOption(option);
        }

        public IList<SexualOrientationOption> GetCodesToBeAdded(ICollection<SexualOrientationOption> codes, ICollection<SexualOrientationOption> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<SexualOrientationOption> GetCodesToBeActivated(ICollection<SexualOrientationOption> codes, ICollection<SexualOrientationOption> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<SexualOrientationOption> GetCodesToBeChanged(ICollection<SexualOrientationOption> codes, ICollection<SexualOrientationOption> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<SexualOrientationOption> GetCodesToBeDeactivated(ICollection<SexualOrientationOption> codes, ICollection<SexualOrientationOption> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<SexualOrientationOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<SexualOrientationOption> codesToDeactivate = new List<SexualOrientationOption>();

            foreach (SexualOrientationOption option in codesToRemove)
            {
                ICollection<SexualOrientation> responsesWithOption = _sexualOrientationWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<SexualOrientationOption> GetCodesToBeDeleted(ICollection<SexualOrientationOption> codes, ICollection<SexualOrientationOption> amsCodes)
        {
            IList<SexualOrientationOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<SexualOrientationOption> codesToDeleted = new List<SexualOrientationOption>();

            foreach (SexualOrientationOption option in codesToRemove)
            {
                ICollection<SexualOrientation> responsesWithOption = _sexualOrientationWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }
    }
}
