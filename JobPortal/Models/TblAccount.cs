using System;
using System.Collections.Generic;

namespace JobPortal.Models;

public partial class TblAccount
{
    public int IUserId { get; set; }

    public string SUsername { get; set; } = null!;

    public string SPassword { get; set; } = null!;

    public int? IRoleId { get; set; }

    public virtual TblRole? IRole { get; set; }

    public virtual TblCandidate? TblCandidate { get; set; }

    public virtual TblEmployer? TblEmployer { get; set; }
}
