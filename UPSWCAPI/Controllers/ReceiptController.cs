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

    public class ReceiptController : Controller
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;

        public ReceiptController(IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
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
            return Ok("Receipt API is running.");
        }

        #region Opening

        [HttpPost("insert-receipt")]
        public async Task<IActionResult> InsertReceipt([FromBody] ReceiveInsertDto model)
        {
            try
            {
                if (model == null || model.Entries == null || !model.Entries.Any())
                {
                    return BadRequest(new { success = false, message = "No receipt entries provided." });
                }

                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                // Create DataTable matching dbo.TVP_ReceiveEntry
                var dt = new DataTable();
                dt.Columns.Add("ItemId", typeof(int));
                dt.Columns.Add("UnitId", typeof(int));
                dt.Columns.Add("MakeId", typeof(int));
                dt.Columns.Add("ReceiptQty", typeof(decimal));

                foreach (var entry in model.Entries)
                {
                    dt.Rows.Add(entry.ItemId, entry.UnitId, entry.MakeId, entry.ReceiptQty);
                }

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@OfficeId", model.OfficeId);
                parameters.Add("@ReceiveEntries", dt.AsTableValuedParameter("dbo.TVP_ReceiveEntry"));

                var result = await connection.QueryAsync("dbo.PROC_ReceiptDetails", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Receipt inserted successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpPut("update-receipt/{id}")]
        public async Task<IActionResult> UpdateReceipt(int id, [FromBody] OpeningModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@ReceiptId", id);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@OfficeId", model.OfficeId);
                parameters.Add("@ItemId", model.ItemId);
                parameters.Add("@UnitId", model.UnitId);
                parameters.Add("@MakeId", model.MakeId);
                parameters.Add("@ReceiptQty", model.ReceiptQty);

                var result = await connection.QueryAsync<dynamic>("PROC_ReceiptDetails", parameters, commandType: CommandType.StoredProcedure);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("get-receipt-by-id/{id}")]
        public async Task<IActionResult> GetReceiptById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);
                parameters.Add("@ReceiptId", id);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>("PROC_ReceiptDetails", parameters, commandType: CommandType.StoredProcedure);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("get-all-receipts")]
        public async Task<IActionResult> GetAllReceipts()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);

                var result = await connection.QueryAsync<dynamic>("PROC_ReceiptDetails", parameters, commandType: CommandType.StoredProcedure);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region purchase

        [HttpPost("insert-purchase")]
        public async Task<IActionResult> InsertPurchase([FromBody] PurchaseInsertDto model)
        {
            try
            {
                if (model == null || model.Entries == null || !model.Entries.Any())
                {
                    return BadRequest(new { success = false, message = "No purchase entries provided." });
                }

                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                // Create DataTable matching dbo.PurchaseEntryType
                var dt = new DataTable();
                dt.Columns.Add("ItemId", typeof(int));
                dt.Columns.Add("UnitId", typeof(int));
                dt.Columns.Add("MakeId", typeof(int));
                dt.Columns.Add("ReceiptQty", typeof(decimal));
                dt.Columns.Add("BasicRate", typeof(decimal));
                dt.Columns.Add("CGSTPer", typeof(decimal));
                dt.Columns.Add("SGSTPer", typeof(decimal));
                dt.Columns.Add("IGSTPer", typeof(decimal));
                dt.Columns.Add("ItemRate", typeof(decimal)); // Computed field

                foreach (var entry in model.Entries)
                {
                    var itemRate = entry.BasicRate +
                                   (entry.BasicRate * entry.CGSTPer / 100) +
                                   (entry.BasicRate * entry.SGSTPer / 100) +
                                   (entry.BasicRate * entry.IGSTPer / 100);

                    dt.Rows.Add(
                        entry.ItemId,
                        entry.UnitId,
                        entry.MakeId,
                        entry.ReceiptQty,
                        entry.BasicRate,
                        entry.CGSTPer,
                        entry.SGSTPer,
                        entry.IGSTPer,
                        itemRate
                    );
                }

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@OfficeId", model.OfficeId);
                parameters.Add("@SupplierId", model.SupplierId);
                parameters.Add("@PurchaseNo", model.PurchaseNo ?? string.Empty); // Optional
                parameters.Add("@ChallanNo", model.ChallanNo ?? string.Empty);
                parameters.Add("@ChallanDt", model.ChallanDt);
                parameters.Add("@PurchaseEntries", dt.AsTableValuedParameter("dbo.PurchaseEntryType"));

                var result = await connection.QueryAsync("dbo.PROC_ReceiptDetails", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Purchase receipt inserted successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        #endregion

        #region Receive

        [HttpPost("insert-receive")]
        public async Task<IActionResult> InsertReceive([FromBody] ReceiveInsertDto model)
        {
            try
            {
                if (model == null || model.Entries == null || !model.Entries.Any())
                    return BadRequest(new { success = false, message = "No receive entries provided." });

                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var dt = new DataTable();
                dt.Columns.Add("ItemId", typeof(int));
                dt.Columns.Add("UnitId", typeof(int));
                dt.Columns.Add("MakeId", typeof(int));
                dt.Columns.Add("ReceiptQty", typeof(decimal));

                foreach (var entry in model.Entries)
                {
                    dt.Rows.Add(entry.ItemId, entry.UnitId, entry.MakeId, entry.ReceiptQty);
                }

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 6);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@OfficeId", model.OfficeId);
                parameters.Add("@PurchaseNo", model.PurchaseNo ?? string.Empty);
                parameters.Add("@IsHoOrRm", model.IsHoOrRm);
                parameters.Add("@IsPurchase", model.IsPurchase);
                parameters.Add("@ReceiveEntries", dt.AsTableValuedParameter("dbo.ReceiveEntryType"));

                var result = await connection.QueryAsync("dbo.PROC_ReceiptDetails", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Receive entry inserted successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }




        #endregion

        #region Drop-down

        [HttpPost("get-master-dropdown")]
        public async Task<IActionResult> GetMasterDropdown([FromBody] MasterRequest request)
        {
            try
            {
                if (request.ProcId <= 0)
                    return BadRequest(new { success = false, message = "Invalid ProcId" });

                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", request.ProcId);

                var result = await connection.QueryAsync<MasterDropdownDto>(
                    "Proc_BindMaster", // your stored procedure name
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(new MasterResponse
                {
                    success = true,
                    message = "Dropdown fetched successfully.",
                    data = result.ToList()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message,
                    data = new List<MasterDropdownDto>()
                });
            }
        }


        #endregion
    }
}
