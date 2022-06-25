using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhonkChief.BLL.Implementation;
using PhonkChief.BLL.Interfaces;
using PhonkChief.Domain.Enum;
using PhonkChief.Domain.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PhonkChief.WebUI.Controllers
{
    public class LoopsController : Controller
    {
        private readonly ILoopService _service;
        private readonly ILogger<LoopsController> _logger;

        public LoopsController(ILoopService service, ILogger<LoopsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [Route("Loops/{page}")]
        public async Task<IActionResult> Loops(SortedViewModel model,int page=1)
        {
            var response = new BaseResponse<LoopsViewModel>();
            int pageSize = 2;
            Category category = (Category)model.Option;

            if(model.Option == 0)
                response = await _service.GetAllAsync();
            else
            {
                response = await _service.GetByCategoryAsync(category);
                page = 1;
            }

            var loopsList = response.Data.LoopsList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            response.Data.LoopsList = loopsList;

            return View(response.Data);
        }
    }
}
