
using BussinessObjects.Models;
using Repositories;
using Repositories.Impl;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService()
        {
            _repo = new ProductRepository();
        }

        public List<Product> GetActiveProducts()
        {
            // Có thể thêm logic: chỉ lấy sản phẩm còn hàng (Quantity > 0)
            return _repo.GetAll();
        }

        public Product GetDetails(int id) => _repo.GetById(id);

        public void CreateNewProduct(Product p) => _repo.Save(p);

        public void UpdateInfo(Product p) => _repo.Update(p);

        public void RemoveProduct(int id) => _repo.Delete(id);
    }
}