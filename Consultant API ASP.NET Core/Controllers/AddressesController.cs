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
    public class AddressesController : ControllerBase
    {
        private readonly IConsultantRepository repository;
        private readonly IMapper mapper;

        public AddressesController(IConsultantRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET api/addresses

        [HttpGet]
        public async Task<ActionResult<AddressViewModel[]>> Get(bool includeConsultants = false)
        {
            try
            {
                var results = await repository.GetAllAddressesAsync();
                return mapper.Map<AddressViewModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
        }

        // GET api/addresses/5

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AddressViewModel>> Get(int id, bool includeConsultant = false)
        {
            try
            {
                Address result = await repository.GetAddressAsync(id);
                if (result == null) return NotFound();
                if (includeConsultant)
                {
                    AddressViewModel address = mapper.Map<AddressViewModel>(result);
                    Consultant resultConsultant = await repository.GetConsultantAsync(result.ConsultantId, false);
                    //TODO When repo is called from here it always returns with addresses
                    resultConsultant.Addresses = null;
                    address.consultant = mapper.Map<ConsultantViewModel>(resultConsultant); ;
                    return address;
                }
                else
                {
                    return mapper.Map<AddressViewModel>(result);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
        }

        //Create new address
        // POST api/addresses

        [HttpPost]
        public async Task<ActionResult<AddressViewModel>> Post(AddressViewModel model)
        {
            try
            {
                Address result = mapper.Map<Address>(model);
                repository.Add(result);
                if (await repository.SaveChangesAsync())
                {
                    return Created($"/api/camps/{result.AddressId}", mapper.Map<AddressViewModel>(result));
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
            return BadRequest();
        }

        // PUT api/addresses/5

        [HttpPut("{id:int}")]
        public async Task<ActionResult<AddressViewModel>> Put(int id, AddressViewModel model)
        {
            try
            {
                Address oldAddress = await repository.GetAddressAsync(id);
                if (oldAddress == null) return NotFound($"Could not find address with ID of {id}");
                mapper.Map(model, oldAddress);
                if (await repository.SaveChangesAsync())
                {
                    return mapper.Map<AddressViewModel>(oldAddress);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error in database");
            }
            return BadRequest();
        }

        // DELETE api/addresses/5

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Address result = await repository.GetAddressAsync(id);
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
            return BadRequest("Failed to delete the address");
        }
    }
}