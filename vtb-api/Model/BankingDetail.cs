using System;
using System.Collections.Generic;

namespace vtb_api.Model;

public partial class BankingDetail
{
    public int Id { get; set; }

    public string? CurrentAccount { get; set; }

    public string? Inn { get; set; }

    public string? Kpp { get; set; }

    public string? Bik { get; set; }

    public string? BankName { get; set; }

    public string? CorrespondentAccount { get; set; }

    public string? DebitCardNumber { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
