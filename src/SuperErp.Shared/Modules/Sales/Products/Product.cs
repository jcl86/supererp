using System;

namespace SuperErp.Sales.Model
{
    public class Product
    {
        public string Id { get;  set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
