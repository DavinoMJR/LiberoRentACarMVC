using Microsoft.VisualStudio.TestTools.UnitTesting;
using LiberoRentACar.Model;
using Moq;
using LiberoRentACar.Persistence;
using LiberoRentACar.Model.Services;
using System.Collections.Generic;

namespace TesteLiberoLocadora
{
    [TestClass]
    public class TesteFabricante
    {
        [TestMethod]
        public void TesteCadastroFabricante()
        {
            //Arrange
            string nome = "MotorollaPorqueCelularEhCarro";
            string nacionalidade = "SuecaEuAcho";
            Fabricante fab = new Fabricante(nome, nacionalidade);
            var mockCtx = new Mock<IContext>();
            var mockDAO = new Mock<IDAO<Fabricante>>();

            mockCtx.Setup(x => x.Fabricantes).Returns(new FakeSET<Fabricante>
            { });
            mockDAO.Setup(x => x.Add(fab)).Callback(() => mockDAO.Setup(x => x.FindById(1)).Returns(fab));
            var service = new FabricanteService(mockDAO.Object);

            //ACT
            service.Adicionar(fab);
            var fabEsperado = mockDAO.Object.FindById(1).Nome;

            //ASSERT
            Assert.AreEqual(fabEsperado, nome);
        }

        [TestMethod]
        public void TesteListarFabricantes()
        {
            List<Fabricante> listaFabricantes = new List<Fabricante>{ 
                new Fabricante(Faker.NameFaker.Name(),Faker.LocationFaker.Country()),
                new Fabricante(Faker.NameFaker.Name(),Faker.LocationFaker.Country()),
                new Fabricante(Faker.NameFaker.Name(),Faker.LocationFaker.Country()),
                };
            //BUILDER NAO RODA NEM COM UMA CACETA! 

            var mockCtx = new Mock<IContext>();
            var mockDAO = new Mock<IDAO<Fabricante>>();
            mockCtx.Setup(x => x.Fabricantes).Returns(new FakeSET<Fabricante>{
                        listaFabricantes[0],
                        listaFabricantes[1],
                        listaFabricantes[2]
                });
            mockDAO.Setup(x => x.List()).Returns(listaFabricantes);
            var service = new FabricanteService(mockDAO.Object);

            //ACT
            List<Fabricante> listaEsperada = (List<Fabricante>)mockDAO.Object.List();
            int contEsperado = listaEsperada.Count;
            List<Fabricante> listaReal = (List<Fabricante>)service.Listar();
            int contReal = listaReal.Count;

            //ASSERT
            Assert.AreEqual(contEsperado, contReal);
        }
    }
}