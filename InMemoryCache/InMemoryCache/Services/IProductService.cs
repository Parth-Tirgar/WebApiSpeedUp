namespace InMemoryCache
{
    public interface IProductService
    {
        Task<ProductDetails> GetProductDetailsAsync(int productId);
    }
}
