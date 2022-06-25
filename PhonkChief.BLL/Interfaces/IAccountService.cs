using PhonkChief.BLL.Implementation;
using PhonkChief.Domain.DTO;
using PhonkChief.Domain.Entities;
using PhonkChief.Domain.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhonkChief.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<string>> LoginAsync(LoginViewModel model);
        Task<bool> RegisterAsync(RegisterViewModel model);
        Task<ClaimsIdentity> GetIdentity(LoginViewModel model);
        Task<BaseResponse<User>> GetByIdAsync(int id);
        Task<BaseResponse<IEnumerable<User>>> GetAllAsync();
        Task<BaseResponse<User>> GetUserByNameAsync(string name);
        Task<UserDataDto> GetUserDataByToken(string token);
    }
}
