using CompaniesDataAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace CompaniesDataAPI.DAL
{
    public class DataInitializer : CreateDatabaseIfNotExists<CompanyDBContext>
    {
        protected override void Seed(CompanyDBContext context)
        {
            context.Companies.AddOrUpdate(x => x.ID,
                new Company() { ID = 1, CompanyName = "Apple Inc.", Exchange = "NASDAQ", Ticker = "APPL", ISIN = "US0378331005", WebsiteURL = "http://www.apple.com", CreationDate = DateTime.Now, UpdateTime = DateTime.Now },
                new Company() { ID = 2, CompanyName = "British Airways Plc", Exchange = "Pink Sheets", Ticker = "BAIRY", ISIN = "US1104193065", WebsiteURL = null, CreationDate = DateTime.Now, UpdateTime = DateTime.Now },
                new Company() { ID = 3, CompanyName = "Heineken NV", Exchange = "Euronext Amsterdam", Ticker = "HEIA", ISIN = "NL0000009165", WebsiteURL = null, CreationDate = DateTime.Now, UpdateTime = DateTime.Now },
                new Company() { ID = 4, CompanyName = "Panasonic Corp", Exchange = "Tokyo Stock Exchange", Ticker = "6752", ISIN = "JP3866800000", WebsiteURL = "http://www.panasonic.co.jp", CreationDate = DateTime.Now, UpdateTime = DateTime.Now },
                new Company() { ID = 5, CompanyName = "Porsche Automobil", Exchange = "Deutsche Börse", Ticker = "PAH3", ISIN = "DE000PAH0038", WebsiteURL = "https://www.porsche.com", CreationDate = DateTime.Now, UpdateTime = DateTime.Now }
            );


            //base.Seed(context);

            //List<Company> companies = new List<Company>
            //{
            //    new Company() { CompanyName = "Apple Inc.", Exchange = "NASDAQ", Ticker = "APPL", ISIN = "US0378331005", WebsiteURL = "http://www.apple.com", CreationDate = DateTime.Now, UpdateTime = DateTime.Now },
            //    new Company() { CompanyName = "British Airways Plc", Exchange = "Pink Sheets", Ticker = "BAIRY", ISIN = "US1104193065", WebsiteURL = null, CreationDate = DateTime.Now, UpdateTime = DateTime.Now },
            //    new Company() { CompanyName = "Heineken NV", Exchange = "Euronext Amsterdam", Ticker = "HEIA", ISIN = "NL0000009165", WebsiteURL = null, CreationDate = DateTime.Now, UpdateTime = DateTime.Now },
            //    new Company() { CompanyName = "Panasonic Corp", Exchange = "Tokyo Stock Exchange", Ticker = "6752", ISIN = "JP3866800000", WebsiteURL = "http://www.panasonic.co.jp", CreationDate = DateTime.Now, UpdateTime = DateTime.Now },
            //    new Company() { CompanyName = "Porsche Automobil", Exchange = "Deutsche Börse", Ticker = "PAH3", ISIN = "DE000PAH0038", WebsiteURL = "https://www.porsche.com", CreationDate = DateTime.Now, UpdateTime = DateTime.Now }
            //};

            //context.Companies.AddRange(companies);
            //context.SaveChanges();
        }
    }
}