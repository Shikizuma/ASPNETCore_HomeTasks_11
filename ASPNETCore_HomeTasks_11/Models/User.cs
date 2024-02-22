using System;
using System.Collections.Generic;

namespace ASPNETCore_HomeTasks_11.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Login { get; set; } = null!;

    public string? Name { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
