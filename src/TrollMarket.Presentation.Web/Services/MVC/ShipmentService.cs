using TrollMarket.Business.Interfaces;
using TrollMarket.Presentation.Web.HelperClass;
using TrollMarket.Presentation.Web.Models.ViewModel.Shipment;

namespace TrollMarket.Presentation.Web.Services.MVC
{
    public class ShipmentService
    {
        private readonly IShipmentRepository _repository;
        public ShipmentService(IShipmentRepository repository)
        {
            _repository = repository;
        }
        public ShipmentIndexViewModel GetShipmentIndexViewModel(int currentPage, int pageSize)
        {
            var paginationResponse = _repository.GetShipmentPaginationResponse(currentPage, pageSize);
            var table = paginationResponse.Data.Select(x => new ShipmentTableViewModel
            {
                ShipmentId = x.Id,
                Name = x.Name,
                Price = x.Price.ToString("c"),
                ServiceStatus = x.ServiceStopDate.HasValue ? "No" : "Yes"

            });
            return new ShipmentIndexViewModel
            {
                ShipmentTable = table.ToList(),
                PaginationInfo = Converter.ConvertToViewModel(paginationResponse)
            };
        }
    }
}
