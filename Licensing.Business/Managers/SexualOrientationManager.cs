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

        public ICollection<SexualOrientationOption> GetOptions()
        {
            return _sexualOrientationWorker.GetOptions();
        }

        public void SetSexualOrientationOption(License license, int optionId)
        {
            SexualOrientationOption option = _sexualOrientationWorker.GetOption(optionId);

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
    }
}
