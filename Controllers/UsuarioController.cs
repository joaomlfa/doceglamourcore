using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoceGlamourCore.Controllers
{
    public class UsuarioController : Controller
    {
        private  UsuarioContext _usuarioContext;

        public UsuarioController(UsuarioContext usuarioContext)
        {
            this._usuarioContext = usuarioContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.CadastrarUsuario(usuario, _usuarioContext);
                TempData["CadastroFeito"] = "Usuário Cadastrado Efetue Login.";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }

        }
        public bool VerificarEmail(String emailUsuario)
        {
            var emailExiste = _usuarioContext.Usuario.Where(op => op.emailUsuario == emailUsuario).FirstOrDefault();
            if(emailExiste == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}