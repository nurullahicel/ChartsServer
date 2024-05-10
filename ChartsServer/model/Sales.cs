using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChartsServer.model;

public partial class Sales
{
    public int Id { get; set; }

    //[ForeignKey(nameof(Employee))]
    public int? EmployeeId { get; set; }

    public int? Price { get; set; }
}
