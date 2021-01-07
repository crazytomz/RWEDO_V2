using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RWEDO.DataTransferObject
{
    public class RWEDODbContext : IdentityDbContext<ApplicationUser>
    {
        public RWEDODbContext(DbContextOptions<RWEDODbContext> options)
            : base(options)
        { }
        public DbSet<Surveyor> Surveyors { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<SurveyFile> SurveyFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
