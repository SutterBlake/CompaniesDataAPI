using CompaniesDataAPI.DAL;
using CompaniesDataAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompaniesDataAPI.Tests.DAL
{
    class TestDBContext : ICompanyDBContext
    {
        public TestDBContext()
        {
            this.Companies = new TestCompanyDbSet();
        }

        public DbSet<Company> Companies { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Company comp) { }
        public void Dispose() { }
    }
}
