namespace InMemoryCache
{
    public class CacheKey
    {
        public static string ProductDetails(int productId) => $"ProductDetails - {productId}";
        public static string StockInfo(int productId) => $"StockInfo - {productId}";
        public static string DependentCancellationTokenSource => "DependentCancellationTokenSource";
    }
}
