using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployesService
{
    public class EmployeeSecurity
    {

        public static bool Login(String username, string password) {
            using (EmployeeDataAccess.EmployeeDBEntities entities = new EmployeeDataAccess.EmployeeDBEntities())
            {
                return entities.Users.Any(user=> user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password==password);

            }

        }
    }
}