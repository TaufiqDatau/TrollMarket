using TrollMarket.Business.Interfaces;
using TrollMarket.Presentation.Web.HelperClass;
using TrollMarket.Presentation.Web.Models.DTO;

namespace TrollMarket.Presentation.Web.Services.API
{
    public class APIShipmentService
    {
        private readonly IShipmentRepository _repository;
        public APIShipmentService(IShipmentRepository repository)
        {
            _repository = repository;
        }
        public ShipmentDTO GetShipmentForEdit(int id)
        {
            if (_repository.CheckDependance(id))
            {
                throw new InvalidOperationException("This shipment is already in transaction you can't edit this");
            }
            var shipment = _repository.GetShipment(id);
            return shipment.ConvertToDTO();
        }
        public void EditShipment(ShipmentDTO dto)
        {
            if(_repository.CheckDependance(dto.ShipmentId))
            {
                throw new InvalidOperationException("This shipment is already in transaction you can't edit this");
            }
            var shipment = dto.ConvertToEntity();
            _repository.EditShipmentCompany(shipment);

        }
        public void InsertNewShipment(ShipmentDTO dto)
        {
            var newShipmentData = dto.ConvertToEntity();
            _repository.NewShipmentCompany(newShipmentData);
        }
        public void StopShipmentService(int id)
        {
            var shipment = _repository.GetShipment(id);
            if(shipment.ServiceStopDate.HasValue) {
                throw new InvalidOperationException("The shipment service already stopped");            
            }
            _repository.StopServiceShipment(shipment);
        }
        public void DeleteShipment(int id)
        {
            if (_repository.CheckDependance(id))
            {
                throw new InvalidOperationException("You cannot delete this shipment since it already in transaction");
            }
            var shipment=_repository.GetShipment(id);
            _repository.DeleteShipmentCompany(shipment);
        }
    }
}
