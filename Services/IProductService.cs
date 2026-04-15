using BussinessObjects.Models;

namespace Services
{
    public interface IProductService
    {
        List<Product> GetActiveProducts();
        Product GetDetails(int id);
        void CreateNewProduct(Product p);
        void UpdateInfo(Product p);
        void RemoveProduct(int id);
    }
}