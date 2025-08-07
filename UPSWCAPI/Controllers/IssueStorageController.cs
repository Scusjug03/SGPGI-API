using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using UPSWCAPI.Model;

namespace UPSWCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueStorageController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public IssueStorageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("InsertIssueStorage")]
        public async Task<IActionResult> SaveIssueStorage([FromBody] IssueStorage issue)
        {
            string message = "";
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("EnquiryCon"));


                var parameters = new DynamicParameters();
                //parameters.Add("@IssueDate", issue.IssueDate);
                //parameters.Add("@WarehouseId", issue.WarehouseId);
                //parameters.Add("@SerialNumber", issue.SerialNumber);
                //parameters.Add("@LotNumber", issue.LotNumber );
                //parameters.Add("@CommodityId", issue.CommodityId);
                //parameters.Add("@AgencyTypeId", issue.AgencyTypeId);
                //parameters.Add("@SenderName", issue.SenderName);
                //parameters.Add("@RemainingStock", issue.RemainingStock);
                //parameters.Add("@RemainingBags", issue.RemainingBags);
                //parameters.Add("@GrossWeight_kg", issue.GrossWeight_kg);
                //parameters.Add("@TruckWeight_kg", issue.TruckWeight_kg);
                //parameters.Add("@NoOfBag_1", issue.NoOfBag_1);
                //parameters.Add("@WeightPerBag_1_kg", issue.WeightPerBag_1_kg);
                //parameters.Add("@BagWeight_1_kg", issue.BagWeight_1_kg);
                //parameters.Add("@NoOfBag_2", issue.NoOfBag_2);
                //parameters.Add("@WeightPerBag_2_kg", issue.WeightPerBag_2_kg);
                //parameters.Add("@BagWeight_2_kg", issue.BagWeight_2_kg);
                //parameters.Add("@TotalNoOf_bag", issue.TotalNoOf_bag);
                //parameters.Add("@TotalBagWeight", issue.TotalBagWeight);
                //parameters.Add("@NetWeight", issue.NetWeight);
                //parameters.Add("@MaterialWeight_kg", issue.MaterialWeight_kg);
                //parameters.Add("@VehicleNo", issue.VehicleNo);
                //parameters.Add("@GatePassNo", issue.GatePassNo);
                //parameters.Add("@IssuerName", issue.IssuerName);
                //parameters.Add("@WeightingWay", issue.WeightingWay);
                //

                parameters.Add("@IssueDate", issue.IssueDate);
                parameters.Add("@FinancialYear", issue.FinancialYear);
                parameters.Add("@RequestedIssueQuantity", issue.RequestedIssueQuantity);
                parameters.Add("@WarehouseId", issue.WarehouseId);
                parameters.Add("@ReceiveId", issue.ReceiveId);
                //parameters.Add("@SerialNumber", issue.SerialNumber);
                parameters.Add("@LotNo", issue.LotNumber);
                parameters.Add("@CommodityId", issue.CommodityId);
                parameters.Add("@AgencyTypeId", issue.AgencyTypeId);
                parameters.Add("@SenderName", issue.SenderName);
                parameters.Add("@SubSenderName", issue.SubSenderName);
                //parameters.Add("@RemainingStock", issue.RemainingStock);
                //parameters.Add("@RemainingBags", issue.RemainingBags);

                parameters.Add("@GrossWeight_kg", issue.GrossWeight_kg);
                parameters.Add("@TruckWeight_kg", issue.TruckWeight_kg);
                parameters.Add("@NoOfBag_1", issue.NoOfBag_1);
                parameters.Add("@WeightPerBag_1_kg", issue.WeightPerBag_1_kg);
                parameters.Add("@BagWeight_1_kg", issue.BagWeight_1_kg);
                parameters.Add("@NoOfBag_2", issue.NoOfBag_2);
                parameters.Add("@WeightPerBag_2_kg", issue.WeightPerBag_2_kg);
                parameters.Add("@BagWeight_2_kg", issue.BagWeight_2_kg);

                parameters.Add("@TotalNoOf_bag", issue.TotalNoOf_bag);
                parameters.Add("@TotalBagWeight", issue.TotalBagWeight);
                parameters.Add("@NetWeight", issue.NetWeight);
                parameters.Add("@MaterialWeight_kg", issue.MaterialWeight_kg);
                parameters.Add("@VehicleNo", issue.VehicleNo);
                parameters.Add("@GatePassNo", issue.GatePassNo);
                parameters.Add("@IssuerName", issue.IssuerName);
                parameters.Add("@WeightingWay", issue.WeightingWay);

                parameters.Add("@Message", dbType: DbType.String, size: 250, direction: ParameterDirection.Output);


                await connection.ExecuteAsync("sp_Insert_IssueStorage", parameters, commandType: CommandType.StoredProcedure);

                message = parameters.Get<string>("@Message");

                return Ok(new { success = true, message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        //public async Task<IActionResult> BulkSaveIssueStorage([FormBody] BulkIssueStorage issue)
        //{

        //}


    }
}
