using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Interfaces
{
    public interface IProductRepository
    {
        public PaginationResponse<Product> GetProductPaginationResponse(string name, string category, string description, int currentPage, int pageSize);
        public PaginationResponse<Product> GetMerchandisePaginationResponse(string seller, int currentPage, int pageSize);
        public Product GetProductDetailById(int id);
        public void AddProduct(Product newProduct);
        public void EditProduct(Product editedProduct);
        public void DeleteProduct(Product targetProduct);
        public void DiscontinueProduct(int productId);
        public bool CheckDependenciesProduct(int id);

    }
}
