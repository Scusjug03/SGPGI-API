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
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace UPSWCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]

    public class SGPGIController : Controller
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
        public SGPGIController(IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
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
            return Ok("SGPGI API is running.");
        }



        [HttpPost("save-employee")]
        public async Task<IActionResult> SaveEmployee([FromBody] EmpMasterDto model)
        {
            if (model == null)
                return BadRequest(new { success = false, message = "Request body is null." });

            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var p = new DynamicParameters();

                // Required control parameter
                p.Add("@procId", 1);

                // Key & audit
                p.Add("@ECODE", model.ECODE);
                p.Add("@LoginUser", model.LoginUser);

                // — Explicit safe mapping: add only SP parameters (map as many as needed)
                p.Add("@EmployementId", model.EmployementId);
                p.Add("@WTypeId", model.WTypeId);

                p.Add("@EMP_NAME", model.EMP_NAME);
                p.Add("@FATH_NAME", model.FATH_NAME);
                p.Add("@DATE_OF_BIRTH", model.DATE_OF_BIRTH);
                p.Add("@QUALIFICATION", model.QUALIFICATION);
                p.Add("@SEX", model.SEX);
                p.Add("@MARITAL_STATUS", model.MARITAL_STATUS);
                p.Add("@LOC_ADD1", model.LOC_ADD1);
                p.Add("@LOC_PIN", model.LOC_PIN);
                p.Add("@PHONE_NO", model.PHONE_NO);
                p.Add("@EMAIL", model.EMAIL);
                p.Add("@ReligionId", model.ReligionId);
                p.Add("@Photo", model.Photo);
                p.Add("@BloodGroupId", model.BloodGroupId);
                p.Add("@AdharNo", model.AdharNo);

                // Department
                p.Add("@GRP", model.GRP);
                p.Add("@SECTION", model.SECTION);
                p.Add("@DEPTT_CODE", model.DEPTT_CODE);
                p.Add("@DESIGID", model.DESIGID);
                p.Add("@DESIG_CODE", model.DESIG_CODE);
                p.Add("@PF_NO", model.PF_NO);
                p.Add("@PROMOTION", model.PROMOTION);
                p.Add("@ORDER_NO", model.ORDER_NO);
                p.Add("@RECFLAG", model.RECFLAG);
                p.Add("@DEPTTID", model.DEPTTID);
                p.Add("@DEP_TYPE", model.DEP_TYPE);
                p.Add("@DATE_OF_JOIN", model.DATE_OF_JOIN);
                p.Add("@CategoryId", model.CategoryId);

                // Account
                p.Add("@BANKID", model.BANKID);
                p.Add("@ACCT_NO", model.ACCT_NO);
                p.Add("@GOVT_ACC_FLAG", model.GOVT_ACC_FLAG);
                p.Add("@ACCO_CODE", model.ACCO_CODE);
                p.Add("@BANK_CODE", model.BANK_CODE);
                p.Add("@PAN_NO", model.PAN_NO);

                // Salary
                p.Add("@PAY_GRADE", model.PAY_GRADE);
                p.Add("@BASIC", model.BASIC);
                p.Add("@PERSONAL_PAY", model.PERSONAL_PAY);
                p.Add("@SPECIAL_PAY", model.SPECIAL_PAY);
                p.Add("@MODE_SAL_PAY", model.MODE_SAL_PAY);
                p.Add("@GRADE_PAY", model.GRADE_PAY);
                p.Add("@PAY_RELEASE_FLAG", model.PAY_RELEASE_FLAG);
                p.Add("@BGT_CAT_CODE", model.BGT_CAT_CODE);

                // Allowances
                p.Add("@OFF_VECH_FLAG", model.OFF_VECH_FLAG);
                p.Add("@HANDICAPT_FLAG", model.HANDICAPT_FLAG);
                p.Add("@NURSING_FLAG", model.NURSING_FLAG);
                p.Add("@PAT_CARE_FLAG", model.PAT_CARE_FLAG);
                p.Add("@UNIFORM_FLAG", model.UNIFORM_FLAG);
                p.Add("@WASHING_FLAG", model.WASHING_FLAG);
                p.Add("@TRAINING_FLAG", model.TRAINING_FLAG);
                p.Add("@MEDI_DED_FLAG", model.MEDI_DED_FLAG);
                p.Add("@NEWS_FLAG", model.NEWS_FLAG);
                p.Add("@MEDI_AMT", model.MEDI_AMT);

                // Deductions
                p.Add("@GPF_DED", model.GPF_DED);
                p.Add("@CDT_AMT", model.CDT_AMT);
                p.Add("@INCOME_TAX", model.INCOME_TAX);
                p.Add("@BUS_KM", model.BUS_KM);
                p.Add("@GIS_DED", model.GIS_DED);

                // Pension & others
                p.Add("@PRAN_NO", model.PRAN_NO);
                p.Add("@GPF_FINAL_DT", model.GPF_FINAL_DT);
                p.Add("@PENSION_TYPE", model.PENSION_TYPE);
                p.Add("@COMMUT_BSK", model.COMMUT_BSK);
                p.Add("@PENS_ORDER_DT", model.PENS_ORDER_DT);
                p.Add("@PPO_NO", model.PPO_NO);
                p.Add("@DT_OF_PENSION", model.DT_OF_PENSION);
                p.Add("@PERSONAL_IDENTITY", model.PERSONAL_IDENTITY);
                p.Add("@HIGHT_OF_PENSION", model.HIGHT_OF_PENSION);

                // Other
                p.Add("@REMARK", model.REMARK);
                p.Add("@IMP_MSG", model.IMP_MSG);
                p.Add("@NO_DUES", model.NO_DUES);
                p.Add("@NO_DUES_REMARK", model.NO_DUES_REMARK);

                var result = await connection.QueryAsync<dynamic>(
                    "[dbo].[sp_M_EMP_MAST_SGPGI]",
                    p,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(new { success = true, message = "Employee saved successfully.", data = result });
            }
            catch (Exception ex)
            {
                // Log ex as needed
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpGet("get-employee")]
        public async Task<IActionResult> GetEmployee([FromQuery] string? ecode)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procId", 2);
                parameters.Add("@ECODE", ecode);

                var result = await connection.QueryAsync<dynamic>(
                    "[dbo].[sp_M_EMP_MAST_SGPGI]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpDelete("delete-employee/{ecode}")]
        public async Task<IActionResult> DeleteEmployee(string ecode)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procId", 3);
                parameters.Add("@ECODE", ecode);

                var result = await connection.ExecuteAsync(
                    "[dbo].[sp_M_EMP_MAST_SGPGI]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(new { success = true, message = $"Employee {ecode} deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

    }
}
