using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LiberoRentACar.Model;
using LiberoRentACar.Persistence;
using LiberoRentACar.Model.Services;

namespace TesteLiberoLocadora
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TesteCarroService
    {

        [TestMethod]
        public void CadastrarCarro()
        {
            //Arrange
            string placa = "FVS1010";
            string placaEsperada;
            Modelo mod = new Modelo("HILUXDAVOLKSWAGEN", 4, "V9", Categorias.Compacto,TipoCombustivel.Diesel, TipoCambio.Automatico, new Fabricante("VOLKSWAGEN", "Alema"));          
            Carro c = new Carro{ Placa=placa, Ano = 1990, ModeloCarro = mod, Cor = Cores.Azul} ;
            var mockCarroDao = new Mock<ICarroDAO>();
            var mockCtx = new Mock<IContext>();
            mockCtx.Setup(x => x.Carros).Returns(new FakeSET<Carro> { c });
            mockCarroDao.Setup(x => x.Add(c)).Callback(() => mockCarroDao.Setup(x => x.FindByPlaca("FVS1010")).Returns(c));
            var serviceCarro = new CarroService(mockCarroDao.Object, null);

            //ACT
            serviceCarro.Adicionar(c);
            placaEsperada = mockCarroDao.Object.FindByPlaca("FVS1010").Placa;

            //ASSERT
            Assert.AreEqual(placa, placaEsperada);
        }


        [TestMethod]
        public void BuscarCarro()
        {
            //Arrange
            string placa;
            string placaEsperada;
            Modelo mod = new Modelo("HILUXDAVOLKSWAGEN", 4, "V9", Categorias.Compacto, TipoCombustivel.Diesel, TipoCambio.Automatico, new Fabricante("VOLKSWAGEN", "Alema"));          
            Carro c = new Carro { Placa = "ASF1010",Ano = 1990, Cor = Cores.Azul, ModeloCarro = mod, Quilometragem = 0};
            var mockCarroDao = new Mock<ICarroDAO>();
            var mock = new Mock<IContext>();
            mock.Setup(x => x.Carros).Returns(new FakeSET<Carro>{
                new Carro { Placa = "ASF1010",Ano = 1990, Cor = Cores.Azul, ModeloCarro = mod, Quilometragem = 0}
            });
            mockCarroDao.Setup(x => x.FindById(1)).Returns(c);
            mockCarroDao.Setup(x => x.FindById(1)).Returns(c);
            var serviceCarro = new CarroService(mockCarroDao.Object, null);

            //ACT
            placaEsperada = mockCarroDao.Object.FindById(1).Placa;
            placa = serviceCarro.Buscar(1).Placa;
                    //    //ASSERT
            Assert.AreEqual(placa, placaEsperada);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void BuscarCarroNaoExistente()
        {
            //Arrange
            string placa;
            string placaEsperada;
            Modelo mod = new Modelo("HILUXDAVOLKSWAGEN", 4, "V9", Categorias.Compacto, TipoCombustivel.Diesel, TipoCambio.Automatico, new Fabricante("VOLKSWAGEN", "Alema"));
            Carro c = new Carro { Placa = "ASF1010", Ano = 1990, Cor = Cores.Azul, ModeloCarro = mod, Quilometragem = 0 };
            var mockCarroDao = new Mock<ICarroDAO>();
            var mock = new Mock<IContext>();
            mock.Setup(x => x.Carros).Returns(new FakeSET<Carro>{
                new Carro { Placa = "ASF1010",Ano = 1990, Cor = Cores.Azul, ModeloCarro = mod, Quilometragem = 0}
            });
            mockCarroDao.Setup(x => x.FindById(1)).Returns(c);
            mockCarroDao.Setup(x => x.FindById(1)).Returns(c);
            var serviceCarro = new CarroService(mockCarroDao.Object, null);

            //ACT
            placaEsperada = mockCarroDao.Object.FindById(1).Placa;
            placa = serviceCarro.Buscar(2).Placa;
           
            //ASSERT
            Assert.AreEqual(placa, placaEsperada);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void CadastrarCarroExistente()
        {
            //ARRANGE
            Modelo mod = new Modelo("HILUXDAVOLKSWAGEN", 4, "V9", Categorias.Compacto, TipoCombustivel.Diesel, TipoCambio.Automatico, new Fabricante("VOLKSWAGEN", "Alema"));
            Carro c = new Carro { Placa = "ASF1010", Ano = 1990, Cor = Cores.Azul, ModeloCarro = mod, Quilometragem = 0 };

            var mockCarroDao = new Mock<ICarroDAO>();
            var mock = new Mock<IContext>();
            mock.Setup(x => x.Carros).Returns(new FakeSET<Carro>{
                new Carro { Placa = "ASF1010",Ano = 1990, Cor = Cores.Azul, ModeloCarro = mod, Quilometragem = 0}
            });
            mockCarroDao.Setup(x => x.FindByPlaca("ASF1010")).Returns(c);

            var serviceCarro = new CarroService(mockCarroDao.Object, null);

            //ACT
            serviceCarro.Adicionar(c);

        }

        [TestMethod]
        public void DeletarCarro()
        {
            //ARRANGE
            Modelo mod = new Modelo("HILUXDAVOLKSWAGEN", 4, "V9", Categorias.Compacto, TipoCombustivel.Diesel, TipoCambio.Automatico, new Fabricante("VOLKSWAGEN", "Alema"));
            Carro c = new Carro { CarroID = 1, Placa = "ASF1010", Ano = 1990, Cor = Cores.Azul, ModeloCarro = mod, Quilometragem = 0 };

            var mockCarroDao = new Mock<ICarroDAO>();
            var mock = new Mock<IContext>();
            mock.Setup(x => x.Carros).Returns(new FakeSET<Carro>{
                new Carro { Placa = "ASF1010",Ano = 1990, Cor = Cores.Azul, ModeloCarro = mod, Quilometragem = 0}
            });
            mockCarroDao.Setup(x => x.FindById(1)).Returns(c);
            mockCarroDao.Setup(x=>x.Delete(c)).Callback( () => mockCarroDao.Setup(x=>x.Exists(1)).Returns(false));

            var serviceCarro = new CarroService(mockCarroDao.Object, null);

            //ACT     
       
            serviceCarro.Remover(1);          
            var result = mockCarroDao.Object.Exists(1);

            //ASSERT
            Assert.AreEqual(result, false);
        }


        [TestMethod]
        public void ListarCarroTeste()
        {
            //ARRANGE
            List<Carro> listaCarros = new List<Carro> {new Carro{ Placa = "ASD1010" },
                                                       new Carro{ Placa = "ASD1212" },
                                                       new Carro{ Placa = "ASD1414" },
            }; 

            var mockCtx = new Mock<IContext>();
            var mockCarroDao = new Mock<ICarroDAO>();
            mockCtx.Setup(x => x.Carros).Returns(new FakeSET<Carro>{
                    listaCarros[0],
                    listaCarros[1],
                    listaCarros[2],
            });


            mockCarroDao.Setup(x => x.List()).Returns(listaCarros);
            var serviceCarro = new CarroService(mockCarroDao.Object, null);

        
            //ACT
            List<Carro> listaEsperada = (List<Carro>)mockCarroDao.Object.List();
            int contEsperado = listaEsperada.Count;
            List<Carro> listaReal = (List<Carro>)serviceCarro.Listar();
            int contReal = listaReal.Count;

            //ASSERT
            Assert.AreEqual(contEsperado, contReal);

        }

     
        [TestMethod]
        public void TesteListarModelosCarro()
        {
            //ARRANGE
            List<Modelo> listaModelos = new List<Modelo>{ new Modelo{ ModeloID = 1},
                                                         new Modelo{ ModeloID = 2}
            };

            var mockCtx = new Mock<IContext>();
            var mockCarroDao = new Mock<ICarroDAO>();
            var mockModeloDao = new Mock<IDAO<Modelo>>();
            var mockFabricanteDao = new Mock<IDAO<Fabricante>>();

            var serviceModelo = new ModeloService(mockModeloDao.Object, mockFabricanteDao.Object);


            mockCtx.Setup(x => x.Modelos).Returns(new FakeSET<Modelo>{
                    listaModelos[0],
                    listaModelos[1],
            });


            mockModeloDao.Setup(x => x.List()).Returns(listaModelos);
            var serviceCarro = new CarroService(mockCarroDao.Object, serviceModelo);


            //ACT
            List<Modelo> listaEsperada = (List<Modelo>)mockModeloDao.Object.List();
            int contEsperado = listaEsperada.Count;
            List<Modelo> listaReal = (List<Modelo>)serviceCarro.ListarModelosCarro();
            int contReal = listaReal.Count;

            //ASSERT
            Assert.AreEqual(contEsperado, contReal);

        }

        //[TestMethod]
        //public void ListarCarrosDisponiveisTeste()
        //{
        //    //ARRANGE
        //    List<Carro> listaCarros = new List<Carro> {new Carro{ CarroID = 1, Placa = "ASD1015" },
        //                                               new Carro{ CarroID = 2, Placa = "ASD1016" },
        //                                               new Carro{ CarroID = 3, Placa = "ASD1017" },
        //    };

        //    List<Carro> listaCarrosDisponiveis = new List<Carro>  {   new Carro{ CarroID = 1, Placa = "ASD1015" },
        //                                               new Carro{ CarroID = 3, Placa = "ASD1017" },
        //    };

        //    var mockCtx = new Mock<IContext>();
        //    var mockCarroDao = new Mock<ICarroDAO>();
        //    var mockModeloDao = new Mock<IDAO<Modelo>>();
        //    var mockFabricanteDao = new Mock<IDAO<Fabricante>>();

        //    var serviceModelo = new ModeloService(mockModeloDao.Object,mockFabricanteDao.Object);


        //    mockCtx.Setup(x => x.Carros).Returns(new FakeSET<Carro>{
        //            listaCarros[0],
        //            listaCarros[1],
        //            listaCarros[2],
        //    });

        //    mockCtx.Setup(x => x.Alugueis).Returns(new FakeSET<Aluguel>
        //    {
        //            new Aluguel{ CarroID = listaCarros[0].CarroID, Carro = listaCarros[0],UserId="asdad@asd.com", DataAluguel = new DateTime(2015,10,31)
        //                , DataDevolucao = DateTime.Now},
        //            new Aluguel{ CarroID = listaCarros[1].CarroID, Carro = listaCarros[1], UserId="asdsdfad@asd.com", DataAluguel = new DateTime(2015,10,31)
        //                , DataDevolucao = new DateTime(2015,11,31)},
        //            new Aluguel{ CarroID = listaCarros[2].CarroID, Carro = listaCarros[2], UserId="asdsdfsad@asd.com", DataAluguel = new DateTime(2015,10,31)
        //                , DataDevolucao = DateTime.Now},                   
        //    });

        //    mockCarroDao.Setup(x => x.ListarCarrosDisponiveis()).Returns(listaCarrosDisponiveis);
        //    var serviceCarro = new CarroService(mockCarroDao.Object, serviceModelo);


        //    //ACT
        //    List<Carro> listaEsperada = (List<Carro>)mockCarroDao.Object.ListarCarrosDisponiveis();
        //    int contEsperado = listaEsperada.Count;
        //    List<Carro> listaReal = (List<Carro>)serviceCarro.Listar();
        //    int contReal = listaReal.Count;

        //    //ASSERT
        //    Assert.AreEqual(contEsperado, contReal);

        //}


    }

}
