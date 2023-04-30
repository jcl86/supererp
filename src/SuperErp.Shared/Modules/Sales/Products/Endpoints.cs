namespace SuperErp.Sales.Model
{
    public static partial class Endpoints
    {
        public static class Products
        {
            public const string Base = "api/products";

            public static string Get(string id) => $"{Base}/{id}";
            public static string GetAll = Base;
            public static string Create = Base;
            public static string Update(string id) => $"{Base}/{id}";
            public static string Delete(string id) => $"{Base}/{id}";
        }
    }
}
