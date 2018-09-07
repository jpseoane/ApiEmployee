using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;


namespace EmployesService.Controllers
{
    public class EmployesController : ApiController
    {
        public IEnumerable<Employees> Get()
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Employees.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {

               var entity= entities.Employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,"La entidad numero " + entity.ID + " no se encontro");
                }

            }
        }


        public HttpResponseMessage Post([FromBody] Employees employees)
        {
            try
            {
                using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
                {
                    employeeDBEntities.Employees.Add(employees);
                    employeeDBEntities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employees);

                    message.Headers.Location = new Uri(Request.RequestUri + "/" + employees.ID.ToString());
                    return message;

                }
            }
            catch (Exception ex) {
              return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
              
            }
        }


        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities dBEntities = new EmployeeDBEntities())
                {
                    var entity = dBEntities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El empleado numero " + id + " no se encontro");
                    }
                    else
                    {
                        dBEntities.Employees.Remove(entity);
                        dBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

        public HttpResponseMessage Put(int id, [FromBody] Employees employees)
        {
            try
            {
                using (EmployeeDBEntities dBEntities = new EmployeeDBEntities())
                {
                    var entity = dBEntities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El empleado numero " + id + " no se encontro para ser actualizado");
                    }
                    else
                    {
                        entity.FirstName = employees.FirstName;
                        entity.LastName = employees.LastName;
                        entity.Gender = employees.Gender;
                        entity.Salary = employees.Salary;
                        dBEntities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }


    }
   
}
