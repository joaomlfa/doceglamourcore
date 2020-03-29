﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoceGlamourCore.Controllers
{

    public class ClienteController : Controller
    {

        private readonly ClienteContext _clienteContext;
        public ClienteController(ClienteContext clienteContext)
        {
            this._clienteContext = clienteContext;
        }


        public IActionResult Index(int? pagina)
        {

            ClienteModel clienteModel = new ClienteModel();
            var cliente = clienteModel.BuscarClientes(_clienteContext, pagina);
            return View(cliente);
        }
        public IActionResult NovoCliente()
        {
            return View();
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
            ClienteModel clienteModel = new ClienteModel();            
            var verificaExclusao = clienteModel.ExcluirCliente( _clienteContext, id);

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
    }
}