using DevJobs.API.Entities;
using DevJobs.API.Models;
using DevJobs.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevJobs.API.Controllers
{
    [Route("api/job-vacancies/{id}/applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly DevJobsContext _context;
        public JobApplicationsController(DevJobsContext context)
        {
            _context = context;
        }

        //POST api/job-vacancies/4/applications
        [HttpPost]
        public IActionResult Post(int id,AddJobApplicationInputModel model)
        {
            var jobVacancy = _context.JobVacancies.SingleOrDefault(x => x.Id == id);

            if (jobVacancy == null)
                return NotFound();

            var jobApplication = new JobApplication(model.ApplicantEmail, model.ApplicantName, model.IdJobVacancy);

            _context.JobApplication.Add(jobApplication);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
