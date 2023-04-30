using System.ComponentModel.DataAnnotations;

namespace SuperErp.Sales.Model
{
    public class UpdateProduct
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
