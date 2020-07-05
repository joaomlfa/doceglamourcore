using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoceGlamourCore.Models
{

    [Table("Usuario", Schema ="dados")]
    public class UsuarioModel 
    {   
        [Key]
        public int idUsuario { get; set; }        
        [Required(ErrorMessage = "Informe a senha!")]
        public string senha { get; set; }
        public int nivel { get; set; }
        [Required(ErrorMessage = "Informe o Nome Completo!")]
        public string nomeUsuario { get; set; }
        [Required(ErrorMessage = "Informe um Email!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email Inválido!")]
        public string emailUsuario { get; set; }
#nullable enable
        public string? telefoneUsuario { get; set; }


        
        public bool ValidarUsuario(UsuarioContext _usuarioContext, string emailUsuario, string senhaUsuario)
        {
            try
            {
                var resultado = _usuarioContext.Usuario.Where(u => u.emailUsuario == emailUsuario && u.senha == senhaUsuario).First();
                this.idUsuario = resultado.idUsuario;
                this.nomeUsuario = resultado.nomeUsuario;
                this.nomeUsuario = resultado.nomeUsuario;
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public bool CadastrarUsuario(UsuarioModel usuario, UsuarioContext _usuarioContext)
        {
            usuario.nivel = 1;
            try
            {
                _usuarioContext.Add(usuario);
                _usuarioContext.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
            
        }
    }
}
