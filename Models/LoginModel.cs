using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DoceGlamourCore.Models
{
    public class LoginModel
    {        
        [Required(ErrorMessage ="Informe um Email!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Email Inválido!")]
        public string email { get; set; }
        [Required(ErrorMessage ="Informe a senha!")]
        public string senha { get; set; }

      

    }
    
}
