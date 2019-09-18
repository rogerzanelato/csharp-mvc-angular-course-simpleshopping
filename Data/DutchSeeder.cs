using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _context;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting, UserManager<StoreUser> userManager)
        {
            _context = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            // caso o banco não esteja criado, cria-o
            _context.Database.EnsureCreated();

            StoreUser user = await _createOrGetDefaultUser();

            var products = _createAndReturnProductsIfNecessary();

            // A Order default já é criada na classe Context
            if (products != null)
                _associateProductAndUserWithDefaultOrder(products.First(), user);

            _context.SaveChanges();
        }

        private async Task<StoreUser> _createOrGetDefaultUser()
        {
            StoreUser user = await _userManager.FindByEmailAsync("rogerzanelato@gmail.com");

            if (user != null)
                return user;

            user = new StoreUser()
            {
                FirstName = "Roger",
                LastName = "Zanelato",
                Email = "rogerzanelato@gmail.com",
                UserName = "rogerzanelato@gmail.com"
            };

            var result = await _userManager.CreateAsync(user, "1234");

            if (result != IdentityResult.Success)
                throw new InvalidOperationException("Could not create new user in seeder");

            return user;
        }
        private IEnumerable<Product> _createAndReturnProductsIfNecessary()
        {
            bool existeProdutoNoBanco = _context.Products.Any();

            if (!existeProdutoNoBanco)
                return null;

            var products = _loadDefaultsProductsFromJson();

            _context.Products.AddRange(products);

            return products;
        }
        private IEnumerable<Product> _loadDefaultsProductsFromJson()
        {
            // Need to create sample data
            var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
            var json = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
        }
        private void _associateProductAndUserWithDefaultOrder(Product product, StoreUser user)
        {
            var order = _context.Orders.Where(pOrder => pOrder.Id == 1).FirstOrDefault();

            if (order == null)
                return;

            order.User = user;

            order.Items = new List<OrderItem>()
            {
                new OrderItem()
                {
                  Product = product,
                  Quantity = 5,
                  UnitPrice = product.Price
                }
            };
        }
    }
}
