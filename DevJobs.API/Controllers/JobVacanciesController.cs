using DevJobs.API.Entities;
using DevJobs.API.Models;
using DevJobs.API.Persistence;
using DevJobs.API.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IJobVacancyRepository _vacancyRepository;

        public JobVacanciesController(IJobVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var jobVancancies = _vacancyRepository.GetAll();
            return Ok(jobVancancies);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var jobVancancy = _vacancyRepository.GetById(id);

            if (jobVancancy == null)
                return NotFound();

            return Ok(jobVancancy);
        }

        [HttpPost()]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            var jobVacancy = new JobVacancy(model.Title, model.Description, model.Company, model.IsRemote, model.SalaryRange);
            _vacancyRepository.Add(jobVacancy);

            return CreatedAtAction("GetById",new { id = jobVacancy.Id}, jobVacancy);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            var jobVacancy = _vacancyRepository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description);
            _vacancyRepository.Update(jobVacancy);

            return NoContent();
        }


    }
}
