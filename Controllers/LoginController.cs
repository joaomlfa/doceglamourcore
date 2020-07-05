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
using DoceGlamourCore.Libraries.LoginUser;

namespace DoceGlamourCore.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioContext _usuarioContext;
        private LoginUser _loginUser;

        public LoginController(UsuarioContext usuarioContext, LoginUser loginUser)
        {
            this._usuarioContext = usuarioContext;
            this._loginUser = loginUser;
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
        #nullable enable
        public IActionResult Login(string? mensagem)
        {
            if (mensagem == null)
            {
                
                return View();
            }
            else
            {
                TempData["ErrorLogin"] = "Efetue Login para Acessar.";
                return View();
            }
            
        }
        [HttpPost]

       
        public IActionResult Login(UsuarioModel usuarioModel)
        {
            if (usuarioModel.ValidarUsuario( _usuarioContext, usuarioModel.emailUsuario, usuarioModel.senha))
            {
                _loginUser.Login(usuarioModel);               
                return RedirectToAction("Index", "Menu");
            }
            else
            {
                TempData["ErrorLogin"] = "Email ou Senha inválido!";
                return View();
            }
           
        }

        
        
    }
}