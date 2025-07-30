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
    public class RecoveryController : Controller
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
        public RecoveryController(IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
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

        [HttpPost("NAFEDReport")]

        public async Task<IActionResult> NAFEDReport([FromBody] NAFEDReportRequest model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                //using var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                // await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@warehouseid", model.WarehouseId);
                parameters.Add("@AgencyTypeId", model.AgencyTypeId);
                parameters.Add("@commodityid", model.CommodityId);
                parameters.Add("@MonthId", model.MonthId);
                parameters.Add("@YearId", model.YearId);
                parameters.Add("@ProcId", model.ProcId);
                parameters.Add("@Remark", model.Remark);


                var result = await connection.QueryAsync("Proc_NAFEDRpt", parameters, commandType: CommandType.StoredProcedure);

                var firstRow = result.FirstOrDefault();
                return Ok(new
                {
                    data = result,
                    CommodityName = firstRow?.CommodityName ?? "",
                    WareHouseName = firstRow?.WareHouseName ?? "",
                    RegionName = firstRow?.RegionName ?? "",
                    BillCode = firstRow?.BillCode ?? "",
                    GenBillDate = firstRow?.GenBillDate ?? "",
                    AgencyName = firstRow?.AgencyName ?? ""
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("NAFEDInsurnaceReport")]

        public async Task<IActionResult> NAFEDInsurnaceReport([FromBody] NAFEDReportRequest model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                //using var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                // await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@warehouseid", model.WarehouseId);
                parameters.Add("@AgencyTypeId", model.AgencyTypeId);
                parameters.Add("@commodityid", model.CommodityId);
                parameters.Add("@MonthId", model.MonthId);
                parameters.Add("@YearId", model.YearId);
                parameters.Add("@ProcId", model.ProcId);
                parameters.Add("@Remark", model.Remark);


                var result = await connection.QueryAsync("Proc_NAFEDRpt", parameters, commandType: CommandType.StoredProcedure);

                var firstRow = result.FirstOrDefault();
                return Ok(new
                {
                    data = result,
                    CommodityName = firstRow?.CommodityName ?? "",
                    WareHouseName = firstRow?.WareHouseName ?? "",
                    RegionName = firstRow?.RegionName ?? "",
                    BillCode = firstRow?.BillCode ?? "",
                    GenBillDate = firstRow?.GenBillDate ?? "",
                    AgencyName = firstRow?.AgencyName ?? ""
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #region Bill-generation
        [HttpPost("Generate-Nafed-Bill")]
        public async Task<IActionResult> GenerateBill([FromBody] BillRequestDto model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Warehouseid", model.WarehouseId);
                parameters.Add("@Agencytypeid", model.Agencytypeid);
                parameters.Add("@CommodityId", model.CommodityId);
                parameters.Add("@BillMonth", model.BillMonth);
                parameters.Add("@BillYear", model.BillYear);
                parameters.Add("@Userid", model.UesrId);
                //parameters.Add("@ProcId", model.ProcId);

                await connection.ExecuteAsync("sp_CalDailyStorageCharges_Nafed", parameters, commandType: CommandType.StoredProcedure);


                return Ok(new { success = true, message = "Bill generated successfully (default month/year used)." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error occurred: " + ex.Message });
            }
        }

        #endregion

        #region Nafed Forward Report
        [HttpPost("NAFEDForwardReport")]

        public async Task<IActionResult> NAFEDForwardReport([FromBody] NAFEDReportRequest model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                //using var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                // await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@warehouseid", model.WarehouseId);
                parameters.Add("@AgencyTypeId", model.AgencyTypeId);
                parameters.Add("@commodityid", model.CommodityId);
                parameters.Add("@MonthId", model.MonthId);
                parameters.Add("@YearId", model.YearId);
                parameters.Add("@BillStatusId", 0);
                parameters.Add("@Remark", model.Remark);
                parameters.Add("@ProcId", model.ProcId);



                var result = await connection.QueryAsync("Proc_NAFEDRpt", parameters, commandType: CommandType.StoredProcedure);

                var firstRow = result.FirstOrDefault();
                return Ok(new
                {
                    data = result,
                    CommodityName = firstRow?.CommodityName ?? "",
                    WareHouseName = firstRow?.WareHouseName ?? "",
                    RegionName = firstRow?.RegionName ?? "",
                    BillCode = firstRow?.BillCode ?? "",
                    GenBillDate = firstRow?.GenBillDate ?? "",
                    AgencyName = firstRow?.AgencyName ?? ""
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        [HttpPost("ForwardToRM")]
        public async Task<IActionResult> ForwardToRM([FromBody] NafedForwardDto model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@WarehouseId", model.WarehouseId);
                parameters.Add("@AgencyTypeId", model.AgencyTypeId);
                parameters.Add("@CommodityId", model.CommodityId);
                parameters.Add("@MonthId", model.MonthId);
                parameters.Add("@YearId", model.YearId);
                parameters.Add("@ProcId", model.ProcId);
                parameters.Add("@Remark", model.Remark);

                await connection.ExecuteAsync("Proc_NAFEDRpt", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Forwarded to R.M. successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error occurred: " + ex.Message });
            }
        }

    }
}
