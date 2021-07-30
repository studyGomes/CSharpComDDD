using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetAll() // [FromServices] IUserService service 
        {
            if(!ModelState.IsValid){ 
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
        
        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        public async Task<ActionResult> Get(Guid id) 
        {
            if(!ModelState.IsValid)
            {
                // StatusCode = Error 400
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.Get(id);
                if(result==null)
                {
                    return NotFound();
                }
                // StatusCode = 200
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                // StatusCode = Error 500
                return StatusCode((int) HttpStatusCode.InternalServerError,e.Message);

            }

        }

        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDTOCreate user) 
        {
            if(!ModelState.IsValid)
            {
                // StatusCode = Error 400
                return BadRequest(ModelState);
            }

            try
            {
                // StatusCode = 200
                //return Ok(await _service.Post(user);
                var result = await _service.Post(user);

                if(result != null){
                    return Created(new Uri(Url.Link("GetWithId", new {id = result.Id})), result) ;
                } 
                else
                {
                    return BadRequest();
                }



            }
            catch (ArgumentException e)
            {
                // StatusCode = Error 500
                return StatusCode((int) HttpStatusCode.InternalServerError,e.Message);

            }

        }

        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserDTOUpdate user) 
        {
            if(!ModelState.IsValid)
            {
                // StatusCode = Error 400
                return BadRequest(ModelState);
            }

            try
            {
                // StatusCode = 200
                //return Ok(await _service.Post(user);
                var result = await _service.Put(user);

                if(result != null){
                    return Ok(result) ;
                } 
                else
                {
                    return BadRequest();
                }



            }
            catch (ArgumentException e)
            {
                // StatusCode = Error 500
                return StatusCode((int) HttpStatusCode.InternalServerError,e.Message);

            }

        }

        [Authorize("Bearer")]
        [HttpDelete ("{id}")]
        public async Task<ActionResult> Delete(Guid id) 
        {
            if(!ModelState.IsValid)
            {
                // StatusCode = Error 400
                return BadRequest(ModelState);
            }

            try
            {
                // StatusCode = 200
                return Ok(await _service.Delete(id));
            }
            catch (ArgumentException e)
            {
                // StatusCode = Error 500
                return StatusCode((int) HttpStatusCode.InternalServerError,e.Message);

            }

        }

    }
}
