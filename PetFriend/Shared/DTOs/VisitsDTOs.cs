using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFriend.Shared.DTOs
{
    public class VisitsDTOs
    {
        public int Id { get; set; }
        public int Pet { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public int Veterinarian { get; set; }
    }
    public class VisitsDTOsAll
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }

    public class VisitsDTOsFilter
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public int Pet { get; set; }
        public int Veter { get; set; }
    }
}
