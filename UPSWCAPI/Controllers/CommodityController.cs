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

//using Microsoft.AspNetCore.Cors;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UPSWCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class CommodityController : ControllerBase
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;

        public CommodityController(IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
        {
            Environment = _environment;
            Configuration = _configuration;
            _context = context;
            _dapper = dapper;
        }

        private readonly EnquiryDbContext _context;

        //[HttpGet("test")]
        //public IActionResult Test()
        //{
        //    return Ok("API working");
        //}

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Demand API is running.");
        }

        #region Commodity

        [HttpPost("InsertCommodity")]
        public async Task<IActionResult> InsertCommodity([FromBody] CommodityModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                //parameters.Add("@CommodityId", model.CommodityId);
                parameters.Add("@CommodityName", model.CommodityName ?? string.Empty);

                await connection.ExecuteAsync("[dbo].[Proc_Commudity]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Commodity inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpPut("UpdateCommodity")]
        public async Task<IActionResult> UpdateCommodity([FromBody] CommodityModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@CommodityId", model.CommodityId);
                parameters.Add("@CommodityName", model.CommodityName ?? string.Empty);

                await connection.ExecuteAsync("[dbo].[Proc_Commudity]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Commodity updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpGet("GetAllCommodities")]
        public async Task<IActionResult> GetAllCommodities()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);

                var result = await connection.QueryAsync("[dbo].[Proc_Commudity]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpGet("GetCommodityById/{commodityId}")]
        public async Task<IActionResult> GetCommodityById(int commodityId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@CommodityId", commodityId);

                var result = await connection.QueryFirstOrDefaultAsync("[dbo].[Proc_Commudity]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteCommodity/{commodityId}")]
        public async Task<IActionResult> DeleteCommodity(int commodityId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@CommodityId", commodityId);

                var result = await connection.ExecuteAsync("[dbo].[Proc_Commudity]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }



        #endregion

        #region Designation

        [HttpPost("InsertDesignation")]
        public async Task<IActionResult> InsertDesignation([FromBody] DesignationModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@DesignationId", model.DesignationId);
                parameters.Add("@DesignationName", model.DesignationName ?? string.Empty);
                parameters.Add("@CategoryId", model.CategoryId);
                parameters.Add("@Status", model.Status ?? "N");
                parameters.Add("@OrderBy", model.OrderBy == 0 ? null : model.OrderBy);
                parameters.Add("@Code", string.IsNullOrWhiteSpace(model.Code) ? null : model.Code);
                parameters.Add("@Type", string.IsNullOrWhiteSpace(model.Type) ? null : model.Type);
                parameters.Add("@DesigUsage", string.IsNullOrWhiteSpace(model.DesigUsage) ? null : model.DesigUsage);
                parameters.Add("@CompId", model.CompId == 0 ? null : model.CompId);
                parameters.Add("@EmployeId", model.EmployeId == 0 ? null : model.EmployeId);
                parameters.Add("@EmployementMapId", model.EmployementMapId == 0 ? null : model.EmployementMapId);
                parameters.Add("@ClassMapId", model.ClassMapId == 0 ? null : model.ClassMapId);

                await connection.ExecuteAsync("[dbo].[Proc_Designation]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Designation inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpPut("UpdateDesignation")]
        public async Task<IActionResult> UpdateDesignation([FromBody] DesignationModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@DesignationId", model.DesignationId);
                parameters.Add("@DesignationName", model.DesignationName ?? string.Empty);
                parameters.Add("@CategoryId", model.CategoryId);
                parameters.Add("@Status", model.Status ?? "N");
                parameters.Add("@OrderBy", model.OrderBy == 0 ? null : model.OrderBy);
                parameters.Add("@Code", string.IsNullOrWhiteSpace(model.Code) ? null : model.Code);
                parameters.Add("@Type", string.IsNullOrWhiteSpace(model.Type) ? null : model.Type);
                parameters.Add("@DesigUsage", string.IsNullOrWhiteSpace(model.DesigUsage) ? null : model.DesigUsage);
                parameters.Add("@CompId", model.CompId == 0 ? null : model.CompId);
                parameters.Add("@EmployeId", model.EmployeId == 0 ? null : model.EmployeId);
                parameters.Add("@EmployementMapId", model.EmployementMapId == 0 ? null : model.EmployementMapId);
                parameters.Add("@ClassMapId", model.ClassMapId == 0 ? null : model.ClassMapId);

                await connection.ExecuteAsync("[dbo].[Proc_Designation]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Designation updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpGet("GetDesignationById/{designationId}")]
        public async Task<IActionResult> GetDesignationById(int designationId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);
                parameters.Add("@DesignationId", designationId);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>("[dbo].[Proc_Designation]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpGet("GetAllDesignations")]
        public async Task<IActionResult> GetAllDesignations()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);

                var result = await connection.QueryAsync("[dbo].[Proc_Designation]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteDesignation/{designationId}")]
        public async Task<IActionResult> DeleteDesignation(int designationId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@DesignationId", designationId);

                var result = await connection.ExecuteAsync("[dbo].[Proc_Designation]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Department


        [HttpPost("InsertDepartment")]
        public async Task<IActionResult> InsertDepartment([FromBody] DepartmentModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@DepartmentId", model.DepartmentId);
                parameters.Add("@M_Department", model.DepartmentHead);
                //parameters.Add("@Status", model.Status ?? "N");

                await connection.ExecuteAsync("[dbo].[Proc_MasterDepartment]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Department inserted successfully." });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });

            }
        }


        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@DepartmentId", model.DepartmentId);
                parameters.Add("@M_Department", model.DepartmentHead);

                await connection.ExecuteAsync("[dbo].[Proc_MasterDepartment]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Department updateed successfully." });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });

            }
        }

        [HttpGet("GetDepartmentById/{departmentId}")]
        public async Task<IActionResult> GetDepartmentById(int departmentId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);
                parameters.Add("@DepartmentId", departmentId);
                //parameters.Add("@DepartmentName", model.DepartmentName);

                var result = await connection.QueryFirstOrDefaultAsync("[dbo].[Proc_MasterDepartment]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });

            }
        }

        [HttpGet("GetAllDepartment")]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                //parameters.Add("@DepartmentId", departmentId);
                //parameters.Add("@DepartmentName", model.DepartmentName);

                var result = await connection.QueryAsync("[dbo].[Proc_MasterDepartment]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });

            }
        }

        [HttpDelete("DeleteDepartment/{departmentId}")]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@DepartmentId", departmentId);

                var result = await connection.ExecuteAsync("[dbo].[Proc_MasterDepartment]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        #endregion

        #region Recruitment Mode

        [HttpPost("InsertRecruitment")]
        public async Task<IActionResult> InsertRecruitment([FromBody] RecruitmentModentModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                //parameters.Add("@CommodityId", model.CommodityId);
                parameters.Add("@RecruitMode", model.RecruitMode ?? string.Empty);

                await connection.ExecuteAsync("[dbo].[Proc_ReruitmentMode]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpPut("UpdateRecruitment")]
        public async Task<IActionResult> UpdateRecruitment([FromBody] RecruitmentModentModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@RecruitId", model.RecruitId);
                parameters.Add("@RecruitMode", model.RecruitMode ?? string.Empty);

                await connection.ExecuteAsync("[dbo].[Proc_ReruitmentMode]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllRecruitment")]
        public async Task<IActionResult> GetAllRecruitment()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);

                var result = await connection.QueryAsync("[dbo].[Proc_ReruitmentMode]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetRecruitmentById/{recruitId}")]
        public async Task<IActionResult> GetRecruitmentById(int recruitId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@RecruitId", recruitId);

                var result = await connection.QueryFirstOrDefaultAsync("[dbo].[Proc_ReruitmentMode]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteRecruitment/{recruitId}")]
        public async Task<IActionResult> DeleteRecruitment(int recruitId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@RecruitId", recruitId);

                var result = await connection.ExecuteAsync("[dbo].[Proc_ReruitmentMode]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }



        #endregion

        #region Payment Head

        [HttpPost("InsertPayHead")]
        public async Task<IActionResult> InsertPayHead([FromBody] PaymentHeadModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@HeadId", model.HeadId);
                parameters.Add("@HeadCode", model.HeadCode ?? string.Empty);
                parameters.Add("@HeadName", model.HeadName ?? string.Empty);
                parameters.Add("@Status", model.Status ?? "N");
                parameters.Add("@OfficeId", model.OfficeId);

                await connection.ExecuteAsync("[dbo].[Proc_PaymentHead]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Payment Head inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdatePayHead")]
        public async Task<IActionResult> UpdatePayHead([FromBody] PaymentHeadModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@HeadId", model.HeadId);
                parameters.Add("@HeadCode", model.HeadCode ?? string.Empty);
                parameters.Add("@HeadName", model.HeadName ?? string.Empty);
                parameters.Add("@Status", model.Status ?? "N");
                parameters.Add("@OfficeId", model.OfficeId);

                await connection.ExecuteAsync("[dbo].[Proc_PaymentHead]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Payment Head update successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllPayHead")]
        public async Task<IActionResult> GetAllPayHead()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);

                var result = await connection.QueryAsync("[dbo].[Proc_PaymentHead]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpGet("GetPayHeadById/{headId}")]
        public async Task<IActionResult> GetPayHeadById(int headId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@HeadId", headId);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>("[dbo].[Proc_PaymentHead]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeletePayHead/{headId}")]
        public async Task<IActionResult> DeletePayHead(int headId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@HeadId", headId);

                var result = await connection.ExecuteAsync("[dbo].[Proc_PaymentHead]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Service Quota

        [HttpPost("InsertServiceQuota")]
        public async Task<IActionResult> InsertServiceQuota([FromBody] ServiceQuotaModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@QuotaName", model.QuotaName ?? string.Empty);

                await connection.ExecuteAsync("[dbo].[Proc_ServiceQuota]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Service Quota inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpPut("UpdateServiceQuota")]
        public async Task<IActionResult> UpdateServiceQuota([FromBody] ServiceQuotaModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@QuotaId", model.QuotaId);
                parameters.Add("@QuotaName", model.QuotaName ?? string.Empty);

                await connection.ExecuteAsync("[dbo].[Proc_ServiceQuota]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Service Quota updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllQuota")]
        public async Task<IActionResult> GetAllQuota()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);

                var result = await connection.QueryAsync("[dbo].[Proc_ServiceQuota]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetQuotaById/{QuotaId}")]
        public async Task<IActionResult> GetQuotaById(int QuotaId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@QuotaId", QuotaId);

                var result = await connection.QueryFirstOrDefaultAsync("[dbo].[Proc_ServiceQuota]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteQuota/{QuotaId}")]
        public async Task<IActionResult> DeleteQuota(int QuotaId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@QuotaId", QuotaId);

                var result = await connection.ExecuteAsync("[dbo].[Proc_ServiceQuota]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region MSP

        [HttpPost("InsertMSP")]
        public async Task<IActionResult> InsertMsp([FromBody] MSPModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@JeansRateid", model.JeansRateid);
                parameters.Add("@CommodityId", model.CommodityId);
                parameters.Add("@PerKuntelRate", model.PerKuntelRate);
                parameters.Add("@PerKgRate", model.PerKgRate);
                parameters.Add("@EffectiveDate", model.EffectiveDate);

                await connection.ExecuteAsync("[dbo].[Proc_MSP]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "MSP inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateMSP")]
        public async Task<IActionResult> UpdateMsp([FromBody] MSPModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@JeansRateid", model.JeansRateid);
                parameters.Add("@CommodityId", model.CommodityId);
                parameters.Add("@PerKuntelRate", model.PerKuntelRate);
                parameters.Add("@PerKgRate", model.PerKgRate);
                parameters.Add("@EffectiveDate", model.EffectiveDate);

                await connection.ExecuteAsync("[dbo].[Proc_MSP]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "MSP updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllMSP")]
        public async Task<IActionResult> GetAllMSP()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);

                var result = await connection.QueryAsync("[dbo].[Proc_MSP]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetMSPById/{jeansRateid}")]
        public async Task<IActionResult> GetMSPById(int jeansRateid)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@JeansRateid", jeansRateid);

                var result = await connection.QueryFirstOrDefaultAsync("[dbo].[Proc_MSP]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteMSP/{jeansRateid}")]
        public async Task<IActionResult> DeleteMSP(int jeansRateid)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@JeansRateid", jeansRateid);

                var result = await connection.ExecuteAsync("[dbo].[Proc_MSP]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        #endregion

    }
}

