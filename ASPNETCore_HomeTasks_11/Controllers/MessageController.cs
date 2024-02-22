using ASPNETCore_HomeTasks_11.Models;
using ASPNETCore_HomeTasks_11.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore_HomeTasks_11.Controllers
{
    public class MessageController : Controller
    {
        UsersMessagesContext usersMessagesContext;

        public MessageController(UsersMessagesContext context)
        {
            usersMessagesContext = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(Message message)
        {
            if (message == null)
            {
                ModelState.AddModelError("", "Некорректна нотатка");
                return View();
            }

            var userLogin = User.Identity.Name;
            var user = await usersMessagesContext.Users.FirstOrDefaultAsync(u => u.Login == userLogin);
            if (user == null)
            {
                ModelState.AddModelError("", "Пользователь не найден");
                return View();
            }

            message.Id = Guid.NewGuid();
            message.IdUser = user.Id;
            message.IdUserNavigation = user;
            usersMessagesContext.Messages.Add(message);
            await usersMessagesContext.SaveChangesAsync();
            ViewBag.Complete = true;
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Messages()
        {
            var userLogin = User.Identity.Name;
            var user = await usersMessagesContext.Users.FirstOrDefaultAsync(u => u.Login == userLogin);

            if (user == null)
            {
                ModelState.AddModelError("", "Пользователь не найден");
                return RedirectToAction("Index", "Home");
            }

            var messages = await usersMessagesContext.Messages
                .Where(m => m.IdUser == user.Id)
                .ToListAsync();

            return View(messages);
        }

    }
}
