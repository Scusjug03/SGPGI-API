using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using UPSWCAPI.Model;
using UPSWCAPI.Model.UPSWCAPI.Model;
using UPSWCAPI.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static UPSWCAPI.Model.EnquiryDbContext;

namespace UPSWCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class StorageController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly EnquiryDbContext _context;

        //private readonly IDapper _dapper;
       // private IWebHostEnvironment Environment;
        

        public StorageController(IConfiguration configuration, EnquiryDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Demand API is running.");
        }

        [HttpPost("insertReceiveAndLedger")]
        public async Task<IActionResult> InsertStorage(StorageRequest request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("EnquiryCon"));
            using var command = new SqlCommand("sp_Insert_ReceiveAndLedger", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ReceiveDate", request.ReceiveDate);
            command.Parameters.AddWithValue("@WarehouseId", request.WarehouseId);
            command.Parameters.AddWithValue("@LotNumber", request.LotNumber);
            command.Parameters.AddWithValue("@CommodityId", request.CommodityId);
            command.Parameters.AddWithValue("@AgencyTypeId", request.AgencyTypeId);
            command.Parameters.AddWithValue("@GrossWeight", request.GrossWeight);
            command.Parameters.AddWithValue("@TotalNoOfBags", request.TotalNoOfBags);
            command.Parameters.AddWithValue("@TotalBagWeight_kg", request.TotalBagWeightKg);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return Ok(new { success = true, message = "Data inserted successfully." });
        }


        [HttpGet("getIncompleteReceiveData")]
        public async Task<IActionResult> GetIncompleteWeights()
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("EnquiryCon"));
            using var cmd = new SqlCommand("sp_GetIncompleteWeights", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await conn.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();
            var results = new List<dynamic>();

            while (await reader.ReadAsync())
            {
                results.Add(new
                {
                    ReceiveId = reader["ReceiveId"],
                    ReceiveDate = reader["ReceiveDate"],
                    LotNumber = reader["LotNumber"],
                    WarehouseId = reader["WarehouseId"],
                    CommodityId = reader["CommodityId"],
                    AgencyTypeId = reader["AgencyTypeId"],
                    GrossWeight = reader["GrossWeight"],
                    TotalBagWeightKg = reader["TotalBagWeight_kg"],
                    TruckWeight = reader["TruckWeight"] == DBNull.Value ? null : reader["TruckWeight"],
                    NetWeight = reader["NetWeight"] == DBNull.Value ? null : reader["NetWeight"]
                });
            }

            return Ok(results);
        }


        [HttpPost("updateReceiveRegister")]
        public async Task<IActionResult> UpdateWeights(WeightUpdateRequest request)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("EnquiryCon"));
            using var cmd = new SqlCommand("sp_UpdateWeights", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ReceiveId", request.ReceiveId);
            cmd.Parameters.AddWithValue("@TruckWeight", request.TruckWeight);
            cmd.Parameters.AddWithValue("@NetWeight", request.NetWeight);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return Ok(new { success = true, message = "Weights updated successfully." });
        }

        ///  =================================================================================================
        ///  ### NEW PROCESS ###==============================================================================
        ///  =================================================================================================
        
        /* ||---------- RECEIVE STORAGE APIs ------------------ || */

        [HttpPost("ReceiveStockAtWindow")]
        public async Task<IActionResult> PostReceiveStock([FromBody] ReceiveStockAtWindow model)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("EnquiryCon"));
            using var cmd = new SqlCommand("sp_InsertReceiveStorageAtWindow", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@WarehouseId", model.WarehouseId);
            cmd.Parameters.AddWithValue("@OfficeId", model.OfficeId);
            cmd.Parameters.AddWithValue("@FinancialYear", model.FinancialYear);
            cmd.Parameters.AddWithValue("@CommodityId", model.CommodityId);
            cmd.Parameters.AddWithValue("@AgencyTypeId", model.AgencyTypeId);
            cmd.Parameters.AddWithValue("@VehicleNumber", model.VehicleNumber ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ChallanNumber", model.ChallanNumber ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@UserID", model.UserID);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return Ok(new { success = true, message = "Inserted successfully" });
        }


        [HttpGet("GetSentStockByWindowAtWBE")]
        public async Task<IActionResult> GetPendingReceives([FromQuery] string type ,int officeId,int userId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@type", type);
                parameters.Add("@officeId", officeId);
                parameters.Add("@userId", userId);

                var result = await connection.QueryAsync<ReceiveStockAtWBE>(
                    "sp_GetPendingReceiveStorageForWBE",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }


        [HttpPut("updateWbInDetails/{receiveId}")]
        public async Task<IActionResult> UpdateWbInDetails(int receiveId, [FromBody] ReceiveStockAtWbIn dto)
        {
            try
            {
                var conn = _context.Database.GetDbConnection();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "sp_Receive_WbInDetails_Update";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@ReceiveId", receiveId));
                    //command.Parameters.Add(new SqlParameter("@TruckWeight", dto.TruckWeight));
                    command.Parameters.Add(new SqlParameter("@TruckWeight", Convert.ToDecimal(dto.TruckWeight)));
                    command.Parameters.Add(new SqlParameter("@TotalNoOfBags", dto.TotalNoOfBags));
                    command.Parameters.Add(new SqlParameter("@GodownId", dto.GodownId));
                    command.Parameters.Add(new SqlParameter("@StackId", dto.StackId));
                    command.Parameters.Add(new SqlParameter("@WbInBy", dto.WbInBy));
                    command.Parameters.Add(new SqlParameter("@BagType", dto.BagType));
                    command.Parameters.Add(new SqlParameter("@BagTypeWeight", dto.BagTypeWeight));

                    await conn.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }

                return Ok(new { success = true, message = "WB-In details updated successfully via stored procedure." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpPut("UpdateQCDetailsAtEntry/{receiveId}")]
        public async Task<IActionResult> UpdateQCDetails(int receiveId, [FromBody] ReceiveStorageAtQc model)
        {
            var conn = _context.Database.GetDbConnection();

            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "usp_UpdateReceiveStorageQCAtEntry";
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    cmd.Parameters.Add(new SqlParameter("@ReceiveId", receiveId));
                    cmd.Parameters.Add(new SqlParameter("@Moisture", model.Moisture));
                    cmd.Parameters.Add(new SqlParameter("@OtherAgencyMoisture", model.OtherAgencyMoisture));
                    cmd.Parameters.Add(new SqlParameter("@TotalWorker", model.TotalWorker));
                    cmd.Parameters.Add(new SqlParameter("@WorkerNames", model.WorkerNames ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@IsStockWeightTaken", model.IsStockWeightTaken));
                    cmd.Parameters.Add(new SqlParameter("@Weight1", model.Weight1 ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@NoOfBag1", model.NoOfBag1 ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@BagWeight1", model.BagWeight1 ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@Weight2", model.Weight2 ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@NoOfBag2", model.NoOfBag2 ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@BagWeight2", model.BagWeight2 ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@ActionType", model.ActionType));
                    cmd.Parameters.Add(new SqlParameter("@UpdatedBy", model.UpdatedBy));

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    return Ok(new { status = true, message = "QC details updated successfully." });
                }
            }
            catch (Exception ex)            {
                
                return BadRequest(new { status = false, message = ex.Message });
            }
        }
        [HttpPut("UpdateFinalDetailsAtEntry/{receiveId}")]
        public async Task<IActionResult> UpdateFinalDetails(int receiveId, [FromBody] ReceiveStorageFinal model)
        {
            var conn = _context.Database.GetDbConnection();

            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_Receive_FinalDetails_Update";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@ReceiveId", receiveId));
                    cmd.Parameters.Add(new SqlParameter("@EmptyTruckWeightKg", model.EmptyTruckWeightKg));
                    cmd.Parameters.Add(new SqlParameter("@TotalNoOfBags", model.TotalNumberOfBags));
                    cmd.Parameters.Add(new SqlParameter("@TotalBagWeight_kg", model.TotalBagWeightKg));
                    cmd.Parameters.Add(new SqlParameter("@NetWeight", model.NetWeight));
                    cmd.Parameters.Add(new SqlParameter("@EnchargeName", model.EnchargeName));
                    cmd.Parameters.Add(new SqlParameter("@UpdatedBy", model.UpdatedBy));

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    return Ok(new { status = true, message = "Details updated successfully." });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { status = false, message = ex.Message });
            }
        }

        /* ||---------- ISSUE STORAGE APIs ------------------ || */


        [HttpPost("IssueStockAtWindow")]
        public async Task<IActionResult> PostIssueStock([FromBody] IssueStockAtWindow model)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("EnquiryCon"));
            using var cmd = new SqlCommand("sp_InsertIssueStorageAtWindow", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@WarehouseId", model.WarehouseId);
            cmd.Parameters.AddWithValue("@OfficeId", model.OfficeId);
            cmd.Parameters.AddWithValue("@FinancialYear", model.FinancialYear);
            cmd.Parameters.AddWithValue("@CommodityId", model.CommodityId);
            cmd.Parameters.AddWithValue("@AgencyTypeId", model.AgencyTypeId);
            cmd.Parameters.AddWithValue("@VehicleNumber", model.VehicleNumber ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ChallanNumber", model.ChallanNumber ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@UserID", model.UserID);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return Ok(new { success = true, message = "Inserted successfully" });
        }


        [HttpGet("GetSentIssueStockByWindowAtWBE")]
        public async Task<IActionResult> GetPendingIssues([FromQuery] string type)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@type", type);

                var result = await connection.QueryAsync<IssueStockAtWBE>(
                    "sp_GetPendingIssueStorageForWBE",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }


        [HttpPut("updateIssueWbInDetails/{issueId}")]
        public async Task<IActionResult> UpdateIssueWbInDetails(int issueId, [FromBody] IssueStockAtWbIn dto)
        {
            try
            {
                var conn = _context.Database.GetDbConnection();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "sp_Issue_WbInDetails_Update";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@IssueId", issueId));
                    command.Parameters.Add(new SqlParameter("@ReceiveId", dto.ReceiveId));
                    command.Parameters.Add(new SqlParameter("@TruckWeight", dto.TruckWeight));
                    command.Parameters.Add(new SqlParameter("@TotalNoOfBags", dto.TotalNoOfBags));
                    command.Parameters.Add(new SqlParameter("@GodownId", dto.GodownId));
                    command.Parameters.Add(new SqlParameter("@StackId", dto.StackId));
                    command.Parameters.Add(new SqlParameter("@WbInBy", dto.WbInBy));
                    command.Parameters.Add(new SqlParameter("@BagType", dto.BagType));
                    command.Parameters.Add(new SqlParameter("@BagTypeWeight", dto.BagTypeWeight));

                    await conn.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }

                return Ok(new { success = true, message = "WB-In details updated successfully via stored procedure." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpPut("UpdateIssueQCDetailsAtEntry/{issueId}")]
        public async Task<IActionResult> UpdateIssueQCDetails(int issueId, [FromBody] IssueStorageAtQc model)
        {
            var conn = _context.Database.GetDbConnection();

            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "usp_UpdateIssueStorageQCAtEntry";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@IssueId", issueId));
                    cmd.Parameters.Add(new SqlParameter("@ReceiveId", model.ReceiveId));
                    cmd.Parameters.Add(new SqlParameter("@Moisture", model.Moisture));
                    cmd.Parameters.Add(new SqlParameter("@OtherAgencyMoisture", model.OtherAgencyMoisture));
                    cmd.Parameters.Add(new SqlParameter("@TotalWorker", model.TotalWorker));
                    cmd.Parameters.Add(new SqlParameter("@WorkerNames", model.WorkerNames ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@IsStockWeightTaken", model.IsStockWeightTaken));
                    cmd.Parameters.Add(new SqlParameter("@Weight1", model.Weight1 ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@NoOfBag1", model.NoOfBag1 ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@BagWeight1", model.BagWeight1 ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@Weight2", model.Weight2 ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@NoOfBag2", model.NoOfBag2 ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@BagWeight2", model.BagWeight2 ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@ActionType", model.ActionType));
                    cmd.Parameters.Add(new SqlParameter("@UpdatedBy", model.UpdatedBy));

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    return Ok(new { status = true, message = "QC details updated successfully." });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { status = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateFinalIssueDetailsAtEntry/{issueId}")]
        public async Task<IActionResult> UpdateFinalIssueDetails(int issueId, [FromBody] IssueStorageFinal model)
        {
            var conn = _context.Database.GetDbConnection();

            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_Issue_FinalDetails_Update";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@IssueId", issueId));
                    cmd.Parameters.Add(new SqlParameter("@ReceiveId", model.ReceiveId));
                    cmd.Parameters.Add(new SqlParameter("@TruckWeightKg", model.TruckWeightKg));
                    cmd.Parameters.Add(new SqlParameter("@TotalNoOfBags", model.TotalNoOfBags));
                    cmd.Parameters.Add(new SqlParameter("@TotalBagWeight_kg", model.TotalBagWeight_kg));
                    cmd.Parameters.Add(new SqlParameter("@NetWeight", model.NetWeight));
                    cmd.Parameters.Add(new SqlParameter("@EnchargeName", model.EnchargeName));
                    cmd.Parameters.Add(new SqlParameter("@UpdatedBy", model.UpdatedBy));

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    return Ok(new { status = true, message = "Details updated successfully." });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { status = false, message = ex.Message });
            }
        }

    }
}
