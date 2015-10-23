using LiberoRentACar.Model;
using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiberoRentACarASPMVC.ViewModels
{
    public class CarroViewModel
    {
        public int CarroID { get; set; }

        [Required(ErrorMessage = "Placa obrigatória")]
        [RegularExpression("^[A-Z]{3}[0-9]{4}$", ErrorMessage = "Placa inválida.")]
        public string Placa { get; set; }

        [Ano(ErrorMessage="Ano de fabricação não pode ser maior que atual.")]
        [Required(ErrorMessage = "Ano de fabricação obrigatório.")]
        [Display(Name = "Ano Fabricação")]
        public int Ano { get; set; }
        [Range(1,int.MaxValue,ErrorMessage="Favor selecionar uma cor.")]
        public Cores Cor { get; set; }  

        [Range(1, int.MaxValue,ErrorMessage="Favor selecionar um tipo de direção.")]
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

        public bool Alugado { get; set; }

        [Display(Name="Modelo")]
        public ModeloViewModel ModeloCarro { get; set; }
        //FK
        public int ModeloID { get; set; }

        public SelectList ListaModelos { get; set; }


        public CarroViewModel()
        {

        }
         
    }
}