using System.Web;
using System.Web.Mvc;

namespace Tourisme_MVC_projet
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
