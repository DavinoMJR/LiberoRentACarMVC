using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LiberoRentACarPersistence
{
    public class FabricanteDAO : IDAO<Fabricante>
    {
        private readonly IContext ctx;

        public FabricanteDAO(IContext ctx)
        {
            this.ctx = ctx;
        }

        public void Add(Fabricante fab)
        {
            ctx.Fabricantes.Add(fab);
            ctx.SaveChanges();

        }

        public void Update(Fabricante fab)
        {
            ctx.Entry(fab).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public bool Exists(object id)
        {
           string nome = id.ToString();
           return ctx.Fabricantes.Any(x => x.Nome == nome);
        }
        public Fabricante FindById(object id)
        {
            return ctx.Fabricantes.SingleOrDefault(x => x.FabricanteID == (int)id);

        }

        public void Delete(Fabricante fabricante)
        {
            ctx.Fabricantes.Remove(fabricante);
            ctx.SaveChanges();
        }

        public IEnumerable<Fabricante> List()
        {
            ctx.Fabricantes.Include(x => x.Modelos).Load();
            return ctx.Fabricantes.ToList();
        }

       
    }

}
