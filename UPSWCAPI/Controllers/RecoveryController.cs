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
                using var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@warehouseid", model.WarehouseId);
                parameters.Add("@AgencyTypeId", model.AgencyTypeId);
                parameters.Add("@commodityid", model.CommodityId);
                parameters.Add("@MonthId", model.MonthId);
                parameters.Add("@YearId", model.YearId);
                parameters.Add("@ProcId", model.ProcId);

                var result = await connection.QueryAsync("Proc_NAFEDRpt", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


    }
}
