using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Housing.Models;
using Housing.Models.ServerClientDTO;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Housing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly HousingContext _context;

        public PropertiesController(HousingContext context)
        {
            _context = context;
        }

        // GET: api/Properties/approved
        [HttpGet]
        [Route("approved")]
        public IEnumerable<BasicProperty> GetApproved()
        {
            return getProperties(Property.VerificationStatus.Approved);
        }

        //Why would this (general rejection) method be needed?
        // GET: api/Properties/rejected
        //[HttpGet]
        //[Route("rejected")]
        //public IEnumerable<BasicProperty> GetRejected()
        //{
        //    return getProperties(Property.VerificationStatus.Rejected);
        //}

        

        // GET: api/properties/pending
        //useful for officer to approve
        [HttpGet]
        [Route("pending")]
        public IEnumerable<BasicProperty> GetPending()
        {
            return getProperties(Property.VerificationStatus.Pending);
        }

        private IEnumerable<BasicProperty> getProperties(Property.VerificationStatus status)
        {
            return _context
                .Property
                .Where(p => p.PropertyStatus == status)
                .Select(p => new BasicProperty
                {
                    ID = p.ID,
                    AddressLine1 = p.AddressLine1,
                    AddressLine2 = p.AddressLine2,
                    City = p.City,
                    County = p.County,
                    Postcode = p.Postcode,
                    PropertyDescription = p.PropertyDescription,
                    Timestamp = p.Timestamp,
                    Landlord = Mapper.Map<AppUser, BasicAppUser>(p.AppUser),
                    Images = Mapper.Map<ICollection<Image>, List<BasicImage>>(p.Images)
                })
                .OrderByDescending(p => p.Timestamp)
                .ToList();
        }

        // GET: api/Properties/5
        //General get, for all
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProperty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @property = await _context.Property.FindAsync(id);

            if (@property == null)
            {
                return NotFound();
            }

            var retProp = _context
                .Property
                .Where(p => p.PropertyStatus == Property.VerificationStatus.Approved
                         && p.ID == id)
                .Select(p => new BasicProperty
                {
                    ID = p.ID,
                    AddressLine1 = p.AddressLine1,
                    AddressLine2 = p.AddressLine2,
                    City = p.City,
                    County = p.County,
                    Postcode = p.Postcode,
                    PropertyDescription = p.PropertyDescription,
                    Timestamp = p.Timestamp,
                    Landlord = Mapper.Map<AppUser, BasicAppUser>(p.AppUser),
                    Images = Mapper.Map<ICollection<Image>, List<BasicImage>>(p.Images)
                });

            return Ok(retProp);
        }

        // PUT: api/Properties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty([FromRoute] int id, [FromBody] Property @property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @property.ID)
            {
                return BadRequest();
            }

            _context.Entry(@property).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
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

        // POST: api/Properties
        [HttpPost]
        public async Task<IActionResult> PostProperty([FromBody] Property @property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Property.Add(@property);
            await _context.SaveChangesAsync();

            return Ok(); //CreatedAtAction("GetProperty", new { id = @property.ID }, @property);
        }

        // DELETE: api/Properties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @property = await _context.Property.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }

            _context.Property.Remove(@property);
            await _context.SaveChangesAsync();

            return Ok(@property);
        }

        private bool PropertyExists(int id)
        {
            return _context.Property.Any(e => e.ID == id);
        }
    }
}