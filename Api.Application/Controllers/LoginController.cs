using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // http://localhost:5000/api/login
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDTO loginDTO, [FromServices] ILoginService service)
        {
            if(!ModelState.IsValid){ 
                // StatusCode = Error 400
                return BadRequest(ModelState);
            }

            if(loginDTO ==null)
            {
                return BadRequest();
            }

            try
            {
                var result = await service.FindByLogin(loginDTO) ;
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
