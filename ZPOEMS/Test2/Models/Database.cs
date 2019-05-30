using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Test2.Models.DBModels;

namespace Test2.Models
{


    /*
     *
     *
     *This file is redundant and can be deleted
     *
     *
     */








    public class Database : DbContext
    {
        public Database() : base("DefaultConnection")
        {
        }

        public DbSet<Department> Departments { get; set; }
    }
}