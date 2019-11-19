/*Production Notes:
 *  Should all tested-classes match? No, create a new instance
 *  
 *  *** review this later marker
 *  
 *  Note: Awaiting injection in controllers, then will test them.
 * 
                     //production notes: does a coordinator get only one notification?
                    //                  can/should a coordinator recieve multiple notifications?
                    //                  set null at first and assign later.
                    //                  due to dependancy requirements
   //circular reference? provider references a message which references a provider?
 */

//System libraries
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Testing API
using Moq;
using Xunit;


//Local Projects
using Revature.Account.Lib.Model;
using Revature.Account.Api;
using Revature.Account.Api.Controllers;
using Revature.Account.Api.Models;

using System;
using System.Collections.Generic;

namespace Revature.Account.Test
{
  public class ControllerTestHelper
    {

        public Mock<Revature.Account.Lib.Interface.IGenericRepository> IRepository { get; private set; }

        //Do we need this?
        //public Mock<IAuthorize> Authorizer { get; private set; }

        public CoordinatorAccountController ICoordinatorAccountController { get; private set; }
        public NotificationController INotificationController { get; private set; }
        public ProviderAccountController IProviderAccountController { get; private set; }

        public List<CoordinatorAccount> ICoordinators { get; private set; }
        public List<Notification> INotificationList { get; private set; }
        public List<ProviderAccount> IProviderAccountList { get; private set; }

        //Local variables for testing
        //setup some times for expirations
        //must run at any time
        public static DateTime now = DateTime.Now;
        public static DateTime nowPSev = DateTime.Now.AddDays(7.0);
        public static DateTime nowPThirty = DateTime.Now.AddDays(30.0);

        //elminiate possibility of errors by using references.
        public static List<Guid> coordinatorGuids;
        public static List<Guid> providerGuids;

    //constructor
    public ControllerTestHelper()
        {
            coordinatorGuids = new List<Guid>
            {
              new Guid("5807044c-de3b-468d-9d59-14dd8a66390c"),
              new Guid("68494bbf-3606-451e-a164-9b93d0b6281c"),
              new Guid("7247d22d-332b-4d42-a49c-76663df10101")
            };

            providerGuids = new List<Guid>
            {
              new Guid("7b4251e8-eb14-4391-94bc-b98235e7783c"),
              new Guid("73b7883a-c965-4ee7-a57a-facd4491f845"),
              new Guid("35988bde-907f-4b5e-b235-cd00a5944018")
            };


            SetUpNotifications(coordinatorGuids, providerGuids);
            SetUpCoordinators(INotificationList, coordinatorGuids);
            SetUpProviderAccount(INotificationList, ICoordinators, providerGuids);
            
            SetUpMocks();
        }

        private void SetUpCoordinators(List<Notification> nList, List<Guid> coordGuidList)
        {
            ICoordinators = new List<CoordinatorAccount>
            {
                //1
                new CoordinatorAccount
                {
                    CoordinatorId = coordGuidList[0],
                    Name = "Jacob",
                    Email = "jacobs.email@gmail.com",
                    Password = "54321",
                    TrainingCenterLocation = "Arlington",
                    TrainingAddress = "604 S. West, Arlington, TX, 76010",

                    //1
                    Notification = nList[0]
                },
                //2
                new CoordinatorAccount
                {
                    CoordinatorId = coordGuidList[1],
                    Name = "Kimberly",
                    Email = "Kimberly.email@gmail.com",
                    Password = "12345",
                    TrainingCenterLocation = "Honolulu",
                    TrainingAddress = "555 Kaumakani St, Honolulu, HI 96825",

                    //2
                    Notification = nList[1]
                },
                //3
                new CoordinatorAccount
                {
                    CoordinatorId = coordGuidList[2],
                    Name = "Jimmy",
                    Email = "Jimmy.email@gmail.com",
                    Password = "87654",
                    TrainingCenterLocation = "NYC Midtown Center",
                    TrainingAddress = "348 e 66th st new york ny 10065",

                    //3
                    Notification =nList[2]
                },


            };
        }

        private void SetUpProviderAccount(List<Notification> nList, List<CoordinatorAccount> cList, List<Guid> providerGuidList)
        {
            IProviderAccountList = new List<ProviderAccount>
            {
                new ProviderAccount
                {
                    ProviderId = providerGuidList[0],
                    CoordinatorId = cList[0].CoordinatorId,
                    Name = "Billys Big Discount Dorms",
                    Password = "54321",
                    Status = "Strawberry Jelly",
                    AccountCreated = now,
                    Expire = nowPSev,
                    Notification = nList[0]


                },
                
                new ProviderAccount
                {
                    ProviderId = providerGuidList[1],
                    CoordinatorId = cList[1].CoordinatorId,
                    Name = "Billys Big Discount Dorms",
                    Password = "54321",
                    Status = "Strawberry Jelly",
                    AccountCreated = now,
                    Expire = nowPSev,
                    Notification = nList[1]


                },

                //
                new ProviderAccount
                {
                    ProviderId = providerGuidList[2],
                    CoordinatorId = cList[2].CoordinatorId,
                    Name = "Billys Big Discount Dorms",
                    Password = "54321",
                    Status = "Strawberry Jelly",
                    AccountCreated = now,
                    Expire = nowPSev,
                    Notification = nList[2]


                }

        };
        }

        private void SetUpNotifications(List<Guid> coordinatGuidList, List<Guid> providerGuidList )
        {
            

            INotificationList = new List<Notification>
            {
                new Notification
                {
                    ProviderId = providerGuidList[0],
                        CoordinatorId = coordinatGuidList[0],
                        Status = "Marmalaide", 
                        AccountExpire = nowPSev
                },
                new Notification
                {
                    ProviderId = providerGuidList[1],
                    CoordinatorId = coordinatGuidList[1],
                    Status = "Marmalaide", // string required for this... ***
                    AccountExpire = nowPSev

                },
                
                new Notification()
                {
                    ProviderId = providerGuidList[2],
                    CoordinatorId = coordinatGuidList[2],
                    Status = "Marmalaide", // string required for this... ***
                    AccountExpire = nowPSev

                }


            };
        }



        private void SetUpMocks()
        {
            //mock the repositoryh 
            IRepository = new Mock<Revature.Account.Lib.Interface.IGenericRepository>();

            //Setup Moqs for all controllers.

            //notifications
            INotificationController = new NotificationController(IRepository.Object);
            INotificationController.ControllerContext = new ControllerContext(); 
            INotificationController.ControllerContext.HttpContext = new DefaultHttpContext();
            INotificationController.ControllerContext.HttpContext.Request.Headers["Authorize"] = "Not a token.";


            //coordinator
            ICoordinatorAccountController = new CoordinatorAccountController(IRepository.Object); //instantiate the controller.
            ICoordinatorAccountController.ControllerContext = new ControllerContext();//mvc related
            ICoordinatorAccountController.ControllerContext.HttpContext = new DefaultHttpContext();//asp.net http related
            ICoordinatorAccountController.ControllerContext.HttpContext.Request.Headers["Authorize"] = "Not a token."; //asp.net http

            //provider
            IProviderAccountController = new ProviderAccountController(IRepository.Object);
            IProviderAccountController.ControllerContext = new ControllerContext();
            IProviderAccountController.ControllerContext.HttpContext = new DefaultHttpContext();
            IProviderAccountController.ControllerContext.HttpContext.Request.Headers["Authorize"] = "Not a token";

        }
    }
}
