using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoglassWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidaLogin(string mostrarSenha) 
        {
            return RedirectToAction("Index", "Produto");
        }
    }
}
