using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "Mia",
                    Email = "miayang@163.com"
                });
        }
    }
}
