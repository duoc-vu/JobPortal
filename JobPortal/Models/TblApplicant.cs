using System;
using System.Collections.Generic;

namespace JobPortal.Models;

public partial class TblApplicant
{
    public int IUserId { get; set; }

    public int IJobId { get; set; }

    public string? SIntroduction { get; set; }

    public string? SCv { get; set; }

    public string? SStatus { get; set; }

    public virtual TblJob IJob { get; set; } = null!;

    public virtual TblCandidate IUser { get; set; } = null!;
}
