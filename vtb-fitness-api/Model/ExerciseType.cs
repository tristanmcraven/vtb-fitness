using System;
using System.Collections.Generic;

namespace vtb_fitness_api.Model;

public partial class ExerciseType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
