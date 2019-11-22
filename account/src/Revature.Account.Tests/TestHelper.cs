/* Helper methods for the Moq-Testing of methods.
 * While not a test itself, the Test Helper assists in testing both API-Coordinator-methods and Data-Access-methods.
 * Source: Adapted from "Database and Dragons" TestHelper class.
 */

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Revature.Account.Api.Controllers;
using Revature.Account.Lib.Model;
using Serilog;
using System;
using System.Collections.Generic;

namespace Revature.Account.Tests
{
  /// <summary>
  /// Helper methods for the Moq-Testing of methods.
  /// </summary>
  public class TestHelper
  {

    public Mock<Revature.Account.Lib.Interface.IGenericRepository> Repository { get; private set; }

    //API Controller Instantiation
    public CoordinatorAccountController CoordinatorAccountController { get; private set; }
    public NotificationController NotificationController { get; private set; }
    public ProviderAccountController ProviderAccountController { get; private set; }


    public List<CoordinatorAccount> Coordinators { get; private set; }
    public List<Notification> Notifications { get; private set; }
    public List<ProviderAccount> Providers { get; private set; }
    public List<Status> Statuses { get; private set; }

    //for testing expiration times
    public static DateTime now;
    public static DateTime nowPSev;
    public static DateTime nowPThirty;

    public static Microsoft.Extensions.Logging.ILogger<TestHelper> _logger;


    public TestHelper()
    {

      _logger = new NullLogger<TestHelper>();

      SetUpCoordinators();
      SetUpStatuses();
      SetUpProviderAccount();
      SetUpNotifications();
      SetUpMocks();

      now = DateTime.Now;
      nowPSev = now.AddDays(7);
      nowPThirty = now.AddDays(30);
    }

    /// <summary>
    /// Set up the example database-entries used by the "moqed" database.
    /// </summary>
    private void SetUpCoordinators()
    {
      Coordinators = new List<CoordinatorAccount>
      {
        new CoordinatorAccount
        {
          Name = "Jacob",
          Email = "jacobs.email@gmail.com",
          TrainingCenterName = "Arlington",
          TrainingCenterAddress = "604 S. West, Arlington, TX, 76010"
        },
        new CoordinatorAccount
        {
          Name = "Kimberly",
          Email = "Kimberly.email@gmail.com",
          TrainingCenterName = "Honolulu",
          TrainingCenterAddress = "555 Kaumakani St, Honolulu, HI 96825"
        },
        new CoordinatorAccount
        {
          Name = "Jimmy",
          Email = "Jimmy.email@gmail.com",
          TrainingCenterName = "NYC Midtown Center",
          TrainingCenterAddress = "348 e 66th st new york ny 10065"
        },
      };
    }


    private void SetUpProviderAccount()
    {
      Providers = new List<ProviderAccount>
      {
        new ProviderAccount
        {
          Coordinator = Coordinators[0],
          Name = "Billys Big Discount Dorms",
          Status = Statuses[0],
          AccountCreatedAt = now,
          AccountExpiresAt = nowPSev
        },
        new ProviderAccount
        {
          Coordinator = Coordinators[0],
          Name = "Bobs Townhomes",
          Status = Statuses[1],
          AccountCreatedAt = now,
          AccountExpiresAt = nowPSev
        },
        new ProviderAccount
        {
          Coordinator = Coordinators[0],
          Name = "Burgundy Hills Barracks",
          Status = Statuses[3],
          AccountCreatedAt = now,
          AccountExpiresAt = nowPSev
        }
      };
    }

    private void SetUpNotifications()
    {
      Notifications = new List<Notification>
      {
        new Notification
        {
          ProviderId = Providers[0].ProviderId,
          CoordinatorId = Coordinators[0].CoordinatorId,
          Status = Statuses[0],
          AccountExpiresAt = nowPSev
        },
        new Notification
        {
          ProviderId = Providers[1].ProviderId,
          CoordinatorId = Coordinators[0].CoordinatorId,
          Status = Statuses[2],
          AccountExpiresAt = nowPSev
        },
        new Notification()
        {
          ProviderId = Providers[2].ProviderId,
          CoordinatorId = Coordinators[0].CoordinatorId,
          Status = Statuses[1],
          AccountExpiresAt = nowPSev
        }
      };
    }

    private void SetUpStatuses()
    {
      Statuses = new List<Status>
      {
        new Status()
        {
          StatusId = 1,
          StatusText = "Pending"
        },
        new Status()
        {
          StatusId = 2,
          StatusText = "Approved"
        },
        new Status()
        {
          StatusId = 3,
          StatusText = "Rejected"
        },
        new Status()
        {
          StatusId = 4,
          StatusText = "Under Review"
        }
      };
    }

    /// <summary>
    /// Instantiates the Moq instance (Mock) and seed-data, specifically the controllers in the API.
    /// </summary>
    private void SetUpMocks()
    {
      Repository = new Mock<Revature.Account.Lib.Interface.IGenericRepository>();

      CoordinatorAccountController = new CoordinatorAccountController(Repository.Object, _logger);
      CoordinatorAccountController.ControllerContext = new ControllerContext();
      CoordinatorAccountController.ControllerContext.HttpContext = new DefaultHttpContext();
      CoordinatorAccountController.ControllerContext.HttpContext.Request.Headers["Authorize"] = "Not a token.";

      ProviderAccountController = new ProviderAccountController(Repository.Object, _logger);
      ProviderAccountController.ControllerContext = new ControllerContext();
      ProviderAccountController.ControllerContext.HttpContext = new DefaultHttpContext();
      ProviderAccountController.ControllerContext.HttpContext.Request.Headers["Authorize"] = "Not a token.";

      NotificationController = new NotificationController(Repository.Object, _logger);
      NotificationController.ControllerContext = new ControllerContext();
      NotificationController.ControllerContext.HttpContext = new DefaultHttpContext();
      NotificationController.ControllerContext.HttpContext.Request.Headers["Authorize"] = "Not a token.";
    }
  }
}
