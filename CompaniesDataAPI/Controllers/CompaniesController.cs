using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CompaniesDataAPI.DAL;
using CompaniesDataAPI.Models;

namespace CompaniesDataAPI.Controllers
{
    public class CompaniesController : ApiController
    {
        private ICompanyDBContext db = new CompanyDBContext();

        public CompaniesController() { }

        public CompaniesController(ICompanyDBContext context)
        {
            db = context;
        }


        // GET: api/Companies
        public IQueryable<Company> GetCompanies()
        {
            return db.Companies;
        }

        // GET: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompany(int id)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
                //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                //{
                //    Content = new StringContent("Company not found")
                //});
            }

            return Ok(company);
        }

        // GET: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompany(string isin)
        {
            ProperlyISINFormat(isin);

            Company company = db.Companies.FirstOrDefault<Company>(c => c.ISIN == isin);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/Companies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompany(int id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != company.ID)
            {
                return BadRequest();
            }

            // Validate ISIN
            if (!ProperlyISINFormat(company.ISIN))
                return StatusCode(HttpStatusCode.BadRequest);
            if (!CheckExistingISIN("put", company))
                return StatusCode(HttpStatusCode.BadRequest);

            company.CreationDate = db.Companies.FirstOrDefault(c => c.ID == id) != null 
                ? db.Companies.FirstOrDefault(c => c.ID == id).CreationDate
                : DateTime.Now;
            company.UpdateTime = DateTime.Now;
            //db.Entry(company).State = EntityState.Modified;
            //db.MarkAsModified(company);
            if (db.Companies.Any(c => c.ID == company.ID)) {
                db.Companies.Find(company.ID).CompanyName = company.CompanyName;
                db.Companies.Find(company.ID).Exchange = company.Exchange;
                db.Companies.Find(company.ID).Ticker = company.Ticker;
                db.Companies.Find(company.ID).ISIN = company.ISIN;
                db.Companies.Find(company.ID).WebsiteURL = company.WebsiteURL;
                db.Companies.Find(company.ID).CreationDate = company.CreationDate;
                db.Companies.Find(company.ID).UpdateTime = DateTime.Now;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }

            //return Ok(company);
            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/Companies
        [ResponseType(typeof(Company))]
        public IHttpActionResult PostCompany(Company company)
        {
            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            company.CreationDate = DateTime.Now;
            company.UpdateTime = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate ISIN
            if (!ProperlyISINFormat(company.ISIN))
                return StatusCode(HttpStatusCode.BadRequest);
            if (!CheckExistingISIN("post", company))
                return StatusCode(HttpStatusCode.Found);

            db.Companies.Add(company);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = company.ID }, company);
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(int id)
        {
            return StatusCode(HttpStatusCode.MethodNotAllowed);
            //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)
            //{
            //    Content = new StringContent("This API does not allows anyone to execute DELETE statements")
            //});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyExists(int id)
        {
            return db.Companies.Count(e => e.ID == id) > 0;
        }


        /// <summary>
        /// Validates that ISIN has a properly value.
        /// </summary>
        /// <param name="isin">ISIN of the Company.</param>
        /// <returns>True or false.</returns>
        private bool ProperlyISINFormat(string isin)
        {
            // ISIN must begin with 2 alphabetic chars, and have a length of 12.
            if (String.IsNullOrEmpty(isin) || !Char.IsLetter(isin[0]) || !Char.IsLetter(isin[1]) || isin.Length != 12)
            {
                return false;
                //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                //{
                //    Content = new StringContent("ISIN field has not a properly format")
                //});
            }
            return true;
        }

        /// <summary>
        /// Check ISIN is not going to be duplicated in DB.
        /// </summary>
        /// <param name="method">The method which is being used (PUT or POST).</param>
        /// <param name="company">Company to get updated.</param>
        private bool CheckExistingISIN(string method, Company company)
        {
            if (method.ToLower() == "put")
            {
                if (db.Companies.Where(c => c.ISIN == company.ISIN && c.ID != company.ID).Count() > 0)
                {
                    return false;
                    //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Found)
                    //{
                    //    Content = new StringContent("There is already an existing Company with this ISIN value")
                    //});
                }
            }
            else if (method.ToLower() == "post")
            {
                if (db.Companies.Any(c => c.ISIN == company.ISIN))
                {
                    return false;
                    //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Found)
                    //{
                    //    Content = new StringContent("There is already an existing Company with this ISIN value")
                    //});
                }
            }

            return true;
        }
    }
}