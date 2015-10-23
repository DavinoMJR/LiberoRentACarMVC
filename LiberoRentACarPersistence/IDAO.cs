using System.Collections.Generic;

namespace LiberoRentACarPersistence
{
    public interface IDAO<T> where T: class
    {
        void Add(T entity);
        void Update(T entity);
        T FindById(object id);
        bool Exists(object id);
        void Delete(T Entity);
        IEnumerable<T> List();
    }
}
