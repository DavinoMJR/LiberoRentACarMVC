using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LiberoRentACarPersistence
{
    public class CarroDAO : ICarroDAO
    {
        private readonly IContext ctx;

        public CarroDAO(IContext ctx)
        {
            this.ctx = ctx;
        }


        public void Add(Carro c)
        { 
                  ctx.Carros.Add(c);
                  ctx.SaveChanges();
        }

        public void Update(Carro carro)
        {
            ctx.Entry(carro).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Carro FindById(object id)
        {
            return ctx.Carros.SingleOrDefault(x => x.CarroID == (int) id);     
        }

        public Carro FindByPlaca(string placa)
        {
            return ctx.Carros.SingleOrDefault(x => x.Placa == placa);
        }

        public void Delete(Carro carro)
        {
            ctx.Carros.Remove(carro);
            ctx.SaveChanges();
        }

        public bool Exists(object id)
        {
            return ctx.Carros.Any(x => x.CarroID == (int) id);
        }


        public IEnumerable<Carro> List()
        {
                ctx.Carros.Include(x => x.ModeloCarro).Load();
                return ctx.Carros.ToList();
        }

        public IEnumerable<Carro> ListarCarrosDisponiveis()
        {
            ctx.Carros.Include(x => x.ModeloCarro).Load();
            return ctx.Carros.Where(x => x.Alugado == false).ToList();}     
    } 
}
