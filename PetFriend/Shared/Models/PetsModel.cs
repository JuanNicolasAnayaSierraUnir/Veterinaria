using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFriend.Shared.Models
{
    public class PetsModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public TypesModel Type { get; set; }
        public OwnersModel Owner { get; set; }
        public List<VisitsModel> Visits { get; set; } = new();
    }
}
