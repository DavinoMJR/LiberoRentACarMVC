
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LiberoRentACarModel
{
    [DisplayColumn("Nome")]
    public abstract class Pessoa
    {
        [Key]
        public string PessoaID { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Endereço necessário.")]
        public string Endereco { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Favor selecionar pessoa física ou jurídica.")]
        public TipoPessoa Tipo { get; set; }

        [Required(ErrorMessage = "CPF/CNPJ obrigatório.")]
        [DadoPessoal(ErrorMessage = "O CPF/CNPJ informado é inválido.")]
        public string DadoPessoal { get; set; }

        public virtual UsuarioLOC IdentityUser { get; set; }

        public Pessoa()
        {

        }

        public Pessoa(string userId, string nome, string end, TipoPessoa tipo, string dadoP)
        {
            PessoaID = userId;
            Nome = nome;
            Endereco = end;
            Tipo = tipo;
            DadoPessoal = dadoP;
        }
    }
}
