using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.BarNews;
using Licensing.Domain.Hardship;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class HardshipExemptionRequestManager
    {
        private LicensingContext _context;
        private HardshipExemptionRequestWorker _barNewsWorker;

        public HardshipExemptionRequestManager(LicensingContext context)
        {
            _context = context;
            _barNewsWorker = new HardshipExemptionRequestWorker(context);
        }

        public void SetHardshipExemptionRequest(License license, HardshipExemptionRequest hardshipExemptionRequest)
        {
            if (license.HardshipExemptionRequest == null)
            {
                license.HardshipExemptionRequest = hardshipExemptionRequest;
            }
            else
            {
                license.HardshipExemptionRequest.Income = hardshipExemptionRequest.Income;
                license.HardshipExemptionRequest.FamilySize = hardshipExemptionRequest.FamilySize;
            }

            _context.SaveChanges();
        }
    }
}
