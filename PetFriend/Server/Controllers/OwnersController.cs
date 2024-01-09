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
    public class OwnersController : ControllerBase
    {
        private readonly DB_ContextEntity context;

        public OwnersController(DB_ContextEntity context)
        {
            this.context = context;
        }


        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> DeleteVeterinarian([FromHeader] int idOwners)
        {
            var answer = await context.Owners.Include(u => u.User).FirstOrDefaultAsync(s => s.Id.Equals(idOwners));
            answer.User.State = false;
            context.Update(answer);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("Update")]
        public async Task<ActionResult<int>> UpdateVeterinarian([FromBody] OwnersDTOs owners)
        {
            var answer = await context.Owners.Include(u => u.User).Include(s => s.City).FirstOrDefaultAsync(s => s.Id.Equals(owners.Id));
            answer.Name = owners.Name;
            answer.LastName = owners.LastName;
            answer.Address = owners.Address;
            answer.Telephone = owners.Telephone;
            answer.User.UserName = owners.UserName;
            answer.User.Password = owners.Password;
            answer.City.Name = owners.City;
            context.Update(answer);
            await context.SaveChangesAsync();
            return Ok("Exito");
        }

        [HttpPost("Create")]
        public async Task<ActionResult<object>> CreateVeterinarian([FromBody] OwnersDTOs owners)
        {
            var answerRol = await context.Roles.FirstOrDefaultAsync(s => s.Id.Equals(2));
            Users_RolesModel createVeterinarian = new() { User = new UsersModel() { UserName = owners.UserName, Password = owners.Password, State = true }, Rol = answerRol };
            await context.AddAsync(createVeterinarian);

            OwnersModel veterinariansModel = new() { 
                Name = owners.Name.ToLower(), 
                LastName = owners.LastName.ToLower(), 
                Address = owners.Address.ToLower(),
                Telephone = owners.Telephone,
                User = createVeterinarian.User, 
                City = new CitiesModel() { Name = owners.City } };

            await context.AddAsync(veterinariansModel);

            await context.SaveChangesAsync();
            return Ok("Exito");
        }


        [HttpGet("All")]
        public async Task<ActionResult<object>> ObtenerVeterinarians()
        {
            List<OwnersDTOsAll> veterinariansAlls = new();
            var answer = await context.Owners.Include(u => u.User).Include(s => s.City).Include(p => p.Pets).Where(u => u.User.State.Equals(true)).ToListAsync();
            foreach (var element in answer)
            {
                string specialties = string.Empty;
                foreach (var item in element.Pets)
                {
                    if (string.IsNullOrEmpty(specialties))
                    {
                        specialties += item.Name;
                    }
                    else
                    {
                        specialties += ", " + item.Name;
                    }
                }

                veterinariansAlls.Add(new OwnersDTOsAll()
                {
                    Id = element.Id,
                    Name = element.Name + " " + element.LastName,
                    Address = element.Address,
                    City = element.City.Name,
                    Telephone = element.Telephone,
                    Pets = specialties
                });
            }
            return Ok(veterinariansAlls);
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<object>> ObtenerFilterVeterinarians([FromHeader] string name)
        {
            List<OwnersDTOsAll> veterinariansAlls = new();
            var answer = await context.Owners.Include(u => u.User).Include(s => s.City).Include(s => s.Pets).Where(s => string.Concat(s.Name, " ", s.LastName).Contains(name) && s.User.State.Equals(true)).ToListAsync();
            foreach (var element in answer)
            {
                string specialties = string.Empty;
                foreach (var item in element.Pets)
                {
                    if (string.IsNullOrEmpty(specialties))
                    {
                        specialties += item.Name;
                    }
                    else
                    {
                        specialties += ", " + item.Name;
                    }
                }

                veterinariansAlls.Add(new OwnersDTOsAll()
                {   
                    Id = element.Id,
                    Name = element.Name + " " + element.LastName,
                    Address = element.Address,
                    City = element.City.Name,
                    Telephone = element.Telephone,
                    Pets = specialties
                });
            }
            return Ok(veterinariansAlls);
        }

        [HttpGet("Filter/Id")]
        public async Task<ActionResult<object>> ObtenerFilterVeterinarianId([FromHeader] int idVeterinario)
        {
            List<string> especialidades = new();
            var answer = await context.Owners.Include(u => u.User).Include(s => s.City).FirstOrDefaultAsync(s => s.Id.Equals(idVeterinario) && s.User.State.Equals(true));
            string specialties = string.Empty;

            var result = new OwnersDTOs
            {
                Id = answer.Id,
                Name = answer.Name,
                LastName = answer.LastName,
                UserName = answer.User.UserName,
                Password = answer.User.Password,
                Address = answer.Address,
                Telephone = answer.Telephone,
                City = answer.City.Name,
            };

            return Ok(result);
        }
    }
}
