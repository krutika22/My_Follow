namespace My_Follow.Migrations
{
    using IdentitySample.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using My_Follow.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentitySample.Models.ApplicationDbContext>
    {



        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IdentitySample.Models.ApplicationDbContext context)
        {

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin@example.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin@example.com" };

                manager.Create(user, "Admin@123456");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Roles.Any(r => r.Name == "ProductOwners"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new IdentityRole { Name = "ProductOwners" };
                manager.AddToRole(user.Id, "ProductOwners");
            }




               var productowner = new List<ProductOwners> {

                    new ProductOwners {ProductOwnersID = 66, Name ="Ritu", Email = "abc@live.com", CompanyName = "TCS", Description="IT Company", DateOfJoining=  DateTime.Parse("2007-09-01") , FoundedIn= "2007", Street1= "MangalPandey Road",
                        Street2="", City= "Ahmedabad", State="Gujarat", Country= "India", Pin= "391320", ContactNumber= "9824108810", WebsiteURL="",
                        FacebookPageURL=""}
                     
                  //  ProductOwnersID = ProductOwners.Single( s => s.Name == "Engineering").DepartmentID,
                 

            };

               productowner.ForEach(s => context.ProductOwners.AddOrUpdate(p => p.Name, s));
               context.SaveChanges();

            var products = new List<Product> {

                    new Product {ProductID = 66, ProductName = "Chocolate", Details = "",
                    ProductOwnersID = productowner.Single(s => s.Name == "Ritu" ).ProductOwnersID,
                 
                    EndUsers = new List<EndUsers>() 


            }
            };

             products.ForEach(s => context.Products.AddOrUpdate(p => p.ProductID, s));
            context.SaveChanges();

            AddOrUpdateEndUsers(context, "Chocolate", "Kritz");
            context.SaveChanges();

    

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
           
  

        }



        void AddOrUpdateEndUsers(ApplicationDbContext context, string ProductName, string EndUsersName)
        {
            var p = context.Products.SingleOrDefault(c => c.ProductName == ProductName);
            var e = p.EndUsers.SingleOrDefault(i => i.Name == EndUsersName);
            if (e == null)
                p.EndUsers.Add(context.EndUsers.Single(i => i.Name == EndUsersName));
        }
    }
}
