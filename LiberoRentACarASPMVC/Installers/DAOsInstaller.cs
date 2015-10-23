using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LiberoRentACarPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiberoRentACarASPMVC.Installers
{
    public class DAOsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
      {
          container.Register(Classes.FromAssemblyNamed("LiberoRentACarPersistence")
              .BasedOn(typeof(IDAO<>))
              .WithServiceAllInterfaces()
              .LifestylePerWebRequest()); //ja atrelado ao service, logo, morre no GC TRASHER

          container.Register(Classes.FromAssemblyNamed("LiberoRentACarPersistence")
                .BasedOn(typeof(IUsuarioDAO))
                .WithServiceAllInterfaces()
                .LifestylePerWebRequest()); //ja atrelado ao service, logo, morre no GC TRASHER
        }
    }
}