using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LiberoRentACar.Model.Services;
using LiberoRentACarPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiberoRentACarASPMVC.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("LiberoRentACar.Model.Services")
                .BasedOn(typeof(IService<>))
                .OrBasedOn(typeof(IUsuarioService))
                .WithServiceAllInterfaces()
                .LifestylePerWebRequest());
           
        }
    }
}