using LiberoRentACarModel;
using LiberoRentACarPersistence;
using System.Collections.Generic;

namespace LiberoRentACar.Model.Services
{
    public class ModeloService : IModeloService
    {
        private readonly IDAO<Modelo> dao;
        private readonly IDAO<Fabricante> daoFab;

        public ModeloService(IDAO<Modelo> dao, IDAO<Fabricante> daoFab)
        {
            this.dao = dao;
            this.daoFab = daoFab;
        }

        public void Adicionar(Modelo mod)
        {
            if (!dao.Exists(mod.Nome))
            {
                dao.Add(mod);
            }
            else
            {
                throw new BusinessException("Modelo já cadastrado.");
            }
        }
        public void Editar(Modelo mod)
        {
            dao.Update(mod);
        }

        public Modelo Buscar(object id)
        {
            Modelo mod = dao.FindById(id);
            if (mod != null)
            {
                return mod;
            }
            else
            {
                throw new BusinessException("Modelo nao encontrado.");
            }
        }
        public IEnumerable<Modelo> Listar()
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
            Modelo mod = Buscar(id);
            dao.Delete(mod);
        }

        public IEnumerable<Fabricante> ListarFabricantes()
        {
            return daoFab.List();
        }
    }
}
