using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPNETCore_HomeTasks_11.Models;

public partial class Message
{
    public Guid Id { get; set; }
    [Display(Name = "Назва тексту")]
    [MinLength(4)]
    [Required]
    public string NameText { get; set; } = null!;
    [Display(Name = "Текст")]
    [MinLength(15)]
    [Required]
    public string? Text { get; set; }

    public Guid IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
