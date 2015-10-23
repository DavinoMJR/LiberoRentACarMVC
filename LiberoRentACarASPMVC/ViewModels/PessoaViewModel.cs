using LiberoRentACarModel;
using System.ComponentModel.DataAnnotations;

namespace LiberoRentACarASPMVC.ViewModels
{
    public class PessoaViewModel
    {
        public string PessoaID { get; set; }

        [Required(ErrorMessage="Nome obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage="Endereço necessário.")]
        public string Endereco { get; set; }

        private TipoPessoa _Tipo;

        [Required]
        [Range(1,int.MaxValue,ErrorMessage="Favor selecionar pessoa física ou jurídica.")]
        public TipoPessoa Tipo
        {
            get
            {
                return _Tipo;
            }
            set
            {
                _Tipo = value;
            }
        }

        [Required(ErrorMessage="CPF/CNPJ obrigatório.")]
        [DadoPessoal(ErrorMessage="O CPF/CNPJ informado é inválido.")]
        public string DadoPessoal { get; set; }

        public virtual UsuarioLOC IdentityUser { get; set; }
       
    }
}
