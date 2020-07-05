using DoceGlamourCore.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace DoceGlamourCore.Models
{
    [Table("pedido", Schema = "dados")]
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
        public decimal? taxa_entrega { get; set; }
        public decimal desconto { get; set; }
        public string obs { get; set; }
        public string status { get; set; }

        [NotMapped]
        public string clienteJSON { get; set; }
        public int? codigo_cliente { get; set; }
        [JsonIgnore]
        [ForeignKey("codigo_cliente")]
        public ClienteModel cliente { get; set; }
        [NotMapped]
        public string produtoPedidoJSON { get; set; }
        [JsonIgnore]
        public ICollection<ProdutoPedidoModel> produto_pedido { get; set; }

        public PedidoModel(int codigo_pedido, string tema, decimal valor_total, string nome_crianca, int idadecrianca, DateTime data_entrega, DateTime data_sinal, DateTime data_pedido, decimal valor_sinal, decimal taxa_entrega, decimal desconto, string obs, string status, string clienteJSON, ClienteModel cliente, ICollection<ProdutoPedidoModel> produtoPedidoModel)
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
            this.clienteJSON = clienteJSON;
            this.cliente = cliente;
            produto_pedido = produtoPedidoModel;
        }
        public PedidoModel()
        {

        }
        public IPagedList<PedidoModel> BuscarPedidosPaginados(PedidoContext _pedidoContext, int? pagina)
        {
            int PageNumber = pagina ?? 1;
            return _pedidoContext.pedido.Include(option => option.cliente).OrderBy(op => op.data_entrega).ToPagedList(PageNumber, 15);
        }
        public IPagedList<PedidoModel> BuscarPedidosAbertosPaginados(PedidoContext _pedidoContext, int? pagina)
        {
            int PageNumber = pagina ?? 1;
            return _pedidoContext.pedido.Where(options => options.status == "Aberto").Include(option => option.cliente).OrderBy(op => op.data_entrega).ToPagedList(PageNumber, 15);
        }
        public List<PedidoModel>BuscarPedidosFechadosDatados(PedidoContext _pedidoContext, DateTime dataDe, DateTime dataAte)
        {
            return _pedidoContext.pedido.Where(options => options.status == "Fechado" && options.data_pedido >= dataDe && options.data_pedido <= dataAte).Include(options => options.cliente).OrderBy(option => option.data_pedido).ToList();
        }

        public PedidoModel BuscarPedidoID(PedidoContext _pedidoContext, ProdutoPedidoContext _produtoPedidoContext ,int id)
        {

            var pedido = _pedidoContext.pedido.AsNoTracking().Where(op => op.codigo_pedido == id).Include(op => op.cliente).Include(op => op.produto_pedido).FirstOrDefault();          
            var listaProdutos = _produtoPedidoContext.produto_pedido.Where(op => op.codigo_pedido == pedido.codigo_pedido).ToList();
            string clienteJsonConvertido = JsonConvert.SerializeObject(pedido.cliente);
            string produtoPedidoJsonConvertido = JsonConvert.SerializeObject(listaProdutos);
            pedido.clienteJSON = clienteJsonConvertido;
            pedido.produtoPedidoJSON = produtoPedidoJsonConvertido;           
            _pedidoContext.Entry(pedido).State = EntityState.Detached;
            
            foreach (var item in listaProdutos)
            {
                _produtoPedidoContext.Entry(item).State = EntityState.Detached;
            }
            
            return pedido;
        }

        public Boolean AdicionarPedido(PedidoContext _pedidoContext)
        {
            if (this.clienteJSON == null && this.produtoPedidoJSON != null)
            {

                List<ProdutoPedidoModel> produtoPedidoConvertido = JsonConvert.DeserializeObject<List<ProdutoPedidoModel>>(this.produtoPedidoJSON);
                this.produto_pedido = produtoPedidoConvertido;
                try
                {
                    _pedidoContext.pedido.Add(this);
                    _pedidoContext.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;




                }
            }
            else if (this.clienteJSON != null && this.produtoPedidoJSON == null)
            {
                ClienteModel clienteJsonConvertido = JsonConvert.DeserializeObject<ClienteModel>(this.clienteJSON);
                this.codigo_cliente = clienteJsonConvertido.codigo_cliente;
                try
                {
                    _pedidoContext.pedido.Add(this);
                    _pedidoContext.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;




                }
            }
            else if(this.clienteJSON == null && this.produtoPedidoJSON == null)
            {
                
                try
                {
                    _pedidoContext.pedido.Add(this);
                    _pedidoContext.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;




                }
            }
            else
            {

                ClienteModel clienteJsonConvertido = JsonConvert.DeserializeObject<ClienteModel>(this.clienteJSON);
                List<ProdutoPedidoModel> produtoPedidoConvertido = JsonConvert.DeserializeObject<List<ProdutoPedidoModel>>(this.produtoPedidoJSON);
                this.codigo_cliente = clienteJsonConvertido.codigo_cliente;
                this.produto_pedido = produtoPedidoConvertido;
                try
                {
                    _pedidoContext.pedido.Add(this);
                    _pedidoContext.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;

                }
            }

        }
        public bool AlterarPedido(PedidoContext _pedidoContext, ProdutoPedidoContext _produtoPedidoContext)
        {
            try
            {
                
                var pedidoAntigo = _pedidoContext.pedido.AsNoTracking().FirstOrDefault(op => op.codigo_pedido == this.codigo_pedido);
                var produtosPedido = _produtoPedidoContext.produto_pedido.Where(op => op.codigo_pedido == this.codigo_pedido);
                
                ClienteModel clienteJsonConvertido = JsonConvert.DeserializeObject<ClienteModel>(this.clienteJSON);
                this.cliente = clienteJsonConvertido;
                List<ProdutoPedidoModel> produtosPedidoJsonConvertido = JsonConvert.DeserializeObject<List<ProdutoPedidoModel>>(this.produtoPedidoJSON);
                this.produto_pedido = produtosPedidoJsonConvertido;
                
                pedidoAntigo.tema = this.tema;
                pedidoAntigo.nome_crianca = this.nome_crianca;
                pedidoAntigo.idadecrianca = this.idadecrianca;
                pedidoAntigo.valor_sinal = this.valor_sinal;
                pedidoAntigo.valor_total = this.valor_total;
                pedidoAntigo.data_entrega = this.data_entrega;
                pedidoAntigo.data_pedido = this.data_pedido;
                pedidoAntigo.data_sinal = this.data_sinal;
                pedidoAntigo.obs = this.obs;
                pedidoAntigo.taxa_entrega = this.taxa_entrega;
                pedidoAntigo.desconto = this.desconto;
                pedidoAntigo.status = this.status;
                pedidoAntigo.cliente = this.cliente;
                pedidoAntigo.produto_pedido = this.produto_pedido;
                
                if(this.cliente == null)
                {
                    pedidoAntigo.codigo_cliente = null;
                }
                else
                {
                    pedidoAntigo.codigo_cliente = this.cliente.codigo_cliente;
                }
               

                foreach (var item in produtosPedido)
                {
                    _produtoPedidoContext.produto_pedido.Remove(item);
                   

                }
                _produtoPedidoContext.SaveChanges();
              

                foreach (var item in this.produto_pedido)
                {
                    _produtoPedidoContext.produto_pedido.Add(item);
                }
                _produtoPedidoContext.SaveChanges();


                _pedidoContext.Entry(pedidoAntigo).State = EntityState.Modified;
                _pedidoContext.SaveChanges();
                


                return true;
            }
            catch (Exception ex)
            {


                throw ex;



            }
        }
        public bool RemoverPedido(PedidoContext _pedidoContext, ProdutoPedidoContext _produtorPedidocontext, int id)
        {
            try
            {


                _pedidoContext.pedido.Remove(this.BuscarPedidoID(_pedidoContext, _produtorPedidocontext, id));
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
