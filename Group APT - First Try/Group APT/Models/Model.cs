using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Group_APT.Models;

namespace Group_APT.Models
{
    public class ExaminationContext : DbContext
    {
        public ExaminationContext(DbContextOptions<ExaminationContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Group_APT.Models.Department> Department { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
