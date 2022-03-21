using DevJobs.API.Entities;
using DevJobs.API.Models;
using DevJobs.API.Persistence;
using DevJobs.API.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
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

        /// <summary>
        /// Obter todas as vagas
        /// </summary>
        /// <param name="model">Todas as vagas retornadas</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var jobVancancies = _vacancyRepository.GetAll();
            return Ok(jobVancancies);
        }

        /// <summary>
        /// Obter dados da Vaga
        /// </summary>
        /// <param name="model">Dados da vaga retornado</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var jobVancancy = _vacancyRepository.GetById(id);

            if (jobVancancy == null)
                return NotFound();

            return Ok(jobVancancy);
        }

        /// <summary>
        /// Cadastrar uma vaga de emprego
        /// </summary>
        /// <param name="model">Dados da vaga.</param>
        /// <returns>Objeto recém-criado</returns>
        [HttpPost()]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            Log.Information("Post JobVancacy chamado");

            var jobVacancy = new JobVacancy(model.Title, model.Description, model.Company, model.IsRemote, model.SalaryRange);
            _vacancyRepository.Add(jobVacancy);

            return CreatedAtAction("GetById",new { id = jobVacancy.Id}, jobVacancy);
        }

        /// <summary>
        /// Editar dados da vaga
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
