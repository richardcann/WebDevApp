using AutoMapper;
using Housing.WebAPI.Models;
using Housing.WebAPI.Models.ClientServerDTO;
using Housing.WebAPI.Models.InternalDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.WebAPI.Services
{
    public class PropertyService
    {
        private IMapper _mapper;
        private readonly HousingContext _context;

        public PropertyService(
            IMapper mapper,
            HousingContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Property> Get(int id)
        {
            return await _context.Property.FindAsync(id);
        }

        public async Task<int> Add(Property prop)
        {
            _context.Property.Add(prop);
            await _context.SaveChangesAsync();
            return prop.ID;
        }

        public async void Remove(int id)
        {
            Property p = await Get(id);
            _context.Property.Remove(p);
            await _context.SaveChangesAsync();
        }

        public async void Update(int id, Property addProperty)
        {
            Property currentProperty = await Get(id);
            currentProperty.AddressLine1 = addProperty.AddressLine1;
            currentProperty.AddressLine2 = addProperty.AddressLine2;
            currentProperty.City = addProperty.City;
            currentProperty.County = addProperty.County;
            currentProperty.Postcode = addProperty.Postcode;
            currentProperty.Longitude = addProperty.Longitude;
            currentProperty.Latitude = addProperty.Latitude;
            currentProperty.PropertyDescription = addProperty.PropertyDescription;
            currentProperty.PropertyStatus = Property.VerificationStatus.Pending;
            await _context.SaveChangesAsync();
        }


        public async void Approve(int id)
        {
            UpdateStatus(id, Property.VerificationStatus.Approved);
        }

        public async void Reject(int id)
        {
            UpdateStatus(id, Property.VerificationStatus.Rejected);
        }

        private async void UpdateStatus(int id, Property.VerificationStatus status)
        {
            Property property = await Get(id);
            property.PropertyStatus = status;
            property.Timestamp = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        
        public IEnumerable<BPImages> GetProperties(Property.VerificationStatus status)
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

        public List<BPIRejections> GetMyProperties(string username)
        {
            List<BPBIRejections> toImageResolve = _context
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

        public BPImages GetPropertyByID(int id)
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

        public BPIRejections GetMyPropertyByID(int id, string landlord)
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
