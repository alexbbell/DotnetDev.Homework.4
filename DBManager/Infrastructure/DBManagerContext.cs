using Microsoft.EntityFrameworkCore;
using System.Configuration;

using System.ComponentModel.DataAnnotations;

namespace DBManager
{
    public class DBManagerContext : DbContext
    {
        private DBManagerContext _context;

        public DbSet<NewTable> NewTables { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments{ get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        


        public DBManagerContext(DbContextOptions<DBManagerContext> options) : base(options) {

            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connStr = ConfigurationManager.ConnectionStrings["postgreStr"].ToString();
            optionsBuilder.UseNpgsql(connStr);


        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<NewTable>().ToTable("newtable");
            modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<Payment>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Payment>().ToTable("Payments");

            modelBuilder.Entity<UserAccount>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserAccount>().ToTable("UserAccounts");

            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                    new User { Id = 1, Name = "Aleksei",  LastName = "Beliaev", MiddleName= "Borisovich", Birthdate = new DateTime(1981, 3, 14, 12, 0, 0), IsActive = true},
                    new User { Id = 2, Name = "Dina",  LastName = "Beliaeva", MiddleName= "Borisovna", Birthdate = new DateTime(1979, 8, 20, 12, 0, 0), IsActive = true},
                    new User { Id = 3, Name = "Ian",  LastName = "Beliaev", MiddleName= "Alekseevich", Birthdate = new DateTime(2020, 2, 25, 12, 0, 0), IsActive = true}, 
                    new User { Id = 4, Name = "Mark",  LastName = "Beliaev", MiddleName= "Alekseevich", Birthdate = new DateTime(2006, 12, 9, 12, 0, 0), IsActive = true}, 
                    new User { Id = 5, Name = "Marta",  LastName = "Beliaev", MiddleName= "Alekseevna", Birthdate = new DateTime(2009, 2, 27, 12, 0, 0), IsActive = true}
                   
                }
                );

            modelBuilder.Entity<UserAccount>().HasData(new UserAccount[]
            {
                new UserAccount { Id = 1,  Currency = "USD", DateCreation = "2021-12-14", UserId = 1},
                new UserAccount { Id = 2,  Currency = "EUR", DateCreation = "2021-12-14", UserId = 1},
                new UserAccount { Id = 3,  Currency = "RUB", DateCreation = "2021-12-14", UserId = 1},
                new UserAccount { Id = 4,  Currency = "USD", DateCreation = "2021-12-14", UserId = 2},
                new UserAccount { Id = 5,  Currency = "EUR", DateCreation = "2021-12-14", UserId = 2},
                new UserAccount { Id = 6,  Currency = "RUB", DateCreation = "2021-12-14", UserId = 2},
            });
            modelBuilder.Entity<Payment>().HasData(new Payment[] {
                new Payment { Id = 1,  AccountId = 1,  PaymentDate = DateTime.Now, PaymentSum = 200, PaymentType="in" },
                new Payment { Id = 2,  AccountId = 2,  PaymentDate = DateTime.Now, PaymentSum = 200, PaymentType="out" },
                new Payment { Id = 3,  AccountId = 2,  PaymentDate = DateTime.Now, PaymentSum = 200, PaymentType="in" },
                new Payment { Id = 4,  AccountId = 5,  PaymentDate = DateTime.Now, PaymentSum = 200, PaymentType="out" },
                new Payment { Id = 5,  AccountId = 1,  PaymentDate = DateTime.Now, PaymentSum = 200, PaymentType="in" },
                //new Payment {  Account = 1, PaymentId = 1, PaymentDate = DateTime.Now, PaymentSum = 200, PaymentType="in" },
            });

        }
    }
}
