using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Taskker.Models.DAL;

namespace Taskker.Models
{
    public class UsersRoleProvider : RoleProvider
    {
        public override string ApplicationName { 
            get => throw new NotImplementedException();
            set => throw new NotImplementedException(); 
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            if (this.RoleExists(roleName))
                return;
            
            Rol newRol = new Rol {
                Nombre = roleName 
            };

            using(var context = new TaskkerContext())
            {
                context.Roles.Add(newRol);
                context.SaveChanges();
            }

        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            using (var context = new TaskkerContext())
            {
                return context.Roles.Select(r => r.Nombre).ToArray();
            }
        }

        public override string[] GetRolesForUser(string email)
        {
            using (TaskkerContext context = new TaskkerContext())
            {
                var userFound = (
                    from user in context.Usuarios
                    where user.Email == email
                    select user
                ).Single();

                List<string> userRoles = new List<string>();

                userFound.Roles.ToList().ForEach(r => userRoles.Add(r.Nombre));

                return userRoles.ToArray();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string email, string roleName)
        {
            using (var context = new TaskkerContext())
            {
                var user = context.Usuarios.SingleOrDefault(u => u.Email == email);
                var userRoles = context.Roles.Select(r => r.Nombre);

                if (user == null)
                    return false;

                return user.Roles != null &&
                    userRoles.Any(r => r == roleName);
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            
            using(var context = new TaskkerContext())
            {
                var rol = context.Roles.SingleOrDefault(r => r.Nombre == roleName);
                return rol != null;
            }
        }
    }
}