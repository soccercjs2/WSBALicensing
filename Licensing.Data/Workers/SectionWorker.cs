﻿using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class SectionWorker
    {
        private LicensingContext _context;

        public SectionWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<Section> GetSections(License license)
        {
            if (license == null)
            {
                return null;
            }

            if (license.Sections == null)
            {
                return null;
            }

            ICollection<Section> sections = license.Sections;

            if (sections.Count == 0) { return null; }
            else { return sections; }
        }

        public void Confirm(License license)
        {
            if (license != null)
            {
                license.SectionsConfirmed = true;
                _context.SaveChanges();
            }
        }
    }
}
