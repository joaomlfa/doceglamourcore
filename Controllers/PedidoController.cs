using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoceGlamourCore.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoContext _pedidoContext;
        public PedidoController(PedidoContext pedidoContext)
        {
            this._pedidoContext = pedidoContext;
        }
        public IActionResult Index(int? pagina)
        {
            PedidoModel pedidoModel = new PedidoModel();
            var pedidos = pedidoModel.BuscarPedidosPaginados(_pedidoContext, pagina);
            return View(pedidos);

        }
        public IActionResult AdicionarPedido()
        {
            return View();
        }
    }
}