using Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mtrans_MDM.Models
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };
            var role3 = new IdentityRole { Name = "api" };
            
            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            // создаем пользователя
            var admin = new ApplicationUser { Email = "alex.malyuk@metrans.com.ua", UserName = "alex.malyuk@metrans.com.ua" };
            string password = @"12qw!@QW";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            base.Seed(context);
        }
    }
}