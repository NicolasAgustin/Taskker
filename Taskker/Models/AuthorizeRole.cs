using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Taskker.Models.DAL;

namespace Taskker.Models
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private TaskkerContext context;
        public AuthorizeRoleAttribute(params object[] roles)
        {
            this.context = new TaskkerContext();
            this.Roles = string.Join(",", roles);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = true;
            HttpCookie cookie = httpContext.Request.Cookies.Get(
                    FormsAuthentication.FormsCookieName
            );

            FormsAuthenticationTicket decryptedCookie =
                FormsAuthentication.Decrypt(cookie.Value);

            int id = Int32.Parse(decryptedCookie.Name);
            var user = this.context.Usuarios
                .Where(u => u.ID == id)
                .SingleOrDefault();

            foreach(var role in this.Roles.Split(','))
            {
                authorize = authorize &&
                    user.Roles.ToList().Any(r => r.Nombre == role);
            }

            return authorize;
        }
    }
}