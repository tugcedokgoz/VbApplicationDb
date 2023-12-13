using System;
using System.Collections.Generic;

namespace VbApplication.Model.Models;

public partial class LegalPerson
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string CompanyName { get; set; } = null!;

    public string TaxNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Fax { get; set; } = null!;

    public long UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
