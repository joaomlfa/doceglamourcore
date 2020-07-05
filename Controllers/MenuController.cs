using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGlamourCore.Libraries.LoginUser;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoceGlamourCore.Controllers
{
    
    public class MenuController : Controller
    {
        private readonly PedidoContext _pedidoContext;
        private LoginUser _loginUser;

        public MenuController(PedidoContext pedidoContext, LoginUser loginUser)
        {
            this._pedidoContext = pedidoContext;
            this._loginUser = loginUser;
        }

        public IActionResult Index(int? pagina)
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                PedidoModel pedidos = new PedidoModel();
                var pedidosAbertos = pedidos.BuscarPedidosAbertosPaginados(_pedidoContext, pagina);
                return View(pedidosAbertos);
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }
            
        }
        public IActionResult Sair()
        {
            _loginUser.Logout();
            var msg = "Efetue Login para Acessar.";
            return RedirectToAction("Login", "Login", new { mensagem = msg });

        }
    }
}