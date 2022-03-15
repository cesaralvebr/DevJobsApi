using DevJobs.API.Entities;
using DevJobs.API.Models;
using DevJobs.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevJobs.API.Controllers
{
    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly DevJobsContext _context;
        public JobVacanciesController(DevJobsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var jobVancancies = _context.JobVacancies.ToList();
            return Ok(jobVancancies);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var jobVancancy = _context.JobVacancies
                .SingleOrDefault(x=>x.Id == id);

            if (jobVancancy == null)
                return NotFound();

            return Ok(jobVancancy);
        }

        [HttpPost()]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            var jobVacancy = new JobVacancy(model.Title, model.Description, model.Company, model.IsRemote, model.SalaryRange);

            _context.JobVacancies.Add(jobVacancy);
            return CreatedAtAction("GetById",new { id = jobVacancy.Id}, jobVacancy);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            var jobVacancy = _context.JobVacancies.SingleOrDefault(x => x.Id == id);

            if (jobVacancy == null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description);

            return NoContent();
        }


    }
}
