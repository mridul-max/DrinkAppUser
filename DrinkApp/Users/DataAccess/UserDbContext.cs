using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Users.Model;


namespace Users.DataAccess
{
    public class UserDbContext : DbContext, IUserDbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<CareGiver> CareGivers { get; set; }
        public DbSet<DrinkRecord> DrinkRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                Environment.GetEnvironmentVariable("DBURI"),
                Environment.GetEnvironmentVariable("dbkey"),
                Environment.GetEnvironmentVariable("DBName"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToContainer("Patients").HasPartitionKey(p => p.Id);
            modelBuilder.Entity<CareGiver>().ToContainer("CareGivers").HasPartitionKey(c => c.Id);    
            //modelBuilder.Entity<Patient>().HasOne<CareGiver>(p => p.CareGiver).WithMany(c => c.Patients);
            modelBuilder.Entity<CareGiver>().HasMany<Patient>(c => c.Patients).WithOne(p => p.CareGiver);

            modelBuilder.Entity<DrinkRecord>().ToContainer("DrinkRecords").HasPartitionKey(p => p.PatientId);
            modelBuilder.Entity<Patient>().HasMany<DrinkRecord>(p => p.DayLogs).WithOne(d => d.Patient).HasForeignKey("PatientId1");
            modelBuilder.Entity<DrinkRecord>().Navigation(c => c.Patient).UsePropertyAccessMode(PropertyAccessMode.Property);
        }
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
        public void MarkAsModifiedPatient(Patient patient) 
        {
            Entry(patient).State = EntityState.Modified;
        }
        public void MarkAsModifiedCareGiver(CareGiver careGiver)
        {
            Entry(careGiver).State = EntityState.Modified;
        }
        public void MarkAsModifiedDrinkRecord(DrinkRecord drinkRecord)
        {
            Entry(drinkRecord).State = EntityState.Modified;
        }
    }
}
