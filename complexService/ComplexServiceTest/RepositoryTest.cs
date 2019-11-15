using System;
using System.Collections.Generic;
using System.Text;
using Entity = ComplexServiceDatabase.Model;
using ComplexServiceDatabase.Repo;
using Logic = ComplexServiceLogic.Model;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ComplexServiceTest
{
    public class RepositoryTest
    {
        [Fact]
        public async void CreateComplexAsyncTest()
        {
            Mapper mapper = new Mapper();
            DbContextOptions<Entity.ComplexDbContext> options
                = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
                    .UseInMemoryDatabase("test")
                    .Options;
            using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
            ComplexRepository repo = new ComplexRepository(testContext, mapper);

            Guid cId = Guid.NewGuid();
            Guid aId = Guid.NewGuid();
            Guid pId = Guid.NewGuid();

            Logic.Complex complex = new Logic.Complex
            {
                ComplexId = cId,
                AddressId = aId,
                ProviderId = pId,
                ComplexName = "Liv+",
                ContactNumber = "(123)456-7890"
            };

            string result = await repo.CreateComplexAsync(complex);

            Assert.Equal("Create!!", result);
        }

        [Fact]
        public async void ReadComplexListTest()
        {
            Mapper mapper = new Mapper();
            DbContextOptions<Entity.ComplexDbContext> options
                = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
                    .UseInMemoryDatabase("test")
                    .Options;
            using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
            ComplexRepository repo = new ComplexRepository(testContext, mapper);

            Guid cId1 = Guid.NewGuid();
            Guid aId1 = Guid.NewGuid();
            Guid pId1 = Guid.NewGuid();
            Guid cId2 = Guid.NewGuid();
            Guid aId2 = Guid.NewGuid();
            Guid pId2 = Guid.NewGuid();

            Logic.Complex complex1 = new Logic.Complex
            {
                ComplexId = cId1,
                AddressId = aId1,
                ProviderId = pId1,
                ComplexName = "Liv+",
                ContactNumber = "(123)456-7890"
            };

            Logic.Complex complex2 = new Logic.Complex
            {
                ComplexId = cId2,
                AddressId = aId2,
                ProviderId = pId2,
                ComplexName = "Liv++",
                ContactNumber = "(321)456-7890"
            };

            string result1 = await repo.CreateComplexAsync(complex1);
            string result2 = await repo.CreateComplexAsync(complex2);
            List<Logic.Complex> complices = repo.ReadComplexList().ToList();

            Assert.Equal(cId1, complices[1].ComplexId);
        }
    }
}

