using System;
using System.Collections.Generic;

namespace VbApplication.Model.Models;

public partial class User
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? Active { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Intern> Interns { get; set; } = new List<Intern>();

    public virtual ICollection<LegalPerson> LegalPeople { get; set; } = new List<LegalPerson>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
