using System;
using System.Collections.Generic;

namespace vtb_api.Model;

public partial class UserTariff
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? TariffId { get; set; }

    public DateTime? AcquiredAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public double? MoneyPaid { get; set; }

    public virtual Tariff? Tariff { get; set; }

    public virtual User? User { get; set; }
}
