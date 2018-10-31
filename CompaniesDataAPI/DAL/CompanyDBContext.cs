using CompaniesDataAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CompaniesDataAPI.DAL
{
    public class CompanyDBContext : DbContext, ICompanyDBContext
    {
        public CompanyDBContext() : base("CompaniesDataAPI")
        {
        }

        public DbSet<Company> Companies { get; set; }

    }
}