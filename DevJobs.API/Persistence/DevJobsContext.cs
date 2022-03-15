using DevJobs.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevJobs.API.Persistence
{
    public class DevJobsContext
    {
        public DevJobsContext()
        {
            JobVacancies = new List<JobVacancy>();
            JobApplication = new List<JobApplication>();
        }
        public List<JobVacancy> JobVacancies { get; set; }
        public List<JobApplication> JobApplication { get; set; }
    }
}
