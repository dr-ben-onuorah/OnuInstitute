using OnuInstitute.Models;

namespace OnuInstitute.Interface

//namespace OnuInstitute.Internal Interface

{
    public interface IAccount
    {
        Task<dynamic> RegisterAsync(Register register);
        Task<dynamic> LoginAsync(Login login);
        Task<dynamic> AddRole(string role);
    }
}
