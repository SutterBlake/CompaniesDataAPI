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
        private CompanyDBContext db = new CompanyDBContext();

        
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
            if (!ValidateISINFormat(isin))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("ISIN field has not a properly format")
                });
            }

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
            if (!ValidateISINFormat(company.ISIN))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("ISIN field has not a properly format")
                });
            }

            company.CreationDate = db.Companies.AsNoTracking().FirstOrDefault<Company>(c => c.ID == id).CreationDate;
            company.UpdateTime = DateTime.Now;
            db.Entry(company).State = EntityState.Modified;

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
                    throw;
                }
            }

            return Ok(company);
            //return StatusCode(HttpStatusCode.NoContent);
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
            if (!ValidateISINFormat(company.ISIN))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("ISIN field has not a properly format")
                });
            }
            
            // Check if ISIN already exists in DB
            if (db.Companies.Any<Company>(c => c.ISIN == company.ISIN))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Found)
                {
                    Content = new StringContent("There is already an existing Company with this ISIN value")
                });
                //Todo: En el cliente web devolvemos el recurso almacenado en DB.
            }

            db.Companies.Add(company);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = company.ID }, company);
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(int id)
        {
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)
            {
                Content = new StringContent("This API does not allows anyone to execute DELETE statements")
            });
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
        private bool ValidateISINFormat(string isin)
        {
            // ISIN must begin with 2 alphabetic chars, and have a length of 12.
            if (!String.IsNullOrEmpty(isin) && ((Char.IsLetter(isin[0]) && Char.IsLetter(isin[1])) || isin.Length != 12))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}