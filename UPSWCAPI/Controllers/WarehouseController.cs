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

namespace UPSWCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class WarehouseController : ControllerBase
    {
        
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;

        public WarehouseController (IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
        {
            Environment = _environment;
            Configuration = _configuration;
            _context = context;
            _dapper = dapper;
        }

        private readonly EnquiryDbContext _context;

        // GET api/warehouse/test
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API working");
        }

        [HttpPost("create-warehouse-detail")]
        public async Task<IActionResult> CreateWarehouseDetail([FromBody] WarehouseDetail model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var dbparams = new DynamicParameters();

                dbparams.Add("Id", model.id);
                dbparams.Add("warehouseId", model.warehouseId);
                dbparams.Add("goDownId", model.goDownId);
                dbparams.Add("stackId", model.stackId);
                dbparams.Add("serialNoId", model.serialNoId);
                dbparams.Add("lotNoId", model.lotNoId);
                dbparams.Add("itemCategoryId", model.itemCategoryId);
                dbparams.Add("itemNameId", model.itemNameId);
                dbparams.Add("agencyTypeId", model.agencyTypeId);
                dbparams.Add("agencyNameId", model.agencyNameId);
                dbparams.Add("senderName", model.senderName);
                dbparams.Add("grossWeight", model.grossWeight);
                dbparams.Add("truckWeight", model.truckWeight);
                dbparams.Add("noOfBags", model.noOfBags);
                dbparams.Add("avgPerBagWeight", model.avgPerBagWeight);
                dbparams.Add("netWeight", model.netWeight);
                dbparams.Add("materialWeight", model.materialWeight);
                dbparams.Add("vehicleNoId", model.vehicleNoId);
                dbparams.Add("gatePassNo", model.gatePassNo);
                dbparams.Add("receiverName", model.receiverName);
                dbparams.Add("receiverDate", model.receiverDate);
                dbparams.Add("weightingWayId", model.weightingWayId);
                dbparams.Add("updatedBy", 0); // Optional: replace with actual user ID if needed
                dbparams.Add("procId", 1);    // Insert
                dbparams.Add("Msg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("[dbo].[Proc_WarehouseDetails]", dbparams, commandType: CommandType.StoredProcedure);

                var message = dbparams.Get<string>("Msg");

                return Ok(new
                {
                    success = true,
                    message = message ?? "Warehouse detail inserted successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error inserting warehouse detail",
                    error = ex.Message
                });
            }
        }

        //get by id procId=4

        [HttpGet("get-warehouse-detail/{id}")]
        public async Task<IActionResult> GetWarehouseDetailById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var dbparams = new DynamicParameters();
                dbparams.Add("Id", id);
                dbparams.Add("ProcId", 4); // 4 = Get By Id
                dbparams.Add("Msg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                var result = await connection.QueryAsync<WarehouseDetail>(
                    "[dbo].[Proc_WarehouseDetails]",
                    dbparams,
                    commandType: CommandType.StoredProcedure
                );

                var message = dbparams.Get<string>("Msg");

                if (result == null || !result.Any())
                {
                    return NotFound(new { success = false, message = "Warehouse detail not found" });
                }

                return Ok(new
                {
                    success = true,
                    data = result.FirstOrDefault()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error fetching warehouse detail",
                    error = ex.Message
                });
            }
        }

        [HttpPut("update-warehouse-detail")]
        public async Task<IActionResult> UpdateWarehouseDetail([FromBody] WarehouseDetail model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var dbparams = new DynamicParameters();

                dbparams.Add("Id", model.id);
                dbparams.Add("WarehouseId", model.warehouseId);
                dbparams.Add("GoDownId", model.goDownId);
                dbparams.Add("StackId", model.stackId);
                dbparams.Add("SerialNoId", model.serialNoId);
                dbparams.Add("LotNoId", model.lotNoId);
                dbparams.Add("ItemCategoryId", model.itemCategoryId);
                dbparams.Add("ItemNameId", model.itemNameId);
                dbparams.Add("AgencyTypeId", model.agencyTypeId);
                dbparams.Add("AgencyNameId", model.agencyNameId);
                dbparams.Add("SenderName", model.senderName);
                dbparams.Add("GrossWeight", model.grossWeight);
                dbparams.Add("TruckWeight", model.truckWeight);
                dbparams.Add("NoOfBags", model.noOfBags);
                dbparams.Add("AvgPerBagWeight", model.avgPerBagWeight);
                dbparams.Add("NetWeight", model.netWeight);
                dbparams.Add("MaterialWeight", model.materialWeight);
                dbparams.Add("VehicleNoId", model.vehicleNoId);
                dbparams.Add("GatePassNo", model.gatePassNo);
                dbparams.Add("ReceiverName", model.receiverName);
                dbparams.Add("WeightingWayId", model.weightingWayId);
                dbparams.Add("UpdatedBy", 0); // Optional: Replace with current user id if applicable
                dbparams.Add("ProcId", 2);    // Update
                dbparams.Add("Msg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("[dbo].[Proc_WarehouseDetails]", dbparams, commandType: CommandType.StoredProcedure);

                var message = dbparams.Get<string>("Msg");

                return Ok(new
                {
                    success = true,
                    message = message ?? "Warehouse detail updated successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error updating warehouse detail",
                    error = ex.Message
                });
            }
        }


        //soft Delete
        [HttpDelete("soft-delete-warehouse-detail/{id}")]
        public async Task<IActionResult> SoftDeleteWarehouseDetail(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var dbparams = new DynamicParameters();
                dbparams.Add("Id", id);
                dbparams.Add("ProcId", 3); // 3 = Soft Delete
                dbparams.Add("Msg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("[dbo].[Proc_WarehouseDetails]", dbparams, commandType: CommandType.StoredProcedure);

                var message = dbparams.Get<string>("Msg");

                return Ok(new
                {
                    success = true,
                    message = message ?? "Warehouse detail soft-deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error soft-deleting warehouse detail",
                    error = ex.Message
                });
            }
        }

        //list of all active records
        [HttpGet("get-all-active-warehouse-details")]
        public async Task<IActionResult> GetAllActiveWarehouseDetails(
        [FromQuery] int? warehouseId,
        //[FromQuery] int? Id),
        [FromQuery] int? id,
        [FromQuery] int? itemCategoryId,
        [FromQuery] int? agencyTypeId)
        //[FromQuery] int? Id)
            {
                try
                {
                    using var connection = _context.Database.GetDbConnection();
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("ProcId", 5); // Get all active records
                    parameters.Add("WarehouseId", warehouseId == 0 ? null : warehouseId);
                    //parameters.Add("id", string.IsNullOrEmpty(receiverDate) ? null : receiverDate);
                    parameters.Add("ItemCategoryId", itemCategoryId == 0 ? null : itemCategoryId);
                    parameters.Add("AgencyTypeId", agencyTypeId == 0 ? null : agencyTypeId);
                    parameters.Add("Id", id == 0 ? null : id);
                    parameters.Add("Msg", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                        var result = await connection.QueryAsync<dynamic>(
                            "Proc_WarehouseDetails",
                            parameters,
                            commandType: CommandType.StoredProcedure
                        );

                        var message = parameters.Get<string>("Msg");

                        if (result == null || !result.Any())
                        {
                            return NotFound(new { success = false, message = "No active warehouse details found." });
                        }

                        return Ok(new
                        {
                            success = true,
                            data = result
                        });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new
                        {
                            success = false,
                            message = "Error fetching active warehouse details",
                            error = ex.Message
                        });
                    }
                }
    }
}
