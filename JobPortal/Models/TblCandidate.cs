using System;
using System.Collections.Generic;

namespace JobPortal.Models;

public partial class TblCandidate
{
    public int ICandidateId { get; set; }

    public string? SAvt { get; set; }

    public string SName { get; set; } = null!;

    public string SEmail { get; set; } = null!;

    public string? SPhone { get; set; }

    public double? FExperience { get; set; }

    public string? SIndustry { get; set; }

    public string? SAddress { get; set; }

    public string? SDetail { get; set; }

    public virtual ICollection<TblApplicant> TblApplicants { get; set; } = new List<TblApplicant>();
}
