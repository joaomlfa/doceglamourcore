using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoceGlamourCore.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoContext _produtoContext;

        public ProdutoController(ProdutoContext produtoContext)
        {
            this._produtoContext = produtoContext;
        }

        public IActionResult Index(int? pagina)
        {
            ProdutoModel produtoModel = new ProdutoModel();
            var produtos = produtoModel.BuscarProdutosPaginados( _produtoContext, pagina);
            return View(produtos);
        }
        public IActionResult AdicionarProduto()
        {
            return View();
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
            ProdutoModel produto = new ProdutoModel();
            produto = produto.BuscarProdutoID(_produtoContext, id);
            return View(produto);
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
            ProdutoModel produtoModel = new ProdutoModel();
            if(produtoModel.ExcluirProduto(_produtoContext, id)){
                TempData["Verificacao"] = "Produto Excluido";
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                TempData["Verificacao"] = "Erro ao excluir Produto";
                return RedirectToAction("Index", "Produto");
            }
        }
    }
}