using Licensing.Domain.Licenses;
using Licensing.Domain.ProBonos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class ProBonoVM
    {
        public int LicenseId { get; set; }
        public ProBono ProBono { get; set; }
        public string ProvidesServiceCssClass { get; set; }
        public string NotProvidesServiceCssClass { get; set; }

        public ProBonoVM() { }

        public ProBonoVM(License license)
        {
            LicenseId = license.LicenseId;
            
            if (license.ProBono != null)
            {
                ProBono = license.ProBono;

                if (license.ProBono.ProvidesService)
                {
                    ProvidesServiceCssClass = "btn-success";
                    NotProvidesServiceCssClass = "btn-default";
                }
                else
                {
                    ProvidesServiceCssClass = "btn-default";
                    NotProvidesServiceCssClass = "btn-danger";
                }
            }
            else
            {
                ProvidesServiceCssClass = "btn-success";
                NotProvidesServiceCssClass = "btn-danger";
            }
        }
    }
}
