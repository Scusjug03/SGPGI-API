using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UPSWCAPI.Model;
using static UPSWCAPI.Model.EnquiryDbContext;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dapper;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using UPSWCAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using UPSWCAPI.Model.UPSWCAPI.Model;




namespace UPSWCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class EmployeeController : ControllerBase
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
        public EmployeeController(IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
        {
            Environment = _environment;
            Configuration = _configuration;
            _context = context;
            _dapper = dapper;
        }

        private readonly EnquiryDbContext _context;

        [HttpGet("getEmployees")]
        public IActionResult getEmployee()
        {
            var list = _context.Employes.ToList();
            return Ok(list);
        }

        [HttpPost("CreateNewEmployee")]
        public IActionResult CreateNewEmployee(EmployeeViewModel obj)
        {
            Employee _emp = new Employee()
            {
                address = obj.address,
                city = obj.city,
                contactNo = obj.contactNo,
                emailid = obj.emailid,
                name = obj.name,
                projectName = obj.projectName
            };
            _context.Employes.Add(_emp);
            _context.SaveChanges();

            foreach (var item in obj.employeeFamilies)
            {
                EmployeeFamily _empFamily = new EmployeeFamily()
                {
                    age = item.age,
                    employeeid = _emp.EmployeeId,
                    name = item.name,
                    relation = item.relation,
                };
                _context.EmployeeFamilies.Add(_empFamily);
                _context.SaveChanges();
            }
            return Created("Employee Created", _emp);

        }


        
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser()
        {
            var dbfilepath = "";
            var httpRequest = HttpContext.Request;
            var postedFile = httpRequest.Form.Files["postedFile"];
            var userData = httpRequest.Form["userData"];

            UserProfile model = JsonConvert.DeserializeObject<UserProfile>(userData);

            string contentPath = this.Environment.ContentRootPath;
            string path = Path.Combine(contentPath, "Uploads/UserProfile");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (postedFile != null && postedFile.Length > 0)
            {
                // Save new file
                string fileName = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await postedFile.CopyToAsync(stream);
                }

                model.photo = "/Uploads/UserProfile/" + fileName;
            }


            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var dbparams = new DynamicParameters();
                dbparams.Add("EmpId", model.empId);
                dbparams.Add("EmpName", model.empName);
                dbparams.Add("FatherName", model.fatherName);
                dbparams.Add("CategoryId", model.categoryId);
                dbparams.Add("Gender", model.gender);
                dbparams.Add("DOB", model.dOB);
                dbparams.Add("MobileNo", model.mobileNo);
                dbparams.Add("IsBoardEmployee", model.isBoardEmployee);
                dbparams.Add("Email", model.email);
                dbparams.Add("DepartmentId", model.departmentId);
                dbparams.Add("DesignationId", model.designationId);
                dbparams.Add("Address", model.address);
                dbparams.Add("StateId", model.stateId);
                dbparams.Add("CityId", model.cityId);
                dbparams.Add("PinCode", model.pinCode);
                dbparams.Add("Photo", model.photo);
                dbparams.Add("ProcId", model.procId);
                dbparams.Add("Msg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("[dbo].[Proc_EmpDetail]", dbparams, commandType: CommandType.StoredProcedure);

                var message = dbparams.Get<string>("Msg");

                return Ok(new
                {
                    success = true,
                    message = message ?? "User inserted successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error inserting user",
                    error = ex.Message
                });
            }
        }

        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile([FromQuery] int procId, [FromQuery] int empId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var dbparams = new DynamicParameters();
                dbparams.Add("ProcId", procId);
                dbparams.Add("EmpId", empId);
                dbparams.Add("Msg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                var user = await connection.QueryFirstOrDefaultAsync<UserProfile>(
                    "[dbo].[Proc_EmpDetail]", dbparams, commandType: CommandType.StoredProcedure);

                if (user == null)
                {
                    return NotFound(new { success = false, message = "User not found" });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error retrieving user", error = ex.Message });
            }
        }


        [HttpGet("GetUserReport")]
        public async Task<IActionResult> GetUserReport(int empId = 0)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("EmpId", empId);
                parameters.Add("ProcId", 4); // Report-specific
                parameters.Add("Msg", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);
                var result = await connection.QueryAsync<dynamic>(
                    "Proc_EmpDetail",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Failed to fetch report",
                    error = ex.Message
                });
            }
        }
        
        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] int procId, [FromQuery] int empId)

        {
            if (procId != 3 || empId <= 0)
                return BadRequest("Invalid request.");

            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", procId);
                parameters.Add("@EmpId", empId);
                parameters.Add("@Msg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("Proc_EmpDetail", parameters, commandType: CommandType.StoredProcedure);

                var outputMsg = parameters.Get<string>("@Msg");

                return Ok(new { success = true, message = outputMsg ?? "User marked as deleted." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error deleting user", error = ex.Message });
            }
        }





        [HttpPost("ShowMultipleApplicant")]
        public async Task<IActionResult> ShowMultipleApplicant(int categoryId = 0, int departmentId = 0)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("CategoryId", categoryId);
                parameters.Add("DepartmentId", departmentId);
                parameters.Add("ProcId", 6);
                parameters.Add("Msg", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

                var result = await connection.QueryAsync<dynamic>(
                    "Proc_EmpDetail",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Failed to load applicants",
                    error = ex.Message
                });
            }
        }

        [HttpPost("SaveSelectedApplicants")]
        public async Task<IActionResult> SaveSelectedApplicants([FromBody] UserProfile model)
        {
            try
            {
                var selectedEmployees = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(model.JSONApproval)
                      ?? new List<Dictionary<string, int>>();

                XElement xmlAmend = new XElement("Amend",
                        selectedEmployees.Select(emp =>
                            new XElement("XMLAmend",
                                new XElement("EmpId", emp["EmpId"])
                            )
                        )
                    );
                string xmlString = xmlAmend.ToString();

                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@EmpName", model.empName);
                parameters.Add("@FatherName", model.fatherName);
                parameters.Add("@CategoryId", model.categoryId);
                parameters.Add("@Gender", model.gender);
                parameters.Add("@Email", model.email);
                parameters.Add("@DepartmentId", model.departmentId);
                parameters.Add("@DesignationId", model.designationId);
                parameters.Add("@XMLApproval", xmlString);
                parameters.Add("@ProcId", 7);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                await connection.ExecuteAsync("Proc_EmpDetail", parameters, commandType: CommandType.StoredProcedure);
                var message = parameters.Get<string>("@Msg");
                return Ok(new { success = true, message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }



        }


        [HttpPost("UserPermissionMenuList")]
        public async Task<IActionResult> UserPermissionMenuList(int moduleId = 0, int menuId = 0)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("ModuleId", moduleId);
                parameters.Add("MenuId", menuId);
                parameters.Add("ProcId", 1);
                parameters.Add("Msg", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

                var result = await connection.QueryAsync<dynamic>(
                    "SP_UserPermission",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Failed to load applicants",
                    error = ex.Message
                });
            }
        }


        [HttpPost("SaveUserPermissions")]
        public async Task<IActionResult> SaveUserPermissions([FromBody] PermissionRequest request)
        {
            try
            {
                // Convert List<PermissionItem> to XML
                XElement xmlPermissions = new XElement("PermissionList",
                    request.Items.Select(item =>
                        new XElement("Row",
                            new XElement("EpmId", item.EmpId),
                            new XElement("ProjectId", item.ProjectId),
                            new XElement("ModuleId", item.ModuleId),
                            new XElement("MenuId", item.MenuId)
                        )
                    )
                );

                string xmlString = xmlPermissions.ToString();

                // Create parameters
                var parameters = new DynamicParameters();
                parameters.Add("@Id", request.Id);
                parameters.Add("@ProjectId", request.ProjectId);
                parameters.Add("@ModuleId", request.ModuleId);
                parameters.Add("@MenuId", request.MenuId);
                parameters.Add("@ProcId", 2); // Use ProcId=2 for saving
                parameters.Add("@Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                parameters.Add("@XML", xmlString);

                // Open connection and execute stored procedure
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                await connection.ExecuteAsync("SP_UserPermission", parameters, commandType: CommandType.StoredProcedure);

                var message = parameters.Get<string>("@Msg");
                return Ok(new { success = true, message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }





    }
}
