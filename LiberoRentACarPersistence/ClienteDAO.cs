using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LiberoRentACarPersistence
{
    public class ClienteDAO : IClienteDAO
    {
        private readonly IContext ctx;

        public ClienteDAO(IContext ctx)
        {
            this.ctx = ctx;
        }

        public void Add(Cliente cliente)
        {
            ctx.Pessoas.Add(cliente);
            ctx.SaveChanges();    
        }

        public void Update(Cliente cliente)
        {
            ctx.Entry(cliente).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public bool Exists(object id)
        {
            string dadoP = id.ToString();
            return ctx.Pessoas.OfType<Cliente>().Any(x => x.DadoPessoal == dadoP);
        }
        public Cliente FindById(object id)
        {
       
                Cliente cliente = ctx.Pessoas.OfType<Cliente>().SingleOrDefault(x => x.PessoaID == (string) id);
                return cliente;
            
        }

        public Cliente FindByDadoPessoal(string dado)
        {
    
            Cliente cliente = ctx.Pessoas.OfType<Cliente>().SingleOrDefault(x => x.DadoPessoal == dado);
            return cliente;
        }

        public void Delete(Cliente cliente)
        {
            ctx.Pessoas.Remove(cliente);
            ctx.SaveChanges();
        }

        public IEnumerable<Cliente> List()
        {
            return ctx.Pessoas.OfType<Cliente>().Include(c => c.DadosCartao);
        }
    }
}
