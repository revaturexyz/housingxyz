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
    private readonly ILogger<Repository> log;

    public Repository(Entity.ComplexDbContext context, Mapper mapper, ILogger<Repository> logger)
    {
      _context = context;
      _map = mapper;
      log = logger;
    }

    /// <summary>
    /// Create new single complex in the database by logic complex object
    /// </summary>
    /// <param name="lComplex"></param>
    /// <returns></returns>
    public async Task<string> CreateComplexAsync(Logic.Complex lComplex)
    {
      Entity.Complex complex = _map.MapComplextoE(lComplex);

      await _context.AddAsync(complex);
      await _context.SaveChangesAsync().ConfigureAwait(false);
      log.LogInformation($"(REPO)new complex: {lComplex.ComplexId} is inserted");

      return "Create!!";
    }

    /// <summary>
    /// Read all existed complices in the database
    /// </summary>
    /// <returns></returns>
    public async Task<List<Logic.Complex>> ReadComplexListAsync()
    {
      try
      {
        List<Entity.Complex> complices = await _context.Complex.ToListAsync();

        return complices.Select(_map.MapEtoComplex).ToList();
      }
      catch (ArgumentNullException ex)
      {
        log.LogError($"(REPO){ex}: Cannot Find complex list");
        throw;
      }
    }

    /// <summary>
    /// Read single Logic complex object from complex Id
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    public async Task<Logic.Complex> ReadComplexByIdAsync(Guid complexId)
    {
      try
      {
        Entity.Complex complexFind = await _context.Complex.FindAsync(complexId).ConfigureAwait(false);

        return _map.MapEtoComplex(complexFind);
      }
      catch(Exception ex)
      {
        log.LogError($"(REPO){ex}: Cannot Find specific complex with id: {complexId}");
        throw;
      }     
    }

    /// <summary>
    /// Read single logic complex object from complex name and complex contact number
    /// </summary>
    /// <param name="name"></param>
    /// <param name="phone"></param>
    /// <returns></returns>
    public async Task<Logic.Complex> ReadComplexByNameAndNumberAsync(string name, string phone)
    {
      try
      {
        List<Entity.Complex> complex = await _context.Complex.Where(c => c.ComplexName == name
                                                          && c.ContactNumber == phone).AsNoTracking().ToListAsync();
        return _map.MapEtoComplex(complex[0]);
      }
      catch(Exception ex)
      {
        log.LogError($"(REPO){ex}: Cannot Find specific complex with name: {name} and phone: {phone}");
        throw;
      }
    }

    /// <summary>
    /// Update existed single complex by passing logic complex object
    /// </summary>
    /// <param name="update"></param>
    /// <returns></returns>
    public async Task<string> UpdateComplexAsync(Logic.Complex update)
    {
      try
      {
        Entity.Complex origin = _context.Complex.Find(update.ComplexId);

        if (update.ComplexName != null)
        {
          origin.ComplexName = update.ComplexName;
        }
        if (update.ContactNumber != null)
        {
          origin.ContactNumber = update.ContactNumber;
        }

        await _context.SaveChangesAsync().ConfigureAwait(false);
        log.LogInformation($"(REPO){update.ComplexId} is updated");

        return "Update completed";
      }
      catch(Exception ex)
      {
        log.LogError($"(REPO){ex}comlex id: {update.ComplexId} update failed");
        throw ex;
      }
    }

    /// <summary>
    /// Delete existed single complex from database by specific complex Id
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    public async Task<string> DeleteComplexAsync(Guid complexId)
    {
      try
      {
        Entity.Complex target = _context.Complex.Find(complexId);

        _context.Remove(target);
        await _context.SaveChangesAsync();
        log.LogInformation($"(REPO)target: {target.ComplexId} is deleted");

        return "delete completed";
      }
      catch (Exception ex)
      {
        log.LogError($"(REPO){ex}: complex id: {complexId} failed to delete");
        throw;
      }

    }

    /// <summary>
    /// Create new single Amenities of Room in database by amenityroom object
    /// </summary>
    /// <param name="ar"></param>
    /// <returns></returns>
    public async Task<string> CreateAmenityRoomAsync(Logic.AmenityRoom ar)
    {
      Entity.AmenityRoom amenityRoom = _map.MapAmenityRoomtoE(ar);

      _context.Add(amenityRoom);
      await _context.SaveChangesAsync().ConfigureAwait(false);
      log.LogInformation($"(REPO)new amenity of room id: {ar.RoomId}");

      return "new amenity room created";
    }

    /// <summary>
    /// Delete ALL amenity record from Amenity of room in database by room Id
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    public async Task<string> DeleteAmenityRoomAsync(Guid roomId)
    {
      try
      {
        _context.AmenityRoom.RemoveRange(_context.AmenityRoom.Where(ar => ar.RoomId == roomId));

        await _context.SaveChangesAsync();
        log.LogInformation($"AmenityRooms with room Id: {roomId} are deleted");

        return $"(REPO)RoomId: {roomId}'s Amenity Record has been deleted!";
      }
      catch( Exception ex)
      {
        log.LogError($"(REPO){ex}: cannot find such room with room id: {roomId}");
        throw ex;
      }
    }

    /// <summary>
    /// Delete ALL amenity record from Amenity of complex in database by complex Id
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    public async Task<string> DeleteAmenityComplexAsync(Guid complexId)
    {
      try
      {
        _context.AmenityComplex.RemoveRange(_context.AmenityComplex.Where(ar => ar.ComplexId == complexId));

        await _context.SaveChangesAsync();

        return $"ComplexId: {complexId}'s Amenity Record has been deleted!";
      }
      catch (ArgumentNullException ex)
      {
        log.LogWarning($"{ex}: cannot find such room with complex id: {complexId}");
        throw ex;
      }
    }

    /// <summary>
    /// Create new single Amenities of Room in database by logic amenitycomplex object
    /// </summary>
    /// <param name="ac"></param>
    /// <returns></returns>
    public async Task<string> CreateAmenityComplexAsync(Logic.AmenityComplex ac)
    {
      Entity.AmenityComplex amenityComplex = _map.MapAmenityComplextoE(ac);

      _context.Add(amenityComplex);
      await _context.SaveChangesAsync().ConfigureAwait(false);
      log.LogInformation($"(REPO)new amenity for complex: {ac.AmenityComplexId} is added");

      return "new amenity for complex created";
    }

    /// <summary>
    /// Create new single Amenity in database by logic amenity object
    /// </summary>
    /// <param name="amenity"></param>
    /// <returns></returns>
    public async Task<string> CreateAmenityAsync(Logic.Amenity amenity)
    {
      Entity.Amenity newAmenity = _map.MapAmenitytoE(amenity);

      _context.Add(newAmenity);
      await _context.SaveChangesAsync().ConfigureAwait(false);
      log.LogInformation($"(REPO)new Amenity: {amenity.AmenityType} is added");

      return "new Amenity created";
    }

    /// <summary>
    /// Read all existed amenities from the database
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Read amenity list for specific complex from database by complex Id
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    public async Task<List<Logic.Amenity>> ReadAmenityListByComplexIdAsync(Guid complexId)
    {
      try
      {
        List<Entity.AmenityComplex> amenityComplices
          = _context.AmenityComplex.Where(a => a.ComplexId == complexId).ToList();

        List<Logic.Amenity> amenities = new List<Logic.Amenity>();
        foreach (var ac in amenityComplices)
        {
          amenities.Add(_map.MapEtoAmenity(await _context.Amenity.FindAsync(ac.AmenityId)));
          log.LogInformation($"(REPO)amenity: {ac.AmenityId} is found and added");
        }

        return amenities;
      }
      catch(Exception ex)
      {
        log.LogError($"(REPO){ex}: amenities of complex is not found");
        throw;
      }

    }

    /// <summary>
    /// Read amenity list for specific room from database by room Id 
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    public async Task<List<Logic.Amenity>> ReadAmenityListByRoomIdAsync(Guid roomId)
    {
      try
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
      catch(Exception ex)
      {
        log.LogError($"(REPO){ex}: (REPO)amenities for room id: {roomId} are not found");
        throw;
      }

    }

    /// <summary>
    /// Read complex list for specific provider from database by provider Id
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    public async Task<List<Logic.Complex>> ReadComplexByProviderIdAsync(Guid pId)
    {
      try
      {
        List<Entity.Complex> complices = await _context.Complex.Where(c => c.ProviderId == pId).ToListAsync();

        return complices.Select(_map.MapEtoComplex).ToList();
      }
      catch(Exception ex)
      {
        log.LogError($"(REPO){ex}: comlices of provider Id: {pId} are not found");
        throw;
      }
    }

    /// <summary>
    /// Update existed single amenity info in the database by logic amenity object
    /// </summary>
    /// <param name="amenity"></param>
    /// <returns></returns>
    public async Task<string> UpdateAmenityAsync(Logic.Amenity amenity)
    {
      try
      {
        Entity.Amenity eAmenity = await _context.Amenity.FindAsync(amenity.AmenityId);

        if (amenity.AmenityType != null)
        {
          eAmenity.AmenityType = amenity.AmenityType;
        }
        if (amenity.Description != null)
        {
          eAmenity.Description = amenity.Description;
        }

        _context.Amenity.OrderBy(a => a.AmenityId);
        await _context.SaveChangesAsync();
        log.LogInformation($"(REPO)amenity: {amenity.AmenityId} {amenity.AmenityType} is updated");

        return "Amenity Updated";
      }
      catch(Exception ex)
      {
        log.LogError($"(REPO){ex}: update failed");
        throw;
      }
    }

    /// <summary>
    /// Delete existed single amenity info in the database by logic amenity object
    /// </summary>
    /// <param name="amenity"></param>
    /// <returns></returns>
    public async Task<string> DeleteAmenityAsync(Logic.Amenity amenity)
    {
      try
      {
        Entity.Amenity dAmenity = await _context.Amenity.FindAsync(amenity.AmenityId);

        _context.Remove(dAmenity);

        await _context.SaveChangesAsync();
        log.LogInformation($"(REPO)amenity: {dAmenity.AmenityId} {dAmenity.AmenityType} is deleted");

        return "Amenity deleted";
      }
      catch (InvalidOperationException ex)
      {
        log.LogError($"(REPO){ex}: detele failed amenity Id: {amenity.AmenityId}");
        throw;
      }


    }


  }//end of class
}
