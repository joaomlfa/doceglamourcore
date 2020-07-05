using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoceGlamourCore.Sessao
{
    public class Sessao
    {
        private IHttpContextAccessor _contextAcessor;

        public Sessao(IHttpContextAccessor contextAccessor)
        {
            this._contextAcessor = contextAccessor;
        }

        public void Cadastrar(string key, string valor)
        {
            _contextAcessor.HttpContext.Session.SetString(key, valor);
        }

        public void Atualizar(string key, string valor)
        {
            if (Existe(key))
            {
                _contextAcessor.HttpContext.Session.Remove(key);
            }
            _contextAcessor.HttpContext.Session.SetString(key, valor);
        }

        public void Remover(string key)
        {
            _contextAcessor.HttpContext.Session.Remove(key);
        }
        public void RemoverTodos()
        {
            _contextAcessor.HttpContext.Session.Clear();
        }

        public String Consultar(string key)
        {
            return _contextAcessor.HttpContext.Session.GetString(key);
        }

        public bool Existe(String key)
        {
            if(_contextAcessor.HttpContext.Session.GetString(key) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
