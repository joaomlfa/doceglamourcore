using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGlamourCore.Context;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoceGlamourCore.Controllers
{
    public class CaixaController : Controller
    {
        private readonly CaixaContext _caixaContext;

        public CaixaController(CaixaContext caixaContext)
        {
            _caixaContext = caixaContext;
        }

        public IActionResult Index()
        {
            CaixaModel caixaModel = new CaixaModel();

            return View(caixaModel.BuscarCaixas(_caixaContext));
        }
    }
}
