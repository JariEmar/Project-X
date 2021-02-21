using Api.Domain;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}
