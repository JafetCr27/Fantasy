using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Frondend.Shared;

public partial class GenericList<Titem>
{
    [Inject] public IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Parameter] public RenderFragment? Loading { get; set; }
    [Parameter] public RenderFragment? NoRecords { get; set; }
    [EditorRequired, Parameter] public RenderFragment Body { get; set; }
    [EditorRequired, Parameter] public List<Titem> MyList { get; set; }
}