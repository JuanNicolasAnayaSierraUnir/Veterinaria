using Microsoft.EntityFrameworkCore;
using Npgsql;
using PetFriend.Shared.Models;

namespace PetFriend.Server.DataBase
{
    public class DB_ContextEntity : DbContext
    {
        public DB_ContextEntity(DbContextOptions options) : base(options)
        {
        }

        #region DbSet
        public DbSet<VisitsModel> Visits { get; set; }
        public DbSet<PetsModel> Pet { get; set; }
        public DbSet<CitiesModel> Cities { get; set; }
        public DbSet<OwnersModel> Owners { get; set; }
        public DbSet<TypesModel> Types { get; set; }
        public DbSet<UsersModel> Users { get; set; }
        public DbSet<Users_RolesModel> Users_Roles { get; set; }
        public DbSet<VeterinariansModel> Veterinarians { get; set; }
        public DbSet<Veterinarians_SpecialtiesModel> Veterinarians_Specialties { get; set; }
        public DbSet<SpecialtiesModel> Specialties { get; set; }
        public DbSet<RolesModel> Roles { get; set; }
        #endregion
    }
}
