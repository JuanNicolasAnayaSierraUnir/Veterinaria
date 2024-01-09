using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFriend.Shared.Models
{
    public class RolesModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Users_RolesModel> Users_Roles { get; set; } = new();
    }
}
