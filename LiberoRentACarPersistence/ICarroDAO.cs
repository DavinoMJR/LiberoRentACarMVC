using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LiberoRentACarPersistence
{
    public interface ICarroDAO : IDAO<Carro>
    {
        Carro FindByPlaca(string placa);
        IEnumerable<Carro> ListarCarrosDisponiveis();
    }
}
