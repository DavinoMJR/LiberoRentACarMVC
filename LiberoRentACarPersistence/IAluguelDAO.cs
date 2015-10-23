using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LiberoRentACarPersistence
{
    public interface IAluguelDAO : IDAO<Aluguel>
    {
        bool ChecarSeEstaAlugado(int id);
        void DevolverCarro(string placa);
        

    }
}
