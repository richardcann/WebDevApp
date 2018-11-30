using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Housing.WebAPI.Models.InternalDTO;
using Housing.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Housing.WebAPI.Utils;
using Housing.WebAPI.Models.ClientServerDTO;
using System.Net.Http;
using System.Net;
using Housing.WebAPI.Services;

namespace Housing.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : Controller
    {
        private IMapper _mapper;
        private readonly HousingContext _context;

        public PropertiesController(
            IMapper mapper,
            HousingContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/properties/approved
        [Authorize]
        [HttpGet]
        [Route("approved")]
        public IActionResult GetApproved()
        {
            PropertyService ps = new PropertyService(_mapper, _context);
            return Ok(ps.GetProperties(Property.VerificationStatus.Approved));
        }
        
        // Useful for officer to approve
        [Authorize]
        [HttpGet]
        [Route("pending")]
        public IActionResult GetPending()
        {
            var userCp = HttpContext.User;
            if (TokenVerifier.CheckOfficer(userCp))
            {
                PropertyService ps = new PropertyService(_mapper, _context);
                return Ok(ps.GetProperties(Property.VerificationStatus.Pending));
            }
            return Unauthorized();
        }

        // GET: api/myproperties
        [Authorize]
        [HttpGet]
        [Route("myproperties")]
        public IActionResult GetMyProperties()
        {
            var userCp = HttpContext.User;
            string landlord = TokenVerifier.GetLandlord(userCp);
            if (landlord != null)
            {
                PropertyService ps = new PropertyService(_mapper, _context);
                return Ok(ps.GetMyProperties(landlord));
            }
            return NotFound();
        }
        
        // GET: api/properties/5
        //General get, for all users (Do not load rejections)
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProperty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PropertyService ps = new PropertyService(_mapper, _context);
            var property = await ps.Get(id);

            if (property == null)
            {
                return NotFound();
            }
            
            var userCp = HttpContext.User;
            string landlord = TokenVerifier.GetLandlord(userCp);
            
            if (landlord == property.AppUserRef)
            {
                return Ok(ps.GetMyPropertyByID(id, landlord));
            } else
            {
                return Ok(ps.GetPropertyByID(id));
            }
        }

        // POST: api/properties/approve/{id}
        [Authorize]
        [HttpPost]
        [Route("approve/{id}")]
        public async Task<IActionResult> ApproveProperty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Check all attributes are there? Will the binding be successful?

            var userCp = HttpContext.User;
            PropertyService ps = new PropertyService(_mapper, _context);

            if (TokenVerifier.CheckOfficer(userCp) && await ps.Get(id) != null)
            {
                RejectionService rs = new RejectionService(_mapper, _context);
                rs.RemovePropertyRejections(id);
                ps.Approve(id);
                return Ok();
            }
            return Unauthorized();
        }

        // POST: api/properties/edit/{id}
        [Authorize]
        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> UpdateProperty([FromRoute] int id, [FromBody] AddProperty addProperty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userCp = HttpContext.User;
            string landlord = TokenVerifier.GetLandlord(userCp);

            PropertyService ps = new PropertyService(_mapper, _context);
            Property currentProperty = await ps.Get(id);
            
            if (landlord == null || landlord != currentProperty.AppUserRef)
            {
                return Unauthorized();
            }
            
            //Generate property
            Property newproperty = _mapper.Map<AddProperty, Property>(addProperty);
            newproperty.PropertyStatus = Property.VerificationStatus.Pending;
            newproperty.AppUserRef = landlord;

            //Validate new property
            if (!TryValidateModel(newproperty))
            {
                return BadRequest();
            }
            
            ImageService ims = new ImageService(_mapper, _context);
            if(!ims.Update(id, addProperty.Images))
            {
                return BadRequest();
            }

            ps.Update(id, newproperty);
            return Ok();
        }

        // POST: api/properties/add
        [Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> PostProperty([FromBody] AddProperty addProperty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userCp = HttpContext.User;
            string landlord = TokenVerifier.GetLandlord(userCp);
            
            //Generate property
            Property property = _mapper.Map<AddProperty, Property>(addProperty);
            property.PropertyStatus = Property.VerificationStatus.Pending;
            property.AppUserRef = landlord;

            //Validate property
            if (!TryValidateModel(property))
            {
                return BadRequest();
            }

            List<BasicImage> images = new List<BasicImage>();

            ImageService ims = new ImageService(_mapper, _context);

            if (ims.AddAll(property.ID, addProperty.Images))
            {
                PropertyService ps = new PropertyService(_mapper, _context);
                await ps.Add(property);
            } else
            {
                return BadRequest();
            }
                
            ////All Images are valid and added
            //_context.Property.Add(property);
            //foreach (BasicImage bi in images)
            //{
            //    bi.PropertyRef = property.ID;
            //    Image im = _mapper.Map<BasicImage, Image>(bi);
            //    _context.Image.Add(im);
            //}

           
            return Ok();
        }
        
        // DELETE: api/properties/propertyid
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userCp = HttpContext.User;
            string landlord = TokenVerifier.GetLandlord(userCp);

            PropertyService ps = new PropertyService(_mapper, _context);
            Property property = await ps.Get(id);
            
            if (landlord == property.AppUserRef)
            {
                ImageService ims = new ImageService(_mapper, _context);
                ims.DeleteAll(id);
                ps.Remove(id);
                return Ok();
            }

            return Unauthorized();
        }
        
 
    }
}