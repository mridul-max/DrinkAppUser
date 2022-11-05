using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Users.Model;

namespace Users.DataAccess
{
    public interface IUserDbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<CareGiver> CareGivers { get; set; }
        public DbSet<DrinkRecord> DrinkRecords { get; set; }
        Task<int> SaveChangesAsync();
        public void MarkAsModifiedPatient(Patient patient);
        public void MarkAsModifiedCareGiver(CareGiver careGiver);
        public void MarkAsModifiedDrinkRecord(DrinkRecord drinkRecord);
    }
}
