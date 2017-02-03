﻿using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class SectionManager
    {
        private LicensingContext _context;
        private SectionWorker _sectionWorker;

        public SectionManager(LicensingContext context)
        {
            _context = context;
            _sectionWorker = new SectionWorker(context);
        }

        public ICollection<Section> GetSections(License license)
        {
            return _sectionWorker.GetSections(license);
        }

        public void Confirm(License license)
        {
            _sectionWorker.Confirm(license);
        }

        public bool IsComplete(License license)
        {
            if (license == null)
            {
                return false;
            }

            return license.SectionsConfirmed;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Section", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("Section", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Sections",
                license.LicenseType.Sections,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                "_Sections",
                GetSections(license)
            );
        }
    }
}
