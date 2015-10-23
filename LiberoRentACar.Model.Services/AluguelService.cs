
using LiberoRentACarModel;
using LiberoRentACarPersistence;
using System;
using System.Collections.Generic;

namespace LiberoRentACar.Model.Services
{

    public class AluguelService : IAluguelService
    {
        private readonly IAluguelDAO dao;
        private readonly ICarroDAO carroDAO;

        public AluguelService(IAluguelDAO dao, ICarroDAO carroDAO)
        {
            this.dao = dao;
            this.carroDAO = carroDAO;
        }

        public void Reservar(DateTime dataAluguel, DateTime dataDevolucao, int carroID, string userID)
        {
            try
            {
                Carro carro = carroDAO.FindById(carroID);
                if (!carro.Alugado)
                {
                    carro.Alugado = true;
                    Aluguel aluguel = new Aluguel()
                    {
                        DataAluguel = dataAluguel,
                        DataDevolucao = dataDevolucao,
                        CarroID = carroID,
                        UserId = userID,
                        KmInicial = carro.Quilometragem
                    };
                    Adicionar(aluguel);
                }
                else
                {
                    throw new BusinessException("Carro já se encontra alugado.");
                }
            }
            catch (NullReferenceException ex)
            {
                throw new BusinessException("Carro nao encontrado.",ex);
            }
        }

        public void Adicionar(Aluguel aluguel)
        {
            dao.Add(aluguel);
        }
        public void Editar(Aluguel aluguel)
        {
            dao.Update(aluguel);
        }

        public Aluguel Buscar(object id)
        {
            Aluguel aluguel = dao.FindById(id);
            if (aluguel != null)
            {
                return aluguel;
            }
            else
            {
                throw new BusinessException("Aluguel nao encontrado.");
            }
        }
        public IEnumerable<Aluguel> Listar()
        {
            return dao.List();
        }
        public bool Existe(object id)
        {
            if (dao.Exists(id))
            {
                return true;
            }

            return false;
        }
        public void Remover(object id)
        {
            Aluguel aluguel = Buscar(id);
            dao.Delete(aluguel);
        }

        public IEnumerable<Carro> ListarCarrosDisponiveis()
        {
            return carroDAO.ListarCarrosDisponiveis();
        }

        public void DevolverCarro(object id)
        {
            try
            {
                Aluguel aluguel = dao.FindById(id);
                Carro carro = aluguel.Carro;
                dao.DevolverCarro(carro.Placa);
            }
            catch (NullReferenceException ex)
            {

                throw new BusinessException("Carro nao encontrado.", ex);
            }
        }

    }
}
