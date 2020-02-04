using System;

namespace TestTaskProducts.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Denomination { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}