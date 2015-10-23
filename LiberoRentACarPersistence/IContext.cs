using LiberoRentACarModel;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LiberoRentACarPersistence
{
    public interface IContext : IDisposable
    {
        IDbSet<Pessoa> Pessoas { get; set; }
        IDbSet<Carro> Carros { get; set; }
        IDbSet<Aluguel> Alugueis { get; set; }
        IDbSet<Cartao> Cartoes { get; set; }

        IDbSet<Fabricante> Fabricantes { get; set; }
        IDbSet<Modelo> Modelos { get; set; }

        IDbSet<UsuarioLOC> Users { get; set; }

        DbEntityEntry Entry(object entity);

        int SaveChanges();

    }
}
