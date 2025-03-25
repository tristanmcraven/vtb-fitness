using System;
using System.Collections.Generic;

namespace vtb_fitness_client.Model;

public partial class Tariff
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public double? Price { get; set; }

    public TimeSpan? Period { get; set; }

    public virtual ICollection<UserTariff> UserTariffs { get; set; } = new List<UserTariff>();
}
