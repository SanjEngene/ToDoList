using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoproj.Models;
using ToDoproj.Repository;
using ToDoproj.ViewModels;

namespace ToDoproj.Controllers
{
    public class AccountController : Controller
    {
        private AuthenticationEntity _authenticationEntity;
        private IUserRepository _userRepository;
        public AccountController(IUserRepository repository)
        {
            _userRepository = repository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _userRepository.GetList().FirstOrDefault(i => i.Email == model.EmailAddress && i.Password == model.Password);
                if (user != null)
                {
                    _authenticationEntity = new AuthenticationEntity(HttpContext);
                    await _authenticationEntity.AuthenticateAsync(user);

                    return RedirectToAction("Index", "Home", new { id = user.Id });
                }

                return NotFound();
            }

            ModelState.AddModelError("", "Invalid model error");
            return BadRequest();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _userRepository.GetList().FirstOrDefault(i => i.Email == model.EmailAddress && i.Password == model.Password);
                if (user == null)
                {
                    user = new User
                    {
                        Email = model.EmailAddress,
                        Password = model.Password,
                        Role = "user"
                    };

                    _userRepository.Create(user);

                    _authenticationEntity = new AuthenticationEntity(HttpContext);
                    await _authenticationEntity.AuthenticateAsync(user);
                    return RedirectToAction("Index", "Home", new { id = user.Id });
                }
            }
            ModelState.AddModelError("", "Invalid model error");

            return BadRequest();
        }

        public async Task<IActionResult> Logout()
        {
            _authenticationEntity = new AuthenticationEntity(HttpContext);
            await _authenticationEntity.Logout();
            return RedirectToAction("Login", "Account");
        }
    }
    
    class AuthenticationEntity
    {
        private HttpContext _context;
        public AuthenticationEntity(HttpContext httpContext)
        {
            _context = httpContext;
        }
        public async Task AuthenticateAsync(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task Logout()
        {
            await _context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
