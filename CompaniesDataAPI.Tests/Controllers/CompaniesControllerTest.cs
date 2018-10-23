using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CompaniesDataAPI.Controllers;
using CompaniesDataAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompaniesDataAPI.Tests.Controllers
{
    [TestClass]
    public class CompaniesControllerTest : ApiController
    {
        [TestMethod]
        public void GetTest()
        {
            // Arrange
            CompaniesController controller = new CompaniesController();

            // Act
            IList<Company> companies = null;
            companies = controller.GetCompanies().ToList();

            // Assert
            Assert.IsNotNull(companies);
            Assert.AreEqual(companies.Count(), 5);
            Assert.AreEqual(companies[0].CompanyName, "Apple Inc.");
            Assert.AreEqual(companies[0].ISIN, "US1104193065");
        }
    }
}
