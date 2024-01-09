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
    public class PetsController : ControllerBase
    {
        private readonly DB_ContextEntity context;

        public PetsController(DB_ContextEntity context)
        {
            this.context = context;
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> DeleteVeterinarian([FromHeader] int idPet)
        {
            var answer = await context.Pet.Include(u => u.Visits).FirstOrDefaultAsync(s => s.Id.Equals(idPet));
            context.Remove(answer);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("Update")]
        public async Task<ActionResult<int>> UpdateVeterinarian([FromBody] PetsDTOs owners)
        {
            var answer = await context.Pet.Include(u => u.Type).FirstOrDefaultAsync(s => s.Id.Equals(owners.Id));
            answer.Name = owners.Name;
            answer.BirthDate = owners.BirthDate;
            answer.Type.Name = owners.Types;
            context.Update(answer);
            await context.SaveChangesAsync();
            return Ok("Exito");
        }

        [HttpPost("Create")]
        public async Task<ActionResult<object>> CreatePet([FromBody] PetsDTOs owners)
        {
            var owner = await context.Owners.FirstOrDefaultAsync(u => u.Id.Equals(owners.Propietario));
            var pet = new PetsModel { Name = owners.Name, BirthDate = owners.BirthDate, Type = new TypesModel() { Name = owners.Types }, Owner = owner };
            await context.AddAsync(pet);
            await context.SaveChangesAsync();
            return Ok("Exito");
        }

        [HttpGet("Filter/Id")]
        public async Task<ActionResult<object>> ObtenerFilterVeterinarianId([FromHeader] int idPet)
        {
            var answer = await context.Pet.Include(u => u.Type).Select(p => new { p.Id, p.BirthDate, p.Name, p.Type }).FirstOrDefaultAsync(s => s.Id.Equals(idPet));


            PetsDTOsId pets = new()
            {
                Id = answer.Id,
                Name = answer.Name,
                BirthDate = answer.BirthDate,
                Types = answer.Type.Name
            };

            return Ok(pets);
        }

        [HttpGet("All")]
        public async Task<ActionResult<object>> ObtenerVeterinarians([FromHeader] int idOwner)
        {
            List<PetsDTOsAll> pets = new();
            var answer = await context.Pet.Include(u => u.Type).Include(s => s.Visits).Where(u => u.Owner.Id.Equals(idOwner)).ToListAsync();

            foreach (var item in answer)
            {
                List<PetVisitDTOsAll> petVisitDTOsAlls = new();

                foreach (var p in item.Visits)
                {
                    petVisitDTOsAlls.Add(new PetVisitDTOsAll()
                    {
                        Description = p.Description,
                        Date = p.Date,
                    });
                }

                pets.Add(new PetsDTOsAll()
                {
                    Id = item.Id,
                    Name = item.Name,
                    BirthDate = item.BirthDate,
                    Types = item.Type.Name,
                    Visits = petVisitDTOsAlls
                });
            }
            return Ok(pets);
        }
    }
}
