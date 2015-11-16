using Fall2015.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Fall2015.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Fall2015.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Fall2015.Models.ApplicationDbContext";
        }

        protected override void Seed(Fall2015.Models.ApplicationDbContext context)
        {
            //get a ref. to the UserManager
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            rm.Create(new IdentityRole("GodLikeAdmin"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            //create an object of the ApplicationUser and provide a username
            var client1 = new ApplicationUser { UserName = "chrk@kea.dk" };

            //Add the client1 object to the database through the usermanager (and suply password).
            var result1 = userManager.Create(client1, Secrets.Password);

            //If that does not go well (username could already exist), look up the user instead.
            if (result1.Succeeded == false)
            {
                client1 = userManager.FindByName("chrk@kea.dk");
            }

            //save this change to the database to get the GUID that is used as an Id.
            context.SaveChanges();

            //Now when creating new users you can use this value.

            //Add the following to Student
            //UserId = client.Id

            userManager.AddToRole(client1.Id, "GodLikeAdmin");



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
            //first param used to check if exists already: http://blogs.msdn.com/b/rickandy/archive/2013/02/12/seeding-and-debugging-entity-framework-ef-dbs.aspx
            context.Students.AddOrUpdate(s => s.Email, new Student[]
            {
                    new Student
                    {
                        Firstname = "Christian",
                        Lastname =  "Kirschberg",
                        Email = "ckirschberg@gmail.com",
                        MobilePhone = "61690509",
                        ProfileImagePath = "/UserUploads/ProfileImages/dfdf6200-5331-4e9a-88a6-b21c01e35708.jpg" //requires the image to be located here
		            },
                    new Student
                    {
                        Firstname = "Hans",
                        Lastname = "Hansen",
                        Email = "hans@hans.dk",
                        MobilePhone = "12345678",
                        ProfileImagePath = "/UserUploads/ProfileImages/d0de098a-d55c-4638-b1b3-6fa69ad54358.jpg"
                    },
                    new Student
                    {
                        Firstname = "Jens",
                        Lastname = "Jensen",
                        Email = "jens@jens.dk",
                        MobilePhone = "12345638",
                        ProfileImagePath = "/UserUploads/ProfileImages/98196844-5057-4573-a6d6-3712a9577e46.jpg"
                    },
                    new Student
                    {
                        Firstname = "Helle",
                        Lastname = "Hellesen",
                        Email = "helle@helle.dk",
                        MobilePhone = "12345632",
                        ProfileImagePath = "/UserUploads/ProfileImages/4934ca3f-b243-42f3-902e-60e421d523ab.jpg"
                    },
                    new Student
                    {
                        Firstname = "Berit",
                        Lastname = "Beritsen",
                        Email = "berit@berit.dk",
                        MobilePhone = "12345631",
                        ProfileImagePath = "/UserUploads/ProfileImages/88e1a78c-2944-4e1d-a162-8dc95fceead6.jpg"
                    },
                    new Student
                    {
                        Firstname = "Allan",
                        Lastname = "Allansen",
                        Email = "allan@allan.dk",
                        MobilePhone = "12345632",
                        ProfileImagePath = "/UserUploads/ProfileImages/25b8920c-164e-47ce-9678-e1debd581531.jpg"
                    },
                    new Student
                    {
                        Firstname = "Jesper",
                        Lastname = "Jespersen",
                        Email = "jesper@jesper.dk",
                        MobilePhone = "12315631",
                        ProfileImagePath = "/UserUploads/ProfileImages/06975321-feba-4964-9de8-0921b75be780.jpg"
                    }
        });

            context.CompetencyHeaders.AddOrUpdate(h => h.CompetencyHeaderId, new CompetencyHeader[]
            {
                new CompetencyHeader
                {
                    CompetencyHeaderId = 1,
                    Name="Design"
                },
                new CompetencyHeader{
                    CompetencyHeaderId = 2,
                    Name="Computer programming"
                },
                new CompetencyHeader
                {
                    CompetencyHeaderId = 3,
                    Name="Software"
                }
            });




            context.Competencies.AddOrUpdate(c => c.CompetencyId, new Competency[]
        {
            new Competency
            {
                CompetencyId = 1,
                CompetencyHeaderId = 1,
                Name = "Fashion"
            },
            new Competency
            {
                CompetencyId = 2,
                CompetencyHeaderId = 1,
                Name = "Sewing"
            },

            new Competency
            {
                CompetencyId = 3,
                CompetencyHeaderId = 2,
                Name = "Java"
            },
            new Competency
            {
                CompetencyId = 4,
                CompetencyHeaderId = 2,
                Name = "MySQL"
            },
            new Competency
            {
                CompetencyId = 5,
                CompetencyHeaderId = 3,
                Name = "Microsoft Office"
            },
        });


        }
    }
}
