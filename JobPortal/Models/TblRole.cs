using System;
using System.Collections.Generic;

namespace JobPortal.Models;

public partial class TblRole
{
    public int IRoleId { get; set; }

    public string SRoleName { get; set; } = null!;

    public virtual ICollection<TblAccount> TblAccounts { get; set; } = new List<TblAccount>();
}
