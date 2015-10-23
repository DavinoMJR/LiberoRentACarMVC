using System.Security.Claims;
using System.Threading.Tasks;
using LiberoRentACar.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LiberoRentACarModel
{
    public class UsuarioLOC : IdentityUser
    {
        public virtual Pessoa PessoaCadastrada { get; set; }
        public int PessoaID { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UsuarioLOC> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}