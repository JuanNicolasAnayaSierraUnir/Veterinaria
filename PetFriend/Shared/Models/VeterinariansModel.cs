using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFriend.Shared.Models
{
    public class VeterinariansModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public UsersModel User { get; set; }
        public List<Veterinarians_SpecialtiesModel> Veterinarians_Specialties { get; set; } = new();
    }
}
