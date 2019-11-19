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
    public class ProviderAccountTest
    {
        [Fact]
        public async Task GetSingleProviderHasCorrectId()
        {
            //get the helper
            ControllerTestHelper helper = new ControllerTestHelper();

            //get the guid for the target to be tested
            Guid targetId = helper.IProviderAccountList[0].ProviderId;

            //look in the repository
            helper.IRepository

                //get all the accounts
                .Setup(x => x.GetProviderAccountById(It.IsAny<Guid>()))

                //then return the one which matches.
                .Returns(Task.Run(() => helper.IProviderAccountList.Where(c => c.ProviderId == targetId).FirstOrDefault()));

            //determine if null, and say so.
            Assert.NotNull((await helper.IProviderAccountController.Get(targetId) as OkObjectResult).Value);
        }

    }
}

