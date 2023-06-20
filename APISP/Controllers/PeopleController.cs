using APISP.Models;
using Microsoft.AspNetCore.Mvc;

namespace APISP.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PeopleController : ControllerBase
   {
      private readonly DALPeople _dalPeople;

      public PeopleController(DALPeople dalPeople)
      {
         _dalPeople = dalPeople;
      }
      [HttpGet]
      public IAsyncEnumerable<People> Get()
      {
         return _dalPeople.FindAllAsync();
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<People>> Get(int id)
      {
         People? model = await _dalPeople.FindAsync(id);
         if (model is null)
         {
            return NotFound();
         }
         return Ok(model);
      }

      [HttpPost]
      public async Task<ActionResult<People>> Post([FromBody] People model)
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

      [HttpPut("{id}")]
      public async Task<ActionResult<People>> Put(int id, [FromBody] People model)
      {
         if (model is null || id == 0 || model.Id != 0)
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
