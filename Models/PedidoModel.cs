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
        public decimal valor_total { get; set; }
        public string nome_crianca { get; set; }
        public int idadecrianca { get; set; }
        public DateTime data_entrega { get; set; }
        public DateTime data_sinal { get; set; }
        public DateTime data_pedido { get; set; }
        public decimal valor_sinal { get; set; }
        public decimal taxa_entrega { get; set; }
        public decimal desconto { get; set; }
        public string obs { get; set; }
        public string status { get; set; }
        [ForeignKey("codigo_cliente")]
        public ClienteModel cliente { get; set; }

        public ICollection<ProdutoPedidoModel> produto_pedido { get; set; }

        public PedidoModel(int codigo_pedido,  string tema, decimal valor_total, string nome_crianca, int idadecrianca, DateTime data_entrega, DateTime data_sinal, DateTime data_pedido, decimal valor_sinal, decimal taxa_entrega, decimal desconto, string obs, string status, ClienteModel cliente, ICollection<ProdutoPedidoModel> produtoPedidoModel)
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
        public IPagedList<PedidoModel> BuscarPedidosAbertosPaginados(PedidoContext _pedidoContext, int? pagina)
        {
            int PageNumber = pagina ?? 1;
            return _pedidoContext.pedido.Where(options => options.status == "Aberto").Include(option => option.cliente).ToPagedList(PageNumber, 15);
        }
       
        public PedidoModel BuscarPedidoID(PedidoContext _pedidoContext, int id)
        {

            var pedido = _pedidoContext.pedido.Where(op => op.codigo_pedido == id).Include(op => op.cliente).Include(op => op.produto_pedido).FirstOrDefault();
            return pedido;
        }
        public bool RemoverPedido(PedidoContext _pedidoContext, int id)
        {
            try
            {
               
                
                _pedidoContext.pedido.Remove(this.BuscarPedidoID(_pedidoContext, id));
                _pedidoContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

    }
}
