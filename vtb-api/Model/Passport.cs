using System;
using System.Collections.Generic;

namespace vtb_api.Model;

public partial class Passport
{
    public int Id { get; set; }

    public string Series { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string IssuedBy { get; set; } = null!;

    public DateOnly IssuedDate { get; set; }

    public string UnitCode { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string BirthPlace { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
