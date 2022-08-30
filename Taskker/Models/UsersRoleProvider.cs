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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}