using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AllocationTask.Dto;
using AllocationTask.Model;

namespace AllocationTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllocationController : ControllerBase
    {
        public Cell[,] matrix;
        public List<Product> products;
        public AllocationController()
        {
            products = new List<Product>();
            products.Add(new Product()
            {
                PrductId = "Bread",
                Adapters= new List<Adapter>() { new Adapter()}
            });
            products.Add(new Product()
            {
                PrductId = "Pasta",
                Adapters = new List<Adapter>() { new Adapter() }
            });
            products.Add(new Product()
            {
                PrductId = "Salt",
                Adapters = new List<Adapter>() { new Adapter() }
            });
            products.Add(new Product()
            {
                PrductId = "Bamba",
                Adapters = new List<Adapter>() { new Adapter() }
            });
            products.Add(new Product()
            {
                PrductId = "Apple",
                Adapters = new List<Adapter>() { new Adapter() }
            });
            products.Add(new Product()
            {
                PrductId = "Milk",
                Adapters = new List<Adapter>() { new ChilledAdapter() }
            });
            products.Add(new Product()
            {
                PrductId = "Yogurt",
                Adapters = new List<Adapter>() { new ChilledAdapter() }
            });
            products.Add(new Product()
            {
                PrductId = "Cheese",
                Adapters = new List<Adapter>() { new ChilledAdapter() }
            });
            products.Add(new Product()
            {
                PrductId = "Insulin",
                Adapters = new List<Adapter>() { new ChilledAdapter() , new HazardousAdapter()}
            });
            products.Add(new Product()
            {
                PrductId = "Bleach",
                Adapters = new List<Adapter>() { new HazardousAdapter() }
            });
            products.Add(new Product()
            {
                PrductId = "Stain removal,",
                Adapters = new List<Adapter>() { new HazardousAdapter() }
            });

            matrix = new Cell[10,10];
            for (int i=0; i<10; i++)
            for (int j = 0; j < 10; j++)
            {
                matrix[i,j]=(new Cell()
                {
                    vertical = i,
                    horizontal = j,
                    quantity = 0
                });
            }

            matrix[0, 1] = new Cell()
            {
                vertical = 0,
                horizontal = 1,
                quantity = 3,
                productId = "Bread"
            };
            matrix[3, 3] = new Cell()
            {
                vertical = 3,
                horizontal = 3,
                quantity = 5,
                productId = "Bamba"
            };

            for (int i=0; i<10; i++)
                matrix[i, 9] = new HazardousCell()
            {
                vertical = i,
                horizontal = 9,
                quantity = 0,
            };


        }

        [HttpGet]
        public string Test()
        {
            return "hi";
        }

        [HttpPost]
        public async Task<AllocationResponse> Post([FromBody] AllocationRequest request)
        {
            Product product=products.FirstOrDefault(x => x.PrductId.Equals(request.ProductId));
            if (product == null)
                throw new HttpRequestException("No product found in super");
            for (int i=0; i<10; i++)
            for (int j = 0; j < 10; j++)
            {
                if (matrix[i, j].AddProduct(product, request.Quantity))
                {
                    return new AllocationResponse()
                    {
                        FoundCell = true,
                        Cell = i.ToString() + "," + j.ToString()
                    };
                }
            }

            return new AllocationResponse()
            {
                FoundCell = false
            };
        }
    }
}
