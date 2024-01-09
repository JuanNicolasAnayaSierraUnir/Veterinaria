using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFriend.Shared.Models
{
    public class TypesModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PetsModel> Pets { get; set; } = new();
    }
}
