﻿using APISP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
namespace APISP.Controllers
{
   [Route("api/[controller]")]
   [Produces(MediaTypeNames.Application.Json)]
   [ApiController]
   public class PeopleController : ControllerBase
   {
      private readonly DALPeople _dalPeople;

      public PeopleController(DALPeople dalPeople)
      {
         _dalPeople = dalPeople;
      }

      [HttpGet]
      [ProducesResponseType(typeof(IEnumerable<People>), StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public IAsyncEnumerable<People> Get()
      {
         try
         {
            return _dalPeople.FindAllAsync();
         }
         catch (Exception)
         {
            throw;
         }
      }

      [HttpGet("{id}")]
      [ProducesResponseType(typeof(People), 200)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<People>> Get(int id)
      {
         try
         {
            People? model = await _dalPeople.FindAsync(id);
            if (model is null)
            {
               return NotFound();
            }
            return Ok(model);
         }
         catch (Exception)
         {
            throw;
         }
         
      }

      [HttpPost]
      [ProducesResponseType(typeof(People), StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<People>> Post([FromBody] People model)
      {
         try
         {
            if (model is null || !ModelState.IsValid)
            {
               return BadRequest();
            }
            try
            {
               await _dalPeople.AddAsync(model);
               return CreatedAtAction(nameof(Get), model);
            }
            catch (Exception)
            {
               throw;
            }
         }
         catch (Exception)
         {

            throw;
         }
         
      }

      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<People>> Put(int id, [FromBody] People model)
      {
         if (model is null || id == 0 || model.Id != id || !ModelState.IsValid)
         {
            return BadRequest();
         }

         try
         {
            bool status = await _dalPeople.UpdateAsync(model);
            return Ok(new { model, status });
         }
         catch (Exception)
         {
            throw;
         }

      }

      [HttpDelete("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult> Delete(int id)
      {         
         if (id == 0)
         {
            return BadRequest();
         }
         try
         {
            bool status = await _dalPeople.DeleteAsync(id);
            return Ok(new { status, description = status ? "excluded" : "not excluded" });
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}
