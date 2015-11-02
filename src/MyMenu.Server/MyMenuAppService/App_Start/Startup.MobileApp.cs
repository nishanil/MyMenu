using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using MyMenuAppService.DataObjects;
using MyMenuAppService.Models;
using Owin;

namespace MyMenuAppService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MyMenuAppInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer(null);

            app.UseMobileAppAuthentication(config);
            app.UseWebApi(config);
        }
    }

    public class MyMenuAppInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MyMenuContext>
    {
        protected override void Seed(MyMenuContext context)
        {
            var foodItems = new List<Food>{
                new Food{
                    Id = "012af9d1-0d3d-4246-af70-36ccdd79b33a",
                    Name = "Waffles",
                    Description = "Fresh waffles with honey and strawberries",
                    ImageUrl = "http://i.imgur.com/IfVirWF.jpg",
                    PricePerQty = 80,
                    IsEnabled = true
                },
                new Food{
                    Id = "4c8cd309-7ed3-4036-a804-838f873c757a",
                    Name = "Pasta",
                    Description = "Italian pasta with garlic bread on the side",
                    ImageUrl = "http://i.imgur.com/rOPvbnl.jpg",
                    PricePerQty = 150,
                    IsEnabled = true
                },
                new Food{
                    Id = "2d46fa35-4fc7-4bac-8827-7df1cbfb2177",
                    Name = "Dosa",
                    Description = "Authentic South Indian masala dosa",
                    ImageUrl = "http://i.imgur.com/q2BqGza.jpg",
                    PricePerQty = 40,
                    IsEnabled = true,
                    IsFeatured = true
                }
            };
            var orderId = Guid.NewGuid().ToString();
            var orderId2 = Guid.NewGuid().ToString();
            var orderId3 = Guid.NewGuid().ToString();

            var orders = new List<Order>
            {
                new Order {
                    Id = orderId,
                    SpecialInstruction = "Alergic to gluten",
                    Address = "328 NGV Koramangala Bangalore",
                    Status = "Order Placed",
                    UserEmail = "nnish@live.com",
                    UserPhone = "+91 9123456789",
                    Payment = "Cash Payment",
                    HasFeedback = false,
                    TotalAmount = 320,
                    UserName = "Nish"
                },
                new Order {
                    Id = orderId2,
                    SpecialInstruction = "Alergic to gluten",
                    Address = "256 M.G Road Bangalore",
                    Status = "Out for Delivery",
                    UserEmail = "anil@live.com",
                    UserPhone = "+91 9123456789",
                    Payment = "Cash Payment",
                    HasFeedback = false,
                    TotalAmount = 16,
                    UserName = "Anil"
                },
                 new Order {
                    Id = orderId3,
                    SpecialInstruction = "Alergic to gluten",
                    Address = "413 Domlur Bangalore",
                    Status = "Delivered",
                    UserEmail = "prashant@live.com",
                    UserPhone = "+91 9123456789",
                    Payment = "Cash Payment",
                    HasFeedback = false,
                    TotalAmount = 160,
                    UserName = "Prashant"
                },
            };
            var orderDetailItems = new List<OrderDetail>
            {
                new OrderDetail
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderId = orderId,
                    FoodId = "2d46fa35-4fc7-4bac-8827-7df1cbfb2177",
                    FoodName = "Dosa",
                    SellingPrice = 40,
                    Quantity = 4
                },
                new OrderDetail
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderId = orderId,
                    FoodId = "4c8cd309-7ed3-4036-a804-838f873c757a",
                    FoodName = "Pasta",
                    SellingPrice = 80,
                    Quantity = 2
                },
                new OrderDetail
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderId = orderId2,
                    FoodId = "4c8cd309-7ed3-4036-a804-838f873c757a",
                    FoodName = "Pasta",
                    SellingPrice = 80,
                    Quantity = 2
                },
                 new OrderDetail
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderId = orderId3,
                    FoodId = "2d46fa35-4fc7-4bac-8827-7df1cbfb2177",
                    FoodName = "Dosa",
                    SellingPrice = 40,
                    Quantity = 4
                },
            };

            foreach (Food food in foodItems)
            {
                context.Set<Food>().Add(food);
            }

            foreach (var order in orders)
            {
                context.Set<Order>().Add(order);
            }

            foreach (var orderDetail in orderDetailItems)
            {
                context.Set<OrderDetail>().Add(orderDetail);
            }
            base.Seed(context);
        }
    }
}

