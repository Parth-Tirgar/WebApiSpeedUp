namespace InMemoryCache
{
    public interface IStockInfoService
    {
        Task<StockInfo> GetStockInfoAsyc(int productId);
    }
}
