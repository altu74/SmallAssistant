using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmallAssistant.DBModels
{
    public class SmallAssistantDBContext: DbContext
    {
        private string _databasePath;
        private const string _dbFileName = "SmallAssistant.db";

        private static SmallAssistantDBContext _context;

        public DbSet<Rate> Rates { get; set; }
        public DbSet<RateType> RateTypes { get; set; }
        public DbSet<RateValue> RateValues { get; set; }
        public DbSet<Meter> Meters { get; set; }
        public DbSet<MeterRate> MeterRates { get; set; }
        public DbSet<MeterValue> MeterValues { get; set; }
        public SmallAssistantDBContext(string dbPath)
        {
            _databasePath = dbPath;
        }

        public static SmallAssistantDBContext GetContext()
        {
            if (_context == null)
            {
                var dbFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _dbFileName);
                //_context = new SmallAssistantDBContext(DependencyService.Get<IDBPath>().GetDatabasePath(_dbFileName));
                _context = new SmallAssistantDBContext(dbFileName);
                _context.Database.Migrate();
                if (!_context.RateTypes.Any())
                {
                    _context.RateTypes.Add(new RateType {RateTypeName = "Постоянный"});
                    _context.RateTypes.Add(new RateType { RateTypeName = "Лимитированный" });
                    _context.RateTypes.Add(new RateType { RateTypeName = "Зональный" });
                    _context.SaveChanges();
                }

                if (!_context.Meters.Any())
                {
                    _context.Meters.Add(new Meter() { MeterName = "Электроэнергия" });
                    _context.Meters.Add(new Meter() { MeterName = "Холодная вода" });
                    _context.Meters.Add(new Meter() { MeterName = "Горячая вода" });
                    _context.SaveChanges();
                }
            }

            return _context;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RateType>().HasKey(e => e.RateTypeId);
            modelBuilder.Entity<RateType>()
                .HasMany(e => e.Rates)
                .WithOne(e => e.RateType)
                .HasForeignKey(e => e.RateTypeId);
            
            modelBuilder.Entity<Rate>().HasKey(e => e.RateId);
            modelBuilder.Entity<Rate>()
                .HasMany(e => e.RateValues)
                .WithOne(e => e.Rate)
                .HasForeignKey(e => e.RateId);

            modelBuilder.Entity<Rate>()
                .HasMany(e => e.MeterRates)
                .WithOne(e => e.Rate)
                .HasForeignKey(e => e.RateId);


            modelBuilder.Entity<RateValue>().HasKey(e => e.RateId);
        }

    }

}
