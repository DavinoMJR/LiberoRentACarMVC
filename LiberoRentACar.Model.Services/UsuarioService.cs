using LiberoRentACar.Model;
using LiberoRentACarModel;
using LiberoRentACarPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiberoRentACar.Model.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioDAO dao;

        public UsuarioService(IUsuarioDAO dao)
        {
            this.dao = dao;
        }

        public void CadastrarUsuario(Pessoa pessoa, string user){
            dao.CadastrarClienteUsuario(pessoa, user);
        }

    }
}