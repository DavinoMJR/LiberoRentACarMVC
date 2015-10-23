using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LiberoRentACar.Persistence;
using Moq;
using LiberoRentACar.Model.Services;
using LiberoRentACar.Model;

namespace TesteLiberoLocadora
{
    /// <summary>
    /// Summary description for TesteAluguel
    /// </summary>
    [TestClass]
    public class TesteAluguel
    {
       [TestMethod]
       public void TesteReserva()
       {
           //Arrange
           DateTime dataAluguel = DateTime.Now;
           DateTime dataDevolucao = DateTime.MaxValue;
           int idCarro = 1;
           string userID = "kasj@hotmail.com";
           Aluguel teste = new Aluguel() { AluguelID=1, DataAluguel = dataAluguel, DataDevolucao = dataDevolucao, CarroID = 1, UserId = userID,Finalizado = false };
           Carro carro = new Carro() { CarroID = 1, Quilometragem = 6291 };
           var mockCtx = new Mock<IContext>();
           mockCtx.Setup(x => x.Alugueis).Returns(new FakeSET<Aluguel> { teste });           

           var mockdaoAluguel = new Mock<IAluguelDAO>();
           var mockdaoCarro = new Mock<ICarroDAO>();
           
           mockdaoCarro.Setup(x => x.FindById(1)).Returns(carro);
           mockdaoAluguel.Setup(x => x.Add(teste)).Callback(() => mockdaoAluguel.Setup(x => x.Exists(1)).Returns(true));

           var service = new AluguelService(mockdaoAluguel.Object, mockdaoCarro.Object);

           //ACT
           service.Reservar(dataAluguel, dataDevolucao, idCarro, userID);
           bool result = mockdaoAluguel.Object.Exists(1);

           //ASSERT
           Assert.AreEqual(false, result);

        }

       [TestMethod]
       public void TesteDevolucao()
       {
           //Arrange
           DateTime dataAluguel = DateTime.Now;
           DateTime dataDevolucao = DateTime.MaxValue;
           int idCarro = 1;
           string userID = "kasj@hotmail.com";
           Aluguel teste = new Aluguel() { AluguelID = 1, DataAluguel = dataAluguel, DataDevolucao = dataDevolucao, CarroID = 1, UserId = userID, Finalizado = true };
           Carro carro = new Carro() { CarroID = 1 };
           var mockCtx = new Mock<IContext>();
           mockCtx.Setup(x => x.Alugueis).Returns(new FakeSET<Aluguel> { teste });

           var mockdaoAluguel = new Mock<IAluguelDAO>();
           var mockdaoCarro = new Mock<ICarroDAO>();

           mockdaoCarro.Setup(x => x.FindById(1)).Returns(carro);
           mockdaoAluguel.Setup(x => x.Add(teste)).Callback(() => mockdaoAluguel.Setup(x => x.ChecarSeEstaAlugado(1)).Returns(false));

           var service = new AluguelService(mockdaoAluguel.Object, mockdaoCarro.Object);

           //ACT
           service.Reservar(dataAluguel, dataDevolucao, idCarro, userID);
           var result = mockdaoAluguel.Object.ChecarSeEstaAlugado(1);

           //ASSERT
           Assert.AreEqual(false, result);

       }
    }
}
