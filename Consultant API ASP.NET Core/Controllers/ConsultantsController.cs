using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using Consultant_API_ASP.NET_Core.Data;
using Consultant_API_ASP.NET_Core.Data.Entities;
using Consultant_API_ASP.NET_Core.Models;

namespace Consultant_API_ASP.NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //Ensures binding to model from body
    public class ConsultantsController : ControllerBase
    {
        private readonly IConsultantRepository repository;
        private readonly IMapper mapper;

        public ConsultantsController(IConsultantRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET api/consultants

        [HttpGet]
        public async Task<ActionResult<ConsultantViewModel[]>> Get(bool includeAddresses = false, bool includeCompetences = false)
        {
            try
            {
                var results = await repository.GetAllConsultantsAsync(includeAddresses);
                if (includeCompetences)
                {
                    ConsultantViewModel[] consultants = mapper.Map<ConsultantViewModel[]>(results);
                    foreach (ConsultantViewModel consultant in consultants)
                    {
                        Competence[] competences = await repository.GetAllCompetencesForConsultantAsync(consultant.ConsultantId);
                        if (competences == null) continue;
                        consultant.Competences = mapper.Map<CompetenceViewModel[]>(competences);
                    }
                    return consultants;
                }
                return mapper.Map<ConsultantViewModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
        }

        // GET api/consultants/5

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ConsultantViewModel>> Get(int id, bool includeAddress = false, bool includeCompetences = false)
        {
            try
            {
                Consultant result = await repository.GetConsultantAsync(id, includeAddress);
                if (result == null) return NotFound();
                if (includeCompetences)
                {
                    ConsultantViewModel consultant = mapper.Map<ConsultantViewModel>(result);
                    Competence[] competences = await repository.GetAllCompetencesForConsultantAsync(consultant.ConsultantId);
                    if (competences != null)
                    {
                        consultant.Competences = mapper.Map<CompetenceViewModel[]>(competences);
                    }
                    return consultant;
                }
                return mapper.Map<ConsultantViewModel>(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
        }

        //Create new consultant
        // POST api/consultants

        [HttpPost]
        public async Task<ActionResult<ConsultantViewModel>> Post(ConsultantViewModel model)
        {
            try
            {
                Consultant consultant = mapper.Map<Consultant>(model);
                repository.Add(consultant);
                if (await repository.SaveChangesAsync())
                {
                    return Created($"/api/camps/{consultant.ConsultantId}", mapper.Map<ConsultantViewModel>(consultant));
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
            return BadRequest();
        }

        // PUT api/consultants/5

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ConsultantViewModel>> Put(int id, ConsultantViewModel model)
        {
            try
            {
                Consultant oldConsultant = await repository.GetConsultantAsync(id);
                if (oldConsultant == null) return NotFound($"Could not find consultant with ID of {id}");

                mapper.Map(model, oldConsultant);

                if (await repository.SaveChangesAsync())
                {
                    return mapper.Map<ConsultantViewModel>(oldConsultant);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
            return BadRequest();
        }

        // DELETE api/consultants/5

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, bool deleteAddress = false)
        {
            try
            {
                Consultant consultant = await repository.GetConsultantAsync(id);
                if (consultant == null) return NotFound();
                repository.Delete(consultant);
                if (await repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
            return BadRequest("Failed to delete the consultant");
        }
    }
}