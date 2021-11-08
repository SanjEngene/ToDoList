using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoproj.Models;
using ToDoproj.Repository;
using ToDoproj.ViewModels.Message;

namespace ToDoproj.Controllers
{
    public class MessageController : Controller
    {
        private IUserRepository _userRepository;
        private IMessageRepository _messageRepository;
        public MessageController(IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        [HttpPost]
        public IActionResult Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Content.Replace(" ", "") != "")
                {
                    Message message = new Message
                    {
                        Content = model.Content,
                        User = _userRepository.Get(model.UserId)
                    };

                    _messageRepository.Create(message);
                }

                return RedirectToAction("ScoreList", "Home", new { id = model.UserId });

            }

            ModelState.AddModelError("", "Invalid model error");

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Remove(RemoveModel model)
        {
            if (ModelState.IsValid)
            {
                _messageRepository.Remove(model.Id);

                return RedirectToAction("ScoreList", "Home", new { id = model.UserId });
            }

            ModelState.AddModelError("", "Invalid model error");

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Update(UpdateModel model)
        {
            if (ModelState.IsValid)
            {
                Message message = _messageRepository.Get(model.Id);
                message.Content = model.Content;
                _messageRepository.Update(message);

                return RedirectToAction("ScoreList", "Home", new { id = model.UserId });
            }

            ModelState.AddModelError("", "Invalid model error");

            return BadRequest();   
        }
    }
}
