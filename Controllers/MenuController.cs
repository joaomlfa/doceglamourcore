using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoceGlamourCore.Controllers
{
    
    public class MenuController : Controller
    {
        private readonly PedidoContext _pedidoContext;

        public MenuController(PedidoContext pedidoContext)
        {
            this._pedidoContext = pedidoContext;
        }
        public IActionResult Index(int? pagina)
        {
            PedidoModel pedidos = new PedidoModel();
            var pedidosAbertos = pedidos.BuscarPedidosAbertosPaginados(_pedidoContext, pagina);
            return View(pedidosAbertos);
        }
    }
}