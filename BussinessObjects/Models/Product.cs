using System;
using System.Collections.Generic;

namespace BussinessObjects.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public int? CategoryId { get; set; }

    public bool? IsRentable { get; set; }

    public decimal? RentalPrice { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
