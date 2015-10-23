using System;
using System.Collections.Generic;
using System.Text;

namespace LiberoRentACarModel
{
    public class Modelo //Modelado segundo a FIPE
    {
        public int ModeloID { get; set; }
        public string Nome { get; set; }
        public int QuantidadePortas { get; set; } 
        public string Motor { get; set; }
        public Categorias Categoria { get; set; }
        public TipoCombustivel Combustivel { get; set; }
        public TipoCambio Cambio { get; set; }


        public virtual Fabricante Fabricante { get; set; }
        public ICollection<Carro> Carros { get; set; }
               
        public int FabricanteID { get; set; }

        public Modelo()
        {

        }

        public Modelo(int id)
        {
            ModeloID = id;
        }

        public Modelo(string nomeModelo, int qtdPortas, string motor, Categorias categoria, TipoCombustivel combustivel,
            TipoCambio cambio, Fabricante fab)
        {
            Nome = nomeModelo;
            QuantidadePortas = qtdPortas;
            Motor = motor;
            Categoria = categoria;
            Combustivel = combustivel;
            Cambio = cambio;
            Fabricante = fab;
            Carros = new List<Carro>();
        }

        public Modelo(string nomeModelo, int qtdPortas, string motor, Categorias categoria, TipoCombustivel combustivel,
            TipoCambio cambio, int fabID)
        {
            Nome = nomeModelo;
            QuantidadePortas = qtdPortas;
            Motor = motor;
            Categoria = categoria;
            Combustivel = combustivel;
            Cambio = cambio;
            FabricanteID = fabID;
            Carros = new List<Carro>();
        }
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", Nome, Categoria, Motor, QuantidadePortas);
                   
        }
    }
}
