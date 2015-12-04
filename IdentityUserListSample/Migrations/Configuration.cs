using IdentityUserListSample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityUserListSample.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityUserListSample.DataContexts.IdentityDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IdentityUserListSample.DataContexts.IdentityDb context)
        {
            //  This method will be called after migrating to the latest version.

            // default user and role
			var defaultRoles = new [] {"Administrator", "User"};
			
			/**
			 * For this purpose only:
			 * PasswordHash = real password
			 * PhoneNumber = Role(s)
			 */
			var defaultUsers = new[] {
				new ApplicationUser {UserName = "Admin", Email = "admin@admin.com", PasswordHash = "W#lcome!", PhoneNumber = defaultRoles[0]},
			};

			// check for exist role
			foreach (var defaultRole in defaultRoles) {
				if (context.Roles.Any(r => r.Name == defaultRole)) {
					continue;
				}
				var roleStore = new RoleStore<IdentityRole>(context);
				var roleManager = new RoleManager<IdentityRole>(roleStore);
				var newRole = new IdentityRole {Name = defaultRole};

				// add role
				var result = roleManager.Create(newRole);
				if (!result.Succeeded) {
					throw new Exception(result.Errors.First());
				}
			}

			// check for existing user
			foreach (var user in defaultUsers) {
				if (context.Users.Any(u => u.UserName == user.UserName)) {
					continue;
				}
				var userStore = new UserStore<ApplicationUser>(context);
				var userManager = new UserManager<ApplicationUser>(userStore);
				var newUser = new ApplicationUser {UserName = user.UserName, Email = user.Email};

				// add user
				var resultUser = userManager.Create(newUser, user.PasswordHash);
				if (!resultUser.Succeeded) {
					throw new Exception(resultUser.Errors.First());
				}

				// add role to the user
				var resultUserRole = userManager.AddToRole(newUser.Id, user.PhoneNumber);
				if (!resultUserRole.Succeeded) {
					throw new Exception(resultUserRole.Errors.First());
				}

				// lets just add another role to the user
				resultUserRole = userManager.AddToRole(newUser.Id, defaultRoles[1]);
				if (!resultUserRole.Succeeded) {
					throw new Exception(resultUserRole.Errors.First());
				}
			}
        }
    }
}
