using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DoceGlamourCore.Controllers
{
    public class Caixa : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
