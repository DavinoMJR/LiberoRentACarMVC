using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiberoRentACarModel
{
    public class Cliente : Pessoa
    {
        //FK
        public int CartaoID { get; set; }

        private Cartao _DadosCartao;

        public virtual Cartao DadosCartao
        {
            get { 
                return _DadosCartao; 
            }
            set
            {
                _DadosCartao = value;
            }
        }


        [Required(ErrorMessage = "Telefone obrigatório.")]
        [Telefone(ErrorMessage = "Telefone inválido. Formato esperado: 00-8888888")]
        public string Telefone { get; set; }

        public ICollection<Cartao> Cartoes { get; set; }
        //HISTORICO
        public ICollection<Aluguel> Alugueis { get; set; }

        public Cliente()
        {

        }

        public Cliente(string userId, string nome, string end, TipoPessoa tipo, string dadoP, string tel, Cartao cartao = null)
            : base(userId,nome, end, tipo, dadoP)
        {
            Telefone = tel;
            _DadosCartao = cartao;
            Cartoes = new List<Cartao>();
            Alugueis = new List<Aluguel>();
        }



        //NECESSARIO MESMO?
        public void AdicionarCartao(Cartao dados)
        {
            DadosCartao = dados;
        }
    }
}
