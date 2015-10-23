using LiberoRentACar.Model;
using LiberoRentACarModel;
using LiberoRentACarPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberoRentACar.Model.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteDAO clienteDAO;
        private readonly IUsuarioDAO usuarioDAO;

        public ClienteService(IClienteDAO clienteDAO, IUsuarioDAO usuarioDAO)
        {
            this.clienteDAO = clienteDAO;
            this.usuarioDAO = usuarioDAO;
        }

        public void Adicionar(Cliente cliente)
        {
                clienteDAO.Add(cliente);
        }
        public bool Existe(object dado)
        {
            if (clienteDAO.Exists(dado))
            {
                return true;
            }

            return false;
        }
        public void CadastrarClienteUsuario(Pessoa cliente, string user)
        {
            if (Existe(cliente.DadoPessoal))
            {
                throw new BusinessException("CPF/CNPJ já cadastrado.");
            }
            else
            {
                usuarioDAO.CadastrarClienteUsuario(cliente, user);
            }        
        }

        public void Editar(Cliente cliente)
        {
            clienteDAO.Update(cliente);
        }

        public Cliente Buscar(object id)
        {
            Cliente cliente = clienteDAO.FindById(id);
            if (cliente != null)
            {
                return cliente;
            }
            else
            {
                throw new BusinessException("Carro nao encontrado.");
            }
        }
        public void Remover(object id)
        {
            Cliente cliente = Buscar(id);
            clienteDAO.Delete(cliente);
        }
        public IEnumerable<Cliente> Listar()
        {
            return clienteDAO.List();
        }
    }
}
