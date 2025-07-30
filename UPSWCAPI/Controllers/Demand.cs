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

    public class Demand : Controller
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;

        public Demand (IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
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
            return Ok("Demand API is running.");
        }

        #region DemandForm

        [HttpPost("insert-demand")]
        public async Task<IActionResult> InsertDemand([FromBody] List<DemandInsertDto> modelList)
        {
            try
            {
                if (modelList == null || !modelList.Any())
                {
                    return BadRequest(new { success = false, message = "No demand entries provided." });
                }

                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                // Create a DataTable matching SQL TVP structure
                var dt = new DataTable();
                dt.Columns.Add("MakeId", typeof(int));
                dt.Columns.Add("OfficeDemandQty", typeof(decimal));
                dt.Columns.Add("UnitId", typeof(int));
                dt.Columns.Add("ItemId", typeof(int));
                dt.Columns.Add("OfficeRemarks", typeof(string));
                dt.Columns.Add("DemandStatus", typeof(string));
                dt.Columns.Add("UserId", typeof(int));

                foreach (var entry in modelList)
                {
                    dt.Rows.Add(
                        entry.MakeId,
                        entry.OfficeDemandQty,
                        entry.UnitId,
                        entry.ItemId,
                        entry.OfficeRemarks ?? string.Empty,
                        entry.DemandStatus ?? "Forward to RM Office",
                        entry.UserId
                    );
                }

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@DemandEntries", dt.AsTableValuedParameter("dbo.DemandEntryType")); // <-- your TVP name

                var result = await connection.QueryAsync("[dbo].[Proc_demandForm]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Batch demand inserted.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("get-forwarded-demands")]
        public async Task<IActionResult> GetForwardedDemands()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2); // For get all where status = 'Forward to RM Office'

                var result = await connection.QueryAsync<dynamic>(
                    "[dbo].[Proc_demandForm]",
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


        [HttpPut("update-demand/{DemandId}")]
        public async Task<IActionResult> UpdateDemand(int DemandId, [FromBody] DemandInsertDto model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3); // For Update
                parameters.Add("@DemandId", DemandId); // Passed in URL
                parameters.Add("@MakeId", model.MakeId);
                parameters.Add("@RmAppQty", model.RmAppQty);
                parameters.Add("@UnitId", model.UnitId);
                parameters.Add("@ItemId", model.ItemId);
                parameters.Add("@RMOfficeRemarks", model.RMOfficeRemarks);
                parameters.Add("@DemandStatus", model.DemandStatus);
                parameters.Add("@UserId", model.UserId);

                var result = await connection.QueryAsync<dynamic>("[dbo].[Proc_demandForm]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Demand updated successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("get-demand-by-id/{id}")]
        public async Task<IActionResult> GetDemandById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5); // For example
                parameters.Add("@DemandId", id);

                var result = await connection.QueryAsync("[dbo].[Proc_demandForm]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
                }
            }

        [HttpGet("get-RM-demands")]
        public async Task<IActionResult> GetRMDemands()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 6); // For get all where status = 'Forward to RM Office'

                var result = await connection.QueryAsync<dynamic>(
                    "[dbo].[Proc_demandForm]",
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

        [HttpPut("update-ho-demand/{DemandId}")]
        public async Task<IActionResult> UpdateHODemand(int DemandId, [FromBody] DemandInsertDto model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 7); // HO Update
                parameters.Add("@DemandId", DemandId); // From URL
                parameters.Add("@ItemId", model.ItemId);
                parameters.Add("@UnitId", model.UnitId);
                parameters.Add("@MakeId", model.MakeId);
                parameters.Add("@HOAppQty", model.HOAppQty); // Approved Quantity by HO
                parameters.Add("@HORemarks", model.HORemarks); // Remarks by HO
                parameters.Add("@DemandStatus", model.DemandStatus);
                parameters.Add("@UserId", model.UserId);

                var result = await connection.QueryAsync("[dbo].[Proc_demandForm]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "HO Demand updated successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("get-emp-demands")]
        public async Task<IActionResult> GetEmpDemands()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 8); // For get all where status = 'Forward to RM Office'

                var result = await connection.QueryAsync<dynamic>(
                    "[dbo].[Proc_demandForm]",
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


        #endregion
    }
}
