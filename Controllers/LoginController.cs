using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoceGlamourCore.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioContext _usuarioContext;

        public LoginController(UsuarioContext usuarioContext)
        {
            this._usuarioContext = usuarioContext;
        }
        [HttpGet]
        public IActionResult Index(int? args)
        {
            if(args != null)
            {
                if (args == 0)
                {
                    HttpContext.Session.Clear();
                    

                }
            }
            
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
    
            return View();
        }
        [HttpPost]

       
        public IActionResult Login(UsuarioModel usuarioModel)
        {
            if (usuarioModel.ValidarUsuario( _usuarioContext, usuarioModel.emailUsuario, usuarioModel.senha))
            {

                LoginAutentication(usuarioModel);
                return RedirectToAction("Index", "Menu");
            }
            else
            {
                TempData["ErrorLogin"] = "Email ou Senha inválido!";
                return View();
            }
           
        }

        public async void LoginAutentication(UsuarioModel usuarioModel)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuarioModel.usuario));
            identity.AddClaim(new Claim(ClaimTypes.Name, usuarioModel.nomeUsuario));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            HttpContext.Session.SetString("IdUsuarioLogado", usuarioModel.idUsuario.ToString());
            HttpContext.Session.SetString("NomeUsuarioLogado", usuarioModel.nomeUsuario);
            HttpContext.Session.SetString("senha", usuarioModel.senha);
            
        }
        
    }
}