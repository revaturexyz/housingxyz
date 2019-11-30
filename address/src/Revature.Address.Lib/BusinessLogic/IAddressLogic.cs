using System.Threading.Tasks;

namespace Revature.Address.Lib.BusinessLogic
{
  public interface IAddressLogic
  {
    public Task<bool> IsInRangeAsync(Address origin, Address destination, int distance);
    public Task<bool> IsValidAddress(Address address);
    public string GetGoogleApiUrl(string origin, string destination);
    public string FormatAddress(Address address);
  }
}
