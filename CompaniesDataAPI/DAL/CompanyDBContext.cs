using CompaniesDataAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CompaniesDataAPI.DAL
{
    public class CompanyDBContext : DbContext
    {
        public CompanyDBContext()
        {
        }

        public DbSet<Company> Companies { get; set; }
    }
}