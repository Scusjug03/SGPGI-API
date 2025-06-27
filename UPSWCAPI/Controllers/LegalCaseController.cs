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
    public class LegalCaseController : Controller
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
        public LegalCaseController(IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
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


        #region CourtMaster
        [HttpPost("InsertCourtMaster")]
        public async Task<IActionResult> InsertCourtMaster([FromBody] CourtMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procId", 1);
                parameters.Add("@CourtType", model.CourtType ?? string.Empty);
                parameters.Add("@ShortName", model.ShortName ?? string.Empty);
                parameters.Add("@Description", model.Description ?? string.Empty);
                parameters.Add("@CreatedBy", model.CreatedBy);

                var result = await connection.QueryAsync("[dbo].[sp_CourtType]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateCourtType")]
        public async Task<IActionResult> UpdateCourtType([FromBody] CourtMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procId", 2);
                parameters.Add("@CourtTypeID", model.CourtTypeID);
                parameters.Add("@CourtType", model.CourtType ?? string.Empty);
                parameters.Add("@ShortName", model.ShortName ?? string.Empty);
                parameters.Add("@Description", model.Description ?? string.Empty);
                parameters.Add("@UpdatedBy", model.UpdatedBy);

                var result = await connection.QueryAsync("[dbo].[sp_CourtType]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetCourtMasterById/{id}")]
        public async Task<IActionResult> GetCourtMasterById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procId", 3);
                parameters.Add("@CourtTypeID", id);
                parameters.Add("@CourtType", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@Description", string.Empty);
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", 0);

                var data = await connection.QueryAsync("[dbo].[sp_CourtType]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllCourtCase")]
        public async Task<IActionResult> GetAllCourtCase()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procId", 4);
                parameters.Add("@CourtType", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@Description", string.Empty);
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", 0);

                var result = await connection.QueryAsync("[dbo].[sp_CourtType]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteCourtMaster/{id}")]
        public async Task<IActionResult> DeleteCourtMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procId", 5);
                parameters.Add("@CourtTypeID", id);
                parameters.Add("@CourtType", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@Description", string.Empty);
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", 0);

                await connection.ExecuteAsync("[dbo].[sp_CourtType]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Court Type deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region CourtTypeMaster
        [HttpPost("InsertCourtTypeMaster")]
        public async Task<IActionResult> InsertCourtTypeMaster([FromBody] CourtTypeMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@CourtTypeId", model.CourtTypeId);
                parameters.Add("@CourtName", model.CourtName ?? string.Empty);
                parameters.Add("@Address", model.Address ?? string.Empty);
                parameters.Add("@ShortName", model.ShortName ?? string.Empty);
                parameters.Add("@UserId", model.CreatedBy);

                var result = await connection.QueryAsync("[dbo].[sp_CourtMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateCourtTypeMaster")]
        public async Task<IActionResult> UpdateCourtTypeMaster([FromBody] CourtTypeMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@CourtId", model.CourtId);
                parameters.Add("@CourtTypeId", model.CourtTypeId);
                parameters.Add("@CourtName", model.CourtName ?? string.Empty);
                parameters.Add("@Address", model.Address ?? string.Empty);
                parameters.Add("@ShortName", model.ShortName ?? string.Empty);
                parameters.Add("@UserId", model.UpdatedBy);

                var result = await connection.QueryAsync("[dbo].[sp_CourtMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetCourtTypeMasterById/{id}")]
        public async Task<IActionResult> GetCourtTypeMasterById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);
                parameters.Add("@CourtId", id);
                parameters.Add("@CourtTypeId", 0);
                parameters.Add("@CourtName", string.Empty);
                parameters.Add("@Address", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@UserId", 0);

                var result = await connection.QueryAsync("[dbo].[sp_CourtMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllCourtTypeMasterCase")]
        public async Task<IActionResult> GetAllCourtTypeMasterCase()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@CourtId", 0);
                parameters.Add("@CourtTypeId", 0);
                parameters.Add("@CourtName", string.Empty);
                parameters.Add("@Address", string.Empty);
                parameters.Add("@ShortName", string.Empty); 
                parameters.Add("@UserId", 0);

                var result = await connection.QueryAsync("[dbo].[sp_CourtMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteCourtTypeMaster/{id}")]
        public async Task<IActionResult> DeleteCourtTypeMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@CourtId", id);
                parameters.Add("@CourtTypeId", 0);
                parameters.Add("@CourtName", string.Empty);
                parameters.Add("@Address", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@UserId", 0);
                await connection.ExecuteAsync("[dbo].[sp_CourtMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Court deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region CaseTypeMaster

        [HttpPost("InsertCaseTypeMaster")]
        public async Task<IActionResult> InsertCaseTypeMaster([FromBody] CaseTypeModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@CaseTypeId", model.CaseTypeId);
                parameters.Add("@CourtTypeID", model.CourtTypeID);
                parameters.Add("@CaseType", model.CaseType ?? string.Empty);
                parameters.Add("@ShortName", model.ShortName ?? string.Empty);
                parameters.Add("@Remark", model.Remarks ?? string.Empty);

                var result = await connection.QueryAsync("[dbo].[sp_M_caseType]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateCaseTypeMaster")]
        public async Task<IActionResult> UpdateCaseTypeMaster([FromBody] CaseTypeModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@CaseTypeId", model.CaseTypeId);
                parameters.Add("@CourtTypeID", model.CourtTypeID);
                parameters.Add("@CaseType", model.CaseType ?? string.Empty);
                parameters.Add("@ShortName", model.ShortName ?? string.Empty);
                parameters.Add("@Remark", model.Remarks ?? string.Empty);

                var result = await connection.QueryAsync("[dbo].[sp_M_caseType]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetCaseTypeMasterById/{id}")]
        public async Task<IActionResult> GetCaseTypeMasterById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);
                parameters.Add("@CaseTypeId", id);

                // Don't add these unless they're required for ProcId=3
                // parameters.Add("@CourtTypeID", 0);
                // parameters.Add("@CaseType", string.Empty);
                // parameters.Add("@ShortName", string.Empty);
                // parameters.Add("@Remark", string.Empty);

                var result = await connection.QueryAsync("[dbo].[sp_M_caseType]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpGet("GetAllCaseTypeMaster")]
        public async Task<IActionResult> GetAllCaseTypeMaster()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@CaseTypeId", 0);
                parameters.Add("@CourtTypeID", 0);
                parameters.Add("@CaseType", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@Remark", string.Empty);

                var result = await connection.QueryAsync("[dbo].[sp_M_caseType]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteCaseTypeMaster/{id}")]
        public async Task<IActionResult> DeleteCaseTypeMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@CaseTypeId", id);
                parameters.Add("@CourtTypeID", 0);
                parameters.Add("@CaseType", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@Remark", string.Empty);

                await connection.ExecuteAsync("[dbo].[sp_M_caseType]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Case Type deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion


    }
}
