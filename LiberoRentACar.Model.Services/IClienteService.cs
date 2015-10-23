using LiberoRentACarModel;

namespace LiberoRentACar.Model.Services
{
    public interface IClienteService : IService<Cliente>
    {
        void CadastrarClienteUsuario(Pessoa cliente, string user);
    }
}
