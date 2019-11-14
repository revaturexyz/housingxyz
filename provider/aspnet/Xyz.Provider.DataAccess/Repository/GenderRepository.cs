using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xyz.Provider.DataAccess.Entities;
using Xyz.Provider.Lib.Exceptions;
using Xyz.Provider.Lib.Interface;

namespace Xyz.Provider.DataAccess.Repository
{
  public class GenderRepository : IGenderRepository
  {
    private readonly RevatureHousingDbContext _dbContext;
    private readonly ILogger _logger;

    public GenderRepository(RevatureHousingDbContext dbContext, ILogger<GenderRepository> logger = null)
    {
      _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
      _logger = logger;
    }

    /// <summary>
    /// NOT IMPLEMENTED - Asynchronously removes a gender from database
    /// </summary>
    /// <param name="id"></param>
    /// <param name="providerId"></param>
    /// <returns></returns>
    public Task RemoveAsync(int id, int providerId)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Asynchronously updates the gender of a room in database
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="roomId"></param>
    /// <returns>Task</returns>
    public async Task UpdateGenderByRoomIdAsync(Lib.Models.Gender gender, int roomId)
    {
      if (roomId <= 0)
      {
        _logger?.LogWarning("Invalid ID Input.");
        throw new ArgumentOutOfRangeException(nameof(roomId), roomId, "ID must be positive.");
      }
      if (gender.GenderId <= 0)
      {
        _logger?.LogWarning("Invalid ID Input.");
        throw new ArgumentException($"Gender ID {gender.GenderId} must be positive.", nameof(gender));
      }
      if (!await _dbContext.Room.AnyAsync(r => r.RoomId == roomId).ConfigureAwait(false))
      {
        _logger?.LogWarning("ID not found in the database.");
        throw new ArgumentNotFoundException("Room", roomId, nameof(roomId));
      }
      if (!await _dbContext.Gender.AnyAsync(g => g.GenderId == gender.GenderId).ConfigureAwait(false))
      {
        _logger?.LogWarning("ID not found in the database.");
        throw new ArgumentNotFoundException("Gender", gender.GenderId, nameof(gender));
      }
      try
      {
        var resultRoom = await _dbContext.Room
          .FirstOrDefaultAsync(r => r.RoomId == roomId)
          .ConfigureAwait(false);
        resultRoom.GenderId = gender.GenderId;
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
      }
      catch (DbUpdateException e)
      {
        _logger?.LogWarning($"Error in UpdateAsync for Gender repo: {e.Message}.");
        throw;
      }
    }

    /// <summary>
    /// NOT IMPLEMENTED - Asynchronously adds a gender to database
    /// </summary>
    /// <param name="newEntity"></param>
    /// <param name="providerId"></param>
    /// <returns>Task-wrapped Library Gender model</returns>
    public Task<Lib.Models.Gender> AddAsync(Lib.Models.Gender newEntity, int providerId)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<Lib.Models.Gender>> GetAllGenderTypesAsync()
    {
      var list = await _dbContext.Gender.ToListAsync().ConfigureAwait(false);
      _logger?.LogInformation($"Genders exist: {list.Count > 0}");
      return list.Select(Mapper.Map);
    }

    /// <summary>
    /// NOT IMPLEMENTED - retrieves a gender from database
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Task-wrapped Library Gender model</returns>
    public Task<Lib.Models.Gender> GetAsync(int id)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// NOT IMPLEMENTED - Asynchronously updates a Gender in database
    /// </summary>
    /// <param name="newEntity"></param>
    /// <param name="providerId"></param>
    /// <returns>Task</returns>
    public Task UpdateAsync(Lib.Models.Gender newEntity, int providerId)
    {
      throw new NotImplementedException();
    }
  }
}
