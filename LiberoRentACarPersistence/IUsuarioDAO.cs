using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LiberoRentACarPersistence
{
    public interface IUsuarioDAO
    {
        void CadastrarClienteUsuario(Pessoa cliente, string user);
        UsuarioLOC FindByUserName(string user);
    }
}
