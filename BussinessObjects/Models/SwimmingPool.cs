using System;
using System.Collections.Generic;

namespace BussinessObjects.Models;

public partial class SwimmingPool
{
    public int PoolId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public TimeOnly? OpenTime { get; set; }

    public TimeOnly? CloseTime { get; set; }
}
