using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNotes.Models;
using MyNotes.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MyNotes.Controllers
{
    // https://metanit.com/sharp/aspnet5/16.3.php
    // В этом контроллере определим метод для регистрации пользователей:
    // 

    public class AccountController : Controller
    {
        /*
         *Поскольку в классе Startup были добавлены сервисы Identity, то здесь в контроллере через конструктор мы можем их получить. 
         *В данном случае мы получаем сервис по управлению пользователями - UserManager и сервис SignInManager, 
         *который позволяет аутентифицировать пользователя и устанавливать или удалять его куки.
         *
         **/
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };

                /*С помощью метода _userManager.CreateAsync пользователь добавляется в базу данных. 
                 *В качестве параметра передается сам пользователь и его пароль.*/
                // adding user
                var result = await _userManager.CreateAsync(user, model.Password);
                /*Данный метод возвращает объект IdentityResult, с помощью которого можно узнать успешность выполненной операции. 
                 *Вполне возможно, что переданные значения не удовлетворяют требованиям, и тогда пользователь не будет добавлен в базу данных. */
                if (result.Succeeded)
                {
                    //setting cookie
                    await _signInManager.SignInAsync(user, false);
                    /*В случае удачного добавления с помощью метода _signInManager.SignInAsync() устанавливаем аутентификационные куки для добавленного пользователя. 
                     *В этот метод передается объект пользователя, который аутентифицируется, и логическое значение, указывающее, 
                     *надо ли сохранять куки в течение продолжительного времени. И далее выполняем переадресацию на главную страницу приложения.*/
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        /*Если добавление прошло неудачно, то добавляем к состоянию модели с помощью метода ModelState все возникшие при добавлении ошибки, 
                         *и отправленная модель возвращается в представление.*/
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
 