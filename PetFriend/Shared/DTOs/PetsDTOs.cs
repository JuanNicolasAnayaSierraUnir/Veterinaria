using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFriend.Shared.DTOs
{
    public class PetsDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Types { get; set; }
        public int Propietario { get; set; }
    }

    public class PetsDTOsId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Types { get; set; }
    }

    public class PetsDTOsAll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Types { get; set; }
        public List<PetVisitDTOsAll> Visits { get; set; }
    }

    public class PetVisitDTOsAll
    {
        public string Description { get; set; }
        public string Date { get; set; }
    }
}
