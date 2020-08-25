using DoceGlamourCore.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoceGlamourCore.Models
{
    [Table("caixa", Schema = "dados")]
    public class CaixaModel
    {
        [Key]
        public int codigo { get; set; }
        public DateTime data_abertura { get; set; }
        public DateTime data_fechamento { get; set; }
        public decimal valor_abertura { get; set; }
        public decimal valor_fechamento { get; set; }
        public string status { get; set; } // Pode ser Aberto ou Fechado

        public CaixaModel()
        {

        }

        public CaixaModel(int codigo, DateTime data_abertura, DateTime data_fechamento, decimal valor_abertura, decimal valor_fechamento, string status)
        {
            this.codigo = codigo;
            this.data_abertura = data_abertura;
            this.data_fechamento = data_fechamento;
            this.valor_abertura = valor_abertura;
            this.valor_fechamento = valor_fechamento;
            this.status = status;
        }

        public List<CaixaModel> BuscarCaixas(CaixaContext _caixaContext)
        {

            return _caixaContext.caixa.ToList();
        }

        public bool AbrirCaixa(CaixaContext _caixaContext)
        {
            if (this.status == "open")
            {
                return false;
            }
            else
            {
                this.status = "open";
                return true;
            }
        }

        public bool FecharCaixa(CaixaContext _caixaContext)
        {
            if (this.status == "open")
            {
                this.status = "close";
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
