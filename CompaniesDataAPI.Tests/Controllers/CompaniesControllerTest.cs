using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using CompaniesDataAPI.Controllers;
using CompaniesDataAPI.Models;
using CompaniesDataAPI.Tests.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompaniesDataAPI.Tests.Controllers
{
    [TestClass]
    public class CompaniesControllerTest : ApiController
    {
        [TestMethod]
        public void GetCompanies_ReturnAllCompanies()
        {
            // Arrange
            var context = new TestDBContext();
            context.Companies.Add(new Company() { ID = 1, CompanyName = "Apple Inc.", Exchange = "NASDAQ", Ticker = "APPL", ISIN = "US0378331005", WebsiteURL = "http://www.apple.com", CreationDate = DateTime.Now, UpdateTime = DateTime.Now });
            context.Companies.Add(new Company() { ID = 2, CompanyName = "British Airways Plc", Exchange = "Pink Sheets", Ticker = "BAIRY", ISIN = "US1104193065", WebsiteURL = null, CreationDate = DateTime.Now, UpdateTime = DateTime.Now });
            context.Companies.Add(new Company() { ID = 3, CompanyName = "Heineken NV", Exchange = "Euronext Amsterdam", Ticker = "HEIA", ISIN = "NL0000009165", WebsiteURL = null, CreationDate = DateTime.Now, UpdateTime = DateTime.Now });

            // Act
            var controller = new CompaniesController(context);
            var result = controller.GetCompanies() as TestCompanyDbSet;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void GetCompany_ReturnCompanyByID()
        {
            // Arrange
            var context = new TestDBContext();
            context.Companies.Add(new Company() { ID = 1, CompanyName = "Apple Inc.", Exchange = "NASDAQ", Ticker = "APPL", ISIN = "US0378331005", WebsiteURL = "http://www.apple.com", CreationDate = DateTime.Now, UpdateTime = DateTime.Now });
            context.Companies.Add(new Company() { ID = 2, CompanyName = "British Airways Plc", Exchange = "Pink Sheets", Ticker = "BAIRY", ISIN = "US1104193065", WebsiteURL = null, CreationDate = DateTime.Now, UpdateTime = DateTime.Now });
            context.Companies.Add(new Company() { ID = 3, CompanyName = "Heineken NV", Exchange = "Euronext Amsterdam", Ticker = "HEIA", ISIN = "NL0000009165", WebsiteURL = null, CreationDate = DateTime.Now, UpdateTime = DateTime.Now });

            // Act
            var controller = new CompaniesController(context);
            var result = controller.GetCompany(3) as OkNegotiatedContentResult<Company>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content.Exchange);
            Assert.AreEqual("Euronext Amsterdam", result.Content.Exchange);
        }

        [TestMethod]
        public void GetCompany_ReturnCompanyByISIN()
        {
            // Arrange
            var context = new TestDBContext();
            context.Companies.Add(new Company() { ID = 1, CompanyName = "Apple Inc.", Exchange = "NASDAQ", Ticker = "APPL", ISIN = "US0378331005", WebsiteURL = "http://www.apple.com", CreationDate = DateTime.Now, UpdateTime = DateTime.Now });
            context.Companies.Add(new Company() { ID = 2, CompanyName = "British Airways Plc", Exchange = "Pink Sheets", Ticker = "BAIRY", ISIN = "US1104193065", WebsiteURL = null, CreationDate = DateTime.Now, UpdateTime = DateTime.Now });
            context.Companies.Add(new Company() { ID = 3, CompanyName = "Heineken NV", Exchange = "Euronext Amsterdam", Ticker = "HEIA", ISIN = "NL0000009165", WebsiteURL = null, CreationDate = DateTime.Now, UpdateTime = DateTime.Now });

            // Act
            var controller = new CompaniesController(context);
            var result = controller.GetCompany(isin: "US0378331005") as OkNegotiatedContentResult<Company>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content.CompanyName);
            Assert.AreEqual("Apple Inc.", result.Content.CompanyName);
        }



        [TestMethod]
        public void PostCompany_ReturnSameCompany()
        {
            // Arrange
            var controller = new CompaniesController(new TestDBContext());

            // Act
            var comp = GetDemoCompany();
            var result = controller.PostCompany(comp) as CreatedAtRouteNegotiatedContentResult<Company>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content.CreationDate);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["ID"], result.Content.ID);
            Assert.AreEqual(result.Content.ISIN, comp.ISIN);
        }

        [TestMethod]
        public void PutCompany_ReturnStatusCode()
        {
            // Arrange
            var controller = new CompaniesController(new TestDBContext());

            // Act
            var comp = GetDemoCompany();
            var result = controller.PutCompany(comp.ID, comp) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void PutCompany_NonExistingID()
        {
            // Arrange
            var controller = new CompaniesController(new TestDBContext());

            // Act
            var badresult = controller.PutCompany(999, GetDemoCompany());

            // Assert
            Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void DeleteCompany_ReturnStatusCode()
        {
            // Arrange
            var controller = new CompaniesController(new TestDBContext());

            // Act
            var comp = GetDemoCompany();
            var result = controller.DeleteCompany(comp.ID) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.MethodNotAllowed, result.StatusCode);
        }

        Company GetDemoCompany()
        {
            return new Company() { ID = 1, CompanyName = "Microsoft", Exchange = "NASDAQ", Ticker = "MSFT", ISIN = "US5949181045", WebsiteURL = "https://www.microsoft.com/" };
        }
    }
}
