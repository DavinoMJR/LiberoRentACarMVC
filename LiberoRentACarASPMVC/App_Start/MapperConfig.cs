using AutoMapper;
using LiberoRentACar.Model;
using LiberoRentACarASPMVC.ViewModels;
using LiberoRentACarModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiberoRentACarASPMVC
{
    public static class Factory
    {
        public static Modelo GetModelo()
        {
            return new Modelo();
        }
    }
    public static class MapperConfig
    {
        public static void Initialize()
        {
            Mapper.CreateMap<Carro, CarroViewModel>();

            Mapper.CreateMap<CarroViewModel, Carro>()
                .ConstructUsing((Func<ResolutionContext, Carro>)(src => new Carro()));


            Mapper.CreateMap<ModeloViewModel, Modelo>()
                 .ConstructUsing((Func<ResolutionContext, Modelo>)(src => new Modelo()));

            Mapper.CreateMap<Modelo, ModeloViewModel>();


            Mapper.CreateMap<Fabricante, FabricanteViewModel>().ReverseMap();

            Mapper.CreateMap<Cliente, ClienteViewModel>().ReverseMap();

            Mapper.CreateMap<Pessoa, PessoaViewModel>().ReverseMap();

            Mapper.CreateMap<ReservaAluguelViewModel, Aluguel>().ReverseMap();

        }
    }
}