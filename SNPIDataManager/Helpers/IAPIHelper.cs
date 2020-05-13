using SNPIDataManager.Models;
using System.Threading.Tasks;

namespace SNPIDataManager.Helpers
{
    public interface IAPIHelper
    {
        Task<PreLoginModel> Authenticate(string username, string password);
    }
}