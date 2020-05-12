using SNPIDataManager.Models;
using System.Threading.Tasks;

namespace SNPIDataManager.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}