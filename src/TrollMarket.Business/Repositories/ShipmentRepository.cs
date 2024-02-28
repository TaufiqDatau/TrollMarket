using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly TrollMarketContext _context;
        public ShipmentRepository(TrollMarketContext context)
        {
            _context = context;
        }
        public List<Shipment> GetAllShipment()
        {
            return _context.Shipments.Where(s=>s.ServiceStopDate==null).ToList();
        }
        public PaginationResponse<Shipment> GetShipmentPaginationResponse(int currentPage, int pageSize){
            var query = _context.Shipments.ToList();
            return new PaginationResponse<Shipment>
            {
                Data = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(),
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItems = query.Count()

            };
        }
        public bool CheckDependance(int shipmentId)
        {
            return _context.Carts.Any(c=>c.ShipmentId==shipmentId) || _context.Orders.Any(o=> o.ShipmentId==shipmentId);
        }
        public Shipment GetShipment(int shipmentId)
        {
            return _context.Shipments
                .FirstOrDefault(s=>s.Id==shipmentId)
                ??
                throw new KeyNotFoundException("Shipment not found");
        }
        public void NewShipmentCompany(Shipment newCompany)
        {
            try
            {
                _context.Shipments.Add(newCompany);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new ApplicationException("Terjadi masalah saat mengunggah ke database");
            }
        }
        public void EditShipmentCompany(Shipment editedCompany)
        {
            try
            {
                _context.Shipments.Update(editedCompany);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new ApplicationException("Terjadi masalah saat mengedit ke database");
            }
        }
        public void DeleteShipmentCompany(Shipment company)
        {
            try
            {
                _context.Shipments.Remove(company);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new ApplicationException("Error when deleting the database");
            }
        }
        public void StopServiceShipment(Shipment shipment)
        {
            if (shipment.ServiceStopDate.HasValue)
            {
                throw new InvalidOperationException("Shipment already stopped.");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    shipment.ServiceStopDate = DateTime.Now;
                    _context.Shipments.Update(shipment);

                    var cartData = _context.Carts.Where(cart => cart.ShipmentId == shipment.Id);

                    _context.Carts.RemoveRange(cartData);

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException("Error stopping shipment.", ex);
                }
            }
        }

    }
}
