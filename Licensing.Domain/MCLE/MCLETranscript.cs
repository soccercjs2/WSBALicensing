using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.MCLE
{
    public class MCLETranscript
    {
        public int TranscriptStatus { get; set; }
        public bool CreditRequirementsFulfilled { get; set; }
        public bool CertifiedViaInboundComity { get; set; }
        public int SubmissionType { get; set; }
    }
}
