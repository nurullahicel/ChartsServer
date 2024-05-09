using System;
using System.Collections.Generic;

namespace ChartsServer.model;

public partial class Sale
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? Price { get; set; }
}
