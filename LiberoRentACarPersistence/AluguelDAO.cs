using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LiberoRentACarPersistence
{
    public class AluguelDAO : IAluguelDAO
    {
        private readonly IContext ctx;

        public AluguelDAO(IContext ctx)
        {
            this.ctx = ctx;
        }
        public void Add(Aluguel aluguel)
        {           

                ctx.Alugueis.Add(aluguel);
                ctx.Carros.Single(x => x.CarroID == aluguel.CarroID).Alugueis.Add(aluguel);
                ctx.SaveChanges();           
        }

        public void Update(Aluguel aluguel)
        {
            ctx.Entry(aluguel).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Aluguel FindById(object dado)
        {
            int id;
            if (int.TryParse(dado.ToString(), out id))
            {
                return ctx.Alugueis.Include(x=>x.Carro).SingleOrDefault(x => x.AluguelID == id);
            }
            else
            {
                throw new ArgumentOutOfRangeException("ID invalido. Apenas numeros permitidos.");
            }
        }

        public void Delete(Aluguel aluguel)
        {
            ctx.Alugueis.Remove(aluguel);
            ctx.SaveChanges();
        }

        public bool ChecarSeEstaAlugado(int carroID)
        {
            return ctx.Alugueis.Any(x=>x.CarroID == carroID && x.Carro.Alugado == true);


        }
        public bool Exists(object id)
        {
            int ID;
            if (int.TryParse(id.ToString(), out ID))
            {
                return ctx.Alugueis.Any(x => x.AluguelID == ID);
            }
            else
            {
                throw new ArgumentOutOfRangeException("ID invalido. Apenas numeros permitidos.");
            }
        }
        public void DevolverCarro(string placa)
        {
            ctx.Alugueis.SingleOrDefault(x => x.Carro.Placa == placa && x.Finalizado == false)
                                        .Finalizado = true;
            ctx.SaveChanges();
            
        }

        public IEnumerable<Aluguel> List()
        {
            return ctx.Alugueis.Include(a => a.Carro);
        }
    }
}
