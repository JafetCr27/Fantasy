using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
namespace Fantasy.Frondend.Pages;

public partial class Home
{
    [Inject] public IStringLocalizer<Literals> Localizer { get; set; } = null!;
}