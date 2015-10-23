using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LiberoRentACar.Model
{
    public static class Util
    {

        public static bool ValidaDado(string value)
        {
            if (value.Length < 12)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                value = value.Trim();
                value = value.Replace(".", "").Replace("-", "");
                if (value.Length != 11)
                    return false;

                tempCpf = value.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                if (value.EndsWith(digito) == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                string digito;
                string tempCnpj;
                value = value.Trim();
                value = value.Replace(".", "").Replace("-", "").Replace("/", "");
                if (value.Length != 14)
                    return false;
                tempCnpj = value.Substring(0, 12);
                soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                if (value.EndsWith(digito) == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


       
        public static bool ValidaPlaca(string placa)
        {
            Regex regNUM = new Regex("^[A-Z]{3}[0-9]{4}$");
            return regNUM.IsMatch(placa);
              
        }


        public static bool ValidaDataAluguel(DateTime data)
        {
            if (data < DateTime.Now)
            {
                return false;
            }
            return true;
        }
    
        public static DateTime ValidaData(string dia, string mes, string ano)
        {
            int diaV, mesV, anoV;
            if (int.TryParse(dia, out diaV) && int.TryParse(mes, out mesV) && int.TryParse(ano, out anoV))
            {
                return new DateTime(anoV, mesV, diaV);
            }
            else
            {
                throw new System.FormatException("Formado invalido de dia, mes e/ou ano");
            }
        }

        public static bool ValidaTelefone(string value)
        {
            //Regex regTEL = new Regex(@"[9]*[0-9]{8}$");
            //return regTEL.IsMatch(value);
            return true;
        }

        public static bool ValidaCartao(long num, long val, int codSeguranca)
        {
            Regex validaNum = new Regex(@"^[0-9]{16}");
            Regex cod = new Regex(@"^[0-9]{3}");

            if(!validaNum.IsMatch(num.ToString()) || !cod.IsMatch(codSeguranca.ToString())){
                return false;
            }
            else
            {
                return true;
            }

        }

        public static IEnumerable<TimeSpan> Tempos()
        {
            DateTime start = DateTime.ParseExact("08:00", "HH:mm", null);
            DateTime end = DateTime.ParseExact("18:00", "HH:mm", null);
            int interval = 30;
            List<TimeSpan> lstTimeIntervals = new List<TimeSpan>();
            for (DateTime i = start; i <= end; i = i.AddMinutes(interval))
            {
                lstTimeIntervals.Add(i.TimeOfDay);
            }

            return lstTimeIntervals;
           
        }
    }
}

