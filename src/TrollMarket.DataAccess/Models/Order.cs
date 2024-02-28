using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Models;

public partial class Order
{
    public int Id { get; set; }

    public string Buyer { get; set; } = null!;

    public int ProductId { get; set; }

    public int ShipmentId { get; set; }

    public DateTime TransactionDate { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal ShipmentPrice { get; set; }

    public int Quantity { get; set; }

    public virtual Account BuyerNavigation { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Shipment Shipment { get; set; } = null!;
}
