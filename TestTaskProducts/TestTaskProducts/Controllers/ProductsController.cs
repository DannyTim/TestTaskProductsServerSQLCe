using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using TestTaskProducts.Models;
using TestTaskProducts.Services;

namespace TestTaskProducts.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpPost]
        [Route("api/products/createtable")]
        public string CreateProductsTable()
        {
            try
            {
                DBService dbs = new DBService();
                dbs.CreateProductsTable();
                return "Table was successfully created";
            }
            catch (Exception e)
            {
                return "Table already exists";
            }
        }

        [HttpDelete]
        [Route("api/products/droptable")]
        public string DropProductsTable()
        {
            try
            {
                DBService dbs = new DBService();
                dbs.DropProductsTable();
                return "Table was successfully dropped";
            }
            catch (Exception e)
            {
                return "Table already dropped";
            }
        }

        [HttpPost]
        [Route("api/products/add")]
        public Product AddProduct(Product product)
        {
            try
            {
                product.Id = Guid.NewGuid();
                DBService dbs = new DBService();
                dbs.AddProduct(product);
                return product;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpDelete]
        [Route("api/products/delete/{id}")]
        public string DeleteProduct([FromUri]Guid id)
        {
            try
            {
                DBService dbs = new DBService();
                dbs.DeleteProduct(id);
                return "Product was successfully deleted";
            }
            catch (Exception e)
            {
                return "Current product doesn't exist";
            }
        }

        [HttpPut]
        [Route("api/products/update")]
        public string UpdateProduct(Product product)
        {
            try
            {
                DBService dbs = new DBService();
                dbs.UpdateProduct(product.Id, product.Denomination, product.Price, product.Quantity);
                return "Product was successfully updated";
            }
            catch (Exception e)
            {
                return "Current product doesn't exist";
            }
        }

        [HttpGet]
        [Route("api/products/get/{id}")]
        public Product GetProduct([FromUri]Guid id)
        {
            try
            {
                DBService dbs = new DBService();
                var product = dbs.GetProduct(id);
                return product;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("api/products/getall")]
        public List<Product> GetAllProducts()
        {
            try
            {
                DBService dbs = new DBService();
                var products = dbs.GetAllProducts();
                return products;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpDelete]
        [Route("api/products/deleteall")]
        public string DeleteAllProducts()
        {
            try
            {
                DBService dbs = new DBService();
                dbs.DeleteAllProducts();
                return "Products were successfully deleted";
            }
            catch (Exception e)
            {
                return "Exception";
            }
        }
    }
}
