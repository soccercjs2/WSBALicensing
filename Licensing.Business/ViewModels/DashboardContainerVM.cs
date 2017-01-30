using Licensing.Domain.Enums;
using Licensing.Business.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licensing.Business.ViewModels
{
    public class DashboardContainerVM
    {
        public string Title { get; set; }
        public RequirementType RequirementType { get; set; }
        public bool Complete { get; set; }
        public RouteContainer EditRoute { get; set; }
        public RouteContainer ConfirmRoute { get; set; }
        public RouteContainer OptOutRoute { get; set; }
        public string PartialViewName { get; set; }
        public object PartialViewData { get; set; }

        public DashboardContainerVM(
            string title, 
            RequirementType requirementType, 
            bool complete, 
            RouteContainer editRoute, 
            RouteContainer confirmRoute, 
            RouteContainer optOutRoute, 
            string partialViewName, 
            object partialViewData)
        {
            Title = title;
            RequirementType = requirementType;
            Complete = complete;
            EditRoute = editRoute;
            ConfirmRoute = confirmRoute;
            OptOutRoute = optOutRoute;
            PartialViewName = partialViewName;
            PartialViewData = partialViewData;
        }
    }
}