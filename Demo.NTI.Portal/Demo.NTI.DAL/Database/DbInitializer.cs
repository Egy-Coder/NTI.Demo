using Demo.NTI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.DAL.Database
{
    public class DbInitializer
    {

        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {

            #region Test


            modelBuilder.Entity<Department>().ToTable("Departments")
            .HasData(
                        new Department { Id = 1, Name = "D1", Code = "D500" },
                        new Department { Id = 2, Name = "D2", Code = "D600" },
                        new Department { Id = 3, Name = "D3", Code = "D700" },
                        new Department { Id = 4,Name= "D800",Code = "D4" }
);

            #endregion

        }

    }
}
