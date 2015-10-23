using LiberoRentACar.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberoRentACarModel
{
    public class DataAluguelAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime data = (DateTime)value;
            return Util.ValidaDataAluguel(data);
        }
        

    }
}
