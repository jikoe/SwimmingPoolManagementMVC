using System;
using System.Collections.Generic;

namespace BussinessObjects.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int? CustomerId { get; set; }

    public int? TicketTypeId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool? IsUsed { get; set; }

    public virtual User? Customer { get; set; }

    public virtual TicketType? TicketType { get; set; }

    public virtual ICollection<TicketUsage> TicketUsages { get; set; } = new List<TicketUsage>();
}
