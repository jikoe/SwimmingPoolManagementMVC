using System;
using System.Collections.Generic;

namespace BussinessObjects.Models;

public partial class InvoiceDetail
{
    public int InvoiceDetailId { get; set; }

    public int? InvoiceId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? SubTotal { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual Product? Product { get; set; }
}
