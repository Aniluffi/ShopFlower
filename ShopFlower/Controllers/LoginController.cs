using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using ShopFlower.Data;
using ShopFlower.Data.Models;
using ShopFlower.IService.ServiceUser;
using ShopFlower.Models;
using System.Security.Claims;

namespace ShopFlower.Controllers
{
    public class LoginController : Controller
    {
        private IServiceUser _serviceUser;
        private DbContextOptions<ApplicationContext> _options;

        public LoginController(IServiceUser serviceUser, DbContextOptions<ApplicationContext> options)
        {
            _serviceUser = serviceUser;
            _options = options;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Registr(LoginRegistrView models)
        {
            var model = models.registr;

            if (!ModelState.IsValid)
            {
                return View("Index", new LoginRegistrView
                {
                    registr = model
                });
            }

            var result = new List<Exception>();

            var dB = new ApplicationContext(_options);
            if (dB.Users.Any(c => c.Email == model.Email)) result.Add(new Exception($"Почта {model.Email} зарегистрирована"));

            if (result.Count > 0)
            {

                return View("Index", new LoginRegistrView
                {
                    registr = new Registr
                    {
                        error = result as List<Exception>
                    }
                });
            }

            Random random = new Random();
            var confirmCode = $"{random.Next(9)}{random.Next(9)}{random.Next(9)}{random.Next(9)}";

            await SendEmail(model.Email, confirmCode);
            return View("Registr", new RegistrConfirm
            {
                Registr = new Registr
                {
                    Email = model.Email,
                    Login = model.Login,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    Message = "Введите код, который пришел к вам на почту",
                },
                ConfirmCode = confirmCode
            });
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(RegistrConfirm confirm)
        {
            if (confirm.ConfirmCode != confirm.Registr.ConfirmCode)
            {
                confirm.Registr.Message = "Неверный код";
                return View("Registr", confirm);
            }

            var result = await _serviceUser.AddUser(new User { Email = confirm.Registr.Email, Login = confirm.Registr.Login, Password = confirm.Registr.Password });

            return View("Index", new LoginRegistrView
            {
                registr = new Registr
                {
                    error = null,
                    Message = "Вы успешно прошли регистрацию"
                }
            });
        }

        public async Task SendEmail(string email, string confirmcodw)
        {
            string path = "jknv dzwr bybb zlwm";
            var emailMessege = new MimeMessage();

            emailMessege.From.Add(new MailboxAddress("Администрация сайта", "LUKANI@sf.com"));
            emailMessege.To.Add(new MailboxAddress("", email));
            emailMessege.Subject = "Добро пожаловать!";
            emailMessege.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<html>" + "<head>" + "<style>" + "body {font-family: Arial,sans-serif; background-color: #f2f2f2;}" +
                ".container {max-width: 600px; margin: 0 auto; padding: 20px;  background-color: #fff; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1);}"
                + ".header {text-align: center; margin-bottom: 20px; }"
                + ".message {font-size: 16px; line-height: 1.6;}" +
                ".container-code { background-color: #f0f0f0; padding: 5px; border-radius: 5px; font-weight: bold; }" +
                ".code {text-align: center; }" +
                "</style>" +
                "</head>"
                + "<body>"
                + "<div class='container'>"
                + "<div class='header'>"
                + "<h1>Добро пожаловать на сайт LUCANI!</h1>"
                + "</div>"
                + "<div class='message'>"
                + "<p>Пожалуйста, введите данный код на сайте, чтобы подтвердить ваш email и завершить регистрацию</p>"
                + "<div class='container-code'><p class='code'>" + confirmcodw + "</p></div>"
                + "</div>"
                + "</div>"
                + "</body>"
                + "</html>"
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("lipapipa361@gmail.com", path);
                await client.SendAsync(emailMessege);

                await client.DisconnectAsync(true);
            }
        }

        public async Task AuthenticationGoogle(string returnURL = "/")
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse", new { returnURL }),
                    Parameters = { { "prompt", "select_account" } }
                });
        }

        public async Task<IActionResult> GoogleResponse(string returnURL = "/")
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return View("Index");

            User model = new User
            {
                Login = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                Email = result.Principal.FindFirst(ClaimTypes.Email)?.Value,
            };

            var dB = new ApplicationContext(_options);

            if (dB.Users.Any(c => c.Email == model.Email))
            {
                var user = await _serviceUser.SearchUserEmail(result.Principal.FindFirst(ClaimTypes.Email)?.Value);
                await Login(new LoginRegistrView
                {
                    login = new Login
                    {
                        Email = user.Email,
                        Password = user.Password
                    }
                });
                return RedirectToAction("Index","Home");
            }

            return View("CreatePassword", new Registr
            {
                Login = model.Login,
                Email = model.Email,
                Message = "Вы должны придумать пароль!"
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePassword(Registr model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreatePassword", model);
            }

            User user = new User
            {
                Login = model.Login,
                Email = model.Email,
                Password = model.Password
            };

            await _serviceUser.AddUser(user);

            await Login(new LoginRegistrView
            {
                login = new Login
                {
                    Email = user.Email,
                    Password = user.Password
                }
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRegistrView models)
        {
            User user = new User();
            try
            {


                var loginDate = models.login;

                user = await _serviceUser.GetUser(loginDate.Email, loginDate.Password);

                if (string.IsNullOrEmpty(user.Email) && string.IsNullOrEmpty(user.Password))
                {

                    return View("Index", new LoginRegistrView
                    {
                        login = models.login
                    });
                }
            }
            catch (Exception ex)
            {
                return View("Index", new LoginRegistrView
                {
                    login = new Login
                    {

                        error = new List<Exception> { ex }
                    }
                });
            }

            await AutheticateAsync(user);
            // Перенаправляем на главную страницу
            return RedirectToAction("Index", "Home");
        }

        public async Task AutheticateAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId",user.Id.ToString()),
                new Claim("UserEmail",user.Email),
                new Claim("UserLogin",user.Login),
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultRoleClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Index");
        }
    }
}
