using Store.Model;
using Store.Model.DTO;

namespace Store.Service
{
    public interface IAuthenticationService
    {
        OpResult LogOn(LoginRequest request);


    }
}
