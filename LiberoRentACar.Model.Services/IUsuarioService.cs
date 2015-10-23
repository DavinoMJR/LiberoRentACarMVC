using LiberoRentACar.Model;
using LiberoRentACar.Model.Services;
using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberoRentACar.Model.Services
{
    public interface IUsuarioService
    {
        void CadastrarUsuario(Pessoa pessoa, string user);
    }
}
