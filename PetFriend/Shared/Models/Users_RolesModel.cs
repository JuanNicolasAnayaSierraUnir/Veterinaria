using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFriend.Shared.Models
{
    public class Users_RolesModel
    {
        [Key]
        public int Id { get; set; }
        public UsersModel User { get; set; }
        public RolesModel Rol { get; set; }
    }
}
