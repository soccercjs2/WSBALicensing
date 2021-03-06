﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Donations
{
    public class Donation
    {
        public int DonationId { get; set; }
        public int LicenseId { get; set; }

        [ForeignKey("DonationProductId")]
        public virtual DonationProduct Product { get; set; }
        public int? DonationProductId { get; set; }
        public decimal Amount { get; set; }
    }
}
