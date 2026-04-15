using System;
using System.Collections.Generic;

namespace BussinessObjects.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UserId { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? Discount { get; set; }

    public decimal? FinalAmount { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual User? User { get; set; }
}
