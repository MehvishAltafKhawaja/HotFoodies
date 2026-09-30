using System;
using System.Collections.Generic;

namespace ASPNETMVC.Models;

public partial class District
{
    public int DistrictId { get; set; }

    public string? DistrictName { get; set; }

    public int? StateId { get; set; }

    public virtual State? State { get; set; }
}
