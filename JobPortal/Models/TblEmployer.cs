using System;
using System.Collections.Generic;

namespace JobPortal.Models;

public partial class TblEmployer
{
    public int IEmployerId { get; set; }

    public string? SAvt { get; set; }

    public string SName { get; set; } = null!;

    public string SEmail { get; set; } = null!;

    public double? FCompanySize { get; set; }

    public string? SPhone { get; set; }

    public string? SCompanyField { get; set; }

    public string? SAddress { get; set; }

    public string? SDetail { get; set; }

    public virtual ICollection<TblJob> TblJobs { get; set; } = new List<TblJob>();
}
