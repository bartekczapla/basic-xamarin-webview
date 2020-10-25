using BasicXamarin.Contract.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicXamarin.Data.Seed
{
    public static class TestSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().HasData(new Test
            {
                Id = 1,
                Name = "Test 1"
            });
            modelBuilder.Entity<Test>().HasData(new Test
            {
                Id = 2,
                Name = "Test 2"
            });
            modelBuilder.Entity<Test>().HasData(new Test
            {
                Id = 3,
                Name = "Test 3"
            });
        }
    }
}
