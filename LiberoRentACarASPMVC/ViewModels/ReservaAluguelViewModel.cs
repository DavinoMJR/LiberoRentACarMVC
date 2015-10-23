using LiberoRentACar.Model;
using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LiberoRentACarASPMVC.ViewModels
{
    public partial class ReservaAluguelViewModel
    {
        public int AluguelID { get; set; }

        [Display(Name = "Data Aluguel")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [DataType(DataType.Date)]
        [DataAluguel(ErrorMessage="Data de aluguel não pode ser anterior ao dia de hoje.")]
        public DateTime DataAluguel { get; set; }


        [Display(Name = "Data Devolucao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [DataType(DataType.Date)]
        [DataDevolucao]
        public DateTime DataDevolucao { get; set; }

        public int CarroID { get; set; }

        public CarroViewModel Carro { get; set; }

        public int KmInicial { get; set; }
        public int KmFinal { get; set; }
        public bool Finalizado { get; set; }
        public IEnumerable<CarroViewModel> CarrosDisponiveis { get; set; }

        public string SelectedCarro { get; set; }
        public IEnumerable<SelectListItem> SelecaoCarros { get; set; }

        public int SelectedHoraAluguel { get; set; }
        public IEnumerable<SelectListItem> SelecaoHoraAluguel { get; set; }

        public int SelectedHoraDevolucao { get; set; }
        public IEnumerable<SelectListItem> SelecaoHoraDevolucao { get; set; }

      

    }
}