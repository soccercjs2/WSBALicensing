using Licensing.Web.Models.Enums;
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
        public string PartialViewName { get; set; }
        public object PartialViewData { get; set; }
    }
}