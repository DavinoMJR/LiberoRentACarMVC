using LiberoRentACarModel;
using System.Collections.Generic;

namespace LiberoRentACar.Model.Services
{
    public interface IModeloService : IService<Modelo>
    {
        IEnumerable<Fabricante> ListarFabricantes();

    }
}
