using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFriend.Server.DataBase;
using PetFriend.Shared.DTOs;
using PetFriend.Shared.Models;

namespace PetFriend.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VeterinarianController : ControllerBase
    {
        private readonly DB_ContextEntity context;

        public VeterinarianController(DB_ContextEntity context)
        {
            this.context = context;
        }


        [HttpDelete("Delete")] 
        public async Task<ActionResult<int>> DeleteVeterinarian([FromHeader] int idVeterinario)
        {
            var answer = await context.Veterinarians.Include(u => u.User).FirstOrDefaultAsync(s => s.Id.Equals(idVeterinario));
            answer.User.State = false;
            context.Update(answer);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("Update")]
        public async Task<ActionResult<int>> UpdateVeterinarian([FromBody] VeterinariansDTOs veterinarians)
        {
            var answer = await context.Veterinarians.Include(u => u.User).Include(s => s.Veterinarians_Specialties).ThenInclude(s => s.Specialty).FirstOrDefaultAsync(s => s.Id.Equals(veterinarians.Id));
            answer.Name = veterinarians.Name;
            answer.LastName = veterinarians.LastName;
            answer.User.UserName = veterinarians.UserName;
            answer.User.Password = veterinarians.Password;
            context.Update(answer);
            await context.SaveChangesAsync();
            return Ok("Exito");
        }

        [HttpPost("Create")]
        public async Task<ActionResult<object>> CreateVeterinarian([FromBody] VeterinariansDTOs veterinarians)
        {
            var answerRol = await context.Roles.FirstOrDefaultAsync(s => s.Id.Equals(1));
            Users_RolesModel createVeterinarian = new() { User = new UsersModel() { UserName = veterinarians.UserName, Password = veterinarians.Password, State = true }, Rol = answerRol };
            await context.AddAsync(createVeterinarian);

            VeterinariansModel veterinariansModel = new() { Name = veterinarians.Name.ToLower(), LastName = veterinarians.LastName.ToLower(), User = createVeterinarian.User };
            await context.AddAsync(veterinariansModel);

            foreach (var item in veterinarians.Specialties)
            {
                Veterinarians_SpecialtiesModel veterinarians_Specialties = new() { Specialty = new SpecialtiesModel() { Name = item }, Veterinarian = veterinariansModel };
                await context.AddAsync(veterinarians_Specialties);
            }
            await context.SaveChangesAsync();
            return Ok("Exito");
        }


        [HttpGet("All")]
        public async Task<ActionResult<object>> ObtenerVeterinarians()
        {
            List<VeterinariansAllDTOs> veterinariansAlls = new();
            var answer = await context.Veterinarians.Include(u => u.User).Include(s => s.Veterinarians_Specialties).ThenInclude(s => s.Specialty).Where(u => u.User.State.Equals(true)).ToListAsync();
            foreach (var element in answer)
            {
                string specialties = string.Empty;
                foreach (var item in element.Veterinarians_Specialties)
                {
                    if (string.IsNullOrEmpty(specialties))
                    {
                        specialties += item.Specialty.Name;
                    }
                    else
                    {
                        specialties += ", "+item.Specialty.Name;
                    }
                }

                veterinariansAlls.Add(new VeterinariansAllDTOs()
                {
                    Id = element.Id,
                    Name = element.Name + " " + element.LastName,
                    Specialties = specialties
                });
            }
            return Ok(veterinariansAlls);
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<object>> ObtenerFilterVeterinarians([FromHeader] string name)
        {
            List<VeterinariansAllDTOs> veterinariansAlls = new();
            var answer = await context.Veterinarians.Include(u => u.User).Include(s => s.Veterinarians_Specialties).ThenInclude(s => s.Specialty).Where(s => string.Concat(s.Name, " ", s.LastName).Contains(name) && s.User.State.Equals(true)).ToListAsync();
            foreach (var element in answer)
            {
                string specialties = string.Empty;
                foreach (var item in element.Veterinarians_Specialties)
                {
                    if (string.IsNullOrEmpty(specialties))
                    {
                        specialties += item.Specialty.Name;
                    }
                    else
                    {
                        specialties += ", " + item.Specialty.Name;
                    }
                }

                veterinariansAlls.Add(new VeterinariansAllDTOs()
                {
                    Id = element.Id,
                    Name = element.Name + " " + element.LastName,
                    Specialties = specialties
                });
            }
            return Ok(veterinariansAlls);
        }

        [HttpGet("Filter/Id")]
        public async Task<ActionResult<object>> ObtenerFilterVeterinarianId([FromHeader] int idVeterinario)
        {
            List<string> especialidades = new();
            var answer = await context.Veterinarians.Include(u => u.User).Include(s => s.Veterinarians_Specialties).ThenInclude(s => s.Specialty).FirstOrDefaultAsync(s => s.Id.Equals(idVeterinario) && s.User.State.Equals(true));
            string specialties = string.Empty;

            var result = new VeterinariansDTOs();
            
            foreach (var item in answer.Veterinarians_Specialties)
            {
                especialidades.Add(item.Specialty.Name);
            }
            result.Id = answer.Id;
            result.Name = answer.Name;
            result.LastName = answer.LastName;
            result.UserName = answer.User.UserName;
            result.Password = answer.User.Password;
            result.Specialties = especialidades;

            return Ok(result);
        }
    }
}
