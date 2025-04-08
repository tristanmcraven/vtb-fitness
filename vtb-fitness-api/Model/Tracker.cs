using System;
using System.Collections.Generic;

namespace vtb_fitness_api.Model;

public partial class Tracker
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ExerciseId { get; set; }

    public int? Sits { get; set; }

    public int? Reps { get; set; }

    public int? Meters { get; set; }

    public DateTime Timestamp { get; set; }

    public int? Weight { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
