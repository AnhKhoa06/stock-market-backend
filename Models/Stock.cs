using System;
using System.Collections.Generic;

namespace StockMarketAPI.Models;

public partial class Stock
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal PreviousPrice { get; set; }

    public string Exchange { get; set; } = null!;

    public bool? Favorite { get; set; }

    public DateTime? CreatedAt { get; set; }
}
