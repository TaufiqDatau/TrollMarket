using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Models;

public partial class AccountRole
{
    public string Username { get; set; } = null!;

    public RoleEnum Role { get; set; } 

    public virtual Account UsernameNavigation { get; set; } = null!;
}
