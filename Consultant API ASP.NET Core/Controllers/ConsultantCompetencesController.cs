using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using Consultant_API_ASP.NET_Core.Data;
using Consultant_API_ASP.NET_Core.Models;

namespace Consultant_API_ASP.NET_Core.Controllers
{
    [Route("api/consultant/{id:int}/competences")]
    [ApiController] //Ensures binding to model from body
    public class ConsultantCompetencesController : ControllerBase
    {
        private readonly IConsultantRepository repository;
        private readonly IMapper mapper;

        public ConsultantCompetencesController(IConsultantRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET api/consultant/{id:int}/competences

        [HttpGet]
        public async Task<ActionResult<CompetenceViewModel[]>> Get(int id)
        {
            try
            {
                var results = await repository.GetAllCompetencesForConsultantAsync(id);
                return mapper.Map<CompetenceViewModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
        }
    }
}