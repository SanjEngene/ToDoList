using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoproj.Models;
using ToDoproj.Repository;

namespace ToDoproj.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AdminController(IUserRepository context)
        {
            _userRepository = context;
        }
     
        [HttpGet]
        public IActionResult Index(int id)
        {
            ViewBag.Admin = _userRepository.Get(id);
            return View(_userRepository.GetList());
        }

        [HttpPost]
        public IActionResult Remove(int userId, int adminId)
        {
            User user = _userRepository.Get(userId);
            if (user != null)
            {
                _userRepository.Remove(user.Id);
                return RedirectToAction("Index", "Admin", new { id = adminId });
            }

            return NotFound();
        }
    }
}
