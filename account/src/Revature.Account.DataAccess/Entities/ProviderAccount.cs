using System;

namespace Revature.Account.DataAccess.Entities
{
  /// <summary>
  /// Data-Access layer representation of  an individual Provider.
  /// </summary>
  public class ProviderAccount
  {
    //set the primary key with guid
    public Guid ProviderId { get; set; }
    public string Name { get; set; }
    public int StatusId { get; set; }
    public DateTime AccountCreatedAt { get; set; }
    public DateTime AccountExpiresAt { get; set; }
    public Guid CoordinatorId { get; set; }
    public CoordinatorAccount Coordinator { get; set; }
    public Status Status { get; set; }
  }
}
