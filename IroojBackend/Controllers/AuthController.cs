using IroojBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace IroojBackend.Controllers
{
    [Route("/auth")]
    public class AuthController: Controller
    {
        [HttpPost]
        [Route("register")]
        public IActionResult Register(string UserData)
            => DBModel.CreateAuth(UserData) ? Ok() : Forbid();

        [HttpPost]
        [Route("check")]
        public IActionResult AuthCheck(string id, string encryptedPassword)
            => DBModel.CheckAuth(id, encryptedPassword) ? Ok() : Forbid();
    }
}