using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGlamourCore.Libraries.LoginUser;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoceGlamourCore.Controllers
{

    public class ClienteController : Controller
    {

        private readonly ClienteContext _clienteContext;
        private LoginUser _loginUser;
        public ClienteController(ClienteContext clienteContext, LoginUser loginUser)
        {
            this._clienteContext = clienteContext;
            this._loginUser = loginUser;
        }


        public IActionResult Index(int? pagina)
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if(usuario != null)
            {
                ClienteModel clienteModel = new ClienteModel();
                var cliente = clienteModel.BuscarClientesPaginados(_clienteContext, pagina);
                return View(cliente);
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }

            
        }
        
        public IActionResult NovoCliente()
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
        public IActionResult NovoCliente(ClienteModel clienteModel)
        {
            
            if (clienteModel.InserirCliente(_clienteContext))
            {
                TempData["Verificacao"] = "Cliente Cadastrado com Sucesso!!!";
            }
            else
            {
                TempData["Verificacao"] = "Erro Ao Inserir Cliente Tente Novamente";
            }
            return RedirectToAction("Index", "Cliente");
        }

        public IActionResult Excluir(int id)
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                ClienteModel clienteModel = new ClienteModel();
                var verificaExclusao = clienteModel.ExcluirCliente(_clienteContext, id);

                if (verificaExclusao)
                {
                    TempData["Verificacao"] = "Cliente Excluido";
                    return RedirectToAction("Index", "Cliente");
                }
                else
                {
                    TempData["Verificacao"] = "Erro ao excluir Cliente";
                    return RedirectToAction("Index", "Cliente");
                }
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }

            
            
        }
        [HttpGet]

        public IActionResult Alterar(int id)
        {
            UsuarioModel usuario = _loginUser.GetUser();
            if (usuario != null)
            {
                ClienteModel clienteModel = new ClienteModel();
                clienteModel = clienteModel.BuscarClienteID(_clienteContext, id);
                return View(clienteModel);
            }
            else
            {
                var msg = "Efetue Login para Acessar.";
                return RedirectToAction("Login", "Login", new { mensagem = msg });
            }
           
        }
        [HttpPost]
        public IActionResult Alterar(ClienteModel clienteModel)
        {
            if (clienteModel.AlterarCliente(_clienteContext))
            {
                TempData["verificacao"] = "Alterado";
                return RedirectToAction("Index", "Cliente");
            }
            else
            {
                TempData["verificacao"] = "Erro ao alterar";
                return RedirectToAction("Index", "Cliente");
            }
       
        }
        public IActionResult CancelarCliente()
        {
            return RedirectToAction("Index", "Cliente");
        }
    }
}