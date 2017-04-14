using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class EthnicityManager
    {
        private LicensingContext _context;
        private EthnicityWorker _ethnicityWorker;

        public EthnicityManager(LicensingContext context)
        {
            _context = context;
            _ethnicityWorker = new EthnicityWorker(context);
        }

        public ICollection<EthnicityOption> GetOptions()
        {
            return _ethnicityWorker.GetOptions();
        }

        public EthnicityOption GetOption(string code)
        {
            return _ethnicityWorker.GetOption(code);
        }

        public void SetEthnicity(License license, int optionId)
        {
            EthnicityOption option = _ethnicityWorker.GetOption(optionId);

            if (license.Ethnicity == null)
            {
                license.Ethnicity = new Ethnicity();
            }

            license.Ethnicity.Option = option;

            _context.SaveChanges();
        }

        public void SetEthnicity(License license, EthnicityOption option)
        {
            if (license.Ethnicity == null)
            {
                license.Ethnicity = new Ethnicity();
            }

            license.Ethnicity.Option = option;

            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return license.Ethnicity != null;
        }

        public IList<EthnicityOption> GetAmsOptions()
        {
            IList<EthnicityOption> options = new List<EthnicityOption>();
            var codes = WSBA.AMS.CodeTypesManager.GetEthnicityCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new EthnicityOption() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetOption(EthnicityOption option)
        {
            if (option.EthnicityOptionId == 0)
            {
                EthnicityOption existingCode = _ethnicityWorker.GetOption(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _ethnicityWorker.SetOption(option);
        }

        public void DeleteOption(EthnicityOption option)
        {
            _ethnicityWorker.DeleteOption(option);
        }

        public IList<EthnicityOption> GetCodesToBeAdded(ICollection<EthnicityOption> codes, ICollection<EthnicityOption> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<EthnicityOption> GetCodesToBeActivated(ICollection<EthnicityOption> codes, ICollection<EthnicityOption> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<EthnicityOption> GetCodesToBeChanged(ICollection<EthnicityOption> codes, ICollection<EthnicityOption> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<EthnicityOption> GetCodesToBeDeactivated(ICollection<EthnicityOption> codes, ICollection<EthnicityOption> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<EthnicityOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<EthnicityOption> codesToDeactivate = new List<EthnicityOption>();

            foreach (EthnicityOption option in codesToRemove)
            {
                ICollection<Ethnicity> responsesWithOption = _ethnicityWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<EthnicityOption> GetCodesToBeDeleted(ICollection<EthnicityOption> codes, ICollection<EthnicityOption> amsCodes)
        {
            IList<EthnicityOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<EthnicityOption> codesToDeleted = new List<EthnicityOption>();

            foreach (EthnicityOption option in codesToRemove)
            {
                ICollection<Ethnicity> responsesWithOption = _ethnicityWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }
    }
}
