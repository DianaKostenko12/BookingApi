using BLL.Services.Auth.Descriptors;
using ErrorOr;

namespace BLL.Services.Auth
{
    public interface IAuthService
    {
        ErrorOr<Success> Register(RegisterDescriptor descriptor);
        ErrorOr<string> Login(LoginDescriptor descriptor);
    }
}
