using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public string? Grade { get; set; }
}
