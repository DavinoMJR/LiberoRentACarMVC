using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberoRentACarModel
{
    public class AnoAttribute : ValidationAttribute
    {        
        public override bool IsValid(object value)
        {
           
            int anoSelecionado = (int)value;
            if (anoSelecionado > DateTime.Now.Year)
            {
                return false;
            }

            return true;
        }

    }
}
