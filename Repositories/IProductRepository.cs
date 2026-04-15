using BussinessObjects.Models;

namespace Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(int id);
        void Save(Product p);
        void Update(Product p);
        void Delete(int id);
    }
}