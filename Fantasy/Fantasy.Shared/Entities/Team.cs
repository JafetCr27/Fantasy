﻿using System.ComponentModel.DataAnnotations;
namespace Fantasy.Shared.Entities;

public class Team
{
    public int Id { get; set; }
    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Image { get; set; }

    public Country Country { get; set; } = null!;

    public int CountryId { get; set; }
}
