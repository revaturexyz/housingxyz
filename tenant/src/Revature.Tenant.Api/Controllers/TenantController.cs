using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Tenant.Lib.Interface;
using Revature.Tenant.Api.Models;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Revature.Tenant.Api.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Revature.Tenant.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TenantController : ControllerBase
  {
    private readonly IServiceBusSender _serviceBusSender;
    private readonly ITenantRepository _tenantRepository;
    private readonly ILogger _logger;

    public TenantController(ITenantRepository tenantRepository, ILogger<TenantController> logger = null)
    {
      _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository), "Tenant cannot be null");
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Get all tenants
    /// </summary>
    /// <returns></returns>

    // GET: api/Tenant
    [HttpGet(Name = "GetAllAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ApiTenant>>> GetAllAsync([FromQuery] ApiSearchParameters apiSearchParameters)
    {

      string firstName = apiSearchParameters.FirstName;
      string lastName = apiSearchParameters.LastName;
      string gender = apiSearchParameters.Gender;
      Guid? trainingCenter;
      if (apiSearchParameters.TrainingCenter != null)
        trainingCenter= Guid.Parse(apiSearchParameters.TrainingCenter);
      else
        trainingCenter = null;


      _logger.LogInformation("GET - Getting tenants");
      var tenants = await _tenantRepository.GetAllAsync(firstName, lastName, gender, trainingCenter);

      List<Lib.Models.Tenant> newTenants = new List<Lib.Models.Tenant>();
      foreach (Lib.Models.Tenant tenant in tenants)
      {
        Lib.Models.Batch batch;
        int? batchId;
        if (tenant.Batch != null)
        {
          batch = new Lib.Models.Batch
          {
            Id = tenant.Batch.Id,
            BatchCurriculum = tenant.Batch.BatchCurriculum,
            TrainingCenter = tenant.Batch.TrainingCenter
          };
          batch.SetStartAndEndDate(tenant.Batch.StartDate, tenant.Batch.EndDate);
          batchId = tenant.BatchId;
        }
        else
        {
          batch = null;
          batchId = null;
        }
        tenant.Batch = batch;
        Lib.Models.Car car;
        int? carId;
        if (tenant.Car != null)
        {
          car = new Lib.Models.Car()
          {
            Id = tenant.Car.Id,
            LicensePlate = tenant.Car.LicensePlate,
            Make = tenant.Car.Make,
            Model = tenant.Car.Model,
            Color = tenant.Car.Color,
            Year = tenant.Car.Year,
            State = tenant.Car.State
          };
          carId = tenant.CarId;
        }
        else
        {
          car = null;
          carId = null;
        }
        tenant.Car = car;

        newTenants.Add(tenant);
      }

      List<ApiTenant> apiTenants = new List<ApiTenant>();
      foreach (Lib.Models.Tenant apiTenant in newTenants)
      {

        ApiTenant newApiTenant = new ApiTenant
        {
          Id = apiTenant.Id,
          Email = apiTenant.Email,
          Gender = apiTenant.Gender,
          FirstName = apiTenant.FirstName,
          LastName = apiTenant.LastName,
          AddressId = apiTenant.AddressId,
          RoomId = apiTenant.RoomId,
          CarId = apiTenant.CarId,
          BatchId = apiTenant.BatchId,
          TrainingCenter = apiTenant.TrainingCenter
        };
        apiTenants.Add(newApiTenant);
      }
      return Ok(apiTenants);


    }

    /// <summary>
    /// Get Tenant by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/Tenant/5
    //api/[controller]
    [HttpGet("{id}", Name = "GetByIdAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiTenant>> GetByIdAsync([FromRoute] Guid id)
    {
      _logger.LogInformation("GET - Getting notifications by Tenant ID: {TenantId}", id);
      try
      {
        var tenant = await _tenantRepository.GetByIdAsync(id);

        var apiTenant = new ApiTenant
        {
          Id = tenant.Id,
          Email = tenant.Email,
          Gender = tenant.Gender,
          FirstName = tenant.FirstName,
          LastName = tenant.LastName,
          AddressId = tenant.AddressId,
          RoomId = tenant.RoomId,
          CarId = tenant.CarId,
          BatchId = tenant.BatchId,
          TrainingCenter = tenant.TrainingCenter
        };

        if (apiTenant.CarId != null)
        {
          apiTenant.ApiCar = new ApiCar
          {
            Id = tenant.Car.Id,
            Color = tenant.Car.Color,
            Make = tenant.Car.Make,
            Model = tenant.Car.Model,
            LicensePlate = tenant.Car.LicensePlate,
            State = tenant.Car.State,
            Year = tenant.Car.Year
          };
        }

        if (apiTenant.BatchId != null)
        {
          apiTenant.ApiBatch = new ApiBatch
          {
            Id = tenant.Batch.Id,
            BatchCurriculum = tenant.Batch.BatchCurriculum,
            StartDate = tenant.Batch.StartDate,
            EndDate = tenant.Batch.EndDate,
            TrainingCenter = tenant.Batch.TrainingCenter
          };
        }

        return Ok(apiTenant);
      }
      catch (ArgumentException)
      {
        _logger.LogWarning("Tenant was not found");

        return NotFound();
      }
      catch (Exception e)
      {
        _logger.LogError("Get request failed. Error: " + e.Message);

        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Get all batches by training center id
    /// </summary>
    /// <returns></returns>
    // GET: api/Tenant/Batch/[guid]
    [HttpGet("Batch", Name = "GetAllBatches")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ApiBatch>>> GetAllBatches([FromQuery] string trainingCenterString)
    {
      try
      {
        Guid trainingCenter = Guid.Parse(trainingCenterString);
        var batches = await _tenantRepository.GetBatchesAsync(trainingCenter);
        return Ok(batches);
      }
      catch (ArgumentException)
      {
        _logger.LogWarning("Training Center was not found");

        return NotFound();
      }
      catch (Exception e)
      {
        _logger.LogError("Get request failed. Error: " + e.Message);

        return StatusCode(500, e.Message);
      }
    }



    /// <summary>
    /// Posts Tenant to Db
    /// </summary>
    /// <param name="value"></param>
    // POST: api/Tenant
    [HttpPost("RegisterTenant", Name = "RegisterTenant")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiTenant>> PostAsync([FromBody] ApiTenant tenant)
    {
      _logger.LogInformation("POST - Making tenant for tenant ID {tenantId}.", tenant.Id);
      try
      {
        
        var newTenant = new Lib.Models.Tenant
        {
          Id = Guid.NewGuid(),
          Email = tenant.Email,
          Gender = tenant.Gender,
          FirstName = tenant.FirstName,
          LastName = tenant.LastName,
          AddressId = tenant.AddressId,
          RoomId = null, //Room Service will set this later
          CarId = null,
          BatchId = tenant.BatchId,
          TrainingCenter = tenant.TrainingCenter,
          
        };
        if (tenant.ApiCar != null)
        {
          newTenant.Car = new Lib.Models.Car
          {
            Color = tenant.ApiCar.Color,
            Make = tenant.ApiCar.Make,
            Model = tenant.ApiCar.Model,
            LicensePlate = tenant.ApiCar.LicensePlate,
            State = tenant.ApiCar.State,
            Year = tenant.ApiCar.Year
          };
          newTenant.CarId = newTenant.Car.Id;
        }

        await _tenantRepository.AddAsync(newTenant);

        await _tenantRepository.SaveAsync();
        _logger.LogInformation("POST Persisted to dB");

        return Created($"api/Tenant/{newTenant.Id}", newTenant);
        return StatusCode(StatusCodes.Status418ImATeapot);
      }
      catch (ArgumentException)
      {
        _logger.LogWarning("Not Found");
        return NotFound();
      }
      catch (InvalidOperationException e)
      {
        _logger.LogError("POST request failed. Error: " + e.Message);
        return Conflict(e.Message);
      }
      catch (Exception e)
      {
        _logger.LogError("POST request failed. Error: " + e.Message);
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Updates Tenant within Db
    /// </summary>
    /// <param name="value"></param>
    // POST: api/Tenant
    [HttpPut("UpdateTenant", Name = "UpdateTenant")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateAsync([FromBody, Bind("tenant")] ApiTenant tenant)
    {
      try
      {
        _logger.LogInformation("PUT - Updating tenant with tenantid {tenantId}.", tenant.Id);
        var newTenant = new Lib.Models.Tenant
        {
          Id = (Guid) tenant.Id,
          Email = tenant.Email,
          Gender = tenant.Gender,
          FirstName = tenant.FirstName,
          LastName = tenant.LastName,
          AddressId = tenant.AddressId,
          RoomId = tenant.RoomId,
          CarId = tenant.CarId,
          BatchId = tenant.BatchId,
          TrainingCenter = tenant.TrainingCenter 
        };

        if (tenant.ApiCar != null)
        {
          newTenant.Car = new Lib.Models.Car
          {
            Id = tenant.ApiCar.Id,
            Color = tenant.ApiCar.Color,
            Make = tenant.ApiCar.Make,
            Model = tenant.ApiCar.Model,
            LicensePlate = tenant.ApiCar.LicensePlate,
            State = tenant.ApiCar.State,
            Year = tenant.ApiCar.Year
          };
        }

        _tenantRepository.Put(newTenant);
        await _tenantRepository.SaveAsync();

        _logger.LogInformation("PUT persisted to dB");
        //ICollection<Lib.Models.Tenant> tenants = await _tenantRepository.GetAllAsync();
        //newTenant = tenants.First(t => t.Id == newTenant.Id);

        //ApiTenant apiTenant = new ApiTenant
        //{
        //  Id = tenant.Id,
        //  Email = tenant.Email,
        //  Gender = tenant.Gender,
        //  FirstName = tenant.FirstName,
        //  LastName = tenant.LastName,
        //  AddressId = tenant.AddressId,
        //  RoomId = tenant.RoomId,
        //  CarId = tenant.CarId,
        //  BatchId = tenant.BatchId,
        //  TrainingCenter = tenant.TrainingCenter
        //};

        //if (newTenant.Car != null)
        //{
        //  apiTenant.ApiCar = new ApiCar
        //  {
        //    Id = tenant.ApiCar.Id,
        //    Color = tenant.ApiCar.Color,
        //    Make = tenant.ApiCar.Make,
        //    Model = tenant.ApiCar.Model,
        //    LicensePlate = tenant.ApiCar.LicensePlate,
        //    State = tenant.ApiCar.State,
        //    Year = tenant.ApiCar.Year
        //  };
        //}

        return StatusCode(204);
      }
      catch (ArgumentException)
      {
        _logger.LogWarning("PUT request failed. Not Found Exception");
        return NotFound();
      }
      catch (InvalidOperationException e)
      {
        _logger.LogError("PUT request failed. Error: " + e.Message);
        return Conflict(e.Message);
      }
      catch (Exception e)
      {
        _logger.LogError("PUT request failed. Error: " + e.Message);
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Delete a tenant by id
    /// </summary>
    /// <param name="id">Guid Id, converted from string in Query String</param>
    [HttpDelete("Delete", Name = "DeleteTenant")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteAsync([FromBody] string id)
    {
      try
      {
        await _tenantRepository.DeleteByIdAsync(Guid.Parse(id));
        await _tenantRepository.SaveAsync();
        return StatusCode(StatusCodes.Status204NoContent);
      }
      catch (ArgumentException)
      {
        _logger.LogWarning("DELETE request failed. Not Found Exception");
        return NotFound();
      }
      catch (InvalidOperationException e)
      {
        _logger.LogError("DELETE request failed. Error: " + e.Message);
        return Conflict(e.Message);
      }
      catch (Exception e)
      {
        _logger.LogError("DELETE request failed. Error: " + e.Message);
        return StatusCode(500, e.Message);
      }
    }

  }
}
