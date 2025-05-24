using CurrieTechnologies.Razor.SweetAlert2;
using Fantasy.Frondend.Repositories;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Frondend.Pages.Countries
{
    public partial class CountryEdit
    {

        private Country? country { get; set; }
        private CountryForm? countryForm { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; } = null;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null;
        [Inject] private IRepository Repository { get; set; } = null;

        [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null;

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHttp = await Repository.GetAsync<Country>($"api/Coutries/{Id}");
            if (responseHttp.Error
)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("countries");

                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessaggeAsync();
                    await SweetAlertService.FireAsync(Localizer["Error"], messageError, SweetAlertIcon.Error);
                }
            }
            else
            {
                country = responseHttp.Response;
            }
        }
        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync($"api/Coutries", country);
            if (responseHttp.Error)
            {
                var messageError = await responseHttp.GetErrorMessaggeAsync();
                await SweetAlertService.FireAsync(Localizer["Error"], Localizer[messageError!], SweetAlertIcon.Error);
                return;
            }
            Return();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.Top,
                ShowConfirmButton = true,
                Timer = 3000
            });
        }

        private void Return()
        {
            countryForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("countries");
        }
    }
}