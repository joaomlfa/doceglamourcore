using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DoceGlamourCore.Libraries.LoginUser;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoceGlamourCore.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly PedidoContext _pedidoContext;
        private readonly ProdutoPedidoContext _produtoPedidoContext;
        private readonly ProdutoContext _produtoContext;
        private LoginUser _loginUser;
        public RelatoriosController(PedidoContext pedidoContext, ProdutoPedidoContext produtoPedidoContext, ProdutoContext produtoContext, LoginUser loginUser)
        {
            this._pedidoContext = pedidoContext;
            this._produtoPedidoContext = produtoPedidoContext;
            this._produtoContext = produtoContext;
            this._loginUser = loginUser;
        }
        public IActionResult Index()
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                return View();
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }
           
        }
        [HttpGet]
        public IActionResult RelatorioPeriodo()
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                return View();
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }
        }
        [HttpPost]
        public IActionResult RelatorioPeriodoCarregado(RelatorioModel relatorioModel)
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                PedidoModel pedidosDatado = new PedidoModel();
                ViewBag.pedidos = pedidosDatado.BuscarPedidosFechadosDatados(_pedidoContext, relatorioModel.datade, relatorioModel.dataate);
                return View(pedidosDatado);
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }
           
        }
        public IActionResult RelatorioGraficoVendas()
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                return View();
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }
        }
        [HttpGet]
        public decimal[] RelatorioGraficoVendasCarregado(String  dataDe, String dataAte)
        {
            DateTime dataDeConvertido = Convert.ToDateTime(dataDe);
            DateTime dataAteConvertido = Convert.ToDateTime(dataAte);
            decimal[] valores = new decimal[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            PedidoModel pedidosDatado = new PedidoModel();
            List<PedidoModel> pedidos = new List<PedidoModel>();
            pedidos = pedidosDatado.BuscarPedidosFechadosDatados(_pedidoContext, dataDeConvertido, dataAteConvertido);
            foreach (var pedido in pedidos)
            {
                if(pedido.data_pedido.Month == 1)
                {
                    valores[0] += pedido.valor_total;
                }
                else if (pedido.data_pedido.Month == 2)
                {
                    valores[1] += pedido.valor_total;
                }
                else if (pedido.data_pedido.Month == 3)
                {
                    valores[2] += pedido.valor_total;
                }
                else if (pedido.data_pedido.Month == 4)
                {
                    valores[3] += pedido.valor_total;
                }
                else if (pedido.data_pedido.Month == 5)
                {
                    valores[4] += pedido.valor_total;
                }
                else if (pedido.data_pedido.Month == 6)
                {
                    valores[5] += pedido.valor_total;
                }
                else if (pedido.data_pedido.Month == 7)
                {
                    valores[6] += pedido.valor_total;
                }
                else if (pedido.data_pedido.Month == 8)
                {
                    valores[7] += pedido.valor_total;
                }
                else if (pedido.data_pedido.Month == 9)
                {
                    valores[8] += pedido.valor_total;
                }
                else if (pedido.data_pedido.Month == 10)
                {
                    valores[9] += pedido.valor_total;
                }
                else if (pedido.data_pedido.Month == 11)
                {
                    valores[10] += pedido.valor_total;
                }
                else if (pedido.data_pedido.Month == 12)
                {
                    valores[11] += pedido.valor_total;
                }
                
            }
            for (int i = 0; i < valores.Length; i++)
            {
                valores[i] = Math.Round(valores[i], 2);
            }


            return valores;
        }
        public IActionResult RelatorioProdutosVendidos()
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                ProdutoPedidoModel produtos = new ProdutoPedidoModel();
                List<ProdutoPedidoModel> produtosVendidos = new List<ProdutoPedidoModel>();
                List<ProdutoPedidoModel> produtosContados = new List<ProdutoPedidoModel>();
                ProdutoModel produtosObjetos = new ProdutoModel();
                produtosVendidos = produtos.BuscarProdutosVendidos(_produtoPedidoContext);

                foreach (var produto in produtosVendidos)
                {
                    if (produtosContados.Any(op => op.codigo_produto == produto.codigo_produto))
                    {
                        int index = produtosContados.FindIndex(op => op.codigo_produto == produto.codigo_produto);
                        produtosContados[index].quantidade += produto.quantidade;
                    }
                    else
                    {
                        produtosContados.Add(produto);
                    }
                }

                ViewBag.produtosPedido = produtosContados.OrderByDescending(op => op.quantidade).ToList();
                ViewBag.produtos = produtosObjetos.BuscarProdutosComExcluidos(_produtoContext).ToList();

                return View();
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }
            

        }
    }
   
}