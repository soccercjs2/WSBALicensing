using Licensing.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licensing.Web.Models
{
    public class DashboardContainerVM
    {
        public string Title { get; set; }
        public RequirementType RequirementType { get; set; }
        public bool Complete { get; set; }
        public bool Confirmable { get; set; }
        public string PartialViewName { get; set; }
        public object PartialViewData { get; set; }

        public DashboardContainerVM(string title, RequirementType requirementType, bool complete, bool confirmable, string partialViewName, object partialViewData)
        {
            Title = title;
            RequirementType = requirementType;
            Complete = complete;
            Confirmable = confirmable;
            PartialViewName = partialViewName;
            PartialViewData = partialViewData;
        }

        public DashboardContainerVM(string title, RequirementType requirementType, bool complete, bool confirmable)
        {
            Title = title;
            RequirementType = requirementType;
            Complete = complete;
            Confirmable = confirmable;
            PartialViewName = null;
            PartialViewData = null;
        }
    }
}