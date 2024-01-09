using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFriend.Shared.Models
{
    public class OwnersModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public CitiesModel  City { get; set; }
        public string  Telephone { get; set; }
        public UsersModel  User { get; set; }
        public List<PetsModel>  Pets { get; set; } = new();
    }
}
