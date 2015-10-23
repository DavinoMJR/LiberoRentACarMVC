using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LiberoRentACar.Model;
using LiberoRentACar.Persistence;
using Moq;
using LiberoRentACarASPMVC.ViewModels;
using AutoMapper;
using LiberoRentACar.Model.Services;
using System.Web.Mvc;
using LiberoRentACarASPMVC.Controllers;

namespace TesteLiberoLocadora
{
    [TestClass]
    public class TesteCarroController
    {
        //Modelo mod = new Modelo("HILUXDAVOLKSWAGEN", 4, "V9", Categorias.Compacto, TipoCombustivel.Diesel, TipoCambio.Automatico, new Fabricante("VOLKSWAGEN", "Alema"));
        //Carro c = new Carro { Placa = "ASD1010", Ano = 1990, ModeloCarro = mod, Cor = Cores.Azul };

        //var mockCtx = new Mock<IContext>();
        //mockCtx.Setup(x => x.Carros).Returns(new FakeSET<Carro> { });
        //mockCtx.Setup(x => x.Modelos).Returns(new FakeSET<Modelo> { });

        [TestInitialize]
        public void Initialize()
        {
            Mapper.CreateMap<Carro, CarroViewModel>()
                .ForMember(dest => dest.ModeloCarro,
                            opt => opt.Ignore());

            Mapper.CreateMap<CarroViewModel, Carro>();

            Mapper.CreateMap<ModeloViewModel, Modelo>()
                 .ConstructUsing((Func<ResolutionContext, Modelo>)(src => new Modelo()));

            Mapper.CreateMap<Modelo, ModeloViewModel>();

            Mapper.CreateMap<Fabricante, FabricanteViewModel>().ReverseMap();

            Mapper.CreateMap<Cliente, ClienteViewModel>().ReverseMap();

            Mapper.CreateMap<Pessoa, PessoaViewModel>().ReverseMap();
            
        }

