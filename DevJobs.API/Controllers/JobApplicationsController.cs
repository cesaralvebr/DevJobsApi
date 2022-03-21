using DevJobs.API.Entities;
using DevJobs.API.Models;
using DevJobs.API.Persistence;
using DevJobs.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevJobs.API.Controllers
{
    [Route("api/job-vacancies/{id}/applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobVacancyRepository _jobVacancyRepository;
        public JobApplicationsController(IJobVacancyRepository jobVacancyRepository)
        {
            _jobVacancyRepository = jobVacancyRepository;
        }

        //POST api/job-vacancies/4/applications
        [HttpPost]
        public IActionResult Post(int id,AddJobApplicationInputModel model)
        {
            var jobVacancy = _jobVacancyRepository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            var jobApplication = new JobApplication(model.ApplicantEmail, model.ApplicantName, model.IdJobVacancy);

            _jobVacancyRepository.AddAplication(jobApplication);

            return NoContent();
        }
    }
}
