using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoceGlamourCore.Models
{
    [Table("produto_pedido", Schema = "dados")]
    public class ProdutoPedidoModel
    {
        
        public int codigo_pedido { get; set; }
  
        public PedidoModel PedidoModel { get; set; }
        public int codigo_produto { get; set; }

        public ProdutoModel ProdutoModel { get; set; }
        public double quantidade { get; set; }
        public decimal valor_final { get; set; }
        
    }
}