        [TestMethod]
        public void TesteCarroViewCreate()
        {
            var mockDAO = new Mock<ICarroDAO>();
            //var mockDAOMODELO = new Mock<IDAO<Modelo>>();
            var carroService = new CarroService(mockDAO.Object,null);

            var controller = new CarroController(carroService);
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TesteCarroViewIndex()
        {          
            var mockDAO = new Mock<ICarroDAO>();
            var mockDAOMODELO = new Mock<IDAO<Modelo>>();
            var carroService = new CarroService(mockDAO.Object, null);
            var controller = new CarroController(carroService);
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("", result.ViewName); //sem passar por pipeline do mvc, nome nao retorna na viewresult
        }

        [TestMethod]
        public void TesteCarroViewDetails()
        {
            Modelo mod = new Modelo("HILUXDAVOLKSWAGEN", 4, "V9", Categorias.Compacto, TipoCombustivel.Diesel, TipoCambio.Automatico, new Fabricante("VOLKSWAGEN", "Alema"));
            Carro c = new Carro { CarroID = 1, Placa = "ASD1010", Ano = 1990, ModeloCarro = mod, Cor = Cores.Azul };
            
            var mockDAO = new Mock<ICarroDAO>();
            mockDAO.Setup(x => x.FindById(1)).Returns(c);
            var mockDAOMODELO = new Mock<IDAO<Modelo>>();
            var carroService = new CarroService(mockDAO.Object, null);
            var controller = new CarroController(carroService);

            var result = controller.Details(1) as ViewResult;

            Assert.AreEqual("", result.ViewName); //sem passar por pipeline do mvc, nome nao retorna na viewresult
        }

        [TestMethod]
        public void TesteCarroViewEdit()
        {
            Modelo mod = new Modelo("HILUXDAVOLKSWAGEN", 4, "V9", Categorias.Compacto, TipoCombustivel.Diesel, TipoCambio.Automatico, new Fabricante("VOLKSWAGEN", "Alema"));
            Carro c = new Carro { CarroID = 1, Placa = "ASD1010", Ano = 1990, ModeloCarro = mod, Cor = Cores.Azul };
            var mockDAO = new Mock<ICarroDAO>();
            mockDAO.Setup(x => x.FindById(1)).Returns(c);
            var mockDAOMODELO = new Mock<IDAO<Modelo>>();
            var carroService = new CarroService(mockDAO.Object, null);
            var controller = new CarroController(carroService);

            var result = controller.Edit(1) as ViewResult;

            Assert.AreEqual("", result.ViewName); //sem passar por pipeline do mvc, nome nao retorna na viewresult
        }

        [TestMethod]
        public void TesteCarroViewDelete()
        {
            Modelo mod = new Modelo("HILUXDAVOLKSWAGEN", 4, "V9", Categorias.Compacto, TipoCombustivel.Diesel, TipoCambio.Automatico, new Fabricante("VOLKSWAGEN", "Alema"));
            Carro c = new Carro { CarroID = 1, Placa = "ASD1010", Ano = 1990, ModeloCarro = mod, Cor = Cores.Azul };

            var mockDAO = new Mock<ICarroDAO>();
            mockDAO.Setup(x => x.FindById(1)).Returns(c);
            var mockDAOMODELO = new Mock<IDAO<Modelo>>();
            var carroService = new CarroService(mockDAO.Object, null);
            var controller = new CarroController(carroService);
            var result = controller.Delete(1) as ViewResult;

            Assert.AreEqual("", result.ViewName); //sem passar por pipeline do mvc, nome nao retorna na viewresult
        }

        //POSTS

        [TestMethod]
        public void TesteCarroPostCreate()
        {
            Modelo mod = new Modelo("HILUXDAVOLKSWAGEN", 4, "V9", Categorias.Compacto, TipoCombustivel.Diesel, TipoCambio.Automatico, new Fabricante("VOLKSWAGEN", "Alema"));
            Carro c = new Carro { CarroID = 1, Placa = "ASD1010", Ano = 1990, ModeloCarro = mod, Cor = Cores.Azul };
            CarroViewModel carroVM = Mapper.Map<CarroViewModel>(c);
            var mockCtx = new Mock<IContext>();
            mockCtx.Setup(x => x.Carros).Returns(new FakeSET<Carro> { c });
            mockCtx.Setup(x => x.Modelos).Returns(new FakeSET<Modelo> { mod });

            var mockDAO = new Mock<ICarroDAO>();
            var mockDAOMODELO = new Mock<IDAO<Modelo>>();
            var carroService = new CarroService(mockDAO.Object, null);
            var controller = new CarroController(carroService);


            RedirectToRouteResult result = controller.Create(carroVM) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(null, result.RouteValues["controller"]);   
        }

        [TestMethod]
        public void TesteCarroPostEdit()
        {
            Modelo mod = new Modelo("HILUXDAVOLKSWAGEN", 4, "V9", Categorias.Compacto, TipoCombustivel.Diesel, TipoCambio.Automatico, new Fabricante("VOLKSWAGEN", "Alema"));
            Carro c = new Carro { CarroID = 1, Placa = "ASD1010", Ano = 1990, ModeloCarro = mod, Cor = Cores.Azul };
            CarroViewModel carroVM = Mapper.Map<CarroViewModel>(c);
            var mockCtx = new Mock<IContext>();
            mockCtx.Setup(x => x.Carros).Returns(new FakeSET<Carro> { c });
            mockCtx.Setup(x => x.Modelos).Returns(new FakeSET<Modelo> { mod });

            var mockDAO = new Mock<ICarroDAO>();
            var mockDAOMODELO = new Mock<IDAO<Modelo>>();
            var carroService = new CarroService(mockDAO.Object, null);
            var controller = new CarroController(carroService);


            RedirectToRouteResult result = controller.Edit(carroVM) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(null, result.RouteValues["controller"]);
        }

        [TestMethod]
        public void TesteCarroPostDelete()
        {
            Modelo mod = new Modelo("HILUXDAVOLKSWAGEN", 4, "V9", Categorias.Compacto, TipoCombustivel.Diesel, TipoCambio.Automatico, new Fabricante("VOLKSWAGEN", "Alema"));
            Carro c = new Carro { CarroID = 1, Placa = "ASD1010", Ano = 1990, ModeloCarro = mod, Cor = Cores.Azul };

            var mockCtx = new Mock<IContext>();
            mockCtx.Setup(x => x.Carros).Returns(new FakeSET<Carro> { c });
            mockCtx.Setup(x => x.Modelos).Returns(new FakeSET<Modelo> { mod });

            var mockDAO = new Mock<ICarroDAO>();
            mockDAO.Setup(x => x.FindById(1)).Returns(c);
            var mockDAOMODELO = new Mock<IDAO<Modelo>>();
            var carroService = new CarroService(mockDAO.Object, null);
            var controller = new CarroController(carroService);

            RedirectToRouteResult result = controller.DeleteConfirmed(1) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(null, result.RouteValues["controller"]);
        }
    }
}
