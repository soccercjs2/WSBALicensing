using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Disabilities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class DisabilityManager
    {
        private LicensingContext _context;
        private DisabilityWorker _disabilityWorker;

        public DisabilityManager(LicensingContext context)
        {
            _context = context;
            _disabilityWorker = new DisabilityWorker(context);
        }

        public ICollection<DisabilityOption> GetOptions()
        {
            return _disabilityWorker.GetOptions();
        }

        public DisabilityOption GetOption(string code)
        {
            return _disabilityWorker.GetOption(code);
        }

        public void SetDisability(License license, int optionId)
        {
            DisabilityOption option = _disabilityWorker.GetOption(optionId);

            if (license.Disability == null)
            {
                license.Disability = new Disability();
            }

            license.Disability.Option = option;

            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return license.Disability != null;
        }

        public IList<DisabilityOption> GetAmsOptions()
        {
            IList<DisabilityOption> options = new List<DisabilityOption>();
            var codes = WSBA.AMS.CodeTypesManager.GetDisabilityCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new DisabilityOption() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetOption(DisabilityOption option)
        {
            if (option.DisabilityOptionId == 0)
            {
                DisabilityOption existingCode = _disabilityWorker.GetOption(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _disabilityWorker.SetOption(option);
        }

        public void DeleteOption(DisabilityOption option)
        {
            _disabilityWorker.DeleteOption(option);
        }

        public IList<DisabilityOption> GetCodesToBeAdded(ICollection<DisabilityOption> codes, ICollection<DisabilityOption> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<DisabilityOption> GetCodesToBeActivated(ICollection<DisabilityOption> codes, ICollection<DisabilityOption> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<DisabilityOption> GetCodesToBeChanged(ICollection<DisabilityOption> codes, ICollection<DisabilityOption> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<DisabilityOption> GetCodesToBeDeactivated(ICollection<DisabilityOption> codes, ICollection<DisabilityOption> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<DisabilityOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<DisabilityOption> codesToDeactivate = new List<DisabilityOption>();

            foreach (DisabilityOption option in codesToRemove)
            {
                ICollection<Disability> responsesWithOption = _disabilityWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<DisabilityOption> GetCodesToBeDeleted(ICollection<DisabilityOption> codes, ICollection<DisabilityOption> amsCodes)
        {
            IList<DisabilityOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<DisabilityOption> codesToDeleted = new List<DisabilityOption>();

            foreach (DisabilityOption option in codesToRemove)
            {
                ICollection<Disability> responsesWithOption = _disabilityWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }
    }
}
