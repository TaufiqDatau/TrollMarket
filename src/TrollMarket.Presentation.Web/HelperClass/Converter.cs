using TrollMarket.Business;
using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.Models.DTO;
using TrollMarket.Presentation.Web.Models.ViewModel.Merchandise;

namespace TrollMarket.Presentation.Web.HelperClass
{
    public static class Converter
    {
        public static PaginationInfoViewModel ConvertToViewModel<T>(PaginationResponse<T> paginationResponse)
        {
            if (paginationResponse == null)
            {
                throw new ArgumentNullException(nameof(paginationResponse));
            }

            return new PaginationInfoViewModel
            {
                CurrentPage = paginationResponse.CurrentPage,
                TotalItem = paginationResponse.TotalItems,
                PageSize = paginationResponse.PageSize
            };
        }
        public static Product ConvertToEntity(this MerchandiseUpsertViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }
            return new Product
            {
                Id = viewModel.ProductId,
                Seller = viewModel.Seller,
                Name = viewModel.Name,
                Category = viewModel.Category,
                Description = viewModel.Description,
                Price = viewModel.Price,
                DiscontinueDate = viewModel.Discontinue ? DateTime.Now : null
            };
        }
        public static Shipment ConvertToEntity(this ShipmentDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof (dto));
            }
            return new Shipment
            {
                Id= dto.ShipmentId,
                Name= dto.Name,
                Price= dto.Price,
                ServiceStopDate= dto.ServiceStatus ? DateTime.Now : null
            };
        }
        public static Cart ConvertToEntity(this  CartDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException (nameof (dto));
            }
            return new Cart
            {
                Buyer = dto.Buyer,
                ProductId = dto.ProductId,
                ShipmentId = dto.ShipmentId,
                Quantity = dto.Quantity
            };
        }
        public static MerchandiseUpsertViewModel ConvertToViewModel(this  Product product)
        {
            if(product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return new MerchandiseUpsertViewModel
            {
                ProductId = product.Id,
                Seller = product.Seller,
                Name = product.Name,
                Category = product.Category,
                Description = product.Description,
                Discontinue = product.DiscontinueDate.HasValue,
                Price = product.Price,
            };
        }
        public static ProductDetailDTO ConvertToDTO(this Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return new ProductDetailDTO
            {
                Name = product.Name,
                Category = product.Category,
                Description = product.Description,
                SellerName = product.SellerNavigation!.Name,
                Discontinue=product.DiscontinueDate.HasValue,
                Price=product.Price.ToString("c")
            };
        }
        public static ShipmentDTO ConvertToDTO(this Shipment shipment)
        {
            if (shipment == null)
            {
                throw new ArgumentNullException(nameof(shipment));
            }
            return new ShipmentDTO
            {
                ShipmentId= shipment.Id,
                Name = shipment.Name,
                Price = shipment.Price,
                ServiceStatus = shipment.ServiceStopDate.HasValue
            };
        }


    }
}
