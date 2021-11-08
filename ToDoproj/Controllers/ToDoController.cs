using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoproj.Models;
using ToDoproj.Repository;
using ToDoproj.ViewModels.ToDo;

namespace ToDoproj.Controllers
{
    public class ToDoController : Controller
    {
        private IToDoRepository _toDoRepository;
        private IUserRepository _userRepository;
        public ToDoController(IToDoRepository repository, IUserRepository userRepository)
        {
            _toDoRepository = repository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Create(int userId)
        {
            User user = _userRepository.Get(userId);
            return View("~/Views/ToDo/Create.cshtml", user);
        }
        [HttpPost]
        public IActionResult Create(ToDoModel model)
        {
            if (ModelState.IsValid)
            {
                ToDo toDo = _toDoRepository.GetList().FirstOrDefault(t => t.Content == model.Content && t.DayOfWeek == model.DayOfWeek);
                if (toDo == null)
                {
                    toDo = new ToDo
                    {
                        DayOfWeek = model.DayOfWeek,
                        Header = model.Header,
                        Content = model.Content,
                        IsFinished = false,
                        User = _userRepository.Get(model.UserId)
                    };

                    _toDoRepository.Create(toDo);
                }

                return RedirectToAction("Index", "Home", new { id = model.UserId });
            }

            ModelState.AddModelError("", "Invalid model error");

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Remove(RemoveModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _userRepository.Get(model.UserId);
                if (model.IsFinished)
                {
                    user.CountOfFinished += 1;
                    _userRepository.Update(user);
                }
                _toDoRepository.Remove(model.Id);

                return RedirectToAction("Index", "Home", new { id = model.UserId });    
            }

            ModelState.AddModelError("", "Invalid model error");

            return BadRequest(model);
        }
    }
}
