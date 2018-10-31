using CompaniesDataAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompaniesDataAPI.Tests.DAL
{
    class TestCompanyDbSet : TestDbSet<Company>
    {
        public override Company Find(params object[] keyValues)
        {
            return this.SingleOrDefault(c => c.ID == (int)keyValues.Single());
        }
    }
}
