using Store.Model;
using Store.Model.DTO;

namespace Store.BLL.Interface
{
    public interface IAuthenticationService
    {
        OpResult LogOn(LoginRequest request);


    }
}
