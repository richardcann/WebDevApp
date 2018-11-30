using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Housing.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Housing.WebAPI.Utils;
using Housing.WebAPI.Models.ClientServerDTO;
using AutoMapper;
using Housing.WebAPI.Services;

namespace Housing.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RejectionsController : ControllerBase
    {
        private IMapper _mapper;
        private readonly HousingContext _context;

        public RejectionsController(
            IMapper mapper,
            HousingContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        
        //// GET: api/Rejections
        //[HttpGet]
        //public IEnumerable<Rejection> GetRejection()
        //{
        //    return _context.Rejection;
        //}

        // GET: api/Rejections/pid/5
        [HttpGet("pid/{id}")]
        public async Task<IActionResult> GetRejection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var userCp = HttpContext.User;
            string landlord = TokenVerifier.GetLandlord(userCp);

            PropertyService ps = new PropertyService(_mapper, _context);
            Property property = await ps.Get(id);
            
            List<Rejection> rejection = null;
                
            if (TokenVerifier.CheckOfficer(userCp) || landlord == property.AppUserRef)
            {
                RejectionService rs = new RejectionService(_mapper, _context);
                rejection = rs.GetRejections(id);
            } else
            {
                return Unauthorized();
            }
            
            return Ok(rejection);
        }

        // POST: api/rejections/pid/{id}
        [Authorize]
        [HttpPost]
        [Route("add/{id}")]
        public async Task<IActionResult> RejectProperty([FromRoute] int id, [FromBody] BasicRejection addRejection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Check all attributes are there? Will the binding be successful?

            var userCp = HttpContext.User;
            PropertyService ps = new PropertyService(_mapper, _context);
            Property property = await ps.Get(id);

            if (property == null)
            {
                return NotFound();
            }
            
            if (TokenVerifier.CheckOfficer(userCp))
            {
                RejectionService rs = new RejectionService(_mapper, _context);
                Rejection rejection = _mapper.Map<BasicRejection, Rejection>(addRejection);
                rejection.PropertyRef = id;
                rejection.Timestamp = DateTime.Now;
          
                if(!TryValidateModel(rejection))
                {
                    return BadRequest();
                }

                ps.Reject(id);
                rs.Add(rejection);
                
                return Ok();
            }
            return Unauthorized();
        }

        //// PUT: api/Rejections/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRejection([FromRoute] int id, [FromBody] Rejection rejection)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != rejection.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(rejection).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RejectionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Rejections
        //[HttpPost]
        //public async Task<IActionResult> PostRejection([FromBody] Rejection rejection)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Rejection.Add(rejection);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetRejection", new { id = rejection.ID }, rejection);
        //}

        //// DELETE: api/Rejections/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRejection([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var rejection = await _context.Rejection.FindAsync(id);
        //    if (rejection == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Rejection.Remove(rejection);
        //    await _context.SaveChangesAsync();

        //    return Ok(rejection);
        //}
        
    }
}