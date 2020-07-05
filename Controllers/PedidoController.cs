using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using DoceGlamourCore.Libraries.LoginUser;

namespace DoceGlamourCore.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoContext _pedidoContext;
        private readonly ClienteContext _clienteContext;
        private readonly ProdutoContext _produtoContext;
        private readonly ProdutoPedidoContext _produtoPedidoContext;
        private LoginUser _loginUser;
        
        public PedidoController(PedidoContext pedidoContext, ClienteContext clienteContext, ProdutoContext produtoContext, ProdutoPedidoContext produtoPedidoContext, LoginUser loginUser)
        {
            this._pedidoContext = pedidoContext;
            this._clienteContext = clienteContext;
            this._produtoContext = produtoContext;
            this._produtoPedidoContext = produtoPedidoContext;
            this._loginUser = loginUser;
            
        }
        public IActionResult Index(int? pagina)
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                PedidoModel pedidoModel = new PedidoModel();
                var pedidos = pedidoModel.BuscarPedidosPaginados(_pedidoContext, pagina);
                return View(pedidos);
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }

            

        }
        [HttpGet]
        public IActionResult AdicionarPedido(int? pagina)
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                ClienteModel buscarClientes = new ClienteModel();
                ProdutoModel produtoModel = new ProdutoModel();
                var cliente = buscarClientes.BuscarClientesPaginados(_clienteContext, pagina);
                var produtos = produtoModel.BuscarProdutos(_produtoContext);
                ViewBag.clientes = cliente;
                ViewBag.produtos = produtos;
                return View();
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }
           
        }
        [HttpPost]
        public IActionResult AdicionarPedido(PedidoModel  pedido)
        
        {

            if (pedido.AdicionarPedido(_pedidoContext))
            {
                TempData["Verificacao"] = "Pedido Cadastrado com Sucesso!!!";
                return RedirectToAction("Index", "Pedido");
            }
            else
            {
                TempData["Verificacao"] = "Erro Ao Inserir Pedido Tente Novamente";
                return RedirectToAction("Index", "Pedido");
            }
        }
        [HttpGet]
        public IActionResult Alterar(int id, int? pagina)
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                PedidoModel pedidoSelecionado = new PedidoModel();
                ClienteModel buscarClientes = new ClienteModel();
                ProdutoModel produtoModel = new ProdutoModel();
                var pedido = pedidoSelecionado.BuscarPedidoID(_pedidoContext, _produtoPedidoContext, id);
                var cliente = buscarClientes.BuscarClientesPaginados(_clienteContext, pagina);
                var produtos = produtoModel.BuscarProdutos(_produtoContext);
                ViewBag.clientes = cliente;
                ViewBag.produtos = produtos;
                return View(pedido);
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }
          
            
        }
        [HttpPost]
        public IActionResult Alterar(PedidoModel pedido)
        {
            
            if (pedido.AlterarPedido(_pedidoContext, _produtoPedidoContext))
            {
                TempData["verificacao"] = "Alterado";
                return RedirectToAction("Index", "Pedido");
            }
            else
            {
                TempData["verificacao"] = "Erro ao alterar";
                return RedirectToAction("Index", "Pedido");
            }
        }
        
        public IActionResult Excluir(int id)
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                PedidoModel pedido = new PedidoModel();
                if (pedido.RemoverPedido(_pedidoContext, _produtoPedidoContext, id))
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
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }

           
        }
        [HttpGet]
        public JsonResult BuscarProdutoID(int idProduto)
        {
            var produto = _produtoContext.produto.Where(op => op.id_produto == idProduto).FirstOrDefault();
            var jsonProduto = JsonSerializer.Serialize(produto);
            return Json(jsonProduto);
        }
        public JsonResult BuscarClienteID(int idCliente)
        {
            var cliente = _clienteContext.cliente.Where(op => op.codigo_cliente == idCliente).FirstOrDefault();
            var jsonCliente = JsonSerializer.Serialize(cliente);
            
            return Json(jsonCliente);
        }
        public IActionResult CancelarPedido()
        {
            return RedirectToAction("Index", "Pedido");
        }
    }
     
}