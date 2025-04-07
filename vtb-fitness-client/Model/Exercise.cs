using System;
using System.Collections.Generic;

namespace vtb_fitness_client.Model;

public partial class Exercise
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Tracker> Trackers { get; set; } = new List<Tracker>();
}
