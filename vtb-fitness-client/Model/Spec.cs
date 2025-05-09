using System;
using System.Collections.Generic;

namespace vtb_fitness_client.Model;

public partial class Spec
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<User> Trainers { get; set; } = new List<User>();
}
