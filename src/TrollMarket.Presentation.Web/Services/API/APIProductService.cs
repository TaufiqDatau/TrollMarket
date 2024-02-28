using System.Runtime.InteropServices;
using TrollMarket.Business.Interfaces;
using TrollMarket.Presentation.Web.HelperClass;
using TrollMarket.Presentation.Web.Models.DTO;

namespace TrollMarket.Presentation.Web.Services.API
{
    public class APIProductService
    {
        private readonly IProductRepository _repository;

        public APIProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        
        public void DiscontinueProduct(int Id)
        {
            _repository.DiscontinueProduct(Id);
        }
        public ProductDetailDTO GetProductDetail(int id)
        {
            var product = _repository.GetProductDetailById(id);
            var dto = product.ConvertToDTO();
            return dto;
        }
    }
}
