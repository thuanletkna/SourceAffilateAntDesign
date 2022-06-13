using AffilateSource.Shared.ViewModel.Users;

using System.Threading.Tasks;

namespace AffilateSource.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task Logout();
    }
}
