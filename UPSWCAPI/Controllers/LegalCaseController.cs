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


        #region CourtTypeMaster
        [HttpPost("InsertCourtTypeMaster")]
        public async Task<IActionResult> InsertCourtTypeMaster([FromBody] CourtTypeMaster model)
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
        public async Task<IActionResult> UpdateCourtType([FromBody] CourtTypeMaster model)
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

        [HttpGet("GetCourtTypeMasterById/{id}")]
        public async Task<IActionResult> GetCourtTypeMasterById(int id)
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

        [HttpDelete("DeleteCourtTypeMaster/{id}")]
        public async Task<IActionResult> DeleteCourtTypeMaster(int id)
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
    }
}
