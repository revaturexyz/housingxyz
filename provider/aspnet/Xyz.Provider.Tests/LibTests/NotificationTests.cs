using System;
using Xunit;

using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Tests.LibTests
{

  public class NotificationTests
  {
    /// <summary>
    /// tests to check that Notification is constructed and values are set.
    /// Provider and Room models also make up Notification model.
    /// </summary>
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // Arrange/Act (create model with values)
      var notification = new Notification
      {
        NotificationId = 1,
        Title = "Test",
        Reason = "Because I can.",
        Provider = new Lib.Models.Provider(),
        Room = new Room()
      };
      // Assert (ensure properties are set to the proper values)
      Assert.Equal(1, notification.NotificationId);
      Assert.Equal("Test", notification.Title);
      Assert.Equal("Because I can.", notification.Reason);
      Assert.NotNull(notification.Provider);
      Assert.NotNull(notification.Room);
    }

    /// <summary>
    /// Notification Title string should be only letters.
    /// if not letters throw ArgumentException or ArgumentNullException
    /// </summary>
    /// <param name="title"></param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("T!tle")]
    [InlineData("Tit1('")]
    [InlineData("A_Title")]
    public void TitleShouldBeOnlyLetters(string title)
    {
      // Arrange (create a new notification)
      var notification = new Notification();
      void BadTitle() => notification.Title = title;
      // Act (try to set the title to an invalid title),
      // Assert (ensure an ArgumentException is thrown)
      Assert.ThrowsAny<ArgumentException>(BadTitle);
    }

    /// <summary>
    /// Notification Reason string should not be null, empty or whitespace.
    /// if so throw Argument or ArgumentNull Exceptions are thrown in Action
    /// </summary>
    /// <param name="reason"></param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ReasonShouldNotBeNullOrWhitespace(string reason)
    {
      // Arrange (create default notification)
      var notification = new Notification();
      void BadReason() => notification.Reason = reason;
      // Act (try to set reason to a bad value),
      // Assert (ensure an ArgumentException is thrown)
      Assert.ThrowsAny<ArgumentException>(BadReason);
    }
  }
}
