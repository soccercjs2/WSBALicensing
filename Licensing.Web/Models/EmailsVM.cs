using Licensing.Domain.ContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licensing.Web.Models
{
    public class EmailsVM
    {
        public Email PrimaryEmail { get; set; }
        public Email HomeEmail { get; set; }
    }
}