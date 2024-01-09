using PetFriend.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFriend.Shared.DTOs
{
    public class VeterinariansDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> Specialties { get; set; }
    }

    public class VeterinariansAllDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialties { get; set; }
    }

    public class VeterinariansAllDTOsVisit
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
