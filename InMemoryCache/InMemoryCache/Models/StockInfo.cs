namespace InMemoryCache
{
    public class StockInfo
    {
        public int StockId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public string StockName { get; set; } = string.Empty;
        public float Price { get; set; } = 0f;
        public float Percentage { get; set; } = 0f;
        public string Status { get; set; } = string.Empty;
    }
}
