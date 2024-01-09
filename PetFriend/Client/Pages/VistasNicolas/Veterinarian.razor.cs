using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using PetFriend.Shared.DTOs;
using PetFriend.Shared.Models;
using System.Net.Http.Json;

namespace PetFriend.Client.Pages.VistasNicolas
{
    public partial class Veterinarian
    {
        [Inject]
        private HttpClient httpClient { get; set; }
        [Inject]
        private SweetAlertService Swal { get; set; }

        public string MenuViews { get; set; }
        public string Specialty { get; set; }
        public bool especialidades { get; set; } = true;
        public string NameVeterinarie { get; set; }
        public List<string> SpecialtiesList = new();
        public VeterinariansDTOs VeterinariansDto = new();
        public List<VeterinariansAllDTOs> veterinariansAllDTs = new();

        private void AddVeterinarian()
        {
            MenuViews = "AddVeterinarian";
            VeterinariansDto = new();

        }

        private void Return()
        {
            MenuViews = "";
            StateHasChanged();

        }

        private void AddSpecialty()
        {
            if (!string.IsNullOrEmpty(Specialty))
            {
                SpecialtiesList.Add(Specialty);
                Specialty = string.Empty;
            }
        }

        private async Task CreateVeterinarian()
        {
            if (VeterinariansDto.Id == 0)
            {
                VeterinariansDto.Specialties = SpecialtiesList;
                await httpClient.PostAsJsonAsync<object>("Veterinarian/Create", VeterinariansDto);
                VeterinariansDto = new();
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Exitoso",
                    Text = "Registro creado con exito",
                    Icon = SweetAlertIcon.Success
                });
                veterinariansAllDTs = await httpClient.GetFromJsonAsync<List<VeterinariansAllDTOs>>("Veterinarian/All");

            }
            else
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Update",
                    Text = "Desea Actualizar la informacion",
                    Icon = SweetAlertIcon.Warning,
                    ShowCancelButton = true,
                    ConfirmButtonText = "SI",
                    CancelButtonText = "No"
                });

                if (!string.IsNullOrEmpty(result.Value))
                {

                    await httpClient.PutAsJsonAsync<object>("Veterinarian/Update", VeterinariansDto);
                    MenuViews = "";
                    veterinariansAllDTs = await httpClient.GetFromJsonAsync<List<VeterinariansAllDTOs>>("Veterinarian/All");
                    StateHasChanged();

                }
                else if (result.Dismiss == DismissReason.Cancel)
                {

                }

            }
        }

        private async Task Update(int id)
        {
            AddVeterinarian();
            httpClient.DefaultRequestHeaders.Add("idVeterinario", id.ToString());
            VeterinariansDto = await httpClient.GetFromJsonAsync<VeterinariansDTOs>("Veterinarian/Filter/Id");
            httpClient.DefaultRequestHeaders.Remove("idVeterinario");
            especialidades = false;
            StateHasChanged();
        }

        private async Task Delete(int id)
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Delete",
                Text = "Desea Eliminar la informacion",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "SI",
                CancelButtonText = "NO"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                httpClient.DefaultRequestHeaders.Add("idVeterinario", id.ToString());
                await httpClient.DeleteAsync("Veterinarian/Delete");
                httpClient.DefaultRequestHeaders.Remove("idVeterinario");
                veterinariansAllDTs = await httpClient.GetFromJsonAsync<List<VeterinariansAllDTOs>>("Veterinarian/All");
                StateHasChanged();
            }
            else if (result.Dismiss == DismissReason.Cancel)
            {

            }

        }

        protected override async Task OnInitializedAsync()
        {
            veterinariansAllDTs = await httpClient.GetFromJsonAsync<List<VeterinariansAllDTOs>>("Veterinarian/All");

        }


        private async Task BuscarVeterinarian()
        {
            if (!string.IsNullOrEmpty(NameVeterinarie))
            {
                httpClient.DefaultRequestHeaders.Add("name", NameVeterinarie);
                veterinariansAllDTs = await httpClient.GetFromJsonAsync<List<VeterinariansAllDTOs>>("Veterinarian/Filter");
                httpClient.DefaultRequestHeaders.Remove("name");
            }
            else
            {
                veterinariansAllDTs = await httpClient.GetFromJsonAsync<List<VeterinariansAllDTOs>>("Veterinarian/All");
            }
        }
    }
}
