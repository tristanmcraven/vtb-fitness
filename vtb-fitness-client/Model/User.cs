﻿using System;
using System.Collections.Generic;

namespace vtb_fitness_client.Model;

public partial class User
{
    public int Id { get; set; }

    public string? Lastname { get; set; }

    public string? Name { get; set; }

    public string? Middlename { get; set; }

    public string Login { get; set; } = null!;

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public byte[]? Pfp { get; set; }

    public int? BankingDetailsId { get; set; }

    public int? PassportId { get; set; }

    public DateOnly? WorkingInVtbSince { get; set; }

    public string? PasswordHash { get; set; }

    public int? TrainerId { get; set; }

    public virtual BankingDetail? BankingDetails { get; set; }

    public virtual ICollection<User> InverseTrainer { get; set; } = new List<User>();

    public virtual Passport? Passport { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<Tracker> TrackerTrainers { get; set; } = new List<Tracker>();

    public virtual ICollection<Tracker> TrackerUsers { get; set; } = new List<Tracker>();

    public virtual User? Trainer { get; set; }

    public virtual ICollection<UserTariff> UserTariffs { get; set; } = new List<UserTariff>();

    public virtual ICollection<Spec> Specs { get; set; } = new List<Spec>();
}
