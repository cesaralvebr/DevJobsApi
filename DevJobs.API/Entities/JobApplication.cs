using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevJobs.API.Entities
{
    public class JobApplication
    {
        public JobApplication(string applicantEmail, string applicantName, int idJobVacancy)
        {
            ApplicantEmail = applicantEmail;
            ApplicantName = applicantName;
            IdJobVacancy = idJobVacancy;
        }

        public int Id { get; private set; }
        public string ApplicantEmail { get; private set; }
        public string ApplicantName { get; private set; }
        public int IdJobVacancy { get; private set; }
    }
}
