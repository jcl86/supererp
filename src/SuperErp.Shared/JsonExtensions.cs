namespace System.Text.Json
{
    public static class JsonExtensions
    {
        public static string Serialize<TModel>(this TModel model)
        {
            return JsonSerializer.Serialize(model, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });
        }

        public static TModel Deserialize<TModel>(this string json)
        {
            return JsonSerializer.Deserialize<TModel>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });
        }
    }
}
