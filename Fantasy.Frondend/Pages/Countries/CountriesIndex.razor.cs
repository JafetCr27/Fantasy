namespace Fantasy.Frondend.Pages.Countries
{
    using Fantasy.Frondend.Repositories;
    using Fantasy.Shared.Entities;
    using Microsoft.AspNetCore.Components;
    public partial class CountriesIndex
    {
        [Inject] private IRepository Repository { get; set; } = null;
        private List<Country>? Countries { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Country>>("api/Coutries");
            Countries = responseHttp.Response;
        }
    }
}