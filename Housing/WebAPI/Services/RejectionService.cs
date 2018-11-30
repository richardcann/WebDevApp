using AutoMapper;
using Housing.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.WebAPI.Services
{
    public class RejectionService
    {
        private IMapper _mapper;
        private readonly HousingContext _context;

        public RejectionService(
            IMapper mapper,
            HousingContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Add(Rejection r)
        {
            _context.Rejection.Add(r);
            await _context.SaveChangesAsync();
        }

        public List<Rejection> GetRejections(int propertyId)
        {
            List<Rejection> rejection = _context.
                            Rejection.
                            Where(p => p.PropertyRef == propertyId).
                            OrderByDescending(p => p.Timestamp).
                            ToList();

            return rejection;
        }

        public async void RemovePropertyRejections(int propertyId)
        {
            List<Rejection> rejections = _context.Rejection.Where(i => i.PropertyRef == propertyId).ToList();
            foreach (Rejection r in rejections)
            {
                _context.Rejection.Remove(r);
            }

            await _context.SaveChangesAsync();
        }
    }
}
