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
using WebApiRegular.Models;

namespace WebApiRegular.Controllers
{
    public class EmployeesApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EmployeesApi
        public IQueryable<Employees> GetEmployeeDB()
        {
            return db.EmployeeDB;
        }

        // GET: api/EmployeesApi/5
        [ResponseType(typeof(Employees))]
        public IHttpActionResult GetEmployees(int id)
        {
            Employees employees = db.EmployeeDB.Find(id);
            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // PUT: api/EmployeesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployees(int id, Employees employees)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employees.Id)
            {
                return BadRequest();
            }

            db.Entry(employees).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmployeesApi
        [ResponseType(typeof(Employees))]
        public IHttpActionResult PostEmployees(Employees employees)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeDB.Add(employees);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employees.Id }, employees);
        }

        // DELETE: api/EmployeesApi/5
        [ResponseType(typeof(Employees))]
        public IHttpActionResult DeleteEmployees(int id)
        {
            Employees employees = db.EmployeeDB.Find(id);
            if (employees == null)
            {
                return NotFound();
            }

            db.EmployeeDB.Remove(employees);
            db.SaveChanges();

            return Ok(employees);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeesExists(int id)
        {
            return db.EmployeeDB.Count(e => e.Id == id) > 0;
        }
    }
}