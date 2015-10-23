using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiberoRentACar.Model.Services
{
    public interface ICarroService : IService<Carro>
    {       
        bool ChecarDisponibilidade(int id);
        IEnumerable<Modelo> ListarModelosCarro();
    }
}
