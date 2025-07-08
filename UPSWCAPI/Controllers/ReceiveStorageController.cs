using System.Data;
using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UPSWCAPI.Model;
using UPSWCAPI.Services;

namespace UPSWCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("allowCors")]
    public class ReceiveStorageController : ControllerBase
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private readonly IConfiguration _configuration;

        public ReceiveStorageController(IConfiguration configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
        {
            _configuration = configuration;
            _dapper = dapper;
            _context = context;
            Environment = _environment;
        }

        private readonly EnquiryDbContext _context;

        [HttpPost("InsertReceiveStorage")]
        public async Task<IActionResult> InsertReceiveStorage([FromBody] ReceiveStorageDto model)
        {
            string message = "";

            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("EnquiryCon"));
                var parameters = new DynamicParameters();

                parameters.Add("@WarehouseId", model.WarehouseId);
                parameters.Add("@CommodityId", model.CommodityId);
                parameters.Add("@AgencyTypeId", model.AgencyTypeId);
                parameters.Add("@SerialNumber", model.SerialNumber);
                parameters.Add("@LotNumber", model.LotNumber);
                parameters.Add("@SenderName", model.SenderName);
                parameters.Add("@GrossWeight", model.GrossWeight);
                parameters.Add("@NetWeight", model.NetWeight);
                parameters.Add("@TruckWeight", model.TruckWeight);
                parameters.Add("@TotalNoOfBags", model.TotalNoOfBags);
                parameters.Add("@NoOfBag_1", model.NoOfBag_1);
                parameters.Add("@WeightPerBag_1_kg", model.WeightPerBag_1_kg);
                parameters.Add("@BagWeight_1_kg", model.BagWeight_1_kg);
                parameters.Add("@NoOfBag_2", model.NoOfBag_2);
                parameters.Add("@WeightPerBag_2_kg", model.WeightPerBag_2_kg);
                parameters.Add("@BagWeight_2_kg", model.BagWeight_2_kg);
                parameters.Add("@TotalBagWeight_kg", model.TotalBagWeight_kg);
                parameters.Add("@NetWeight_kg", model.NetWeight_kg);
                parameters.Add("@MaterialWeight_kg", model.MaterialWeight_kg);
                parameters.Add("@VehicleNumber", model.VehicleNumber);
                parameters.Add("@GatePassNumber", model.GatePassNumber);
                parameters.Add("@ReceiverName", model.ReceiverName);
                parameters.Add("@WeightingWay", model.WeightingWay);
                parameters.Add("@ReceiveDate", model.ReceiveDate);

                parameters.Add("@Message", dbType: DbType.String, size: 250, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("Save_ReceiveStorage", parameters, commandType: CommandType.StoredProcedure);

                message = parameters.Get<string>("@Message");

                return Ok(new { success = true, message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
