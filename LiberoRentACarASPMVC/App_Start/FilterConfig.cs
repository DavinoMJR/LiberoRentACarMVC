using System.Web;
using System.Web.Mvc;

namespace LiberoRentACarASPMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogFilter()); //FILTRO GLOBAL LOG4NET
            filters.Add(new HandleErrorAttribute());            
        }
    }
}
