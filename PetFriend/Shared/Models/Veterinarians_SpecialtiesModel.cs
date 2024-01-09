using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFriend.Shared.Models
{
    public class Veterinarians_SpecialtiesModel
    {
        [Key]
        public int Id { get; set; }
        public VeterinariansModel Veterinarian { get; set; }
        public SpecialtiesModel Specialty { get; set; }
    }
}
