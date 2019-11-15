using System.Linq;
using ComplexServiceDatabase.Model;
using System.Threading.Tasks;
using Logic = ComplexServiceLogic.Model;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace ComplexServiceDatabase.Repo
{
    public class ComplexRepository : IComplexRepository
    {

        private readonly ComplexDbContext _context;
        private readonly IMapper _map;
        private readonly ILogger logger;

        public ComplexRepository(ComplexDbContext context, IMapper mapper)
        {
            _context = context;
            _map = mapper;
        }

        public async Task<string> CreateComplexAsync(Logic.Complex lComplex)
        {
            Complex complex = Mapper.MapComplextoE(lComplex);

            await _context.AddAsync(complex);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return "Create!!";
        }

        public IEnumerable<Logic.Complex> ReadComplexList()
        {
            try
            {
                return _context.Complex.Select(_map.MapEtoComplex);
            }
            catch( ArgumentNullException ex )
            {
                return null;
            }
        }

        public async Task<Logic.Complex> ReadComplexAsync(Guid complexId)
        {
            Complex complexFind = await _context.Complex.FindAsync(complexId).ConfigureAwait(false);
            return _map.MapEtoComplex(complexFind);
        }

        public async Task<string> UpdateComplexAsync(Logic.Complex update)
        {
            Complex origin = _context.Complex.Find(update.ComplexId);

            if( origin.ComplexName == null )
            {
                return "no such complex";
            }

            if( update.ComplexName != null )
            {
                origin.ComplexName = update.ComplexName;
            }
            if( update.ContactNumber != null )
            {
                origin.ContactNumber = update.ContactNumber;
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return "Update completed";
        }

        public string DeleteComplex(Guid complexId)
        {
            Complex target = _context.Complex.Find(complexId);

            if ( target.ComplexName == null )
            {
                return "no such complex";
            } 
            else
            {
                _context.Remove(target);
                _context.SaveChanges();

                return "delete completed";
            }
          
        }

        public async Task<string> CreateAmenityRoomAsync(Logic.AmenityRoom ar)
        {
            AmenityRoom amenityRoom = _map.MapAmenityRoomtoE(ar);

            _context.Add(amenityRoom);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return "new amenity room created";
        }

        public async Task<string> CreateAmenityComplexAsync(Logic.AmenityComplex ac)
        {
            AmenityComplex amenityComplex = _map.MapAmenityComplextoE(ac);

            _context.Add(amenityComplex);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return "new amenity complex created";
        }

        public async Task<string> CreateAmenityAsync(Logic.Amenity amenity)
        {
            Amenity newAmenity = _map.MapAmenitytoE(amenity);

            _context.Add(newAmenity);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return "new Amenity created";
        }

        public IEnumerable<Logic.Amenity> ReadAmenityList()
        {
            return _context.Amenity.Select(_map.MapEtoAmenity);
        }

        public IEnumerable<Logic.Amenity> ReadAmenityListByComplexId(Guid guid)
        {
            List<AmenityComplex> amenityComplices
                = _context.AmenityComplex.Where(a => a.ComplexId == guid)
                                        .AsNoTracking()
                                        .ToList();

            List<Logic.Amenity> amenities = new List<Logic.Amenity>();
            foreach(var ac in amenityComplices)
            {
                amenities.Add(_map.MapEtoAmenity(_context.Amenity.Find(ac.AmenityId)));
            }

            return amenities;

        }

        public IEnumerable<Logic.Amenity> ReadAmenityListByRoomId(Guid roomId)
        {
            List<AmenityRoom> amenityRooms = _context.AmenityRoom.Where(a => a.RoomId == roomId)
                                                        .AsNoTracking()
                                                        .ToList();

            List<Logic.Amenity> amenities = new List<Logic.Amenity>();
            foreach (var ac in amenityRooms)
            {
                amenities.Add(_map.MapEtoAmenity(_context.Amenity.Find(ac.AmenityId)));
            }

            return amenities;

        }
    }
}
