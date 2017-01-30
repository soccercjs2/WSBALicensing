using Licensing.Domain.ContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licensing.Business.ViewModels
{
    public class EmailsVM
    {
        public Email PrimaryEmail { get; set; }
        public Email HomeEmail { get; set; }

        public EmailsVM(Email primaryEmail, Email homeEmail)
        {
            PrimaryEmail = primaryEmail;
            HomeEmail = homeEmail;
        }
    }
}