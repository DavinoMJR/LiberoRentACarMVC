using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberoRentACar.Model.Services
{
    public interface IAluguelService : IService<Aluguel>
    {
        void Reservar(DateTime dataAluguel, DateTime dataDevolucao, int carroID, string userID);
        IEnumerable<Carro> ListarCarrosDisponiveis();
        void DevolverCarro(object id);

    }
}
