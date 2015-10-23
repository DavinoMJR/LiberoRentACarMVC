using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LiberoRentACarASPMVC
{
    public class LogFilter : ActionFilterAttribute
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (Logger.IsInfoEnabled)
            {
                StringBuilder msg = new StringBuilder();
                msg.Append(string.Format("Rodando a acao {0} no controller {1}.", filterContext.ActionDescriptor.ActionName,
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName));
                Logger.Info(msg);
            }

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append(string.Format("Terminou de rodar a acao {0} no controller {1}.", filterContext.ActionDescriptor.ActionName,
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName));

            Logger.Info(msg);

        }
    }
}