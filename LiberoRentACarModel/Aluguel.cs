using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiberoRentACarModel
{
    public class Aluguel
    {
        [Key]
        public int AluguelID { get; set; }  

        [Display(Name = "Data Aluguel")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [DataAluguel(ErrorMessage = "Data de aluguel não pode ser anterior ao dia de hoje.")]
        public DateTime DataAluguel { get; set; }

        [Display(Name = "Data Devolucao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [DataDevolucao]
        public DateTime DataDevolucao { get; set; }

        [Display(Name = "Carro")]
        public Carro Carro { get; set; }

        public bool Finalizado { get; set; }
        public IEnumerable<Carro> CarrosDisponiveis { get; set; }

        public int KmInicial { get; set; }
        public int KmFinal { get; set; }

        //FKS
        public string UserId { get; set; }
        public int CarroID { get; set; }

        public Aluguel()
        {

        }

    }
}
