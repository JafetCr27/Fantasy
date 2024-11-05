namespace Fantasy.Frondend.Pages.Countries;

using CurrieTechnologies.Razor.SweetAlert2;
using Fantasy.Frondend.Repositories;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

public partial class CountriesIndex
{
    [Inject] public IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null;
    [Inject] private NavigationManager NavigationManager { get; set; } = null;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null;
    private List<Country>? Countries { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Country>>("api/Coutries");
        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessaggeAsync();
            await SweetAlertService.FireAsync(Localizer["Error"], messageError, SweetAlertIcon.Error);
            return;
        }
        Countries = responseHttp.Response!;
    }

    private async Task DeleteAsync(Country country)
    {
        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = Localizer["Confirm"],
            Text = string.Format(Localizer["DeleteConfirm"], Localizer["Country"], country.Name),
            Icon = SweetAlertIcon.Question,
            ShowCancelButton = true,
            CancelButtonText = Localizer["Cancel"],
        }); ;
        var confirm = string.IsNullOrEmpty(result.Value);
        if (confirm)
        {
            return;
        }
        var responseHttp = await Repository.DeleteAsync($"api/Coutries/{country.Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessaggeAsync();
                await SweetAlertService.FireAsync(Localizer["Error"], messageError, SweetAlertIcon.Error);
            }
            return;
        }
        await LoadAsync();
        var toast = SweetAlertService.Mixin(new SweetAlertOptions
        {
            Toast = true,
            Position = SweetAlertPosition.BottomEnd,
            ShowConfirmButton = true,
            Timer = 3000,
            ConfirmButtonText = Localizer["Yes"]
        });
        toast.FireAsync(icon: SweetAlertIcon.Success, message: "Eliminado con éxito");
    }
}