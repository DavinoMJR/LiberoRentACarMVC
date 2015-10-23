using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiberoRentACarASPMVC.App_Start
{
    public class CastleConfig
    {
        private static IWindsorContainer container = new WindsorContainer();

        public static IWindsorContainer Container
        {
            get { return container; }
        }

        public static void Configure()
        {
            container = new WindsorContainer()
                            .Install(FromAssembly.This());

            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public static void Unload()
        {
            container.Dispose();
        }

    }
}