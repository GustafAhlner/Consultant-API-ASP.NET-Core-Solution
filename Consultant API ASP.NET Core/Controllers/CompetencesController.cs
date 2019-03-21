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
    public class CompetencesController : ControllerBase
    {
        private readonly IConsultantRepository repository;
        private readonly IMapper mapper;

        public CompetencesController(IConsultantRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET api/competences

        [HttpGet]
        public async Task<ActionResult<CompetenceViewModel[]>> Get(bool includeConsultants = false)
        {
            try
            {
                var results = await repository.GetAllCompetencesAsync();
                return mapper.Map<CompetenceViewModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
        }

        // GET api/competences/5

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CompetenceViewModel>> Get(int id, bool includeConsultant = false)
        {
            try
            {
                Competence result = await repository.GetCompetenceAsync(id);
                if (result == null) return NotFound();
                return mapper.Map<CompetenceViewModel>(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
        }

        //Create new competence
        // POST api/competences

        [HttpPost]
        public async Task<ActionResult<CompetenceViewModel>> Post(CompetenceViewModel model)
        {
            try
            {
                Competence result = mapper.Map<Competence>(model);
                repository.Add(result);
                if (await repository.SaveChangesAsync())
                {
                    return Created($"/api/camps/{result.CompetenceId}", mapper.Map<CompetenceViewModel>(result));
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
            return BadRequest();
        }

        // PUT api/competences/5

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CompetenceViewModel>> Put(int id, CompetenceViewModel model)
        {
            try
            {
                Competence oldCompetence = await repository.GetCompetenceAsync(id);
                if (oldCompetence == null) return NotFound($"Could not find competence with ID of {id}");
                mapper.Map(model, oldCompetence);
                if (await repository.SaveChangesAsync())
                {
                    return mapper.Map<CompetenceViewModel>(oldCompetence);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
            return BadRequest();
        }

        // DELETE api/competences/5

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Competence result = await repository.GetCompetenceAsync(id);
                if (result == null) return NotFound();
                repository.Delete(result);
                if (await repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
            return BadRequest("Failed to delete the competence");
        }
    }
}