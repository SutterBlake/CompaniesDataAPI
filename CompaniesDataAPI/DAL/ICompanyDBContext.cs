using CompaniesDataAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CompaniesDataAPI.DAL
{
    public interface ICompanyDBContext : IDisposable
    {
        DbSet<Company> Companies { get; }
        int SaveChanges();
    }
}