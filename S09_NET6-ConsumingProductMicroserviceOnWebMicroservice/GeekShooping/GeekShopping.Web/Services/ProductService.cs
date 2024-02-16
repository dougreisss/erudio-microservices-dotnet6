using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;

        public const string basePath = "api/v1/product";

        public ProductService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            var response = await _client.GetAsync(basePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindProductById(long id)
        {
            var response = await _client.GetAsync($"{basePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
           var response = await _client.PostAsJson(basePath, model);

           if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong when calling API");

           return await response.ReadContentAs<ProductModel>();
        }
        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var response = await _client.PutAsJson(basePath, model);

            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong when calling API");

            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<bool> DeleteProductById(long id)
        {
            var response = await _client.DeleteAsync($"{basePath}/{id}");

            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong when calling API");

            return await response.ReadContentAs<bool>();
        }
    }
}
