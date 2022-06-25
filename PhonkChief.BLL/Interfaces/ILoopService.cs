using PhonkChief.BLL.Implementation;
using PhonkChief.Domain.Entities;
using PhonkChief.Domain.Enum;
using PhonkChief.Domain.ViewModels;
using System.Threading.Tasks;

namespace PhonkChief.BLL.Interfaces
{
    public interface ILoopService
    {
        Task<BaseResponse<LoopsViewModel>> GetAllAsync();
        Task<BaseResponse<Loop>> CreateAsync(Loop loop);
        Task<BaseResponse<LoopsViewModel>> GetByCategoryAsync(Category category);
    }
}
