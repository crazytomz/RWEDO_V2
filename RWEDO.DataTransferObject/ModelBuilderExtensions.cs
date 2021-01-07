using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWEDO.DataTransferObject
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Surveyor>().HasData(
                    new Surveyor
                    {
                        ID = 1,
                        Name = "SAdmin",
                        IdentityNumber = "Master",
                        Email = "thomsonkvarkey@outlook.com",
                        ISDeactivated = false
                    }
                );            
            modelBuilder.Entity<Status>().HasData(
                    new Status
                    {
                        ID = 1,
                        Description = "Opened"
                    },
                    new Status
                    {
                        ID = 2,
                        Description = "File Received"
                    },
                    new Status
                    {
                        ID = 3,
                        Description = "File Received"
                    },
                    new Status
                    {
                        ID = 4,
                        Description = "Surveyed"
                    },
                    new Status
                    {
                        ID = 5,
                        Description = "Estimate Received"
                    },
                    new Status
                    {
                        ID = 6,
                        Description = "Bill Received"
                    },
                    new Status
                    {
                        ID = 7,
                        Description = "Report Prepared"
                    },
                    new Status
                    {
                        ID = 8,
                        Description = "Report Submitted"
                    }
                );
        }
    }
        
}
