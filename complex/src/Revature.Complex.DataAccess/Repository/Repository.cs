using System.Linq;
using System.Threading.Tasks;
using Logic = Revature.Complex.Lib.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using Revature.Complex.Lib.Interface;
using Entity = Revature.Complex.DataAccess.Entities;

namespace Revature.Complex.DataAccess.Repository
{
  public class Repository : IRepository
  {

    private readonly Entity.ComplexDbContext _context;
    private readonly Mapper _map;
    private readonly ILogger log;

    public Repository(Entity.ComplexDbContext context, Mapper mapper)
    {
      _context = context;
      _map = mapper;
    }

    public async Task<string> CreateComplexAsync(Logic.Complex lComplex)
    {
      Entity.Complex complex = _map.MapComplextoE(lComplex);

      await _context.AddAsync(complex);
      await _context.SaveChangesAsync().ConfigureAwait(false);

      return "Create!!";
    }

    public async Task<List<Logic.Complex>> ReadComplexListAsync()
    {
      try
      {
        List<Entity.Complex> complices = await _context.Complex.ToListAsync();

        return complices.Select(_map.MapEtoComplex).ToList();
      }
      catch (ArgumentNullException ex)
      {
        throw ex;
      }
    }

    public async Task<Logic.Complex> ReadComplexAsync(Guid complexId)
    {
      Entity.Complex complexFind = await _context.Complex.FindAsync(complexId).ConfigureAwait(false);
      return _map.MapEtoComplex(complexFind);
    }

    public async Task<string> UpdateComplexAsync(Logic.Complex update)
    {
      Entity.Complex origin = _context.Complex.Find(update.ComplexId);

      if (origin.ComplexName == null)
      {
        return "no such complex";
      }

      if (update.ComplexName != null)
      {
        origin.ComplexName = update.ComplexName;
      }
      if (update.ContactNumber != null)
      {
        origin.ContactNumber = update.ContactNumber;
      }

      await _context.SaveChangesAsync().ConfigureAwait(false);
      return "Update completed";
    }

    public async Task<string> DeleteComplexAsync(Guid complexId)
    {
      Entity.Complex target = _context.Complex.Find(complexId);

      if (target.ComplexName == null)
      {
        return "no such complex";
      }
      else
      {
        _context.Remove(target);
        await _context.SaveChangesAsync();

        return "delete completed";
      }

    }

    public async Task<string> CreateAmenityRoomAsync(Logic.AmenityRoom ar)
    {
      Entity.AmenityRoom amenityRoom = _map.MapAmenityRoomtoE(ar);

      _context.Add(amenityRoom);
      await _context.SaveChangesAsync().ConfigureAwait(false);

      return "new amenity room created";
    }

    public async Task<string> CreateAmenityComplexAsync(Logic.AmenityComplex ac)
    {
      Entity.AmenityComplex amenityComplex = _map.MapAmenityComplextoE(ac);

      _context.Add(amenityComplex);
      await _context.SaveChangesAsync().ConfigureAwait(false);

      return "new amenity complex created";
    }

    public async Task<string> CreateAmenityAsync(Logic.Amenity amenity)
    {
      Entity.Amenity newAmenity = _map.MapAmenitytoE(amenity);

      _context.Add(newAmenity);
      await _context.SaveChangesAsync().ConfigureAwait(false);

      return "new Amenity created";
    }

    public async Task<List<Logic.Amenity>> ReadAmenityListAsync()
    {
      try
      {
        List<Entity.Amenity> amenities = await _context.Amenity.ToListAsync();

        return amenities.Select(_map.MapEtoAmenity).ToList();
      }
      catch (ArgumentNullException ex)
      {
        throw ex;
      }
    }

    public async Task<List<Logic.Amenity>> ReadAmenityListByComplexIdAsync(Guid complexId)
    {
      List<Entity.AmenityComplex> amenityComplices
        = _context.AmenityComplex.Where(a => a.ComplexId == complexId).ToList();

      List<Logic.Amenity> amenities = new List<Logic.Amenity>();
      foreach (var ac in amenityComplices)
      {
        amenities.Add(_map.MapEtoAmenity(await _context.Amenity.FindAsync(ac.AmenityId)));
      }

      return amenities;

    }

    public async Task<List<Logic.Amenity>> ReadAmenityListByRoomIdAsync(Guid roomId)
    {
      List<Entity.AmenityRoom> amenityRooms
        = await _context.AmenityRoom.Where(a => a.RoomId == roomId).AsNoTracking().ToListAsync();

      List<Logic.Amenity> amenities = new List<Logic.Amenity>();
      foreach (var ac in amenityRooms)
      {
        amenities.Add(_map.MapEtoAmenity(_context.Amenity.Find(ac.AmenityId)));
      }

      return amenities;

    }

    public async Task<List<Logic.Complex>> ReadComplexByProviderID(Guid pId)
    {
      List<Entity.Complex> complices = await _context.Complex.Where(c => c.ProviderId == pId).ToListAsync();

      return complices.Select(_map.MapEtoComplex).ToList();
    }

    public async Task<string> UpdateAmenityAsync(Logic.Amenity amenity)
    {
      try
      {
        Entity.Amenity eAmenity = await _context.Amenity.FindAsync(amenity.AmenityId);

        eAmenity.AmenityType = amenity.AmenityType;
        eAmenity.Description = amenity.Description;

        _context.Amenity.OrderBy(a => a.AmenityId);
        await _context.SaveChangesAsync();

        return "Amenity Updated";
      }
      catch(ArgumentNullException ex)
      {
        throw ex;
      }
    }

    public async Task<string> DeleteAmenityAsync(Logic.Amenity amenity)
    {
      try
      {
        Entity.Amenity dAmenity = await _context.Amenity.FindAsync(amenity.AmenityId);
        _context.Remove(dAmenity);

        await _context.SaveChangesAsync();

        return "Amenity deleted";
      }
      catch( ArgumentNullException ex )
      {
        Entity.Amenity dAmenity1 = _context.Amenity.Where(a => a.AmenityType == amenity.AmenityType
                                                           || a.Description == amenity.Description)
                                                  .AsNoTracking().First();
        _context.Remove(dAmenity1);

        await _context.SaveChangesAsync();

        return "Amenity deleted";
      }
      catch( ArgumentException ex)
      {
        throw;
      }
    }
  }//end of class
}
