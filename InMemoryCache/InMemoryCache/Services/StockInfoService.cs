namespace InMemoryCache
{
    public class StockInfoService : IStockInfoService
    {
        // Here You have to write code to fetch data from DataBase

        public async Task<StockInfo> GetStockInfoAsyc(int productId)
        {
            var data = new StockInfo
            {
                StockId = 1,
                ProductId = productId,
                StockName = "Parle-G",
                Price = 1000.56f,
                Percentage = 0.26f,
                Status = "Up"
            };
            return (data);
        }
    }
}
