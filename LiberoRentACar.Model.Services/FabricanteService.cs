
using LiberoRentACarModel;
using LiberoRentACarPersistence;
using System.Collections.Generic;

namespace LiberoRentACar.Model.Services
{
    public class FabricanteService : IService<Fabricante>
    {
        private readonly IDAO<Fabricante> dao;

        public FabricanteService(IDAO<Fabricante> dao)
        {
            this.dao = dao;
        }

        public void Adicionar(Fabricante fab)
        {
            if (!dao.Exists(fab.Nome))
            {
                dao.Add(fab);
            }
            else
            {
                throw new BusinessException("Fabricante já cadastrado.");
            }      
        }
        public void Editar(Fabricante fab)
        {
            dao.Update(fab);
        }

        public Fabricante Buscar(object id)
        {
            Fabricante fab = dao.FindById(id);
            if (fab != null)
            {
                return fab;
            }
            else
            {
                throw new BusinessException("Fabricante nao encontrado.");
            }
        }
        public IEnumerable<Fabricante> Listar()
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
            Fabricante fab = Buscar(id);
            dao.Delete(fab);
        }   
    }
}
