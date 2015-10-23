using LiberoRentACarModel;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace LiberoRentACarPersistence
{
    public class UsuarioDAO : IUsuarioDAO
    {
        private readonly IContext ctx;

        public UsuarioDAO(IContext ctx)
        {
            this.ctx = ctx;
        }

        public void CadastrarClienteUsuario(Pessoa cliente, string user)
        {
            UsuarioLOC usuario = ctx.Users.SingleOrDefault(x => x.UserName == user);
            usuario.PessoaCadastrada = cliente;
            ctx.SaveChanges();
        }

        public UsuarioLOC FindByUserName(string user)
        {
            UsuarioLOC usuario = ctx.Users.SingleOrDefault(x => x.PessoaCadastrada.PessoaID == user);
            return usuario;
        }
    }
}
