using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace DoceGlamourCore.Models
{
    [Table("pedido", Schema ="dados")]
    public class PedidoModel
    {
        [Key]
        public int codigo_pedido { get; set; }
       
        public string tema { get; set; }
        public double valor_total { get; set; }
        public string nome_crianca { get; set; }
        public int idadecrianca { get; set; }
        public DateTime data_entrega { get; set; }
        public DateTime data_sinal { get; set; }
        public DateTime data_pedido { get; set; }
        public double valor_sinal { get; set; }
        public double taxa_entrega { get; set; }
        public double desconto { get; set; }
        public string obs { get; set; }
        public string status { get; set; }
        [ForeignKey("codigo_cliente")]
        public ClienteModel cliente { get; set; }
        public ICollection<ProdutoPedidoModel> produto_pedido { get; set; }

        public PedidoModel(int codigo_pedido,  string tema, double valor_total, string nome_crianca, int idadecrianca, DateTime data_entrega, DateTime data_sinal, DateTime data_pedido, double valor_sinal, double taxa_entrega, double desconto, string obs, string status, ClienteModel cliente, ICollection<ProdutoPedidoModel> produtoPedidoModel)
        {
            this.codigo_pedido = codigo_pedido;
            
            this.tema = tema;
            this.valor_total = valor_total;
            this.nome_crianca = nome_crianca;
            this.idadecrianca = idadecrianca;
            this.data_entrega = data_entrega;
            this.data_sinal = data_sinal;
            this.data_pedido = data_pedido;
            this.valor_sinal = valor_sinal;
            this.taxa_entrega = taxa_entrega;
            this.desconto = desconto;
            this.obs = obs;
            this.status = status;
            this.cliente = cliente;
            produto_pedido = produtoPedidoModel;
        }

        public PedidoModel()
        {
        }
        public IPagedList<PedidoModel> BuscarPedidosPaginados(PedidoContext _pedidoContext, int? pagina)
        {
            int PageNumber = pagina ?? 1;
            return _pedidoContext.pedido.Include(option => option.cliente).ToPagedList(PageNumber, 15);
        }

    }
}
