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
        [ForeignKey("codigo_pedido")]
        public PedidoModel PedidoModel { get; set; }
        public int id_produto { get; set; }
        [ForeignKey ("id_produto")]
        public ProdutoModel ProdutoModel { get; set; }
        public double quantidade { get; set; }
        public double valor_final { get; set; }
        
    }
}
