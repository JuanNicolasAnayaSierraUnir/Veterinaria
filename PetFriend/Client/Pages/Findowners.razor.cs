using Microsoft.AspNetCore.Components;
using PetFriend.Shared.DTOs;
using System.Net.Http;
using System.Net.Http.Json;


namespace PetFriend.Client.Pages
{
    public partial class Findowners
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        public string MenuViews { get; set; }
        public string NameVeterinarie { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaVisita { get; set; }
        public OwnersDTOs OwnersDTOs = new();
        public PetsDTOsId PetsDT = new();
        public PetsDTOs petsDTOs = new();
        public VisitsDTOs visitsDTOs = new();
        public VisitsDTOsFilter visitsDTOsFilter = new();
        public List<OwnersDTOsAll> OwnersDTOsAll = new();
        public List<PetsDTOsAll> petsDTOsAlls = new();
        public List<VisitsDTOsAll> visitsDTOsAlls = new();
        public List<VeterinariansAllDTOsVisit> veterinariansAlls = new();


        private void AddOwner()
        {
            MenuViews = "AddOwner";
        }
        private void Pets()
        {
            MenuViews = "Pets";
        }
        private void Visit()
        {
            MenuViews = "Visit";
        }
        private void AddPets()
        {
            MenuViews = "AddPets";
        }
        private void Return()
        {
            MenuViews = "";
            StateHasChanged();
        }
        private async Task CreateVeterinarian()
        {
            if (OwnersDTOs.Id == 0)
            {
                await httpClient.PostAsJsonAsync<object>("Owners/Create", OwnersDTOs);
                OwnersDTOs = new();
            }
            else
            {
                await httpClient.PutAsJsonAsync<object>("Owners/Update", OwnersDTOs);
                MenuViews = "FindOwners";
                OwnersDTOsAll = await httpClient.GetFromJsonAsync<List<OwnersDTOsAll>>("Owners/All");
            }
        }

        private async Task Update(int id)
        {
            AddOwner();
            httpClient.DefaultRequestHeaders.Add("idVeterinario", id.ToString());
            OwnersDTOs = await httpClient.GetFromJsonAsync<OwnersDTOs>("Owners/Filter/Id");
            httpClient.DefaultRequestHeaders.Remove("idVeterinario");
            StateHasChanged();
        }

        private async Task Mascota(int id)
        {
            Pets();
            httpClient.DefaultRequestHeaders.Add("idVeterinario", id.ToString());
            OwnersDTOs = await httpClient.GetFromJsonAsync<OwnersDTOs>("Owners/Filter/Id");
            httpClient.DefaultRequestHeaders.Remove("idVeterinario");

            httpClient.DefaultRequestHeaders.Add("idOwner", id.ToString());
            petsDTOsAlls = await httpClient.GetFromJsonAsync<List<PetsDTOsAll>>("Pets/All");
            httpClient.DefaultRequestHeaders.Remove("idOwner");

            StateHasChanged();
        }

        private async Task Delete(int id)
        {
            httpClient.DefaultRequestHeaders.Add("idOwners", id.ToString());
            await httpClient.DeleteAsync("Owners/Delete");
            httpClient.DefaultRequestHeaders.Remove("idOwners");
            OwnersDTOsAll = await httpClient.GetFromJsonAsync<List<OwnersDTOsAll>>("Owners/All");
            StateHasChanged();

        }

        protected override async Task OnInitializedAsync()
        {
            OwnersDTOsAll = await httpClient.GetFromJsonAsync<List<OwnersDTOsAll>>("Owners/All");
        }


        private async Task BuscarVeterinarian()
        {
            if (!string.IsNullOrEmpty(NameVeterinarie))
            {
                httpClient.DefaultRequestHeaders.Add("name", NameVeterinarie);
                OwnersDTOsAll = await httpClient.GetFromJsonAsync<List<OwnersDTOsAll>>("Owners/Filter");
                httpClient.DefaultRequestHeaders.Remove("name");
            }
            else
            {
                OwnersDTOsAll = await httpClient.GetFromJsonAsync<List<OwnersDTOsAll>>("Owners/All");
            }
        }


        private async Task CreatePets()
        {
            if (petsDTOs.Id == 0)
            {
                petsDTOs.Propietario = OwnersDTOs.Id;
                petsDTOs.BirthDate = Fecha.ToString();
                await httpClient.PostAsJsonAsync<object>("Pets/Create", petsDTOs);
                petsDTOs = new();
            }
            else
            {
                petsDTOs.Propietario = OwnersDTOs.Id;
                petsDTOs.BirthDate = Fecha.ToString();
                await httpClient.PutAsJsonAsync<object>("Pets/Update", petsDTOs);
                MenuViews = "FindOwners";
                //OwnersDTOsAll = await httpClient.GetFromJsonAsync<List<OwnersDTOsAll>>("Pets/All");
            }
        }
        private async Task UpdatePets(int id)
        {
            AddPets();
            httpClient.DefaultRequestHeaders.Add("idPet", id.ToString());
            PetsDT = await httpClient.GetFromJsonAsync<PetsDTOsId>("Pets/Filter/Id");
            httpClient.DefaultRequestHeaders.Remove("idPet");
            Fecha = DateTime.Parse(PetsDT.BirthDate);
            StateHasChanged();
        }

        private async Task DeletePets(int id)
        {
            httpClient.DefaultRequestHeaders.Add("idPet", id.ToString());
            await httpClient.DeleteAsync("Pets/Delete");
            httpClient.DefaultRequestHeaders.Remove("idPet");
            //OwnersDTOsAll = await httpClient.GetFromJsonAsync<List<OwnersDTOsAll>>("Pets/All");
            StateHasChanged();

        }

        private async Task CreateVisit()
        {
            if (visitsDTOs.Id == 0)
            {
                visitsDTOs.Pet = PetsDT.Id;
                visitsDTOs.Date = Fecha.ToString();
                await httpClient.PostAsJsonAsync<object>("Visits/Create", visitsDTOs);
                visitsDTOs = new();
            }
            else
            {
                visitsDTOs.Pet = PetsDT.Id;
                visitsDTOs.Date = Fecha.ToString();
                await httpClient.PutAsJsonAsync<object>("Visits/Update", visitsDTOs);
                MenuViews = "FindOwners";
                visitsDTOsAlls = await httpClient.GetFromJsonAsync<List<VisitsDTOsAll>>("Visits/All");
            }
        }

        private async Task Visitas(int id)
        {
            Visit();

            httpClient.DefaultRequestHeaders.Add("idPet", id.ToString());
            PetsDT = await httpClient.GetFromJsonAsync<PetsDTOsId>("Pets/Filter/Id");
            httpClient.DefaultRequestHeaders.Remove("idPet");


            httpClient.DefaultRequestHeaders.Add("idPet", PetsDT.Id.ToString());
            visitsDTOsAlls = await httpClient.GetFromJsonAsync<List<VisitsDTOsAll>>("Visits/All");
            httpClient.DefaultRequestHeaders.Remove("idPet");

            veterinariansAlls = await httpClient.GetFromJsonAsync<List<VeterinariansAllDTOsVisit>>("Visits/All/Veterinarians");

            StateHasChanged();
        }

        private async Task UpdateVisitas(int id)
        {
            httpClient.DefaultRequestHeaders.Add("idVisit", id.ToString());
            visitsDTOsFilter = await httpClient.GetFromJsonAsync<VisitsDTOsFilter>("Visits/Filter/Id");
            httpClient.DefaultRequestHeaders.Remove("idVisit");
            FechaVisita = DateTime.Parse(visitsDTOsFilter.Date);
            visitsDTOs.Description = visitsDTOsFilter.Description;
            visitsDTOs.Pet = visitsDTOsFilter.Pet;
            visitsDTOs.Veterinarian = visitsDTOsFilter.Veter;
            visitsDTOs.Id = visitsDTOsFilter.Id;
            StateHasChanged();
        }
        private async Task DeleteVisitas(int id)
        {
            httpClient.DefaultRequestHeaders.Add("idVisit", id.ToString());
            await httpClient.DeleteAsync("Visits/Delete");
            httpClient.DefaultRequestHeaders.Remove("idVisit");
            visitsDTOsAlls = await httpClient.GetFromJsonAsync<List<VisitsDTOsAll>>("Visits/All");
            StateHasChanged();

        }
    }


}
