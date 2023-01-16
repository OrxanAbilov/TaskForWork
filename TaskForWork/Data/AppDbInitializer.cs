using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TaskForWork.Models;
using TaskForWork.Repositories;

namespace TaskForWork.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {



            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Items.Any())
                {
                    List<Item> items = new List<Item>()
                {
                    new Item() {ItemName="Samsung Galaxy A32",ItemPrice=600,ItemStock=20},
                    new Item() {ItemName="IPhone",ItemPrice=3050,ItemStock=25},
                    new Item() {ItemName="HP Pavilion 15",ItemPrice=1500,ItemStock=30},
                    new Item() {ItemName="Logitech Mouse M171",ItemPrice=18,ItemStock=35},
                    new Item() {ItemName="Nokia 6300",ItemPrice=800,ItemStock=40},
                    new Item() {ItemName="Honor X9",ItemPrice=600,ItemStock=45},
                    new Item() {ItemName="Redmi Note 4X",ItemPrice=400,ItemStock=50},
                    new Item() {ItemName="AirDots",ItemPrice=50,ItemStock=55}
                };

                    context.Items.AddRange(items);
                    context.SaveChanges();
                }
            }


        }
    }
}
