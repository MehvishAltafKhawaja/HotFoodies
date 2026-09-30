using System;
using System.Collections.Generic;

namespace ASPNETMVC.Models;

public partial class State
{
    public int StateId { get; set; }

    public string? StateName { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
