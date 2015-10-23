using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using LiberoRentACar.Model;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using LiberoRentACarPersistence;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Reflection;
using log4net;
using LiberoRentACarModel;
namespace LiberoRentACarASPMVC
{
    public class CustomDropDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<LocadoraContext>
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected override void Seed(LocadoraContext ctx)
        {

            var userManager = new UserManager<UsuarioLOC>(new UserStore<UsuarioLOC>(ctx));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));

            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("Funcionario"));

            var admin = new UsuarioLOC { UserName = "admin@libero.com" };
            var funcionario = new UsuarioLOC { UserName = "funcionario@libero.com" };
            var result = userManager.Create(admin, "AdminBR10");
            var result2 = userManager.Create(funcionario, "Func10@");

            if (result.Succeeded && result2.Succeeded)
            {
                userManager.AddToRole(admin.Id, "Admin");
                userManager.AddToRole(funcionario.Id, "Funcionario");
            }
            else
            {
                log.Error("Seed falhou na hora de criar funcionario.");
            }
        

            List<Fabricante> fabs = new List<Fabricante>(){ 
                new Fabricante("HONDA","Japao"), //0
                new Fabricante("TOYOTA","Japao"), //1
                new Fabricante("FIAT","Italiana"), //2
                new Fabricante("FORD","Estados Unidos"), //3
                new Fabricante("VOLKSWAGEN","Alemanha"), //4
                new Fabricante("CHEVROLET","Estados Unidos"), //5
                new Fabricante("FERRARI","Italiana"), //6
                new Fabricante("BMW","Alemanha"), //7
                new Fabricante("MERCEDES-BENZ","Alemanha"), //8,
                new Fabricante("HYUNDAI","Japao") //9
            };
            ctx.Fabricantes.AddRange(fabs);

            List<Modelo> mods = new List<Modelo>(){
                //VOLKSWAGEN
                new Modelo("AMAROK",5,"CD 2.0 16V/S",Categorias.PickUp,TipoCombustivel.Diesel,TipoCambio.Automatico,fabs[4]),
                new Modelo("CROSSFOX",5,"1.6 T 16V",Categorias.Intermediario,TipoCombustivel.Flex,TipoCambio.Manual,fabs[4]),
                new Modelo("FOX",5,"1.0 8V",Categorias.Economico,TipoCombustivel.Flex,TipoCambio.Manual,fabs[4]),
                new Modelo("GOL TREND",5,"1.6 8V",Categorias.Economico,TipoCombustivel.Flex,TipoCambio.Manual,fabs[4]),
                new Modelo("JETTA",5,"GLX 3 2.8 VR6",Categorias.Executivo,TipoCombustivel.Flex,TipoCambio.Automatico,fabs[4]),
                //HONDA
                new Modelo("CIVIC",4,"LX 1.6 16V",Categorias.Executivo,TipoCombustivel.Flex,TipoCambio.Automatico,fabs[0]),
                new Modelo("CITY",4,"DX 1.5 16V",Categorias.Intermediario,TipoCombustivel.Flex,TipoCambio.Automatico,fabs[0]),
                new Modelo("ACCORD",4,"EX 3.5 V6 24V",Categorias.Executivo,TipoCombustivel.Flex,TipoCambio.Automatico,fabs[0]),
                //TOYOTA
                new Modelo("COROLA",4,"2.0 16V",Categorias.Executivo,TipoCombustivel.Flex,TipoCambio.Automatico,fabs[1]),
                new Modelo("ETIOS",5,"1.5 16V",Categorias.Intermediario,TipoCombustivel.Flex,TipoCambio.Manual,fabs[1]),
                new Modelo("HILUX",5,"3.0 TDI",Categorias.SUV,TipoCombustivel.Diesel,TipoCambio.Automatico,fabs[1]),
                //FIAT
                new Modelo("DOBLO CARGO",5,"1.6 16v",Categorias.Minivan,TipoCombustivel.Flex,TipoCambio.Manual,fabs[2]),
                new Modelo("IDEA",5,"1.8 16v",Categorias.Intermediario,TipoCombustivel.Flex,TipoCambio.Manual,fabs[2]),
                new Modelo("PALIO",4,"1.5 8v",Categorias.Economico,TipoCombustivel.Gasolina,TipoCambio.Manual,fabs[2]),
                new Modelo("UNO WAY",5,"1.4 EVO FIRE 8V",Categorias.Economico,TipoCombustivel.Flex,TipoCambio.Manual,fabs[2]),
                //FORD
                new Modelo("ECOSPORT",5,"1.6 16V",Categorias.Compacto,TipoCombustivel.Flex,TipoCambio.Manual,fabs[3]),
                new Modelo("FIESTA",3,"GL 1.0",Categorias.Economico,TipoCombustivel.Gasolina,TipoCambio.Manual,fabs[3]),
                new Modelo("KA",4,"1.6 MPI 8V",Categorias.Economico,TipoCombustivel.Gasolina,TipoCambio.Manual,fabs[3]),
                //CHEVROLET
                new Modelo("CORSA",5,"1.8 MPFI 8V",Categorias.Economico,TipoCombustivel.Flex,TipoCambio.Manual,fabs[5]),
                new Modelo("S10",5,"4.3 V6",Categorias.PickUp,TipoCombustivel.Diesel,TipoCambio.Manual,fabs[5]),
                new Modelo("VECTRA ELITE",5,"2.4 MPFI 16V",Categorias.Executivo,TipoCombustivel.Flex,TipoCambio.Automatico,fabs[5]),
                //BMW
                new Modelo("Z8",5,"5.0 V8",Categorias.Luxo,TipoCombustivel.Flex,TipoCambio.Automatico,fabs[7]),
                //MERCEDES
                new Modelo("GLA 250 SPORT",5,"2.0 TB 16V",Categorias.Luxo,TipoCombustivel.Flex,TipoCambio.Automatico,fabs[8]),
                //HYUNDAI
                new Modelo("ELANTRA",4,"2.0 16V",Categorias.Executivo,TipoCombustivel.Flex,TipoCambio.Automatico,fabs[9]),
                new Modelo("HB20 COMF",4,"1.6 16V",Categorias.Compacto,TipoCombustivel.Flex,TipoCambio.Manual,fabs[9]),
                new Modelo("HB20 PREMIUM",4,"1.6 16V",Categorias.Compacto,TipoCombustivel.Flex,TipoCambio.Manual,fabs[9]),
                new Modelo("TUCSON",4,"2.0 16V",Categorias.SUV,TipoCombustivel.Flex,TipoCambio.Manual,fabs[9]),
                //FERRARI
                new Modelo("GTS SPIDER",3,"V8 TWIN TURBO 670CV",Categorias.Luxo,TipoCombustivel.Flex,TipoCambio.Automatico,fabs[6])

            };

            ctx.Modelos.AddRange(mods);

            List<Carro> listaC = new List<Carro>(){
               new Carro("ASD1010",mods[1],2007,6291,Cores.Amarelo,Direcao.Hidraulica,false,true,4),
               new Carro("SDF3123",mods[7],2010,6291,Cores.Amarelo,Direcao.Hidraulica,true,true,4),
                new Carro("KQN2311",mods[6],2012,6291,Cores.Amarelo,Direcao.Hidraulica,true,true,5),
            new Carro("SDX5433",mods[2],2015,6291,Cores.Prata,Direcao.Manual,true,true,2),
            new Carro("POQ2030",mods[5],2014,6291,Cores.Cinza,Direcao.Manual,true,true,4),
            new Carro("MNX1211",mods[4],2006,6291,Cores.Rosa,Direcao.Hidraulica,false,true,4),

            };

            ctx.Carros.AddRange(listaC);

            //List<Carro> cs = new List<Carro>(){
            //    new Carro("PAS2010",2001,mods.ElementAt(2),Cores.Amarelo,Direcao.Hidraulica,true,4,true),
            //    new Carro("AZD3322",2006,mods.ElementAt(4),Cores.Azul,Direcao.Hidraulica,true,4,true)
            //};

            //ctx.Carros.AddRange(cs);

           // var controller = new AccountController();


           // controller.RegisterSeed("davinomaurojr@hotmail.com", "Dh1ch2@");

            //List<Pessoa> pes = new List<Pessoa>(){
            //    new Cliente("Foo","RuaFOO",TipoPessoa.PessoaFisica,"85832378245","995646264"),
            //    new Cliente("Foobar","RuaFoobar",TipoPessoa.PessoaFisica,"45792964602","989595326"),
            //};

            //ctx.Pessoas.AddRange(pes);
            ctx.SaveChanges();
            base.Seed(ctx);

        }



    }
}
