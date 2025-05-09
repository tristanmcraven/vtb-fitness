using System;
using System.Collections.Generic;

namespace vtb_fitness_client.Model;

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

    public int? TrainerId { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual User? Trainer { get; set; }

    public virtual User User { get; set; } = null!;
}
