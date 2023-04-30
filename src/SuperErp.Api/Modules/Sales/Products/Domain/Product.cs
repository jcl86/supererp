using SuperErp.Core;

namespace SuperErp.Sales.Domain
{
    public class Product : Entity<string>
    {
        public string Code { get; private set; }
        public void UpdateCode(string value) => Code = value;

        public string Name { get; private set; }
        public void UpdateName(string value) => Name = value; 

        public string Description { get; private set; }
        public void UpdateDescription(string value) => Description = value; 

        public decimal Price { get; private set; }
        public void UpdatePrice(decimal value) => Price = value; 

        public Product(string code, string name, string description, decimal price) : base(Guid.NewGuid().ToString())
        {
            UpdateCode(code);
            UpdateName(name);
            UpdateDescription(description);
            UpdatePrice(price);
        }

        public override string ToString() => Name;
    }

}
