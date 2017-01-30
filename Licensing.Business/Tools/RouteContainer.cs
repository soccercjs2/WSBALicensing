using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licensing.Business.Tools
{
    public class RouteContainer
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public int Id { get; set; }

        public RouteContainer(string controller, string action, int id)
        {
            Controller = controller;
            Action = action;
            Id = id;
        }
    }
}