using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Group_APT.Models
{
    public class ExaminationContext : DbContext
    {
        public ExaminationContext(DbContextOptions<ExaminationContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<StudentUnit_Relationship> StudentUnitRelationships { get; set; }
    }
}
