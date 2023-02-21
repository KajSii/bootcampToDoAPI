using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bootcamp.ToDoList.Backend.Database
{
    public class ApplicationContext : DbContext
    {
        
        public ApplicationContext(IConfiguration configuration, DbContextOptions<ApplicationContext> options)
            : base(options)
        { 
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    opts =>
                    {
                        opts.MigrationsAssembly("Bootcamp.ToDoList.Backend");
                    });
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Lists> Lists { get; set; }
    }
}