using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToDoproj.Models;
using ToDoproj.Repository;
using ToDoproj.ViewModels.Home;

namespace ToDoproj.Controllers
{
    [Authorize(Policy = "Users")]
    public class HomeController : Controller
    {
        private IUserRepository _userRepository;
        private IWebHostEnvironment _webHostEnv;
        public HomeController(IUserRepository userRepository, IWebHostEnvironment environment)
        {
            _userRepository = userRepository;
            _webHostEnv = environment;
        }
        public IActionResult Index(int id)
        {
            ViewBag.DaysOfWeek = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            return View(_userRepository.Get(id));
        }
        public IActionResult ScoreList(int id)
        {
            ViewBag.User = _userRepository.Get(id);
            List<Message> messages = new List<Message>();
            foreach(User user in _userRepository.GetList())
            {
                messages.AddRange(user.Messages);
            }
            ViewBag.Messages = messages.OrderBy(i => i.DateTimeStr);
            
            return View(_userRepository.GetList());
        }
        
        [HttpPost]
        public IActionResult ChangeImage(ImageUploadModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    User user = _userRepository.Get(model.UserId);
                    string uploadFolder = Path.Combine(_webHostEnv.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    string imageFilePath = Path.Combine(uploadFolder, uniqueFileName);
                    model.File.CopyTo(new FileStream(imageFilePath, FileMode.Create));
                    user.PhotoFilePath = uniqueFileName;
                    _userRepository.Update(user);
                }

                return RedirectToAction("Index", "Home", new { id = model.UserId });
            }

            return BadRequest();
        }
    }
}
