using System;
using System.Collections.Generic;

namespace vtb_fitness_api.Model;

public partial class User
{
    public int Id { get; set; }

    public string? Lastname { get; set; }

    public string? Name { get; set; }

    public string? Middlename { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public byte[]? Pfp { get; set; }

    public int? BankingDetailsId { get; set; }

    public int? PassportId { get; set; }

    public DateOnly? WorkingInVtbSince { get; set; }

    public virtual BankingDetail? BankingDetails { get; set; }

    public virtual Passport? Passport { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserTariff> UserTariffs { get; set; } = new List<UserTariff>();
}
