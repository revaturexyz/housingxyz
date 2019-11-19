using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using Moq;

using Revature.Account.Api;

namespace Revature.Account.Test.API_Tests
{
    public class CoordinatorAccountTests
    {
        [Fact]
        public async Task GetSingleCoordinatorHasCorrectId()
        {
            //get the helper
            ControllerTestHelper helper = new ControllerTestHelper();
            
            //get the guid for the target to be tested
            Guid targetId = helper.ICoordinators[0].CoordinatorId;

            //look in the repository
            helper.IRepository
                //get all the accounts
                .Setup( x => x.GetCoordinatorAccountById( It.IsAny<Guid>() )  )
                //then return the one which matches.
                .Returns( Task.Run( () => helper.ICoordinators.Where(c => c.CoordinatorId == targetId).FirstOrDefault() ) );

            //determine if null, and say so.
            Assert.NotNull( ( await helper.ICoordinatorAccountController.Get( targetId ) as OkObjectResult ).Value );
        }

    }
}
