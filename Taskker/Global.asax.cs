using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Optimization;
using System.Security.Principal;

namespace Taskker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            // Intentamos obtener la cookie de autenticacion
            var authCookie = HttpContext.Current.Request.Cookies[
                FormsAuthentication.FormsCookieName
            ];

            if (authCookie == null)
                return;

            // Desencriptamos el ticket de autenticacion
            FormsAuthenticationTicket authTicket = FormsAuthentication
                .Decrypt(authCookie.Value);

            // Si es null o si el ticket expiro 
            if (authTicket == null || authTicket.Expired)
                return;

            var roles = authTicket.UserData.Split(',');
            HttpContext.Current.User = new GenericPrincipal(
                new FormsIdentity(authTicket),
                roles
            );
        }
    }
}
