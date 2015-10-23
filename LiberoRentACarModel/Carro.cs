using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiberoRentACarModel
{
    public class Carro
    {
        public int CarroID { get; set; }

        [Required(ErrorMessage = "Placa obrigatória")]
        [RegularExpression("^[A-Z]{3}[0-9]{4}$", ErrorMessage = "Placa inválida.")]
        public string Placa { get; set; }

        [Ano(ErrorMessage = "Ano de fabricação não pode ser maior que atual.")]
        [Required(ErrorMessage = "Ano de fabricação obrigatório.")]
        [Display(Name = "Ano Fabricação")]
        public int Ano { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Favor selecionar uma cor.")]
        public Cores Cor { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Favor selecionar um tipo de direção.")]
        public Direcao Direcao { get; set; }

        [Display(Name = "KM")]
        public int Quilometragem { get; set; }

        [Display(Name = "Ar Condicionado")]
        public bool ArCondicionado { get; set; }

        [Display(Name = "Freios ABS")]
        public bool FreioABS { get; set; }

        [Required(ErrorMessage = "Capacidade invalida. Favor confirmar os dados.")]
        [Range(1, 10)]
        public int Capacidade { get; set; }

        [Display(Name = "Modelo")]
        public virtual Modelo ModeloCarro { get; set; }

        public bool Alugado { get; set; }
        
        //FK
        public int ModeloID { get; set; }

        //HISTORICO
        public virtual ICollection<Aluguel> Alugueis { get; set; }

        public Carro()
        {

        }

        public Carro(string placa, int modeloID, int ano, int km, Cores cor, Direcao dir, bool freio, bool ar, int capacidade)
        {
            Placa = placa;
            ModeloID = modeloID;
            Ano = ano;
            Quilometragem = km;
            Cor = cor;
            Direcao = dir;
            FreioABS = freio;
            ArCondicionado = ar;
            Capacidade = capacidade;
            Alugado = false;
        }

        public Carro(string placa, Modelo modelo, int ano, int km, Cores cor, Direcao dir, bool freio, bool ar, int capacidade)
        {
            Placa = placa;
            ModeloCarro = modelo;
            Ano = ano;
            Quilometragem = km;
            Cor = cor;
            Direcao = dir;
            FreioABS = freio;
            ArCondicionado = ar;
            Capacidade = capacidade;
            Alugado = false;
        }
      
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", Placa, ModeloCarro, Ano, Cor);


            //    StringBuilder str = new StringBuilder();
            //    str.AppendFormat("{0} || Ano - {1} || Cor - {2} || Placa - {3}",ModeloCarro.ToString(),Ano,Cor.ToString(),Placa);
            //    return str.ToString();
            //}
        }
    }
}
