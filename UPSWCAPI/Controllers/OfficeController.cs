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
using System.Globalization;
using UPSWCAPI.Model.UPSWCAPI.Model;

namespace UPSWCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class OfficeController : Controller
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
        public OfficeController(IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
        {
            Environment = _environment;
            Configuration = _configuration;
            _context = context;
            _dapper = dapper;
        }
        private readonly EnquiryDbContext _context;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("InsertEmpDetail")]
        public async Task<IActionResult> InsertEmpDetail()
        {
            var dbfilepath = "";
            var httpRequest = HttpContext.Request;
            var postedFile = httpRequest.Form.Files["postedFile"];
            var userData = httpRequest.Form["userData"];

            EmpMaster model = JsonConvert.DeserializeObject<EmpMaster>(userData);

            string contentPath = this.Environment.ContentRootPath;
            string path = Path.Combine(contentPath, "Uploads/UserProfile");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (postedFile != null && postedFile.Length > 0)
            {
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
                var parameters = new DynamicParameters();

                parameters.Add("@UserId", model.userId);
                parameters.Add("@EmployementId", model.employementId);
                parameters.Add("@OfficeId", model.officeId);
                parameters.Add("@WTypeId", model.wTypeId);
                parameters.Add("@EmpName", model.empName);
                parameters.Add("@Sex", model.sex);
                parameters.Add("@Married", model.married);
                parameters.Add("@FatherName", model.fatherName);
                parameters.Add("@DistrictId", model.districtId);
                parameters.Add("@PermAddress", model.permAddress);
                parameters.Add("@PostAddress", model.postAddress);
                parameters.Add("@EmpQualification", model.empQualification);
                parameters.Add("@DepartmentID", model.departmentID);
                parameters.Add("@BankId", model.bankId);
                parameters.Add("@BranchId", model.branchId);
                parameters.Add("@UANNO", model.uanno);
                parameters.Add("@AccountNo", model.accountNo);
                parameters.Add("@PANNo", model.panNo);
                parameters.Add("@CategoryId", model.categoryId);
                parameters.Add("@DesignationId", model.designationId);
                parameters.Add("@NPSNo", model.npsNo);
                parameters.Add("@Nominee", model.nominee);
                parameters.Add("@RelationId", model.relationId);
                parameters.Add("@MobileNo", model.mobileNo);
                parameters.Add("@EmailId", model.emailId);
                //parameters.Add("@DOB", model.dOB);
                parameters.Add("@DOB", string.IsNullOrWhiteSpace(model.dOB) ? DBNull.Value : DateTime.ParseExact(model.dOB, "yyyy-MM-dd", CultureInfo.InvariantCulture));
                parameters.Add("@DOJ", string.IsNullOrWhiteSpace(model.doj) ? DBNull.Value : DateTime.ParseExact(model.doj, "yyyy-MM-dd", CultureInfo.InvariantCulture));
                parameters.Add("@DOD", model.dod);
                //parameters.Add("@DOD", string.IsNullOrWhiteSpace(model.dod) ? DBNull.Value : DateTime.ParseExact(model.dod, "yyyy-MM-dd", CultureInfo.InvariantCulture));
                parameters.Add("@AdharNo", model.adharNo);
                parameters.Add("@Remarks", model.remarks);
                parameters.Add("@Photo", model.photo);
                parameters.Add("@GPFTypeId", model.gpfTypeId);
                parameters.Add("@GPFCode", model.gpfCode);
                parameters.Add("@GISCode", model.gisCode);
                parameters.Add("@EPFCode", model.epfCode);
                parameters.Add("@GradePayId", model.gradePayId);
                parameters.Add("@LevelID", model.levelID);
                parameters.Add("@IncrementId", model.incrementId);
                parameters.Add("@BasicSalary", model.basicSalary);
                parameters.Add("@MA", model.ma);
                parameters.Add("@WA", model.wa);
                parameters.Add("@CCA", model.cca);
                parameters.Add("@HRA", model.hra);
                parameters.Add("@SalaryStatus", model.salaryStatus);
                parameters.Add("@EmployeeStatus", model.employeeStatus);
                parameters.Add("@BloodGroupId", model.bloodgroupId);
                parameters.Add("@EmergencyNo", model.emergencyNo);
                parameters.Add("@AuthSignatory", model.authSignatory);
                parameters.Add("@EmpCode", model.empCode);
                parameters.Add("@CasteId", model.casteId);
                parameters.Add("@IPAddress", model.ipAddress);
                parameters.Add("@ContractValidity", model.contractvalidity);
                parameters.Add("@ReligionId", model.religionId);
                parameters.Add("@OfficeDOJ", string.IsNullOrWhiteSpace(model.officeDOJ) ? DBNull.Value : DateTime.ParseExact(model.officeDOJ, "yyyy-MM-dd", CultureInfo.InvariantCulture));
                parameters.Add("@IncDt", model.incDt);
                parameters.Add("@IsEPF", model.isEPF);
                parameters.Add("@IsESIC", model.isESIC);
                parameters.Add("@IsPPF", model.isPPF);
                parameters.Add("@ServiceQuota", model.serviceQuota);
                parameters.Add("@RecruitmentMode", model.recruitmentMode);
                parameters.Add("@PFMSCode", model.pfmsCode); 
                parameters.Add("@IsPenCon", model.isPenCon);
                parameters.Add("@IsNPSCon", model.isNPSCon);
                parameters.Add("@ESICCode", model.esicCode);
                parameters.Add("@SubDeptID", model.subDeptID);
                parameters.Add("@DptEmpCode", model.dptEmpCode);
                parameters.Add("@PreEmpId", model.preEmpId);
                parameters.Add("@IFSCCode", model.ifscCode);
                parameters.Add("@HeadId", model.headId);
                parameters.Add("@PayCommissionId", model.payCommissionId);
                parameters.Add("@PayScaleID", model.payScaleID);
                parameters.Add("@SourceId", model.sourceId);
                parameters.Add("@OrderNo", model.orderNo);
                parameters.Add("@OrderDt", model.orderDt);
                parameters.Add("@GPFAc", model.gpfAc);
                parameters.Add("@CPFAc", model.cpfAc);
               


                //parameters.Add("@StateId", model.stateId);
                //parameters.Add("@IncrementCode", model.incrementCode);
                //parameters.Add("@DOR", model.dor);
                //parameters.Add("@PPONo", model.ppoNo);
                //parameters.Add("@IsLock", model.isLock);
                //parameters.Add("@EntryDate", model.entryDate);
                //parameters.Add("@UpdateDate", model.updateDate);
                //parameters.Add("@OrderRemarks", model.orderRemarks);
                //parameters.Add("@LastIncDate", model.lastIncDate);

                parameters.Add("@EmpCode", dbType: DbType.String, direction: ParameterDirection.Output, size: 15);

                await connection.ExecuteAsync("[dbo].[SP_EmpDetail_Insert]", parameters, commandType: CommandType.StoredProcedure);

                string empCode = parameters.Get<string>("@EmpCode");

                return Ok(new { success = true, empCode });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpPost("GetEmployeeList")]
        public async Task<IActionResult> GetEmployeeList([FromBody] UpdateEmp model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@OfficeId", model.officeId);
                parameters.Add("@WtypeId", model.wTypeId); 
                parameters.Add("@EmployementId", model.employementId);
                parameters.Add("@DepartmentId", model.departmentId);
                parameters.Add("@DesignationId", model.designationId);
                parameters.Add("@DptEmpCode", model.dptEmpCode ?? string.Empty);
                parameters.Add("@EmpName", model.empName ?? string.Empty);
                parameters.Add("@IsLock", model.isLock ?? "N");
                parameters.Add("@SubDeptId", model.subDeptId);
                parameters.Add("@PPONo", model.ppoNo ?? string.Empty);
                parameters.Add("@OrderBy", model.orderBy);
                parameters.Add("@EmpId", model.empId);
                parameters.Add("@ProcId", 1);

                var result = await connection.QueryAsync<dynamic>(
                    "Proc_GetEmployeeList",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet("GetEmpDetail")]
        public async Task<IActionResult> GetEmpDetail([FromQuery] int procId, [FromQuery] int empId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var dbparams = new DynamicParameters();
                dbparams.Add("ProcId", procId);
                dbparams.Add("EmpId", empId);
                dbparams.Add("IsLock", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                var user = await connection.QueryFirstOrDefaultAsync<UpdateEmpMaster>(
                    "[dbo].[Proc_GetEmployeeList]", dbparams, commandType: CommandType.StoredProcedure);

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


        [HttpPost("UpdateEmpDetail")]
        public async Task<IActionResult> UpdateEmpDetail()
        {
            var dbfilepath = "";
            var httpRequest = HttpContext.Request;
            var postedFile = httpRequest.Form.Files["postedFile"];
            var userData = httpRequest.Form["userData"];

            UpdateEmpMaster model = JsonConvert.DeserializeObject<UpdateEmpMaster>(userData);

            string contentPath = this.Environment.ContentRootPath;
            string path = Path.Combine(contentPath, "Uploads/UserProfile");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (postedFile != null && postedFile.Length > 0)
            {
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
                if (model.EmpId > 0)
                {
                    using var connection = _context.Database.GetDbConnection();
                    await connection.OpenAsync();
                    var parameters = new DynamicParameters();

                    parameters.Add("@EmpId", model.EmpId);
                    parameters.Add("@UserId", model.userId);
                    parameters.Add("@EmployementId", model.employementId);
                    parameters.Add("@OfficeId", model.officeId);
                    parameters.Add("@WTypeId", model.wTypeId);
                    parameters.Add("@EmpName", model.empName);
                    parameters.Add("@Sex", model.sex);
                    parameters.Add("@Married", model.married);
                    parameters.Add("@FatherName", model.fatherName);
                    parameters.Add("@DistrictId", model.districtId);
                    parameters.Add("@PermAddress", model.permAddress);
                    parameters.Add("@PostAddress", model.postAddress);
                    parameters.Add("@EmpQualification", model.empQualification);
                    parameters.Add("@DepartmentID", model.departmentID);
                    parameters.Add("@BankId", model.bankId);
                    parameters.Add("@BranchId", model.branchId);
                    parameters.Add("@UANNO", model.uanno);
                    parameters.Add("@AccountNo", model.accountNo);
                    parameters.Add("@PANNo", model.panNo);
                    parameters.Add("@CategoryId", model.categoryId);
                    parameters.Add("@DesignationId", model.designationId);
                    parameters.Add("@NPSNo", model.npsNo);
                    parameters.Add("@Nominee", model.nominee);
                    parameters.Add("@RelationId", model.relationId);
                    parameters.Add("@MobileNo", model.mobileNo);
                    parameters.Add("@EmailId", model.emailId);
                    //parameters.Add("@DOB", model.dOB);
                    parameters.Add("@DOB", model.dOB);
                    parameters.Add("@DOJ", model.doj);
                    parameters.Add("@DOR", model.dor);
                    parameters.Add("@DOD", model.dod);
                    //parameters.Add("@DOD", string.IsNullOrWhiteSpace(model.dod) ? DBNull.Value : DateTime.ParseExact(model.dod, "yyyy-MM-dd", CultureInfo.InvariantCulture));
                    parameters.Add("@AdharNo", model.adharNo);
                    parameters.Add("@Remarks", model.remarks);
                    parameters.Add("@Photo", model.photo);
                    parameters.Add("@GPFTypeId", model.gpfTypeId);
                    parameters.Add("@GPFCode", model.gpfCode);
                    parameters.Add("@GISCode", model.gisCode);
                    parameters.Add("@EPFCode", model.epfCode);
                    parameters.Add("@GradePayId", model.gradePayId);
                    parameters.Add("@LevelID", model.levelID);
                    parameters.Add("@IncrementId", model.incrementId);
                    parameters.Add("@BasicSalary", model.basicSalary);
                    parameters.Add("@MA", model.ma);
                    parameters.Add("@WA", model.wa);
                    parameters.Add("@CCA", model.cca);
                    parameters.Add("@HRA", model.hra);
                    parameters.Add("@SalaryStatus", model.salaryStatus);
                    parameters.Add("@EmployeeStatus", model.employeeStatus);
                    parameters.Add("@BloodGroupId", model.bloodgroupId);
                    parameters.Add("@EmergencyNo", model.emergencyNo);
                    parameters.Add("@AuthSignatory", model.authSignatory);
                    parameters.Add("@CasteId", model.casteId);
                    parameters.Add("@IPAddress", model.ipAddress);
                    parameters.Add("@ContractValidity", model.contractvalidity);
                    parameters.Add("@ReligionId", model.religionId);
                    parameters.Add("@OfficeDOJ", model.officeDOJ);
                    parameters.Add("@IncDt", model.incDt);
                    parameters.Add("@IsEPF", model.isEPF);
                    parameters.Add("@IsESIC", model.isESIC);
                    //parameters.Add("@IsPPF", model.isPPF);
                    parameters.Add("@ServiceQuota", model.serviceQuota);
                    parameters.Add("@RecruitmentMode", model.recruitmentMode);
                    parameters.Add("@PFMSCode", model.pfmsCode);
                    parameters.Add("@IsPenCon", model.isPenCon);
                    parameters.Add("@IsNPSCon", model.isNPSCon);
                    parameters.Add("@IsLock", model.isLock);
                    parameters.Add("@IsPPF", model.isPPF);
                    parameters.Add("@ESICCode", model.esicCode);
                    parameters.Add("@SubDeptID", model.subDeptID);
                    parameters.Add("@DptEmpCode", model.dptEmpCode);
                    parameters.Add("@PreEmpId", model.preEmpId);
                    parameters.Add("@IFSCCode", model.ifscCode);
                    parameters.Add("@HeadId", model.headId);
                    parameters.Add("@PayCommissionId", model.payCommissionId);
                    parameters.Add("@PayScaleID", model.payScaleID);
                    parameters.Add("@SourceId", model.sourceId);
                    parameters.Add("@OrderNo", model.orderNo);
                    parameters.Add("@OrderDt", model.orderDt);
                    parameters.Add("@GPFAc", model.gpfAc);
                    parameters.Add("@CPFAc", model.cpfAc);



                    //parameters.Add("@StateId", model.stateId);
                    //parameters.Add("@IncrementCode", model.incrementCode);
                    //parameters.Add("@DOR", model.dor);
                    //parameters.Add("@PPONo", model.ppoNo);
                    //parameters.Add("@IsLock", model.isLock);
                    //parameters.Add("@EntryDate", model.entryDate);
                    //parameters.Add("@UpdateDate", model.updateDate);
                    //parameters.Add("@OrderRemarks", model.orderRemarks);
                    //parameters.Add("@LastIncDate", model.lastIncDate);
                    //parameters.Add("@EmpCode", dbType: DbType.String, direction: ParameterDirection.Output, size: 15);

                    await connection.ExecuteAsync("[dbo].[SP_EmpDetail_Update]", parameters, commandType: CommandType.StoredProcedure);

                    //  string empCode = parameters.Get<string>("@EmpCode");

                    return Ok(new { success = true });
                }
                else
                {
                    return BadRequest("EmpId is missing");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        #region Allowance&Deduction

        [HttpGet("GetEmpDetailById/{empId}")]
        public async Task<IActionResult> GetEmpDetailById(int empId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1); // 1 for GetById
                parameters.Add("@EmpId", empId);

                var result = await connection.QueryFirstOrDefaultAsync<EmpMaster>(
                    "Proc_EarningAndDeduction",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                if (result == null)
                {
                    return NotFound(new { success = false, message = "Employee not found." });
                }

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpGet("GetEmpDetailByDptEmpCode/{DptEmpCode}")]
        public async Task<IActionResult> GetEmpDetailByDptEmpCode(string DptEmpCode)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3); // 3 for GetByDptEmpCode
                parameters.Add("@DptEmpCode", DptEmpCode);

                var result = await connection.QueryFirstOrDefaultAsync<EmpMaster>(
                    "Proc_EarningAndDeduction",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                if (result == null)
                {
                    return NotFound(new { success = false, message = "Employee not found." });
                }

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("InsertEmpAllowance")]
        public async Task<IActionResult> InsertEmpAllowance([FromBody] EmpAllowanceModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@EmpId", model.EmpId);
                parameters.Add("@GradePay", model.GradePay);
                parameters.Add("@DAPer", model.DAPer);
                parameters.Add("@IR", model.IR);
                parameters.Add("@PerPay", model.PerPay);
                parameters.Add("@Bla", model.Bla);
                parameters.Add("@Other", model.Other);
                parameters.Add("@LWP", model.LWP);
                parameters.Add("@LevelC", model.LevelC);
                parameters.Add("@Arrear", model.Arrear);
                parameters.Add("@PayFixation", model.PayFixation);
                parameters.Add("@HeavyDuty", model.HeavyDuty);
                parameters.Add("@MiscAmt1", model.MiscAmt1);
                parameters.Add("@MiscDesc1", model.MiscDesc1 ?? string.Empty);
                parameters.Add("@MiscAmt2", model.MiscAmt2);
                parameters.Add("@MiscDesc2", model.MiscDesc2 ?? string.Empty);
                parameters.Add("@MiscAmt3", model.MiscAmt3);
                parameters.Add("@MiscDesc3", model.MiscDesc3 ?? string.Empty);
                parameters.Add("@MiscAmt4", model.MiscAmt4);
                parameters.Add("@MiscDesc4", model.MiscDesc4 ?? string.Empty);
                parameters.Add("@DAStatus", model.DAStatus);

                await connection.ExecuteAsync("[dbo].[SP_EmpAllowance_Insert]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Employee allowance inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateEmpAllowance")]
        public async Task<IActionResult> UpdateEmpAllowance([FromBody] EmpAllowanceModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2); // Update operation
                parameters.Add("@EmpId", model.EmpId);
                parameters.Add("@GradePay", model.GradePay);
                parameters.Add("@DAPer", model.DAPer);
                parameters.Add("@IR", model.IR);
                parameters.Add("@PerPay", model.PerPay);
                parameters.Add("@Bla", model.Bla);
                parameters.Add("@Other", model.Other);
                parameters.Add("@LWP", model.LWP);
                parameters.Add("@LevelC", model.LevelC);
                parameters.Add("@Arrear", model.Arrear);
                parameters.Add("@PayFixation", model.PayFixation);
                parameters.Add("@HeavyDuty", model.HeavyDuty);
                parameters.Add("@MiscAmt1", model.MiscAmt1);
                parameters.Add("@MiscDesc1", model.MiscDesc1 ?? string.Empty);
                parameters.Add("@MiscAmt2", model.MiscAmt2);
                parameters.Add("@MiscDesc2", model.MiscDesc2 ?? string.Empty);
                parameters.Add("@MiscAmt3", model.MiscAmt3);
                parameters.Add("@MiscDesc3", model.MiscDesc3 ?? string.Empty);
                parameters.Add("@MiscAmt4", model.MiscAmt4);
                parameters.Add("@MiscDesc4", model.MiscDesc4 ?? string.Empty);
                parameters.Add("@DAStatus", model.DAStatus);

                await connection.ExecuteAsync("[dbo].[SP_EmpAllowance_Insert]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Employee allowance updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }



        [HttpPost("InsertEmpDeduction")]
        public async Task<IActionResult> InsertEmpDeduction([FromBody] EmpDeductionModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@EmpId", model.EmpId);
                parameters.Add("@CPF", model.CPF);
                parameters.Add("@Vol", model.Vol);
                // parameters.Add("@CPFAdv", model.CPFAdv);
                parameters.Add("@NSC", model.NSC);
                parameters.Add("@LIC", model.LIC);
                parameters.Add("@GI", model.GI);
                parameters.Add("@ITax", model.ITax);
                // parameters.Add("@SCCar", model.SCCar);
                // parameters.Add("@SCCarInt", model.SCCarInt);
                // parameters.Add("@BidAdv", model.BidAdv);
                // parameters.Add("@BidInt", model.BidInt);
                // parameters.Add("@HDFC", model.HDFC);
                parameters.Add("@RD", model.RD);
                // parameters.Add("@NameAC", model.NameAC);
                parameters.Add("@CoUnionInsurance", model.CoUnionInsurance);
                parameters.Add("@NSF", model.NSF);
                parameters.Add("@EleCharges", model.EleCharges);
                parameters.Add("@HatkarghaNigamAdv", model.HatkarghaNigamAdv);
                // parameters.Add("@FestivalAdv", model.FestivalAdv);
                // parameters.Add("@Genloan", model.Genloan);
                // parameters.Add("@GenLoanInt", model.GenLoanInt);
                // parameters.Add("@CALoan", model.CALoan);
                // parameters.Add("@CALoanInt", model.CALoanInt);
                parameters.Add("@MiscAmt1", model.MiscAmt1);
                parameters.Add("@MiscDesc1", model.MiscDesc1 ?? string.Empty);
                parameters.Add("@MiscAmt2", model.MiscAmt2);
                parameters.Add("@MiscDesc2", model.MiscDesc2 ?? string.Empty);
                parameters.Add("@MiscAmt3", model.MiscAmt3);
                parameters.Add("@MiscDesc3", model.MiscDesc3 ?? string.Empty);
                parameters.Add("@MiscAmt4", model.MiscAmt4);
                parameters.Add("@MiscDesc4", model.MiscDesc4 ?? string.Empty);

                await connection.ExecuteAsync("[dbo].[SP_EmpDeduction_Insert]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Employee deduction inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateEmpDeduction")]
        public async Task<IActionResult> UpdateEmpDeduction([FromBody] EmpDeductionModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2); // Update mode
                parameters.Add("@EmpId", model.EmpId);
                parameters.Add("@CPF", model.CPF);
                parameters.Add("@Vol", model.Vol);
                //  parameters.Add("@CPFAdv", model.CPFAdv);
                parameters.Add("@NSC", model.NSC);
                parameters.Add("@LIC", model.LIC);
                parameters.Add("@GI", model.GI);
                parameters.Add("@ITax", model.ITax);
                // parameters.Add("@SCCar", model.SCCar);
                // parameters.Add("@SCCarInt", model.SCCarInt);
                //  parameters.Add("@BidAdv", model.BidAdv);
                // parameters.Add("@BidInt", model.BidInt);
                // parameters.Add("@HDFC", model.HDFC);
                parameters.Add("@RD", model.RD);
                // parameters.Add("@NameAC", model.NameAC);
                parameters.Add("@CoUnionInsurance", model.CoUnionInsurance);
                parameters.Add("@NSF", model.NSF);
                parameters.Add("@EleCharges", model.EleCharges);
                parameters.Add("@HatkarghaNigamAdv", model.HatkarghaNigamAdv);
                // parameters.Add("@FestivalAdv", model.FestivalAdv);
                //  parameters.Add("@Genloan", model.Genloan);
                //  parameters.Add("@GenLoanInt", model.GenLoanInt);
                // parameters.Add("@CALoan", model.CALoan);
                //  parameters.Add("@CALoanInt", model.CALoanInt);
                parameters.Add("@MiscAmt1", model.MiscAmt1);
                parameters.Add("@MiscDesc1", model.MiscDesc1 ?? string.Empty);
                parameters.Add("@MiscAmt2", model.MiscAmt2);
                parameters.Add("@MiscDesc2", model.MiscDesc2 ?? string.Empty);
                parameters.Add("@MiscAmt3", model.MiscAmt3);
                parameters.Add("@MiscDesc3", model.MiscDesc3 ?? string.Empty);
                parameters.Add("@MiscAmt4", model.MiscAmt4);
                parameters.Add("@MiscDesc4", model.MiscDesc4 ?? string.Empty);

                await connection.ExecuteAsync("[dbo].[SP_EmpDeduction_Insert]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Employee deduction updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        #endregion


        #region OfficeandRegion 

        [HttpPost("InsertRegion")]
        public async Task<IActionResult> InsertRegion([FromBody] region model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@RegionId", model.RegionId);
                parameters.Add("@RegionName", model.RegionName ?? string.Empty);
                parameters.Add("@DivisionId", model.DivisionId);

                await connection.ExecuteAsync("[dbo].[Proc_Region]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Region inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateRegion")]
        public async Task<IActionResult> UpdateRegion([FromBody] region model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@RegionId", model.RegionId);
                parameters.Add("@RegionName", model.RegionName ?? string.Empty);
                parameters.Add("@DivisionId", model.DivisionId);

                await connection.ExecuteAsync("[dbo].[Proc_Region]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Region updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);

                var regions = await connection.QueryAsync("[dbo].[Proc_Region]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = regions });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetRegionById/{regionId}")]
        public async Task<IActionResult> GetRegionById(int regionId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@RegionId", regionId);

                var region = await connection.QueryFirstOrDefaultAsync("[dbo].[Proc_Region]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = region });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteRegion/{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@RegionId", id);

                await connection.ExecuteAsync("[dbo].[Proc_Region]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Region deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        //Office

        [HttpPost("InsertOffice")]
        public async Task<IActionResult> InsertOffice([FromBody] Office model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@RegionId", model.RegionId);
                parameters.Add("@DivisionId", model.DivisionId);
                parameters.Add("@OfficeName", model.OfficeName ?? string.Empty);
                //parameters.Add("@Status", model.Status ?? "A");
                parameters.Add("@OfficeCode", model.OfficeCode);
                //parameters.Add("@AgencyTypeId", model.AgencyTypeId);
                parameters.Add("@CreatedOn", DateTime.Now);

                await connection.ExecuteAsync("[dbo].[Proc_Office]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Office inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateOffice")]
        public async Task<IActionResult> UpdateOffice([FromBody] Office model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2); // 2 for update
                parameters.Add("@OfficeId", model.OfficeId);
                parameters.Add("@DivisionId", model.DivisionId);
                parameters.Add("@RegionId", model.RegionId);
                parameters.Add("@OfficeName", model.OfficeName ?? string.Empty);
                //parameters.Add("@Status", model.Status ?? "A");
                parameters.Add("@OfficeCode", model.OfficeCode ?? string.Empty);
                //parameters.Add("@AgencyCode", model.AgencyCode ?? string.Empty); // Include this if used in proc
                //parameters.Add("@AgencyTypeId", model.AgencyTypeId);
                parameters.Add("@UpdatedOn", DateTime.Now);

                await connection.ExecuteAsync("[dbo].[Proc_Office]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Office updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpGet("GetAllOffices")]
        public async Task<IActionResult> GetAllOffices()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);

                var offices = await connection.QueryAsync("[dbo].[Proc_Office]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = offices });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetOfficeById/{id}")]
        public async Task<IActionResult> GetOfficeById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@OfficeId", id);

                var office = await connection.QueryFirstOrDefaultAsync("[dbo].[Proc_Office]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = office });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpDelete("DeleteOffice/{id}")]
        public async Task<IActionResult> DeleteOffice(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@OfficeId", id);
                parameters.Add("@UpdatedOn", DateTime.Now);

                await connection.ExecuteAsync("[dbo].[Proc_Office]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Office deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        #endregion OffiecAndRegion


        #region SalaryGenerate
        [HttpPost("GenerateSalary")]
        public async Task<IActionResult> GenerateSalary([FromBody] SalaryGenerateRequest model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                model.TotalDays = DateTime.DaysInMonth(model.PayYear, model.PayMonth);
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@IPAddress", model.IPAddress);
                parameters.Add("@WtypeId", model.WtypeId);
                parameters.Add("@DepartmentId", model.DepartmentId);
                parameters.Add("@ESubDeptId", model.ESubDeptId);
                parameters.Add("@AllEmp", model.AllEmp);
                parameters.Add("@EmpId", model.EmpId ?? (object)DBNull.Value);
                parameters.Add("@PayYear", model.PayYear);
                parameters.Add("@PayMonth", model.PayMonth);
                parameters.Add("@Circleid", model.CircleId);
                parameters.Add("@Officeid", model.OfficeId);
                parameters.Add("@TotalDays", model.TotalDays);
                //parameters.Add("@TotalDays", DateTime.DaysInMonth(model.PayYear, model.PayMonth));

                parameters.Add("@outmsg", dbType: DbType.String, size: 250, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(
                    "SP_GenerateSalary",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                var outMsg = parameters.Get<string>("@outmsg");

                if (!string.Equals(outMsg, "success", StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest(new { message = outMsg });
                }

                return Ok(new { message = "Employee salary generated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        #endregion

        #region PaySlip
        [HttpGet("GetPayRegisterByEmpId/{empId}")]
        public async Task<IActionResult> GetPayRegisterByEmpId(int empId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4); // 4 for GetById
                parameters.Add("@EmpId", empId);

                var result = await connection.QueryFirstOrDefaultAsync<PayRegister>(
                    "SP_PayRegister",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                if (result == null)
                {
                    return NotFound(new { success = false, message = "Pay register data not found." });
                }

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        #endregion

        #region SalaryBillRpt
        [HttpPost("GetSalaryBillRpt")]
        public async Task<IActionResult> GetSalaryBillRpt([FromBody] SalaryRptRequest model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@OfficeId", model.OfficeId);
                parameters.Add("@EmpId", model.EmpId);
                parameters.Add("@PayYear", model.PayYear);
                parameters.Add("@PayMonth", model.PayMonth);
                parameters.Add("@DepartmentId", model.DepartmentId);
                parameters.Add("@SubDeptId", model.SubDeptId);
                parameters.Add("@DesignationId", model.DesignationId);
                parameters.Add("@Salarytype", model.Salarytype);
                parameters.Add("@ProcId", 1);
                

                var result = await connection.QueryAsync<dynamic>(
                    "Proc_SalaryRpt",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion
    }
}
