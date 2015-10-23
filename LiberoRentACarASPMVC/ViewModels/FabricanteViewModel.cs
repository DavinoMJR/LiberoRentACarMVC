using LiberoRentACar.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LiberoRentACarASPMVC.ViewModels
{
    public class FabricanteViewModel
    {
        public int FabricanteID { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Nacionalidade obrigatória.")]
        public string Nacionalidade { get; set; }

        //HISTORICO
        public ICollection<ModeloViewModel> Modelos { get; set; }
    }
}