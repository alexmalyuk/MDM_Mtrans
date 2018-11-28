using System.Web;
using System.Web.Mvc;

namespace Mtrans_MDM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

#if !DEBUG
            filters.Add(new RequireHttpsAttribute());
#endif

            //if (Config.IsProduction) //Some flag that you can tell if you are in your production environment.
            //{
            //    filters.Add(new RequireHttpsAttribute());
            //}
        }
    }
}
