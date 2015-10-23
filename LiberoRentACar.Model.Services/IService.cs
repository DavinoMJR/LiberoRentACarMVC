using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiberoRentACar.Model.Services
{
    public interface IService<T> where T: class
    {
        void Adicionar(T entity);
        void Editar(T entity);
        T Buscar(object id);
        IEnumerable<T> Listar();
        bool Existe(object id);
        void Remover(object id);        
    }
}
