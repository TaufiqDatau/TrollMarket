using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Repositories
{
    public class ProductRepository:IProductRepository
    {
        public readonly TrollMarketContext _context;
        public ProductRepository(TrollMarketContext context)
        {
            _context = context;
        }
        public PaginationResponse<Product> GetProductPaginationResponse(string name, string category, string description, int currentPage, int pageSize)
        {
            var query = (from product in _context.Products.Include(p=>p.SellerNavigation)
                        where (product.Name.ToLower().Contains(name.ToLower()) || String.IsNullOrEmpty(name))
                        && (product.Category.ToLower().Contains(category.ToLower()) || String.IsNullOrEmpty(category))
                        && (product.Description.ToLower().Contains(description.ToLower()) || String.IsNullOrEmpty(description))
                        && product.DiscontinueDate==null
                        select product).ToList();
            var totalItem = query.Count();
            var dataRetrieve = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResponse<Product>
            {
                Data = dataRetrieve,
                CurrentPage = currentPage,
                TotalItems=totalItem,
                PageSize=pageSize
            };


        }
        public PaginationResponse<Product> GetMerchandisePaginationResponse(string seller, int currentPage, int pageSize)
        {
            var query = from product in _context.Products
                        where product.Seller==seller
                        select product;

            var totalItem = query.Count();
            var dataRetrieve = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResponse<Product>
            {
                Data = dataRetrieve,
                CurrentPage = currentPage,
                TotalItems = totalItem,
                PageSize = pageSize
            };


        }
        public Product GetProductDetailById(int id)
        {
            return _context.Products.Include(p => p.SellerNavigation).FirstOrDefault(p => p.Id == id)
                        ?? throw new KeyNotFoundException($"Product with Id : {id} not found in the database");
        }
        public void AddProduct(Product newProduct)
        {
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }
        public void EditProduct(Product editedProduct)
        {
            try
            {
                _context.Products.Update(editedProduct);
                _context.SaveChanges();
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public void DeleteProduct(Product targetProduct)
        {
            try
            {
                _context.Products.Remove(targetProduct);
                _context.SaveChanges();
            }catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DiscontinueProduct(int productId)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == productId);
                if(product == null)
                {
                    throw new KeyNotFoundException("The Product with the Id is not found");
                }
                if (product.DiscontinueDate.HasValue)
                {
                    throw new InvalidOperationException("The product already discontinue");
                }
                try
                {
                    product.DiscontinueDate = DateTime.Now;
                    _context.Products.Update(product);

                    var cartData = _context.Carts.Where(c => c.ProductId == productId).ToList();
                    _context.Carts.RemoveRange(cartData);
                    _context.SaveChanges();
                    transaction.Commit();

                }catch(Exception ex)
                {
                    transaction.Rollback();
                    throw new ApplicationException("Error occurs in the database", ex);
                }


            }
        }
        public bool CheckDependenciesProduct(int id)
        {
            return _context.Carts.Any(c=>c.ProductId==id) || _context.Orders.Any(c=>c.ProductId==id); 
        }
    }
}
