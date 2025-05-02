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

    public List<string>? Pros { get; set; }

    public TimeOnly? GymStartTime { get; set; }

    public TimeOnly? GymEndTime { get; set; }

    public TimeOnly? PoolStartTime { get; set; }

    public TimeOnly? PoolEndTime { get; set; }

    public TimeOnly? HammamStartTime { get; set; }

    public TimeOnly? HammamEndTime { get; set; }

    public int? TrainerWorkoutsPerWeek { get; set; }

    public virtual ICollection<UserTariff> UserTariffs { get; set; } = new List<UserTariff>();
}
