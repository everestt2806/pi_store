using pi_store.Models;
using pi_store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pi_store.DataAccess;

namespace pi_store.Controllers
{
    public class ProductController
    {
        private ProductDAO productDAO;

        public ProductController()
        {
            productDAO = new ProductDAO();
        }

        public List<Product> GetAllProducts()
        {
            return productDAO.GetAllProducts();
        }

        public Product GetProductById(string id)
        {
            return productDAO.GetProductById(id);
        }

        public Product GetProductByName(string name) { 
            return productDAO.GetProductByName(name);
        }

        public void AddProduct(Product product)
        {
            productDAO.AddProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            productDAO.UpdateProduct(product);
        }

        public void DeleteProduct(string id)
        {
            productDAO.DeleteProduct(id);
        }

        public List<Product> SearchProducts(string searchString)
        {
            return productDAO.SearchProducts(searchString);
        }
    }
}
