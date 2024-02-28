using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Models;

public partial class Ledger
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public TransactionTypeEnum Type { get; set; } 
    public DateTime Timestamps { get; set; }

    public decimal? CurrentBalance { get; set; }

    public decimal? TransactionValue { get; set; }

    public decimal? FinalBalance { get; set; }

    public virtual Account UsernameNavigation { get; set; } = null!;
}
