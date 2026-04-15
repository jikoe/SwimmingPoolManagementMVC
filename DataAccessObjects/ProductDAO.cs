
using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null) instance = new ProductDAO();
                    return instance;
                }
            }
        }

        public List<Product> GetProducts()
        {
            using var context = new SwimmingPoolManagementFinalContext();
            return context.Products.Include(p => p.Category).ToList();
        }

        public Product GetProductById(int id)
        {
            using var context = new SwimmingPoolManagementFinalContext();
            return context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
        }

        public void AddProduct(Product p)
        {
            using var context = new SwimmingPoolManagementFinalContext();
            context.Products.Add(p);
            context.SaveChanges();
        }

        public void UpdateProduct(Product p)
        {
            using var context = new SwimmingPoolManagementFinalContext();
            context.Entry(p).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            using var context = new SwimmingPoolManagementFinalContext();
            var p = context.Products.Find(id);
            if (p != null)
            {
                context.Products.Remove(p);
                context.SaveChanges();
            }
        }
    }
}