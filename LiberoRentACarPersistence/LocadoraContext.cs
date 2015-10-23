using LiberoRentACarModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Microsoft.AspNet.Identity.EntityFramework;


namespace LiberoRentACarPersistence
{
    public class LocadoraContext : IdentityDbContext<UsuarioLOC>, IContext
    {   
        public IDbSet<Pessoa> Pessoas { get; set; }
        public IDbSet<Carro> Carros { get; set; }
        public IDbSet<Aluguel> Alugueis { get; set; }
        public IDbSet<Cartao> Cartoes { get; set; }

        public IDbSet<Fabricante> Fabricantes { get; set; }
        public IDbSet<Modelo> Modelos { get; set; }



        public LocadoraContext()
            : base("LocadoraDb", throwIfV1Schema: false)
        {
            Debug.Write(Database.Connection.ConnectionString);
        }

        public static LocadoraContext Create()
        {
        
            return new LocadoraContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<Pessoa>()
               .HasRequired(x => x.IdentityUser)
               .WithOptional(y => y.PessoaCadastrada);


            modelBuilder.Entity<Modelo>()
                .HasKey(t => t.ModeloID);
            
            //0 OU 1 CARTOES
            modelBuilder.Entity<Cartao>()
                .HasKey(x => x.CartaoID);
            modelBuilder.Entity<Cartao>()
                .HasRequired(x => x.Titular)
                .WithOptional(x => x.DadosCartao);


            modelBuilder.Entity<Pessoa>().ToTable("Pessoas");
            modelBuilder.Entity<UsuarioLOC>().ToTable("UsuarioLOC");
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Aluguel>().ToTable("Alugueis");
            modelBuilder.Entity<Carro>().ToTable("Carros");
            modelBuilder.Entity<Cartao>().ToTable("Cartoes");
            modelBuilder.Entity<Fabricante>().ToTable("Fabricantes");
            modelBuilder.Entity<Modelo>().ToTable("Modelos");
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Recupera erros como string
                var errorMessages = ex.EntityValidationErrors
                   .SelectMany(x => x.ValidationErrors)
                   .Select(x => x.ErrorMessage);

                // Monta a lista como uma string apenas
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combina a excecao original com a nova mensagem
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Relanca a excecao com mensagem nova
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public void Dispose()
        {
            base.Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
