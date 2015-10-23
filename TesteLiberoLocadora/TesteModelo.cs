using Microsoft.VisualStudio.TestTools.UnitTesting;
using LiberoRentACar.Model;
using Moq;
using System.Collections.Generic;
using LiberoRentACar.Persistence;
using LiberoRentACar.Model.Services;

namespace TesteLiberoLocadora
{
    [TestClass]
    public class TesteModelo
    {
        [TestMethod]
        public void TesteCadastroModelo()
        {
         
            //Arrange
            Modelo mod = new Modelo("MAQUINA MORTIFERA",4,"V9",Categorias.Economico,TipoCombustivel.Alcool,TipoCambio.Automatico,new Fabricante("AQUI","CHINA"));

            var mockCtx = new Mock<IContext>();
            var mockDAO = new Mock<IDAO<Modelo>>();
            var fabmockDAO = new Mock<IDAO<Fabricante>>();

            mockCtx.Setup(x => x.Modelos).Returns(new FakeSET<Modelo> { mod });
            mockDAO.Setup(x => x.Add(mod)).Callback(() => mockDAO.Setup(x => x.FindById(1)).Returns(mod));
            var service = new ModeloService(mockDAO.Object,fabmockDAO.Object);

            //ACT
            service.Adicionar(mod);
            var modEsperado = mockDAO.Object.FindById(1).Nome;

            //ASSERT
            Assert.AreEqual("MAQUINA MORTIFERA",modEsperado);
        }

        [TestMethod]
        public void TesteListarFabricantes()
        {
            //ARRANGE
            List<Modelo> listaModelos = new List<Modelo>{ 
                new Modelo(Faker.NameFaker.Name(),4,"V9",Categorias.Minivan,TipoCombustivel.Diesel,TipoCambio.Manual,new Fabricante("UM","AI")),
                new Modelo(Faker.NameFaker.Name(),4,"V10",Categorias.SUV,TipoCombustivel.Diesel,TipoCambio.Manual,new Fabricante("UM","AI")),
                new Modelo(Faker.NameFaker.Name(),4,"V11",Categorias.Intermediario,TipoCombustivel.Gasolina,TipoCambio.Automatico,new Fabricante("UM","AI"))
            };

            var mockCtx = new Mock<IContext>();
            var mockDAO = new Mock<IDAO<Modelo>>();
            var fabMock = new Mock<IDAO<Fabricante>>();
            mockCtx.Setup(x => x.Modelos).Returns(new FakeSET<Modelo>{
                        listaModelos[0],
                        listaModelos[1],
                        listaModelos[2]
                });
            mockDAO.Setup(x => x.List()).Returns(listaModelos);
            var service = new ModeloService(mockDAO.Object,fabMock.Object);

            //ACT
            List<Modelo> listaEsperada = (List<Modelo>)mockDAO.Object.List();
            int contEsperado = listaEsperada.Count;
            List<Modelo> listaReal = (List<Modelo>)service.Listar();
            int contReal = listaReal.Count;

            //ASSERT
            Assert.AreEqual(contEsperado, contReal);
        }
    }
}