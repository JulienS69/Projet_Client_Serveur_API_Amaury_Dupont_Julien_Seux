using System.Web;
using System.Web.Mvc;

namespace Projet_30_05_22_Julien_Seux
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
