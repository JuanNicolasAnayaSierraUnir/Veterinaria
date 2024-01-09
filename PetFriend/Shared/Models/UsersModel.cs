using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFriend.Shared.Models
{
    public class UsersModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool State { get; set; }
        public List<VeterinariansModel> Veterinarians { get; set; } = new();
        public List<OwnersModel> Owners { get; set; } = new();
        public List<Users_RolesModel> Users_Roles { get; set; } = new();
    }
}
