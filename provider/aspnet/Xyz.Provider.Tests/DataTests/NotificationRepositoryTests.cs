using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xyz.Provider.DataAccess;
using Xyz.Provider.DataAccess.Repository;

namespace Xyz.Provider.Tests.DataTests
{
  /// <summary>
  /// Unit tests for data access methods in NotificationRepository class
  /// </summary>
  public class NotificationRepositoryTests
  {
    /// <summary>
    /// Checks that the constructor can construct a new NotificationRepository
    /// </summary>
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // arrange (create database)
      var options = TestDbInitializer.InitializeDbOptions("NotificationConstructorTest");
      using var db = TestDbInitializer.CreateTestDb(options);
      // act (pass database into constructor)
      var test = new NotificationRepository(db);
      // assert (test passes if no exception thrown)
    }

    /// <summary>
    /// Checks that GetNotificationsAsync gets Notifications w/ given providerId
    /// </summary>
    [Fact]
    public async Task GetShouldGet()
    {
      // arrange (create database and repo)
      var options = TestDbInitializer.InitializeDbOptions("NotificationGetTest");
      using var db = TestDbInitializer.CreateTestDb(options);
      var repo = new NotificationRepository(db);

      // act (get all the notifications from one provider)
      var test = await repo.GetNotificationsAsync(1);

      // assert (assure that the list received isn't null)
      Assert.NotNull(test);
    }

    /// <summary>
    /// Checks that PostRequestAsync adds Notification record to DB
    /// </summary>
    [Fact]
    public async Task AddShouldAdd()
    {
      // arrange (create database, repo and a new notification)
      var options = TestDbInitializer.InitializeDbOptions("NotificationAddTest");
      using var db = TestDbInitializer.CreateTestDb(options);
      var repo = new NotificationRepository(db);
      var add = new Lib.Models.Notification
      {
        NotificationId = 1,
        Provider = Mapper.Map(db.Provider.First(p => p.ProviderId == 1)),
        Reason = "Something Stupid",
        Room = Mapper.Map(db.Room.First(r => r.RoomId == 1)),
        Title = "AAAAAAAAA"
      };

      // act (add the notification to the database)
      var test = await repo.PostRequestAsync(add);

      // assert (check to see if the notification is in the database
      Assert.NotNull(test);
      var check = await repo.GetNotificationsAsync(1);
      Assert.Contains(check, n => n.NotificationId == add.NotificationId);
    }
  }
}
