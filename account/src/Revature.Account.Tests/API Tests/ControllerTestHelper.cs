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
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Local Projects
using Revature.Account.Api;
using Revature.Account.Api.Controllers;
using Revature.Account.Lib;
using Revature.Account.Lib.Model;


//Testing API
using Xunit;
using Moq;

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

        //constructor
        public ControllerTestHelper()
        {
            SetUpNotifications();
            SetUpCoordinators(INotificationList);
            SetUpProviderAccount(INotificationList);
            
            SetUpMocks();
        }

        private void SetUpCoordinators(List<Notification> nList)
        {
            ICoordinators = new List<CoordinatorAccount>
            {
                //1
                new CoordinatorAccount
                {
                    CoordinatorId = new Guid("d9beb26e-11e5-490f-a27f-1467ac5d6a68"),
                    Name = "Jacob",
                    Email = "jacobs.email@gmail.com",
                    Password = "54321",
                    TrainingName = "Arlington",
                    TrainingAddress = "604 S. West, Arlington, TX, 76010",

                    //1
                    //Notification = nList[0]
                },
                //2
                new CoordinatorAccount
                {
                    CoordinatorId = new Guid("d9beb26e-11e5-490f-a27f-1467ac5d6aaa"),
                    Name = "Kimberly",
                    Email = "Kimberly.email@gmail.com",
                    Password = "12345",
                    TrainingName = "Honolulu",
                    TrainingAddress = "555 Kaumakani St, Honolulu, HI 96825",

                    //2
                   // Notification = nList[1]
                },
                //3
                new CoordinatorAccount
                {
                    CoordinatorId = new Guid("d9beb26e-11e5-490f-a27f-aaaaac5d6bbb"),
                    Name = "Jimmy",
                    Email = "Jimmy.email@gmail.com",
                    Password = "87654",
                    TrainingName = "NYC Midtown Center",
                    TrainingAddress = "348 e 66th st new york ny 10065",

                    //3
                    //Notification =nList[2]
                },


            };
        }

        private void SetUpProviderAccount(List<Notification> nList )
        {
            IProviderAccountList = new List<ProviderAccount>
            {
                new ProviderAccount
                {
                    ProviderId = new Guid("johny5xx-was5-h7re-4ndd-leftthisme4g"),
                    CoordinatorId = new Guid("d9beb26e-11e5-490f-a27f-1467ac5d6a68"),
                    Name = "Billys Big Discount Dorms",
                    Password = "54321",
                    Status = "Strawberry Jelly",
                    AccountCreated = now,
                    Expire = nowPSev,
                    Notification = nList[0]


                },
                
                new ProviderAccount
                {
                    ProviderId = new Guid("johny5xx-was5-h7re-4ndd-leftthisme4g"),
                    CoordinatorId = new Guid("d9beb26e-11e5-490f-a27f-1467ac5d6a68"),
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
                    ProviderId = new Guid("johny5xx-was5-h7re-4ndd-leftthisme4g"),
                    CoordinatorId = new Guid("d9beb26e-11e5-490f-a27f-1467ac5d6a68"),
                    Name = "Billys Big Discount Dorms",
                    Password = "54321",
                    Status = "Strawberry Jelly",
                    AccountCreated = now,
                    Expire = nowPSev,
                    Notification = nList[2]


                }

        };
        }

        private void SetUpNotifications()
        {
            

            INotificationList = new List<Notification>
            {
                new Notification
                {
                    ProviderId = new Guid("johny5xx-was5-here-4ndd-leftthisme4g"),
                        CoordinatorId = new Guid("d9beb26e-11e5-490f-a27f-1467ac5d6a68"),
                        Status = "Marmalaide", 
                        AccountExpire = nowPSev
                },
                new Notification
                {
                    ProviderId = new Guid("j0hny5xx-was5-h7r3-4ndd-l3ftth1smssg"),
                    CoordinatorId = new Guid("d9beb26e-11e5-490f-a27f-1467ac5d6aaa"),
                    Status = "Marmalaide", // string required for this... ***
                    AccountExpire = nowPSev

                },
                
                new Notification()
                {
                    ProviderId = new Guid("j08ny5xx-w455-h7r3-4ndd-l3f77h15m55g"),
                    CoordinatorId = new Guid("d9beb26e-11e5-490f-a27f-aaaaac5d6bbb"),
                    Status = "Marmalaide", // string required for this... ***
                    AccountExpire = nowPSev

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
