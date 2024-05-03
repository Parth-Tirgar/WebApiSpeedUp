namespace InMemoryCache
{
    public class ProductService : IProductService
    {
        // Here You have to write code to fetch data from DataBase
        
        public async Task<ProductDetails> GetProductDetailsAsync(int productId)
        {
            var data = new ProductDetails 
            {
                ProductId = productId,
                ProductName = "Parle-G",
                ProductDescription = "Parle-G is a small, rectangular flat-baked sweet biscuit, manufactured and marketed by Parle Foods. It is filled with the goodness of milk and wheat, and the G apparently stands for Glucose and Genius since it is a source of strength for body and mind."
            };
            return (data);
        }
    }
}
