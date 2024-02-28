using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Seller { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public DateTime? DiscontinueDate { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Account SellerNavigation { get; set; } = null!;
}
