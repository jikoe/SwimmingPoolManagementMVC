using BussinessObjects.Models;

using DataAccessObjects;

namespace Repositories.Impl
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetAll() => ProductDAO.Instance.GetProducts();
        public Product GetById(int id) => ProductDAO.Instance.GetProductById(id);
        public void Save(Product p) => ProductDAO.Instance.AddProduct(p);
        public void Update(Product p) => ProductDAO.Instance.UpdateProduct(p);
        public void Delete(int id) => ProductDAO.Instance.DeleteProduct(id);
    }
}