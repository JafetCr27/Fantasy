using CurrieTechnologies.Razor.SweetAlert2;
using Fantasy.Frondend.Repositories;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Frondend.Pages.Countries;

public partial class CountryCreate
{
    private CountryForm? countryForm;
    private Country country = new();

    [Inject] private IRepository Repository { get; set; } = null;
    [Inject] private NavigationManager NavigationManager { get; set; } = null;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("api/Coutries", country);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessaggeAsync();
            await SweetAlertService.FireAsync(Localizer["Error"], Localizer[message!]);
            return;
        }
        Return();
        var toast = SweetAlertService.Mixin(new SweetAlertOptions
        {
            Toast = true,
            Position = SweetAlertPosition.BottomEnd,
            ShowConfirmButton = true,
            Timer = 3000
        });
        await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Creado con �xito");
    }

    private void Return()
    {
        countryForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/countries");
    }
}