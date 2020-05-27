using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DoceGlamourCore.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoContext _pedidoContext;
        private readonly ClienteContext _clienteContext;
        private readonly ProdutoContext _produtoContext;
        
        public PedidoController(PedidoContext pedidoContext, ClienteContext clienteContext, ProdutoContext produtoContext)
        {
            this._pedidoContext = pedidoContext;
            this._clienteContext = clienteContext;
            this._produtoContext = produtoContext;
            
        }
        public IActionResult Index(int? pagina)
        {
            PedidoModel pedidoModel = new PedidoModel();
            var pedidos = pedidoModel.BuscarPedidosPaginados(_pedidoContext, pagina);
            return View(pedidos);

        }
        [HttpGet]
        public IActionResult AdicionarPedido(int? pagina)
        {

            ClienteModel buscarClientes = new ClienteModel();
            ProdutoModel produtoModel = new ProdutoModel();
            var cliente = buscarClientes.BuscarClientesPaginados(_clienteContext, pagina);
            var produtos = produtoModel.BuscarProdutos(_produtoContext);
            ViewBag.clientes = cliente;
            ViewBag.produtos = produtos;
            return View();
        }
        [HttpPost]
        public JsonResult AdicionarPedido(String  pedido)
        {
            
            return Json(pedido);
        }
        public IActionResult Excluir(int id)
        {
            PedidoModel pedido = new PedidoModel();
            if(pedido.RemoverPedido(_pedidoContext, id))
            {
                TempData["Verificacao"] = "Pedido Excluido";
                return RedirectToAction("Index", "Pedido");
               
            }
            else
            {
                TempData["Verificacao"] = "Erro ao excluir Pedido";
                return RedirectToAction("Index", "Pedido");
            }
            
        }
        [HttpGet]
        public JsonResult BuscarProdutoID()
        {
            var produto = _produtoContext.produto.Where(op => op.id_produto == 17).FirstOrDefault();
            var jsonProduto = JsonSerializer.Serialize(produto);
            return Json(jsonProduto);
        }
        public JsonResult BuscarClienteID()
        {
            var cliente = _clienteContext.cliente.Where(op => op.codigo_cliente == 7).FirstOrDefault();
            var jsonCliente = JsonSerializer.Serialize(cliente);
            
            return Json(jsonCliente);
        }
    }
}