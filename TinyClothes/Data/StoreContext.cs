using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Models;

namespace TinyClothes.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            
        }

        // Add a DbSet for each entity that needs to be tracked by the database.
        public DbSet<Clothing> Clothing { get; set; }
        public DbSet<Account> Accounts { get; set; }
        /*
        interface IPayProvider
        {
            MakePayment();
        }

        public void MakePayment(IPayProvider pay)
        {
            pay.MakePayment(50.00);
        }
        */
    }
}
