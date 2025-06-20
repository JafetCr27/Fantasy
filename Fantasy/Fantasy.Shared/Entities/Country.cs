﻿using Fantasy.Shared.Resources;
using System.ComponentModel.DataAnnotations;
namespace Fantasy.Shared.Entities;
public class Country
{
    public int Id { get; set; }
    [Display(Name = "Country", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string Name { get; set; } = string.Empty;

    public ICollection<Team>? Teams { get; set; }

    public int TeamsCount => Teams == null ? 0 : Teams.Count;
}