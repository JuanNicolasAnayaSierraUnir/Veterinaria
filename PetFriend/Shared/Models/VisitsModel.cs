using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFriend.Shared.Models
{
    public class VisitsModel
    {
        [Key]
        public int Id { get; set; }
        public PetsModel Pet { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public VeterinariansModel Veterinarian { get; set; }
    }
}
