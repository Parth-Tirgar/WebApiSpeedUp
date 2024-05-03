using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace InMemoryCache
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IProductService _productService;
        private readonly IStockInfoService _stockInfoService;

        public ProductController(IMemoryCache cache, IProductService productService, IStockInfoService stockInfoService)
        {
            _cache = cache;
            _productService = productService;
            _stockInfoService = stockInfoService;
        }

        // Now let's implement cache for this API

        [HttpGet("products/{productId}")]
        public async Task<IActionResult> GetProductAsync(int productId)
        {
            var result = new object();

            try
            {
                var cancellationTokenSource = _cache.Get<CancellationTokenSource>(CacheKey.DependentCancellationTokenSource);

                if (cancellationTokenSource == null)
                {
                    cancellationTokenSource = new CancellationTokenSource();
                    _cache.Set(CacheKey.DependentCancellationTokenSource, cancellationTokenSource);
                }

                // Get Product Details async and save in cache
                var productDetailsTask = _cache.GetOrCreateAsync(CacheKey.ProductDetails(productId), async cacheEntry =>
                {
                    cacheEntry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(10);
                    return await _productService.GetProductDetailsAsync(productId);
                });

                // Same for stockInfo
                var stockInfoTask = _cache.GetOrCreateAsync(CacheKey.StockInfo(productId), async cacheEntry =>
                {
                    cacheEntry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(10);
                    cacheEntry.AddExpirationToken(new CancellationChangeToken(cancellationTokenSource.Token));
                    return await _stockInfoService.GetStockInfoAsyc(productId);
                });


                var productDetails = await productDetailsTask;
                var stockInfo = await stockInfoTask;
                var APIStatus = 200;

                result = new { productDetails, stockInfo, APIStatus };

            }
            catch (Exception ex)
            {
                var APIStatus = 400;
                string error = ex.Message;
                result = new { error, APIStatus };
            }

            return Ok(result);

        }
    }
}
