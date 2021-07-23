using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    // http://localhost:5000/api/login
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<object> Login([FromBody] UserEntity userEntity, [FromServices] ILoginService service)
        {
            if(!ModelState.IsValid){ 
                // StatusCode = Error 400
                return BadRequest(ModelState);
            }

            if(userEntity ==null)
            {
                return BadRequest();
            }

            try
            {
                var result = await service.FindByLogin(userEntity) ;
                if( result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }

                // StatusCode = 200
                //return Ok(await service.FindByLogin(userEntity));
            }
            catch (ArgumentException e)
            {
                // StatusCode = Error 500
                return StatusCode((int) HttpStatusCode.InternalServerError,e.Message);

            } 
        }
    }
}
