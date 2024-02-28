using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.HelperClass;
using TrollMarket.Presentation.Web.Models.DTO;
using TrollMarket.Presentation.Web.Models.ViewModel.Merchandise;
using TrollMarket.Presentation.Web.Models.ViewModel.Shop;

namespace TrollMarket.Presentation.Web.Services.MVC
{
    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICartRepository _cartRepository;
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repository, ICartRepository cartRepository, IShipmentRepository shipmentRepository,IMapper mapper)
        {
            _repository = repository;
            _cartRepository = cartRepository;
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
        }
        //ShopSection
        public ShopIndexViewModel GetShopIndexViewModel(string name, string category, string description, int currentPage, int pageSize)
        {
            var paginationResponse = _repository.GetProductPaginationResponse(name, category, description, currentPage, pageSize);
            var vm = new ShopIndexViewModel
            {
                Name = name,
                Category = category,
                Description = description,
                SelectShipment = _shipmentRepository.GetAllShipment()
                                .Select(s => new SelectListItem
                                {
                                    Value = s.Id.ToString(),
                                    Text = s.Name,
                                }).ToList(),
                ShopTable = (paginationResponse.Data
                            .Select(p => new ShopTableViewModel
                            {
                                ProductId = p.Id,
                                SellerName=p.SellerNavigation.Name,
                                ProductName = p.Name,
                                Price = p.Price.ToString("c")


                            })).ToList(),
                PaginationInfo = Converter.ConvertToViewModel(paginationResponse)
            };
            return vm;
        }
        public void AddToCart(CartDTO dto)
        {
            var newCart = _mapper.Map<Cart>(dto);
            if (_cartRepository.CheckCartAlreadyExist(newCart))
            {
                _cartRepository.UpdateCartQuantity(newCart);
            }
            else
            {
                _cartRepository.AddToCart(newCart);
            }
        }
        //Merchandise Section
        public MerchandiseIndexViewModel GetIndexViewModel(string seller, int currentPage, int pageSize)
        {
            var paginationResponse = _repository.GetMerchandisePaginationResponse(seller, currentPage, pageSize);
            return new MerchandiseIndexViewModel
            {
                Merchandises = (from merch in paginationResponse.Data
                                select new MerchandiseTableViewModel
                                {
                                    ProductId = merch.Id,
                                    ProductName = merch.Name,
                                    Category = merch.Category,
                                    DiscontinuedDate = merch.DiscontinueDate
                                }).ToList(),
                PaginationInfo = Converter.ConvertToViewModel(paginationResponse)
            };
        }
        public void InsertNewProduct(MerchandiseUpsertViewModel vm)
        {
            var product = vm.ConvertToEntity();
            _repository.AddProduct(product);
        }
        public MerchandiseUpsertViewModel GetProductById(int id)
        {
            var vm = _repository.GetProductDetailById(id).ConvertToViewModel();
            return vm;
        }
        public void EditProduct(MerchandiseUpsertViewModel vm)
        {
            var product = vm.ConvertToEntity();
            _repository.EditProduct(product);
        }
        public void DeleteProduct(int id)
        {
            var product = _repository.GetProductDetailById(id);
            _repository.DeleteProduct(product);
        }
        public bool CheckDependencies(int id)
        {
            return _repository.CheckDependenciesProduct(id);
        }
    }
}
