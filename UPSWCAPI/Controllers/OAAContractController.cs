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
    public class OAAContractController : ControllerBase
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;

        public OAAContractController(IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
        {
            Environment = _environment;
            Configuration = _configuration;
            _context = context;
            _dapper = dapper;
        }

        private readonly EnquiryDbContext _context;

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API working");
        }

        #region Over and Above

        [HttpPost("InsertOaaContract")]
        public async Task<IActionResult> InsertOaaContract([FromBody] OAAContractModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@OaaName", model.OaaName ?? string.Empty);
                parameters.Add("@WareHouseId", model.WareHouseId);
                parameters.Add("@Officeid", model.Officeid);
                parameters.Add("@GodownId", model.GodownId);
                parameters.Add("@ContractRate", model.ContractRate);
                parameters.Add("@ContractStartDt", model.ContractStartDt);
                parameters.Add("@ContractEndDt", model.ContractEndDt);
                parameters.Add("@Remarks", model.Remarks ?? string.Empty);

                await connection.ExecuteAsync("[dbo].[Proc_OAAContract]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Contract inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateOaaContract")]
        public async Task<IActionResult> UpdateOaaContract([FromBody] OAAContractModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@OaaId", model.OaaId);
                parameters.Add("@OaaName", model.OaaName ?? string.Empty);
                parameters.Add("@WareHouseId", model.WareHouseId);
                parameters.Add("@Officeid", model.Officeid);
                parameters.Add("@GodownId", model.GodownId);
                parameters.Add("@ContractRate", model.ContractRate);
                parameters.Add("@ContractStartDt", model.ContractStartDt);
                parameters.Add("@ContractEndDt", model.ContractEndDt);
                parameters.Add("@Remarks", model.Remarks ?? string.Empty);

                await connection.ExecuteAsync("[dbo].[Proc_OAAContract]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Contract updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllContract")]
        public async Task<IActionResult> GetAllContract()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);

                var result = await connection.QueryAsync("[dbo].[Proc_OAAContract]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetContractById/{oaaId}")]
        public async Task<IActionResult> GetContractById(int oaaId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);
                parameters.Add("@OaaId", oaaId);

                var result = await connection.QueryFirstOrDefaultAsync("[dbo].[Proc_OAAContract]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteContract/{oaaId}")]
        public async Task<IActionResult> DeleteContract(int oaaId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@OaaId", oaaId);

                var result = await connection.ExecuteAsync("[dbo].[Proc_OAAContract]", parameters, commandType: CommandType.StoredProcedure);

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
