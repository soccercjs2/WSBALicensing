using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.BarNews;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class BarNewsManager
    {
        private LicensingContext _context;
        private BarNewsWorker _barNewsWorker;

        public BarNewsManager(LicensingContext context)
        {
            _context = context;
            _barNewsWorker = new BarNewsWorker(context);
        }

        public void SetBarNewsResponse(License license, bool? response)
        {
            if (response != null)
            {
                if (license.BarNewsResponse == null)
                {
                    license.BarNewsResponse = new BarNewsResponse();
                }

                license.BarNewsResponse.Response = (bool)response;
                license.BarNewsResponse.Confirmed = true;
            }

            _context.SaveChanges();
        }

        public void Confirm(BarNewsResponse barNews)
        {
            barNews.Confirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return (license.BarNewsResponse != null && license.BarNewsResponse.Confirmed);
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {

            RouteContainer editRoute = new RouteContainer("BarNews", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Bar News",
                license.LicenseType.BarNews,
                IsComplete(license),
                editRoute,
                null,
                true,
                "_BarNews",
                license.BarNewsResponse
            );
        }
    }
}
