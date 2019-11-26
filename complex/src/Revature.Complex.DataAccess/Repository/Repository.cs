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
    public async Task<bool> CreateComplexAsync(Logic.Complex lComplex)
    {
      Entity.Complex complex = _map.MapComplextoE(lComplex);

      await _context.AddAsync(complex);
      await _context.SaveChangesAsync();
      log.LogInformation($"(REPO)new complex: {lComplex.ComplexId} is inserted");

      return true;
    }

    /// <summary>
    /// Read all existed complices in the database
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">list of complex not found</exception>
    public async Task<List<Logic.Complex>> ReadComplexListAsync()
    {
      try
      {
        List<Entity.Complex> complices = await _context.Complex.ToListAsync();

        return complices.Select(_map.MapEtoComplex).ToList();
      }
      catch (ArgumentNullException ex)
      {
        log.LogError($"(REPO){ex} Cannot find list of complex");
        throw;
      }
    }

    /// <summary>
    /// Read single Logic complex object from complex Id
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">complex not found by id</exception>
    public async Task<Logic.Complex> ReadComplexByIdAsync(Guid complexId)
    {
      try
      {
        Entity.Complex complexFind = await _context.Complex.FindAsync(complexId).ConfigureAwait(false);
        return _map.MapEtoComplex(complexFind);
      }
      catch (ArgumentException ex)
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
    /// <exception cref="ArgumentNullException">list of complex not found</exception>
    public async Task<Logic.Complex> ReadComplexByNameAndNumberAsync(string name, string phone)
    {
      try
      {
        Entity.Complex complex = _context.Complex.Where(c => c.ComplexName == name
                                                          && c.ContactNumber == phone).AsNoTracking().First();
        return _map.MapEtoComplex(complex);
      }
      catch(ArgumentException ex)
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
    /// <exception cref="ArgumentNullException">complex not found</exception>
    public async Task<bool> UpdateComplexAsync(Logic.Complex update)
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

        await _context.SaveChangesAsync();
        log.LogInformation($"(REPO){update.ComplexId} is updated");

        return true;
      }
      catch(ArgumentException ex)
      {
        log.LogError($"(REPO){ex}comlex id: {update.ComplexId} update failed");
        throw;
      }
    }

    /// <summary>
    /// Delete existed single complex from database by specific complex Id
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">complex not found</exception>
    public async Task<bool> DeleteComplexAsync(Guid complexId)
    {
      try
      {
        Entity.Complex target = _context.Complex.Find(complexId);

        _context.Remove(target);
        await _context.SaveChangesAsync();
        log.LogInformation($"(REPO)target: {target.ComplexId} is deleted");

        return true;
      }
      catch (ArgumentException ex)
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
    public async Task<bool> CreateAmenityRoomAsync(Logic.AmenityRoom ar)
    {
      Entity.AmenityRoom amenityRoom = _map.MapAmenityRoomtoE(ar);

      _context.Add(amenityRoom);
      await _context.SaveChangesAsync();
      log.LogInformation($"(REPO)new amenity of room id: {ar.RoomId}");

      return true;
    }

    /// <summary>
    /// Delete ALL amenity record from Amenity of room in database by room Id
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">list of amenityroom not found</exception>
    public async Task<bool> DeleteAmenityRoomAsync(Guid roomId)
    {
      try
      {
        _context.AmenityRoom.RemoveRange(_context.AmenityRoom.Where(ar => ar.RoomId == roomId));

        await _context.SaveChangesAsync();
        log.LogInformation($"AmenityRooms with room Id: {roomId} are deleted");

        return true;
      }
      catch(ArgumentException ex)
      {
        log.LogError($"(REPO){ex}: cannot find such room with room id: {roomId}");
        throw;
      }
    }

    /// <summary>
    /// Delete ALL amenity record from Amenity of complex in database by complex Id
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">list of amenitycomplex not found</exception>
    public async Task<bool> DeleteAmenityComplexAsync(Guid complexId)
    {
      try
      {
        _context.AmenityComplex.RemoveRange(_context.AmenityComplex.Where(ar => ar.ComplexId == complexId));

        await _context.SaveChangesAsync();

        return true;
      }
      catch (ArgumentNullException ex)
      {
        log.LogWarning($"{ex}: cannot find such room with complex id: {complexId}");
        throw;
      }
    }

    /// <summary>
    /// Create new single Amenities of Room in database by logic amenitycomplex object
    /// </summary>
    /// <param name="ac"></param>
    /// <returns></returns>
    public async Task<bool> CreateAmenityComplexAsync(Logic.AmenityComplex ac)
    {
      Entity.AmenityComplex amenityComplex = _map.MapAmenityComplextoE(ac);

      _context.Add(amenityComplex);
      await _context.SaveChangesAsync();
      log.LogInformation($"(REPO)new amenity for complex: {ac.AmenityComplexId} is added");

      return true;
    }

    /// <summary>
    /// Create new single Amenity in database by logic amenity object
    /// </summary>
    /// <param name="amenity"></param>
    /// <returns></returns>
    public async Task<bool> CreateAmenityAsync(Logic.Amenity amenity)
    {
      Entity.Amenity newAmenity = _map.MapAmenitytoE(amenity);

      _context.Add(newAmenity);
      await _context.SaveChangesAsync();
      log.LogInformation($"(REPO)new Amenity: {amenity.AmenityType} is added");

      return true;
    }

    /// <summary>
    /// Read all existed amenities from the database
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">list of amenity not found</exception>
    public async Task<List<Logic.Amenity>> ReadAmenityListAsync()
    {
      try
      {
        List<Entity.Amenity> amenities = await _context.Amenity.ToListAsync();

        return amenities.Select(_map.MapEtoAmenity).ToList();
      }
      catch (ArgumentNullException ex)
      {
        log.LogError($"(REPO){ex}: cannot find Amenity list is the database");
        throw;
      }
    }

    /// <summary>
    /// Read amenity list for specific complex from database by complex Id
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">list of amenity by complex id not found</exception>
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
      catch(ArgumentException ex)
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
    /// <exception cref="ArgumentNullException">list of amenity by room id not found</exception>
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
        log.LogError($"(REPO){ex}amenities for room id: {roomId} are not found");
        throw;
      }

    }

    /// <summary>
    /// Read complex list for specific provider from database by provider Id
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">list of complex not found</exception>
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
    /// <exception cref="ArgumentNullException">list of complex not found</exception>
    public async Task<bool> UpdateAmenityAsync(Logic.Amenity amenity)
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

        return true;
      }
      catch(ArgumentException ex)
      {
        log.LogWarning($"{ex}: Unable to update the amenity.");
        throw;
      }
    }

    /// <summary>
    /// Delete existed single amenity info in the database by logic amenity object
    /// </summary>
    /// <param name="amenity"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Unable to delete the amenity</exception>
    public async Task<bool> DeleteAmenityAsync(Logic.Amenity amenity)
    {
      try
      {
        Entity.Amenity dAmenity = await _context.Amenity.FindAsync(amenity.AmenityId);

        _context.Remove(dAmenity);

        await _context.SaveChangesAsync();
        log.LogInformation($"(REPO)amenity: {dAmenity.AmenityId} {dAmenity.AmenityType} is deleted");

        return true;
      }
      catch (InvalidOperationException ex)
      {
        log.LogWarning($"{ex}: Unable to delete the amenity.");
        throw;
      }
    }
  }//end of class
}
