using System;
using System.Collections.Generic;

namespace VbApplication.Model.Models;

public partial class Customer
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string Title { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
