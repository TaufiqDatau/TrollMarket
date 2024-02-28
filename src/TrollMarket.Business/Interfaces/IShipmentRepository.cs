using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Interfaces
{
    public interface IShipmentRepository
    {
        public List<Shipment> GetAllShipment();
        public PaginationResponse<Shipment> GetShipmentPaginationResponse(int currentPage, int pageSize);
        public bool CheckDependance(int shipmentId);
        public Shipment GetShipment(int shipmentId);
        public void NewShipmentCompany(Shipment newCompany);
        public void EditShipmentCompany(Shipment editedCompany);
        public void DeleteShipmentCompany(Shipment company);
        public void StopServiceShipment(Shipment shipment);
    }
}
