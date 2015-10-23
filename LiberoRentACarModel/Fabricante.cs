using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiberoRentACarModel
{
    public class Fabricante
    {
        public int FabricanteID { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage="Nacionalidade obrigatória.")]
        public string Nacionalidade { get; set; }

        //HISTORICO
        public ICollection<Modelo> Modelos { get; set; }

        public Fabricante()
        {

        }
        public Fabricante(string nome,string nacionalidade)
        {
            Nome = nome;
            Nacionalidade = nacionalidade;
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
