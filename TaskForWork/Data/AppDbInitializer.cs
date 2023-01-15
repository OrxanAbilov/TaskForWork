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
                    new Item() {ItemName="Item1",ItemPrice=101,ItemStock=1001},
                    new Item() {ItemName="Item2",ItemPrice=102,ItemStock=1002},
                    new Item() {ItemName="Item3",ItemPrice=103,ItemStock=1003},
                    new Item() {ItemName="Item4",ItemPrice=104,ItemStock=1004},
                    new Item() {ItemName="Item5",ItemPrice=105,ItemStock=1005}
                };

                    context.Items.AddRange(items);
                    context.SaveChanges();
                }
            }


        }
    }
}
