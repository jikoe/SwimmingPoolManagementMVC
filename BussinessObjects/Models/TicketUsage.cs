using System;
using System.Collections.Generic;

namespace BussinessObjects.Models;

public partial class TicketUsage
{
    public int UsageId { get; set; }

    public int? TicketId { get; set; }

    public DateOnly? UseDate { get; set; }

    public string? Shift { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
