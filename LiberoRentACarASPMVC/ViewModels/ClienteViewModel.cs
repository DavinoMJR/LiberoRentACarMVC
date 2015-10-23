using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiberoRentACarASPMVC.ViewModels;
using LiberoRentACar.Model;
using System.ComponentModel.DataAnnotations;
using LiberoRentACarModel;

namespace LiberoRentACarASPMVC.ViewModels
{
    public class ClienteViewModel : PessoaViewModel
    {

        [Required(ErrorMessage="Telefone obrigatório.")]
        [Telefone(ErrorMessage="Telefone inválido. Formato esperado: 00-8888888")]
        public string Telefone { get; set; }

        public ClienteViewModel(){

        }

        //HISTORICO
        public ICollection<Aluguel> Alugueis { get; set; }
      

    }
}
