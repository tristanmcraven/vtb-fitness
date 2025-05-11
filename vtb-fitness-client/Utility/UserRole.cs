using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vtb_fitness_client.Utility
{
    public enum UserRole
    {
        Admin = 1,
        Moderator = 2,
        Client = 3,
        Trainer = 4
    }

    public record ViewVisibilityRule(string ViewName, Func<UserRole, bool> ShouldBeVisible);
}
