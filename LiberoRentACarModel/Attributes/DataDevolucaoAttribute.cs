using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberoRentACarModel
{
    public class DataDevolucaoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherPropertyInfo = validationContext.ObjectInstance.GetType();
                var otherValue = otherPropertyInfo.GetProperty("DataAluguel").GetValue(validationContext.ObjectInstance,null);      
                DateTime dataAluguel = Convert.ToDateTime(otherValue);
                DateTime dataDevolucao = Convert.ToDateTime(value);
                if (dataAluguel > dataDevolucao)
                {
                    return new ValidationResult("Data de devolução não pode ser anterior a data de aluguel.");
                }
                return null;
            }
            return null;

        }
    }
}