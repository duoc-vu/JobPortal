using System;
using System.Collections.Generic;

namespace JobPortal.Models;

public partial class TblJob
{
    public int IJobId { get; set; }

    public string? SAvt { get; set; }

    public string SJobName { get; set; } = null!;

    public double? FSalary { get; set; }

    public string? SDetail { get; set; }

    public DateTime DTime { get; set; }

    public string? SStatus { get; set; }

    public int? IEmployerId { get; set; }

    public virtual TblEmployer? IEmployer { get; set; }

    public virtual ICollection<TblApplicant> TblApplicants { get; set; } = new List<TblApplicant>();
}
