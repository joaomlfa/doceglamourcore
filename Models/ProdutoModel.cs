using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace DoceGlamourCore.Models
{
    [Table("produto", Schema = "dados")]
    public class ProdutoModel
    {
        [Key]
        public int id_produto { get; set; }
        [Required(ErrorMessage = "Informe um Nome")]
        public string nome { get; set; }
        public string observacao { get; set; }
        public decimal valor { get; set; }
        public string categoria { get; set; }
        public string link_foto { get; set; }
        [Required(ErrorMessage = "Informe um Código")]
        public string codigo_produto { get; set; }
        
        public ICollection<ProdutoPedidoModel>produtoPedidoModels { get; set; }

        public ProdutoModel(int id_produto, string nome, string observacao, decimal valor, string categoria, string link_foto, string codigo_produto, ICollection<ProdutoPedidoModel> produtoPedidoModels)
        {
            this.id_produto = id_produto;
            this.nome = nome;
            this.observacao = observacao;
            this.valor = valor;
            this.categoria = categoria;
            this.link_foto = link_foto;
            this.codigo_produto = codigo_produto;
            this.produtoPedidoModels = produtoPedidoModels;
        }

        public ProdutoModel()
        {
        }

        public IPagedList<ProdutoModel> BuscarProdutosPaginados(ProdutoContext _produtoContext, int? pagina)
        {
            int PageNumber = pagina ?? 1;
            return _produtoContext.produto.ToPagedList(PageNumber, 5);
        }
        public List<ProdutoModel> BuscarProdutos(ProdutoContext _produtoContext)
        {
            return _produtoContext.produto.ToList();
        }
        public bool AdicionarProduto(ProdutoContext _produtoContext)
        {
            try
            {
                _produtoContext.Add(this);
                _produtoContext.SaveChanges();
                return true;
            }
            catch
            {

                return false;
                throw;
            }
           
        }
        public bool Alterar(ProdutoContext _produtoContext)
        {
            try
            {
                var produtoAntigo = _produtoContext.produto.Where(option => option.id_produto == this.id_produto).FirstOrDefault();
                produtoAntigo.nome = this.nome;
                produtoAntigo.observacao = this.observacao;
                produtoAntigo.valor = this.valor;
                produtoAntigo.categoria = this.categoria;
                produtoAntigo.link_foto = this.link_foto;
                produtoAntigo.codigo_produto = this.codigo_produto;
                _produtoContext.SaveChanges();
                return true;
            }
            catch 
            {

                return false;
            }
            
        }
        public ProdutoModel BuscarProdutoID(ProdutoContext _produtoContext,int id)
        {
            var produto = _produtoContext.produto.Where(option => option.id_produto == id).FirstOrDefault();
            return produto;
        }
        public bool ExcluirProduto(ProdutoContext _produtoContext ,int id)
        {
            try
            {

                _produtoContext.produto.Remove(this.BuscarProdutoID(_produtoContext, id));
                _produtoContext.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
                
            }
        }

    }
}
