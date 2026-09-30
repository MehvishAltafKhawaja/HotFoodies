using System;
using System.Collections.Generic;

namespace ASPNETMVC.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string? EmpName { get; set; }

    public string? EmpGender { get; set; }

    public DateOnly? EmpDob { get; set; }
    public int? DistrictId { get; set; }
    public int? StateId { get; set; }

    public virtual State? State { get; set; }
    public virtual District? District { get; set; }
}
