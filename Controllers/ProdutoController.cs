using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGlamourCore.Libraries.LoginUser;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoceGlamourCore.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoContext _produtoContext;
        private LoginUser _loginUser;

        public ProdutoController(ProdutoContext produtoContext, LoginUser loginUser)
        {
            this._produtoContext = produtoContext;
            this._loginUser = loginUser;
        }

        public IActionResult Index(int? pagina)
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                ProdutoModel produtoModel = new ProdutoModel();
                var produtos = produtoModel.BuscarProdutosPaginados(_produtoContext, pagina);
                return View(produtos);
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }
           
        }
        public IActionResult AdicionarProduto()
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
        public IActionResult AdicionarProduto(ProdutoModel produtoModel)
        {
            if (produtoModel.AdicionarProduto(_produtoContext))
            {
                TempData["Verificacao"] = "Produto Cadastrado com Sucesso!!!";
            }
            else
            {
                TempData["Verificacao"] = "Erro Ao Inserir Produto Tente Novamente";
            }
            return RedirectToAction("Index", "Produto");
        }
        [HttpGet]
        public IActionResult Alterar(int id)
        {

            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                ProdutoModel produto = new ProdutoModel();
                produto = produto.BuscarProdutoID(_produtoContext, id);
                return View(produto);
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }
           
        }
        [HttpPost]
        public IActionResult Alterar(ProdutoModel produto)
        {
            if (produto.Alterar(_produtoContext)){
                TempData["verificacao"] = "Alterado";
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                TempData["verificacao"] = "Erro ao alterar";
                return RedirectToAction("Index", "Produto");
            }
        }
        public IActionResult Excluir(int id)
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                ProdutoModel produtoModel = new ProdutoModel();
                if (produtoModel.ExcluirProduto(_produtoContext, id))
                {
                    TempData["Verificacao"] = "Produto Excluido";
                    return RedirectToAction("Index", "Produto");
                }
                else
                {
                    TempData["Verificacao"] = "Erro ao excluir Produto";
                    return RedirectToAction("Index", "Produto");
                }
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }

            
        }
        public IActionResult CancelarProduto()
        {
            return RedirectToAction("Index", "Produto");
        }
    }
}