using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Revature.Account.Api.Controllers;
using Revature.Account.Lib.Model;
using System;
using System.Collections.Generic;

namespace Revature.Account.Tests
{
  public class TestHelper
  {
    public Mock<Revature.Account.Lib.Interface.IGenericRepository> IRepository { get; private set; }
    public CoordinatorAccountController ICoordinatorAccountController { get; private set; }
    public NotificationController INotificationController { get; private set; }
    public ProviderAccountController IProviderAccountController { get; private set; }
    public List<CoordinatorAccount> ICoordinators { get; private set; }
    public List<Notification> INotificationList { get; private set; }
    public List<ProviderAccount> IProviderAccountList { get; private set; }

    public static DateTime now = DateTime.Now;
    public static DateTime nowPSev = DateTime.Now.AddDays(7.0);
    public static DateTime nowPThirty = DateTime.Now.AddDays(30.0);

    public TestHelper()
    {
      SetUpCoordinators(INotificationList);
      SetUpProviderAccount(INotificationList);
      SetUpNotifications();
      SetUpMocks();
    }

    private void SetUpCoordinators(List<Notification> nList)
    {
      ICoordinators = new List<CoordinatorAccount>
            {
                //1
                new CoordinatorAccount
                {
                    Name = "Jacob",
                    Email = "jacobs.email@gmail.com",
                    TrainingCenterName = "Arlington",
                    TrainingCenterAddress = "604 S. West, Arlington, TX, 76010",
                    //1
                    //Notification = nList[0]
                },
                //2
                new CoordinatorAccount
                {
                    Name = "Kimberly",
                    Email = "Kimberly.email@gmail.com",
                    TrainingCenterName = "Honolulu",
                    TrainingCenterAddress = "555 Kaumakani St, Honolulu, HI 96825",
                    //2
                   // Notification = nList[1]
                },
                //3
                new CoordinatorAccount
                {
                    Name = "Jimmy",
                    Email = "Jimmy.email@gmail.com",
                    TrainingCenterName = "NYC Midtown Center",
                    TrainingCenterAddress = "348 e 66th st new york ny 10065",
                    //3
                    //Notification =nList[2]
                },
            };
    }

    private void SetUpProviderAccount(List<Notification> nList)
    {
      IProviderAccountList = new List<ProviderAccount>
            {
                new ProviderAccount
                {
                    Coordinator = ICoordinators[0],
                    Name = "Billys Big Discount Dorms",
                    Status = "Strawberry Jelly",
                    AccountCreatedAt = now,
                    AccountExpiresAt = nowPSev
                },
                new ProviderAccount
                {
                    Coordinator = ICoordinators[0],
                    Name = "Billys Big Discount Dorms",
                    Status = "Strawberry Jelly",
                    AccountCreatedAt = now,
                    AccountExpiresAt = nowPSev
                },
                new ProviderAccount
                {
                    Coordinator = ICoordinators[0],
                    Name = "Billys Big Discount Dorms",
                    Status = "Strawberry Jelly",
                    AccountCreatedAt = now,
                    AccountExpiresAt = nowPSev
                }
        };
    }

    private void SetUpNotifications()
    {
      INotificationList = new List<Notification>
            {
                new Notification
                {
                    ProviderId = IProviderAccountList[0].ProviderId,
                        CoordinatorId = ICoordinators[0].CoordinatorId,
                        Status = "Marmalaide",
                        AccountExpiresAt = nowPSev
                },
                new Notification
                {
                    ProviderId = IProviderAccountList[0].ProviderId,
                    CoordinatorId = ICoordinators[0].CoordinatorId,
                    Status = "Marmalaide", // string required for this... ***
                    AccountExpiresAt = nowPSev
                },
                new Notification()
                {
                    ProviderId = IProviderAccountList[0].ProviderId,
                    CoordinatorId = ICoordinators[0].CoordinatorId,
                    Status = "Marmalaide", // string required for this... ***
                    AccountExpiresAt = nowPSev
                }
            };
    }

    private void SetUpMocks()
    {
      IRepository = new Mock<Revature.Account.Lib.Interface.IGenericRepository>();
      ICoordinatorAccountController = new CoordinatorAccountController(IRepository.Object);
      ICoordinatorAccountController.ControllerContext = new ControllerContext();//mvc
      ICoordinatorAccountController.ControllerContext.HttpContext = new DefaultHttpContext();//http
      ICoordinatorAccountController.ControllerContext.HttpContext.Request.Headers["Authorize"] = "Not a token.";
    }
  }
}
