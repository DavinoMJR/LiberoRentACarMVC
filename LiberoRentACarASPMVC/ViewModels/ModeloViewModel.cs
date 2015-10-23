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
    public class ModeloViewModel
    {
        public int ModeloID { get; set; }

        [Display(Name="Modelo")]
        public string Nome { get; set; }

        [Required(ErrorMessage="Quantidade de portas obrigatório.")]
        [Display(Name="Número Portas")]
        [Range(1,10,ErrorMessage="Número de portas de 1 a 10 apenas.")]
        public int QuantidadePortas { get; set; }

        [Required(ErrorMessage = "Motor obrigatório.")]
        [Display(Name = "Motor")]
        public string Motor { get; set; }

        [Required(ErrorMessage="Categoria obrigatória.")]
        public Categorias Categoria { get; set; }

        [Required(ErrorMessage="Tipo de combustível obrigatório.")]
        public TipoCombustivel Combustivel { get; set; }

        [Required(ErrorMessage="Tipo de câmbio obrigatório.")]
        public TipoCambio Cambio { get; set; }

        public virtual FabricanteViewModel Fabricante { get; set; }
        public ICollection<CarroViewModel> Carros { get; set; }

        public int FabricanteID { get; set; }

        public SelectList ListaFabricantes { get; set; }
    }
}