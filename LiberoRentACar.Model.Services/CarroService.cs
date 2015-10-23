
using LiberoRentACarModel;
using LiberoRentACarPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberoRentACar.Model.Services
{
    public class CarroService : ICarroService
    {
        private readonly ICarroDAO dao;
        private readonly IService<Modelo> serviceModelo;

        public CarroService(ICarroDAO dao, IService<Modelo> serviceModelo)
        {
            this.dao = dao;
            this.serviceModelo = serviceModelo;
        }

        public void Adicionar(Carro carro)
        {
            if (dao.FindByPlaca(carro.Placa) == null)
            {
                carro.Alugado = false;
                dao.Add(carro);
            }
            else
            {
                throw new BusinessException("Carro ja cadastrado.");
            }
        }

        public bool Existe(object id)
        {
            return dao.Exists(id);
        }
        public void Editar(Carro carro)
        {
            dao.Update(carro);
        }

        public bool ChecarDisponibilidade(int id)
        {
            return default(bool);
        }
        public Carro Buscar(object id)
        {
            Carro carro = dao.FindById(id);
            if (carro != null)
            {
                return carro;
            }
            else
            {
                throw new BusinessException("Carro nao encontrado.");
            }
        }

        public void Remover(object id)
        {
            Carro carro = Buscar(id);
            dao.Delete(carro);
        }

        public IEnumerable<Carro> Listar()
        {
            return dao.List();
        }

        public IEnumerable<Modelo> ListarModelosCarro()
        {
            return serviceModelo.Listar();
        }
    }
}
