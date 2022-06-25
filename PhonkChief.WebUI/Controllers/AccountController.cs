using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhonkChief.BLL.Interfaces;
using PhonkChief.Domain.ViewModels;
using System.Threading.Tasks;

namespace PhonkChief.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly ILogger<AccountController> _logger;
 
        public AccountController(IAccountService service, ILogger<AccountController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var response = await _service.RegisterAsync(model);

            if (!response)
            {
                _logger.LogError($"Ошибка регистрации пользователя: {model.Email}");
                return BadRequest();
            }

            _logger.LogInformation($"Зарегистрирован новый пользователь: {model.Email} {model.NickName}");
            return RedirectToAction("Loops","Loops");
        }

        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.LoginAsync(model);
            if (response.Data == null)
            {
                _logger.LogError($"Неудачный вход пользователя: {model.Email}");
                return BadRequest(response.Message);
            }
            string token = response.Data;
            HttpContext.Response.Cookies.Append("Bearer", token);
            _logger.LogInformation($"Успешный вход пользователя: {model.Email} ");

            return RedirectToAction("Loops","Loops");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("Bearer");
            return RedirectToAction("Loops","Loops");
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var token = HttpContext.Request.Cookies["Bearer"];
            if (token == null)
                return BadRequest();

            var userData = await _service.GetUserDataByToken(token);
            if (userData == null)
                return BadRequest();

            var user = await _service.GetUserByNameAsync(userData.Name);
            return View("Profile", user.Data);
        }

        [Route("/Profile/{username}")]
        public async Task<IActionResult> Profile(string username)
        {
            var user = await _service.GetUserByNameAsync(username);

            if (user == null)
                return NotFound();

            return View("Profile", user.Data);
        }
    }
}
