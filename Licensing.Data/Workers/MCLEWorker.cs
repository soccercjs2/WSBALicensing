using Licensing.Data.Context;
using Licensing.Domain.MCLE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class MCLEWorker
    {
        private string _connectionString { get; set; }

        public MCLEWorker(LicensingContext context)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["TAMIContext"].ConnectionString;
        }

        public MCLETranscript GetMCLETranscript(string barNumber, int licensingYear)
        {
            DataTable dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string commandString = String.Format(@"SELECT
                                                            T.Status,
                                                            T.CreditRequirementsFulfilled,
                                                            T.CertifiedViaInboundComity,
                                                            T.SubmissionType
                                                        FROM Transcript T
                                                        JOIN ReportingPeriod RP
                                                        ON RP.ReportingPeriodId = T.ReportingPeriodId
                                                        JOIN AmsUser AU
                                                        ON AU.UserId = T.UserId
                                                        WHERE YEAR(RP.EndDate) = ({0} - 1)
                                                        AND AU.MasterCustomerId = '{1}'", licensingYear, barNumber);

                using (var command = new SqlCommand(commandString, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);

                        if (dataTable.Rows.Count > 0)
                        {
                            int transactionStatus = int.Parse(dataTable.Rows[0]["Status"].ToString());
                            bool creditRequirementsFulfilled = bool.Parse(dataTable.Rows[0]["CreditRequirementsFulfilled"].ToString());
                            bool certifiedViaInboundComity = bool.Parse(dataTable.Rows[0]["CertifiedViaInboundComity"].ToString());

                            int submissionType = 0;
                            if (dataTable.Rows[0]["SubmissionType"].ToString() != "")
                            {
                                submissionType = int.Parse(dataTable.Rows[0]["SubmissionType"].ToString());
                            }

                            return new MCLETranscript()
                            {
                                TranscriptStatus = transactionStatus,
                                CreditRequirementsFulfilled = creditRequirementsFulfilled,
                                CertifiedViaInboundComity = certifiedViaInboundComity,
                                SubmissionType = submissionType
                            };
                        }
                    }
                }

                connection.Close();
            }

            return null;
        }

        public bool HasUnpaidMCLELateFee(string barNumber, int licensingYear)
        {
            DataTable dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string commandString = String.Format(@"SELECT P.*
                                                        FROM Payment P
                                                        JOIN AmsUser AU
                                                        ON AU.UserId = P.UserId
                                                        JOIN ReportingPeriod RP
                                                        ON RP.ReportingPeriodId = P.ReportingPeriodId
                                                        WHERE YEAR(RP.EndDate) = ({0} - 1)
                                                        AND AU.MasterCustomerId = '{1}'
                                                        AND P.IsPaid = 0
                                                        AND P.IsWaived = 0
                                                        AND P.ProductKey = 'member_late_fee'", licensingYear, barNumber);

                using (var command = new SqlCommand(commandString, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);

                        if (dataTable.Rows.Count > 0)
                        {
                            
                        }
                    }
                }

                connection.Close();
            }

            return false;
        }

        public bool HasUnpaidMCLEComityFee(string barNumber, int licensingYear)
        {
            DataTable dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string commandString = String.Format(@"SELECT P.*
                                                        FROM Payment P
                                                        JOIN AmsUser AU
                                                        ON AU.UserId = P.UserId
                                                        JOIN ReportingPeriod RP
                                                        ON RP.ReportingPeriodId = P.ReportingPeriodId
                                                        WHERE YEAR(RP.EndDate) = ({0} - 1)
                                                        AND AU.MasterCustomerId = '{1}'
                                                        AND P.IsPaid = 0
                                                        AND P.IsWaived = 0
                                                        AND P.ProductKey = 'comity'", licensingYear, barNumber);

                using (var command = new SqlCommand(commandString, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);

                        if (dataTable.Rows.Count > 0)
                        {

                        }
                    }
                }

                connection.Close();
            }

            return false;
        }
    }
}
