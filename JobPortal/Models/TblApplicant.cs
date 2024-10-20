using System;
using System.Collections.Generic;

namespace JobPortal.Models;

public partial class TblApplicant
{
    public int ICandidateId { get; set; }

    public int IJobId { get; set; }

    public string? SIntroduction { get; set; }

    public string? SCv { get; set; }

    public string? SStatus { get; set; }

    public virtual TblCandidate ICandidate { get; set; } = null!;

    public virtual TblJob IJob { get; set; } = null!;
}
