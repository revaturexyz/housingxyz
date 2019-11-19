namespace Revature.Room.DataAccess.Entities
{
  /// <summary>
  /// Entity for the Gender Types i.e Male, Female, NonBinary
  /// </summary>
  public class Gender
  {
    public int GenderId { get; set; }
    public string Type { get; set; }
  }
}
