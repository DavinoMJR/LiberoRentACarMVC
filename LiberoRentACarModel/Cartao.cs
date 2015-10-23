using LiberoRentACar.Model;
using System;
using System.Collections.Generic;

namespace LiberoRentACarModel
{

    public class Cartao
    {
        public int CartaoID { get; set; }
        private long Numero { get; set; }

        private long _Validade;
        private long Validade
        {
            get
            {
                return _Validade;
            }
            set
            {
                if (value < DateTime.Now.Year)
                {
                    throw new ArgumentOutOfRangeException("Cartao com validade vencida.");
                }
                else
                {
                    _Validade = value;
                }
            }
        }
        private int CodigoSeguranca { get; set; }


        public virtual Cliente Titular { get; set; }
        //FK
        public int PessoaID { get; set; }

        public Cartao()
        {

        }
        public Cartao(long num, long val, int codSeguranca)
        {
            if (Util.ValidaCartao(num, val, codSeguranca) == true)
            {
                Numero = num;
                Validade = val;
                CodigoSeguranca = codSeguranca;
            }
            else
            {
                throw new ArgumentException("Numero de cartao invalido.");
            }
        }

        public Cartao EnviarDados() //Funcionario como parametro? //retornando lista com dados de cartao
        {
            return this;
        }
    }
}
