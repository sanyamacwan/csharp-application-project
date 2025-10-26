using System;
using System.Linq;
using lab_5.Models;

namespace lab_5.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DealsFinderDbContext context)
        {
            context.Database.EnsureCreated();

            // Seed customers if none
            if (!context.Customers.Any())
            {
                var customers = new[]
                {
                    new Customer{FirstName="Carson",  LastName="Alexander", BirthDate=DateTime.Parse("1995-01-09")},
                    new Customer{FirstName="Meredith",LastName="Alonso",    BirthDate=DateTime.Parse("1992-09-05")},
                    new Customer{FirstName="Arturo",  LastName="Anand",     BirthDate=DateTime.Parse("1993-10-09")},
                    new Customer{FirstName="Gytis",   LastName="Barzdukas", BirthDate=DateTime.Parse("1992-01-09")},
                };
                context.Customers.AddRange(customers);
                context.SaveChanges();

                // capture real IDs
                var c1 = customers[0].Id;
                var c2 = customers[1].Id;
                var c3 = customers[2].Id;
                // var c4 = customers[3].Id; // unused in sample
            }

            // Seed services if none
            if (!context.FoodDeliveryServices.Any())
            {
                context.FoodDeliveryServices.AddRange(
                    new FoodDeliveryService { Id = "A1", Title = "Alpha", Fee = 300 },
                    new FoodDeliveryService { Id = "B1", Title = "Beta", Fee = 130 },
                    new FoodDeliveryService { Id = "O1", Title = "Omega", Fee = 390 }
                );
                context.SaveChanges();
            }

            // Seed subscriptions if none (use actual IDs from DB now)
            if (!context.Subscriptions.Any())
            {
                var cIds = context.Customers
                    .OrderBy(c => c.LastName)
                    .Select(c => c.Id)
                    .ToList();

                if (cIds.Count >= 3)
                {
                    var c1 = cIds[0];
                    var c2 = cIds[1];
                    var c3 = cIds[2];

                    context.Subscriptions.AddRange(
                        new Subscription { CustomerId = c1, FoodDeliveryServiceId = "A1" },
                        new Subscription { CustomerId = c1, FoodDeliveryServiceId = "B1" },
                        new Subscription { CustomerId = c1, FoodDeliveryServiceId = "O1" },
                        new Subscription { CustomerId = c2, FoodDeliveryServiceId = "A1" },
                        new Subscription { CustomerId = c2, FoodDeliveryServiceId = "B1" },
                        new Subscription { CustomerId = c3, FoodDeliveryServiceId = "A1" }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
