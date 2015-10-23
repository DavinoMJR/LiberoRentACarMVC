using LiberoRentACar.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberoRentACarModel
{
    public class DadoPessoalAttribute : ValidationAttribute
    {
        public override bool IsValid(object dadoPessoal)
        {
            return Util.ValidaDado(dadoPessoal.ToString());
        }

    }
}
