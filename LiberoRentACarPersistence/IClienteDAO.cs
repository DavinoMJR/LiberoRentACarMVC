using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace LiberoRentACarPersistence
{
    public interface IClienteDAO : IDAO<Cliente>
    {
        Cliente FindByDadoPessoal(string dado);

    }
}
