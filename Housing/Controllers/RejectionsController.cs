using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Housing.Models;

namespace Housing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RejectionsController : ControllerBase
    {
        private readonly HousingContext _context;

        public RejectionsController(HousingContext context)
        {
            _context = context;
        }

        // GET: api/Rejections
        [HttpGet]
        public IEnumerable<Rejection> GetRejection()
        {
            return _context.Rejection;
        }

        // GET: api/Rejections/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRejection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rejection = await _context.Rejection.FindAsync(id);

            if (rejection == null)
            {
                return NotFound();
            }

            return Ok(rejection);
        }

        // PUT: api/Rejections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRejection([FromRoute] int id, [FromBody] Rejection rejection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rejection.ID)
            {
                return BadRequest();
            }

            _context.Entry(rejection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RejectionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rejections
        [HttpPost]
        public async Task<IActionResult> PostRejection([FromBody] Rejection rejection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Rejection.Add(rejection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRejection", new { id = rejection.ID }, rejection);
        }

        // DELETE: api/Rejections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRejection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rejection = await _context.Rejection.FindAsync(id);
            if (rejection == null)
            {
                return NotFound();
            }

            _context.Rejection.Remove(rejection);
            await _context.SaveChangesAsync();

            return Ok(rejection);
        }

        private bool RejectionExists(int id)
        {
            return _context.Rejection.Any(e => e.ID == id);
        }
    }
}