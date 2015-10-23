using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LiberoRentACarASPMVC.Controllers
{
    public class TestController : ApiController
    {

        public IEnumerable<string> Get()
        {
            return new string[] { "valor1, valor2" };
        }

        public Carro Get(int id)
        {
            return new Carro() { Placa = "PGI9694", Ano = 2013 };
        }

    }
}