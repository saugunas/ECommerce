using AutoMapper;
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrderDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrderDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                /*
                dbContext.Add(new Db.OrderItem() { Id = 1, ProductId = 1, Quantity = 10, UnitPrice = 100 });
                dbContext.Add(new Db.OrderItem() { Id = 2, ProductId = 2, Quantity = 20, UnitPrice = 50 });
                dbContext.Add(new Db.OrderItem() { Id = 3, ProductId = 3, Quantity = 5, UnitPrice = 1000 });
                dbContext.Add(new Db.OrderItem() { Id = 4, ProductId = 4, Quantity = 100, UnitPrice = 250 });
                */

                dbContext.Add(new Db.Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Total = 2000,
                    Items =
                    new List<Db.OrderItem>()
                    {
                        new Db.OrderItem
                        {
                            Id = 1,
                            ProductId = 1,
                            Quantity = 10,
                            UnitPrice = 100
                        },
                        new Db.OrderItem
                        {
                            Id = 5,
                            ProductId = 2,
                            Quantity = 20,
                            UnitPrice = 50
                        },

                    }
                    

                });

                dbContext.Add(new Db.Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    Total = 26000,
                    Items =
                    new List<Db.OrderItem>()
                    {
                        new Db.OrderItem
                        {
                            Id = 2,
                            ProductId = 2,
                            Quantity = 20,
                            UnitPrice = 50
                        },
                        new Db.OrderItem
                        {
                            Id = 6,
                            ProductId = 4,
                            Quantity = 100,
                            UnitPrice = 250
                        }

                    },
                   

                });

                dbContext.Add(new Db.Order()
                {
                    Id = 3,
                    CustomerId = 3,
                    OrderDate = DateTime.Now,
                    Total = 6000,
                    Items =
                    new List<Db.OrderItem>()
                    {
                        new Db.OrderItem
                        {
                            Id = 3,
                            ProductId = 3,
                            Quantity = 5,
                            UnitPrice = 1000
                        },
                        new Db.OrderItem
                        {
                            Id = 7,
                            ProductId = 1,
                            Quantity = 10,
                            UnitPrice = 100
                        }
                    }
                    



                });

                dbContext.Add(new Db.Order()
                {
                    Id = 4,
                    CustomerId = 4,
                    OrderDate = DateTime.Now,
                    Total = 25000,
                    Items =
                    new List<Db.OrderItem>()
                    {
                        new Db.OrderItem
                        {
                            Id = 4,
                            ProductId = 4,
                            Quantity = 100,
                            UnitPrice = 250
                        }
                    }



                });



                dbContext.SaveChanges();
            }

        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders.Where(p => p.CustomerId == customerId).ToListAsync();

                if (orders != null)
                {
                   var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not found");
                
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
