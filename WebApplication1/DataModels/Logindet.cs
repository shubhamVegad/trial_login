using System;
using System.Collections.Generic;

namespace WebApplication1.DataModels;

public partial class Logindet
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
