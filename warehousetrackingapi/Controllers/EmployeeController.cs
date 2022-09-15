using Microsoft.AspNetCore.Mvc;
using warehousetrackingapi.Models;

namespace warehousetrackingapi.Controllers
{
    [Route("employee")]
    public class EmployeeController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("getEmployees")]
        public async Task<IEnumerable<EmployeeModel>> Get()
        {
            await using (var context = new Context())
            {
                return context.Employees.ToList();
            }
        }


        [HttpGet]
        [Route("getemployeebyid")]
        public async Task<EmployeeModel> GetEmployeeById(string Id)
        {
            await using (var context = new Context())
            {
                return context.Employees.Where(a => a.Guid.ToString() == Id).SingleOrDefault();

            }
        }

        [HttpGet]
        [Route("getemployeebyemployeeid")]
        public async Task<EmployeeModel> GetEmployeeByEmployeeId(string Id)
        {
            await using (var context = new Context())
            {
                return context.Employees.Where(a => a.Employeeid == Id && a.Active=="Active").SingleOrDefault();

            }
        }

        [HttpGet]
        [Route("getemployeebyrfid")]
        public async Task<EmployeeModel> GetEmployeeByRFID(string RFID)
        {
            await using (var context = new Context())
            {
                return context.Employees.Where(a => a.Tag == RFID).SingleOrDefault();

            }
        }



        [HttpPost]
        [Route("addemployee")]
        public async Task<ResponseModel> AddEmployee([FromBody] EmployeeModel EmployeeModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
                if (IsDataExist(EmployeeModel.Employeeid, ""))
                {

                    response.StatusCode = "200";
                    response.StatusMessage = "Employee already exist!";
                    return response;
                }
                else
                {
                    Guid guid = Guid.NewGuid();
                    EmployeeModel.Guid = guid;
                    EmployeeModel.Dateupdated = DateTime.Now;
                    EmployeeModel.Updatedby = "currentuser";
                    EmployeeModel.Datecreated = DateTime.Now;
                    EmployeeModel.Tag = EmployeeModel.Tag;
                    EmployeeModel.Createdby = "currentuser";
                    context.Employees.Add(EmployeeModel);
                    await context.SaveChangesAsync();
                    response.StatusCode = "200";
                    response.StatusMessage = "Employee successfully added!";
                    return response;
                }
            }

        }

        private Boolean IsDataExist(string EmployeeId, string Id)
        {
            using (var context = new Context())
            {

                var result = context.Employees.Where(a => a.Employeeid.ToLower() == EmployeeId.ToLower()).FirstOrDefault();

                if (result == null)
                {
                    return false;
                }
                else
                {
                    if (Id == "")
                    {
                        return true;
                    }
                    else
                    {
                        if (Id == result.Employeeid)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                }

            }

        }

        [HttpPost]
        [Route("updateemployee")]
        public async Task<ResponseModel> UpdateEmployee([FromBody] EmployeeModel EmployeeModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                await using (var context = new Context())
                {



                    var employee = context.Employees.Where(a => a.Guid == EmployeeModel.Guid).SingleOrDefault();

                    if (employee == null)
                    {
                        response.StatusCode = "200";
                        response.StatusMessage = "Invalid Id!";
                        return response;
                    }
                    else
                    {
                        if (IsDataExist(employee.Employeeid, EmployeeModel.Employeeid))
                        {

                            response.StatusCode = "200";
                            response.StatusMessage = "Employee already exist!";
                            return response;
                        }
                        else
                        {
                            employee.Dateupdated = DateTime.Now;
                            employee.Updatedby = "currentuser";
                            employee.Active = EmployeeModel.Active;
                            employee.Firstname = EmployeeModel.Firstname;
                            employee.Middlename = EmployeeModel.Middlename;
                            employee.Lastname = EmployeeModel.Lastname;
                            employee.Email = EmployeeModel.Email;
                            employee.Employeeid = EmployeeModel.Employeeid;
                            employee.Tag = EmployeeModel.Tag;

                            context.Employees.Update(employee).Property(x => x.Id).IsModified = false;
                            await context.SaveChangesAsync();
                            response.StatusCode = "200";
                            response.StatusMessage = "Employee successfully updated!";
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = "500";
                response.StatusMessage = "Something went wrong!";
                return response;
            }

        }

    }
}
