using DoceGlamourCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoceGlamourCore.Libraries.LoginUser
{
    public class LoginUser
    {
        private Sessao.Sessao _sessao;
        private String key = "Login.User";
        public LoginUser(Sessao.Sessao sessao)
        {
            this._sessao = sessao;
        }

        public void Login(UsuarioModel usuario)
        {
            string usuarioJSON = JsonConvert.SerializeObject(usuario);
            _sessao.Cadastrar(key, usuarioJSON);
        }

        public UsuarioModel GetUser()
        {
            var stringUser = _sessao.Consultar(key);
            if(stringUser == null)
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<UsuarioModel>(stringUser);
            }

            
        }

        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
