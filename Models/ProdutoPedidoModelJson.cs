using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoceGlamourCore.Models
{
    public class ProdutoPedidoModelJson
    {
        public int codigo_pedido { get; set; }
        public string codigo_produto { get; set; }
        public string quantidade { get; set; }
        public double valor_final { get; set; }
    }
}
