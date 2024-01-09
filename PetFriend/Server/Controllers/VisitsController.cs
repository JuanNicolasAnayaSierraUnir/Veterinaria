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
    public class VisitsController : ControllerBase
    {
        private readonly DB_ContextEntity context;

        public VisitsController(DB_ContextEntity context)
        {
            this.context = context;
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> DeleteVeterinarian([FromHeader] int idVisit)
        {
            var answer = await context.Visits.FirstOrDefaultAsync(s => s.Id.Equals(idVisit));
            context.Remove(answer);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("Update")]
        public async Task<ActionResult<int>> UpdateVeterinarian([FromBody] VisitsDTOs owners)
        {
            var answerVet = await context.Veterinarians.FirstOrDefaultAsync(s => s.Id.Equals(owners.Veterinarian));
            var answer = await context.Visits.FirstOrDefaultAsync(s => s.Id.Equals(owners.Id));
            answer.Date = owners.Date;
            answer.Description = owners.Description;
            answer.Veterinarian = answerVet;
            context.Update(answer);
            await context.SaveChangesAsync();
            return Ok("Exito");
        }

        [HttpPost("Create")]
        public async Task<ActionResult<object>> CreateVeterinarian([FromBody] VisitsDTOs owners)
        {
            var answerPet = await context.Pet.FirstOrDefaultAsync(s => s.Id.Equals(owners.Veterinarian));
            var answerVet = await context.Veterinarians.FirstOrDefaultAsync(s => s.Id.Equals(owners.Veterinarian));
            var pet = new VisitsModel { Date = owners.Date, Description = owners.Description, Pet = answerPet, Veterinarian = answerVet };
            await context.AddAsync(pet);
            await context.SaveChangesAsync();
            return Ok("Exito");
        }

        [HttpGet("All/Veterinarians")]
        public async Task<ActionResult<object>> ObtenerVisitas()
        {
            List<VeterinariansAllDTOsVisit> visitsDTOsAlls = new();
            var answer = await context.Veterinarians.ToListAsync();

            foreach (var item in answer)
            {
                visitsDTOsAlls.Add(new VeterinariansAllDTOsVisit()
                {
                    Id = item.Id,
                    Name = item.Name + " " + item.LastName,
                });
            }

            return Ok(visitsDTOsAlls);
        }

        [HttpGet("All")]
        public async Task<ActionResult<object>> ObtenerVisitas([FromHeader] int idPet)
        {
            List<VisitsDTOsAll> visitsDTOsAlls = new();
            var answer = await context.Visits.Include(p => p.Pet).Where(s => s.Pet.Id.Equals(idPet)).ToListAsync();

            foreach (var item in answer)
            {
                visitsDTOsAlls.Add(new VisitsDTOsAll()
                {
                    Id = item.Id,
                    Date = item.Date,
                    Description = item.Description,
                });
            }

            return Ok(answer);
        }


        [HttpGet("Filter/Id")]
        public async Task<ActionResult<object>> ObtenerFilterVeterinarianId([FromHeader] int idVisit)
        {
            var answer = await context.Visits.Include(v => v.Veterinarian).Include(p => p.Pet).FirstOrDefaultAsync(s => s.Id.Equals(idVisit));

            var result = new VisitsDTOsFilter
            {
                Id = answer.Id,
                Date = answer.Date,
                Description = answer.Description,
                Pet = answer.Pet.Id,
                Veter = answer.Veterinarian.Id
            };

            return Ok(result);
        }
    }
}
