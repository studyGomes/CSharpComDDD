using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    // http://localhost:5000/api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service ;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() // [FromServices] IUserService service 
        {
            if(!ModelState.IsValid)
            {
                // StatusCode = Error 400
                return BadRequest(ModelState);
            }

            try
            {
                // StatusCode = 200
                return Ok(await _service.GetAll());
            }
            catch (ArgumentException e)
            {
                // StatusCode = Error 500
                return StatusCode((int) HttpStatusCode.InternalServerError,e.Message);

            }

        }
        
    }
}
