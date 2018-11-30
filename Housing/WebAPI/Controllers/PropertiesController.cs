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
        public IEnumerable<BasicProperty> GetApproved()
        {
            return GetProperties(Property.VerificationStatus.Approved);
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
        // Useful for officer to approve
        [Authorize]
        [HttpGet]
        [Route("pending")]
        public IActionResult GetPending()
        {
            var userCp = HttpContext.User;
            if (TokenVerifier.CheckOfficer(userCp))
            {
                return Ok(GetProperties(Property.VerificationStatus.Pending));
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
                return Ok(GetMyProperties(landlord));
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
            
            var property = await _context.Property.FindAsync(id);

            if (property == null)
            {
                return NotFound();
            }
            
            var userCp = HttpContext.User;
            string landlord = TokenVerifier.GetLandlord(userCp);
            
            if (landlord == property.AppUserRef)
            {
                return Ok(GetMyPropertyByID(id, landlord));
            } else
            {
                return Ok(GetPropertyByID(id));
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
            var property = await _context.Property.FindAsync(id);

            if (property == null)
            {
                return NotFound();
            }

            if (TokenVerifier.CheckOfficer(userCp))
            {
                property.PropertyStatus = Property.VerificationStatus.Approved;
                property.Timestamp = DateTime.Now;

                List<Rejection> rejections = _context.Rejection.Where(i => i.PropertyRef == id).ToList();
                foreach(Rejection r in rejections)
                {
                    _context.Rejection.Remove(r);
                }

                await _context.SaveChangesAsync();

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
            
            Property currentProperty = await _context.Property.FindAsync(id);

            if (currentProperty == null)
            {
                return NotFound();
            }

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

            List<BasicImage> images = new List<BasicImage>();

            int imgCount = 0;
            foreach (string imageStr in addProperty.Images)
            {
                BasicImage bi = WriteImage(imageStr, imgCount);

                if (bi != null)
                {
                    images.Add(bi);
                    imgCount++;
                }
                else
                {
                    CleanImages(images);
                    return BadRequest();
                }
            }

            List<Image> oldImages = _context.Image.Where(i => i.PropertyRef == id).ToList();
            List<BasicImage> oldBasicImages = _mapper.Map<List<Image>, List<BasicImage>>(oldImages);
            
            //From DB
            foreach (Image i in oldImages)
            {
                _context.Image.Remove(i);
            }

            //From disk
            CleanImages(oldBasicImages);
            
            currentProperty.AddressLine1 = addProperty.AddressLine1;
            currentProperty.AddressLine2 = addProperty.AddressLine2;
            currentProperty.City = addProperty.City;
            currentProperty.County = addProperty.County;
            currentProperty.Postcode = addProperty.Postcode;
            currentProperty.Longitude = addProperty.Longitude;
            currentProperty.Latitude = addProperty.Latitude;
            currentProperty.PropertyDescription = addProperty.PropertyDescription;
            currentProperty.PropertyStatus = Property.VerificationStatus.Pending;

            foreach (BasicImage bi in images)
            {
                bi.PropertyRef = currentProperty.ID;
                Image im = _mapper.Map<BasicImage, Image>(bi);
                _context.Image.Add(im);
            }

            await _context.SaveChangesAsync();
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

            int imgCount = 0;
            foreach (string imageStr in addProperty.Images)
            {
                BasicImage bi = WriteImage(imageStr, imgCount);

                if(bi != null)
                {
                    images.Add(bi);
                    imgCount++;
                } else
                {
                    CleanImages(images);
                    return BadRequest();
                }
            }

            //All Images are valid and added
            _context.Property.Add(property);
            foreach (BasicImage bi in images)
            {
                bi.PropertyRef = property.ID;
                Image im = _mapper.Map<BasicImage, Image>(bi);
                _context.Image.Add(im);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private static BasicImage WriteImage(string imageStr, int imgCount)
        {
            //Block injection attempt
            if (imageStr.IndexOf("data:image/") != 0)
            {
                return null;
            }

            int pStart = imageStr.IndexOf("data:image/") + "data:image/".Length;
            int pEnd = imageStr.IndexOf(";");
            string mime = imageStr.Substring(pStart, pEnd - pStart);

            //Only allowing these types of files to be uploaded
            //otherwise completely reject
            //image/jpeg
            //image/png

            if (mime == "jpeg" || mime == "png")
            {
                string b64Content = imageStr.Substring(pStart + mime.Length + ";base64,".Length);
                byte[] imageBytes = Convert.FromBase64String(b64Content);
                string fileext = "." + mime;
                string guid = Guid.NewGuid().ToString();
                string pathUID = guid + fileext;

                System.IO.File.WriteAllBytes("c:\\temp\\" + pathUID, imageBytes);

                BasicImage bi = new BasicImage
                {
                    //Initialise as -1 prior to autogeneration of PropertyID image is bound to
                    PropertyRef = -1,
                    Position = ++imgCount,
                    Path = pathUID
                };

                return bi;
            } else
            {
                return null;
            }
  
        }

        private static void CleanImages(List<BasicImage> images)
        {
            foreach (BasicImage bi in images)
            {
                string filePath = "c:\\temp\\" + bi.Path;
                System.IO.File.Delete(filePath);
            }
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
            var property = await _context.Property.FindAsync(id);

            if (property == null)
            {
                return NotFound();
            }

            if (landlord == property.AppUserRef)
            {
                _context.Property.Remove(property);
                await _context.SaveChangesAsync();

                return Ok();
            }

            return Unauthorized();
        }

        private bool PropertyExists(int id)
        {
            return _context.Property.Any(e => e.ID == id);
        }

        private IEnumerable<BPImages> GetProperties(Property.VerificationStatus status)
        {
            IEnumerable<BPBasicImages> toImageResolve = _context
                .Property
                .Where(p => p.PropertyStatus == status)
                .Select(p => new BPBasicImages
                {
                    ID = p.ID,
                    AddressLine1 = p.AddressLine1,
                    AddressLine2 = p.AddressLine2,
                    City = p.City,
                    County = p.County,
                    Postcode = p.Postcode,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    PropertyDescription = p.PropertyDescription,
                    Timestamp = p.Timestamp,
                    Landlord = _mapper.Map<AppUser, BasicAppUser>(p.AppUser),
                    Images = _mapper.Map<ICollection<Image>, List<BasicImage>>(p.Images)
                })
                .OrderByDescending(p => p.Timestamp)
                .ToList();

            return _mapper.Map<IEnumerable<BPBasicImages>, IEnumerable<BPImages>>(toImageResolve);
        }

        private List<BPIRejections> GetMyProperties(string username)
        {
            List<BPBIRejections> toImageResolve =_context
                .Property
                .Select(p => new BPBIRejections
                {
                    ID = p.ID,
                    AddressLine1 = p.AddressLine1,
                    AddressLine2 = p.AddressLine2,
                    City = p.City,
                    County = p.County,
                    Postcode = p.Postcode,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    PropertyDescription = p.PropertyDescription,
                    PropertyStatus = p.PropertyStatus,
                    Timestamp = p.Timestamp,
                    Landlord = _mapper.Map<AppUser, BasicAppUser>(p.AppUser),
                    Images = _mapper.Map<ICollection<Image>, List<BasicImage>>(p.Images),
                    Rejections = _mapper.Map<ICollection<Rejection>, List<BasicRejection>>(p.Rejections)
                })
                .Where(p => p.Landlord.Username == username)
                .OrderByDescending(p => p.Timestamp)
                .ToList();

            return _mapper.Map<List<BPBIRejections>, List<BPIRejections>>(toImageResolve);
        }

        private BPImages GetPropertyByID(int id)
        {
            BPBasicImages toImageResolve = _context
                 .Property
                 .Select(p => new BPBasicImages
                 {
                     ID = p.ID,
                     AddressLine1 = p.AddressLine1,
                     AddressLine2 = p.AddressLine2,
                     City = p.City,
                     County = p.County,
                     Postcode = p.Postcode,
                     Latitude = p.Latitude,
                     Longitude = p.Longitude,
                     PropertyDescription = p.PropertyDescription,
                     Timestamp = p.Timestamp,
                     Landlord = _mapper.Map<AppUser, BasicAppUser>(p.AppUser),
                     Images = _mapper.Map<ICollection<Image>, List<BasicImage>>(p.Images)
                 })
                .Where(p => p.ID == id)
                .FirstOrDefault();
            
            return _mapper.Map<BPBasicImages, BPImages>(toImageResolve);
        }

        private BPIRejections GetMyPropertyByID(int id, string landlord)
        {
            BPBIRejections toImageResolve = _context
                .Property
                .Select(p => new BPBIRejections
                {
                    ID = p.ID,
                    AddressLine1 = p.AddressLine1,
                    AddressLine2 = p.AddressLine2,
                    City = p.City,
                    County = p.County,
                    Postcode = p.Postcode,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    PropertyDescription = p.PropertyDescription,
                    PropertyStatus = p.PropertyStatus,
                    Timestamp = p.Timestamp,
                    Landlord = _mapper.Map<AppUser, BasicAppUser>(p.AppUser),
                    Images = _mapper.Map<ICollection<Image>, List<BasicImage>>(p.Images),
                    Rejections = _mapper.Map<ICollection<Rejection>, List<BasicRejection>>(p.Rejections)
                })
                .Where(p => p.ID == id &&
                            p.Landlord.Username == landlord)
                .FirstOrDefault();

            return _mapper.Map<BPBIRejections, BPIRejections>(toImageResolve);
        }
    }
}