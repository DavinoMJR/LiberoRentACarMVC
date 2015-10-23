using LiberoRentACarModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LiberoRentACarPersistence
{
    public class ModeloDAO :IDAO<Modelo>
    {
        private readonly IContext ctx;

        public ModeloDAO(IContext ctx)
        {
            this.ctx = ctx;
        }
        public void Add(Modelo m)
        {
                ctx.Modelos.Add(m);
                ctx.SaveChanges();
        }

        public void Update(Modelo m)
        {
            ctx.Entry(m).State = EntityState.Modified;
            ctx.SaveChanges();
        }
        public Modelo FindById(object id)
        {
            return ctx.Modelos.SingleOrDefault(x => x.ModeloID == (int) id);
        }

        public bool Exists(object nomeModelo)
        {
            string nome = nomeModelo.ToString();
            return ctx.Modelos.Any(x => x.Nome == nome);
        }
        public void Delete(Modelo modelo)
        {
            ctx.Modelos.Remove(modelo);
            ctx.SaveChanges();
        }

        public IEnumerable<Modelo> List()
        {
            ctx.Modelos.Include(x => x.Fabricante).Load();
            return ctx.Modelos.ToList();            
        }


    }
}
