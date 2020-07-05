using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using X.PagedList;

namespace DoceGlamourCore.Models
{
    [Table("cliente", Schema = "dados")]
    public class ClienteModel
    {
        [Key]
        public int codigo_cliente { get; set; }
        [Required(ErrorMessage = "Informe um Nome")]
        public string nome { get; set; }
        [Required(ErrorMessage = "Informe um Telefone")]
        public string telefone { get; set; }
        public string rua { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cnpj_cpf { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
        public bool? excluido { get; set; }
        public ICollection<PedidoModel>  pedido { get; set; }

        public ClienteModel(int codigo_cliente, string nome, string telefone, string rua, string bairro, string cidade, string numero, string complemento, string cnpj_cpf, string uf, bool excluido)
        {
            this.codigo_cliente = codigo_cliente;
            this.nome = nome;
            this.telefone = telefone;
            this.rua = rua;
            this.bairro = bairro;
            this.cidade = cidade;
            this.numero = numero;
            this.complemento = complemento;
            this.cnpj_cpf = cnpj_cpf;
            this.uf = uf;
            this.excluido = excluido;
        }

        public ClienteModel()
        {

        }
        public IPagedList<ClienteModel> BuscarClientesPaginados(ClienteContext clienteContext, int? pagina)
        {
            int PageNumber = pagina ?? 1;
            return clienteContext.cliente.Where(op => op.excluido == false).ToPagedList(PageNumber, 15);
        }
        public Boolean InserirCliente(ClienteContext _clienteContext)
        {
            try
            {
                this.excluido = false;
                _clienteContext.Add(this);
                _clienteContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
    
        }
        public bool ExcluirCliente(ClienteContext _clienteContext,int id)
        {
            try
            {
                var cliente = _clienteContext.cliente.Where(op => op.codigo_cliente == id).FirstOrDefault();
                cliente.excluido = true;
                _clienteContext.SaveChanges();
                return true;
            }
            catch 
            {
                
                return false;
               
            }
            

           
        }
        public ClienteModel BuscarClienteID(ClienteContext _clienteContext, int id)
        {
            var cliente = _clienteContext.cliente.Where(option => option.codigo_cliente == id).FirstOrDefault();

            return cliente;
        }
        public bool AlterarCliente(ClienteContext _clienteContext)
        {
            try
            {
                var clienteAntigo = _clienteContext.cliente.Where(option => option.codigo_cliente == this.codigo_cliente).FirstOrDefault();
                clienteAntigo.numero = this.numero;
                clienteAntigo.nome = this.nome;
                clienteAntigo.rua = this.rua;
                clienteAntigo.telefone = this.telefone;
                clienteAntigo.uf = this.uf;
                clienteAntigo.cep = this.cep;
                clienteAntigo.cnpj_cpf = this.cnpj_cpf;
                clienteAntigo.cidade = this.cidade;
                clienteAntigo.bairro = this.bairro;
                clienteAntigo.complemento = this.complemento;
                _clienteContext.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
             
            }
         
        }

    }
}
