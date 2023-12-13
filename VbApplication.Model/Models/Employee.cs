using System;
using System.Collections.Generic;

namespace VbApplication.Model.Models;

public partial class Employee
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string Department { get; set; } = null!;

    public string Position { get; set; } = null!;

    public decimal Salary { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
