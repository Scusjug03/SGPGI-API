//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;
//using UPSWCAPI.Model;
//using static UPSWCAPI.Model.EnquiryDbContext;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Dapper;
//using Microsoft.AspNetCore.Http;
//using static System.Runtime.InteropServices.JavaScript.JSType;
//using UPSWCAPI.Services;
//using Microsoft.EntityFrameworkCore;
//using System.Xml.Linq;
//using Microsoft.Extensions.Configuration;
//using System.Globalization;
//using UPSWCAPI.Model.UPSWCAPI.Model;
//using Microsoft.Data.SqlClient;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

//namespace UPSWCAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [EnableCors("allowCors")]

//    public class Demand : Controller
//    {
//        private readonly IDapper _dapper;
//        private IWebHostEnvironment Environment;
//        private IConfiguration Configuration;

//        public Demand (IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
//        {
//            Environment = _environment;
//            Configuration = _configuration;
//            _context = context;
//            _dapper = dapper;
//        }
//        private readonly EnquiryDbContext _context;

//        [HttpGet]
//        public IActionResult Index()
//        {
//            return Ok("Demand API is running.");
//        }

//        #region DemandForm

//        [HttpPost("insert-demand")]
//        public async Task<IActionResult> InsertDemand([FromBody] List<DemandInsertDto> modelList)
//        {
//            try
//            {
//                if (modelList == null || !modelList.Any())
//                {
//                    return BadRequest(new { success = false, message = "No demand entries provided." });
//                }

//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                // Create a DataTable matching SQL TVP structure
//                var dt = new DataTable();
//                dt.Columns.Add("MakeId", typeof(int));
//                dt.Columns.Add("OfficeDemandQty", typeof(decimal));
//                dt.Columns.Add("UnitId", typeof(int));
//                dt.Columns.Add("ItemId", typeof(int));
//                dt.Columns.Add("OfficeRemarks", typeof(string));
//                dt.Columns.Add("StatusId", typeof(int));
//                dt.Columns.Add("UserId", typeof(int));

//                foreach (var entry in modelList)
//                {
//                    dt.Rows.Add(
//                        entry.MakeId,
//                        entry.OfficeDemandQty,
//                        entry.UnitId,
//                        entry.ItemId,
//                        entry.OfficeRemarks ?? string.Empty,
//                        entry.StatusId ?? 1,
//                        entry.UserId
//                    );
//                }

//                var parameters = new DynamicParameters();
//                parameters.Add("@ProcId", 1);
//                parameters.Add("@DemandEntries", dt.AsTableValuedParameter("dbo.DemandEntryType")); // <-- your TVP name

//                var result = await connection.QueryAsync("[dbo].[Proc_demandForm]", parameters, commandType: CommandType.StoredProcedure);

//                return Ok(new { success = true, message = "Batch demand inserted.", data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
//            }
//        }


//        [HttpGet("get-forwarded-demands")]
//        public async Task<IActionResult> GetForwardedDemands()
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@ProcId", 2); // For get all where status = 'Forward to RM Office'

//                var result = await connection.QueryAsync<dynamic>(
//                    "[dbo].[Proc_demandForm]",
//                    parameters,
//                    commandType: CommandType.StoredProcedure
//                );

//                return Ok(new { success = true, data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
//            }
//        }


//        [HttpPut("update-demand/{DemandId}")]
//        public async Task<IActionResult> UpdateDemand(int DemandId, [FromBody] DemandInsertDto model)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@ProcId", 3); // For Update
//                parameters.Add("@DemandId", DemandId); // Passed in URL
//                parameters.Add("@MakeId", model.MakeId);
//                parameters.Add("@RmAppQty", model.RmAppQty);
//                parameters.Add("@UnitId", model.UnitId);
//                parameters.Add("@ItemId", model.ItemId);
//                parameters.Add("@OfficeRemarks", model.OfficeRemarks);
//                parameters.Add("@StatusId", model.StatusId);
//                parameters.Add("@UserId", model.UserId);

//                var result = await connection.QueryAsync<dynamic>("[dbo].[Proc_demandForm]", parameters, commandType: CommandType.StoredProcedure);

//                return Ok(new { success = true, message = "Demand updated successfully.", data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
//            }
//        }

//        [HttpPut("update-demand-emp/{DemandId}")]
//        public async Task<IActionResult> UpdateDemandEmp(int DemandId, [FromBody] DemandInsertDto model)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@ProcId", 4); // For Update
//                parameters.Add("@DemandId", DemandId); // Passed in URL
//                parameters.Add("@MakeId", model.MakeId);
//                parameters.Add("@OfficeDemandQty", model.OfficeDemandQty);
//                parameters.Add("@UnitId", model.UnitId);
//                parameters.Add("@ItemId", model.ItemId);
//                parameters.Add("@RMOfficeRemarks", model.RMOfficeRemarks);
//                parameters.Add("@StatusId", model.StatusId);
//                parameters.Add("@UserId", model.UserId);

//                var result = await connection.QueryAsync<dynamic>("[dbo].[Proc_demandForm]", parameters, commandType: CommandType.StoredProcedure);

//                return Ok(new { success = true, message = "Demand updated successfully.", data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
//            }
//        }

//        [HttpGet("get-demand-by-id/{id}")]
//        public async Task<IActionResult> GetDemandById(int id)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@ProcId", 5); // For example
//                parameters.Add("@DemandId", id);

//                var result = await connection.QueryAsync("[dbo].[Proc_demandForm]", parameters, commandType: CommandType.StoredProcedure);
//                return Ok(new { success = true, data = result });
//            }
//                catch (Exception ex)
//                {
//                    return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
//                }
//            }

//        [HttpGet("get-RM-demands")]
//        public async Task<IActionResult> GetRMDemands()
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@ProcId", 6); // For get all where status = 'Forward to RM Office'

//                var result = await connection.QueryAsync<dynamic>(
//                    "[dbo].[Proc_demandForm]",
//                    parameters,
//                    commandType: CommandType.StoredProcedure
//                );

//                return Ok(new { success = true, data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
//            }
//        }

//        [HttpPut("update-ho-demand/{DemandId}")]
//        public async Task<IActionResult> UpdateHODemand(int DemandId, [FromBody] DemandInsertDto model)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@ProcId", 7); // HO Update
//                parameters.Add("@DemandId", DemandId); // From URL
//                parameters.Add("@ItemId", model.ItemId);
//                parameters.Add("@UnitId", model.UnitId);
//                parameters.Add("@MakeId", model.MakeId);
//                parameters.Add("@HOAppQty", model.HOAppQty); // Approved Quantity by HO
//                parameters.Add("@HORemarks", model.HORemarks); // Remarks by HO
//                parameters.Add("@StatusId", model.StatusId);
//                parameters.Add("@UserId", model.UserId);

//                var result = await connection.QueryAsync("[dbo].[Proc_demandForm]", parameters, commandType: CommandType.StoredProcedure);

//                return Ok(new { success = true, message = "HO Demand updated successfully.", data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
//            }
//        }

//        [HttpGet("get-emp-demands")]
//        public async Task<IActionResult> GetEmpDemands()
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@ProcId", 8); // For get all where status = 'Forward to RM Office'

//                var result = await connection.QueryAsync<dynamic>(
//                    "[dbo].[Proc_demandForm]",
//                    parameters,
//                    commandType: CommandType.StoredProcedure
//                );

//                return Ok(new { success = true, data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
//            }
//        }


//        #endregion

//        #region Item Details


//        [HttpPost("insert-item")]
//        public async Task<IActionResult> InsertItem([FromBody] ItemDetailsModel model)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@Itemid", 0);
//                parameters.Add("@Rackno", model.Rackno);
//                parameters.Add("@ItemName", model.ItemName);
//                parameters.Add("@ManufctName", model.ManufctName);
//                parameters.Add("@ModelId", model.ModelId);
//                parameters.Add("@SetID", model.SetID);
//                parameters.Add("@SetQty", model.SetQty);
//                parameters.Add("@UnitId", model.UnitId);
//                parameters.Add("@Manufatureno", model.Manufatureno);
//                parameters.Add("@Itemgrpid", model.Itemgrpid);
//                parameters.Add("@ItemTypeId", model.ItemTypeId);
//                parameters.Add("@ItemCategoryId", model.ItemCategoryId);
//                parameters.Add("@UserId", model.UserId);
//                parameters.Add("@ProcID", 1); // Insert

//                var result = await connection.QueryAsync("[dbo].[PROC_ItemDetails]", parameters, commandType: CommandType.StoredProcedure);
//                return Ok(new { success = true, message = "Item inserted successfully.", data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { success = false, message = ex.Message });
//            }
//        }

//        [HttpPut("update-item")]
//        public async Task<IActionResult> UpdateItem([FromBody] ItemDetailsModel model)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@Itemid", model.Itemid);
//                parameters.Add("@Rackno", model.Rackno);
//                parameters.Add("@ItemName", model.ItemName);
//                parameters.Add("@ManufctName", model.ManufctName);
//                parameters.Add("@ModelId", model.ModelId);
//                parameters.Add("@SetID", model.SetID);
//                parameters.Add("@SetQty", model.SetQty);
//                parameters.Add("@UnitId", model.UnitId);
//                parameters.Add("@Manufatureno", model.Manufatureno);
//                parameters.Add("@Itemgrpid", model.Itemgrpid);
//                parameters.Add("@ItemTypeId", model.ItemTypeId);
//                parameters.Add("@ItemCategoryId", model.ItemCategoryId);
//                parameters.Add("@UserId", model.UserId);
//                parameters.Add("@ProcID", 2); // Update

//                var result = await connection.QueryAsync("[dbo].[PROC_ItemDetails]", parameters, commandType: CommandType.StoredProcedure);
//                return Ok(new { success = true, message = "Item updated successfully.", data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { success = false, message = ex.Message });
//            }
//        }

//        [HttpGet("get-item/{id}")]
//        public async Task<IActionResult> GetItemById(int id)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@Itemid", id);
//                parameters.Add("@Rackno", null);
//                parameters.Add("@ItemName", null);
//                parameters.Add("@ManufctName", null);
//                parameters.Add("@ModelId", 0);
//                parameters.Add("@SetID", 0);
//                parameters.Add("@SetQty", 0); 
//                parameters.Add("@UnitId", 0);
//                parameters.Add("@Manufatureno", null);
//                parameters.Add("@Itemgrpid", 0);
//                parameters.Add("@ItemTypeId", 0);
//                parameters.Add("@ItemCategoryId", 0);
//                parameters.Add("@UserId", 0);
//                parameters.Add("@ProcID", 3); // Get by ID

//                var result = await connection.QueryAsync("[dbo].[PROC_ItemDetails]", parameters, commandType: CommandType.StoredProcedure);

//                return Ok(new { success = true, data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { success = false, message = ex.Message });
//            }
//        }


//        [HttpGet("get-all-items")]
//        public async Task<IActionResult> GetAllItems()
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                //parameters.Add("@Itemid", id);
//                parameters.Add("@Rackno", null);
//                parameters.Add("@ItemName", null);
//                parameters.Add("@ManufctName", null);
//                parameters.Add("@ModelId", 0);
//                parameters.Add("@SetID", 0);
//                parameters.Add("@SetQty", 0);
//                parameters.Add("@UnitId", 0);
//                parameters.Add("@Manufatureno", null);
//                parameters.Add("@Itemgrpid", 0);
//                parameters.Add("@ItemTypeId", 0);
//                parameters.Add("@ItemCategoryId", 0);
//                parameters.Add("@UserId", 0);
//                parameters.Add("@ProcID", 4); // Get all

//                var result = await connection.QueryAsync("[dbo].[PROC_ItemDetails]", parameters, commandType: CommandType.StoredProcedure);
//                return Ok(new { success = true, data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { success = false, message = ex.Message });
//            }
//        }

//        [HttpDelete("delete-item/{id}")]
//        public async Task<IActionResult> DeleteItem(int id)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@Itemid", id);
//                parameters.Add("@Rackno", null);
//                parameters.Add("@ItemName", null);
//                parameters.Add("@ManufctName", null);
//                parameters.Add("@ModelId", 0);
//                parameters.Add("@SetID", 0);
//                parameters.Add("@SetQty", 0);
//                parameters.Add("@UnitId", 0);
//                parameters.Add("@Manufatureno", null);
//                parameters.Add("@Itemgrpid", 0);
//                parameters.Add("@ItemTypeId", 0);
//                parameters.Add("@ItemCategoryId", 0);
//                parameters.Add("@UserId", 0);
//                parameters.Add("@ProcID", 5); // Soft delete

//                await connection.ExecuteAsync("[dbo].[PROC_ItemDetails]", parameters, commandType: CommandType.StoredProcedure);
//                return Ok(new { success = true, message = "Item deleted successfully." });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { success = false, message = ex.Message });
//            }
//        }

//        [HttpGet("search-item")]
//        public async Task<IActionResult> SearchItem([FromQuery] string name)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@ItemName", string.IsNullOrWhiteSpace(name) ? null : name);
//                parameters.Add("@ProcID", 6); // Search

//                var result = await connection.QueryAsync("[dbo].[PROC_ItemDetails]", parameters, commandType: CommandType.StoredProcedure);
//                return Ok(new { success = true, data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { success = false, message = ex.Message });
//            }
//        }

//        #endregion

//        #region Supplier Details

//        [HttpPost("insert-supplier")]
//        public async Task<IActionResult> InsertSupplier([FromBody] SupplierDetailsModel model)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@SupplierId", 0);
//                parameters.Add("@SupplierName", model.SupplierName);
//                parameters.Add("@SupplierAddress", model.SupplierAddress);
//                parameters.Add("@MobileNo", model.MobileNo);
//                parameters.Add("@PhoneNo", model.PhoneNo);
//                parameters.Add("@FaxNo", model.FaxNo);
//                parameters.Add("@TinNo", model.TinNo);
//                parameters.Add("@STDCode", model.STDCode);
//                parameters.Add("@ContactPerson", model.ContactPerson);
//                parameters.Add("@ContactPersonNo", model.ContactPersonNo);
//                parameters.Add("@SuppCategId", model.SuppCategId);
//                parameters.Add("@EmailId", model.EmailId);
//                parameters.Add("@PANNo", model.PANNo);
//                parameters.Add("@GSTNo", model.GSTNo);
//                parameters.Add("@Website", model.Website);
//                parameters.Add("@UserId", model.UserId);
//                parameters.Add("@ProcID", 1); // Insert operation

//                var result = await connection.QueryAsync("[dbo].[PROC_SupplierDetails]", parameters, commandType: CommandType.StoredProcedure);
//                return Ok(new { success = true, message = "Supplier inserted successfully.", data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { success = false, message = ex.Message });
//            }
//        }

//        [HttpPut("update-supplier")]
//        public async Task<IActionResult> UpdateSupplier([FromBody] SupplierDetailsModel model)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@SupplierId", model.SupplierId);
//                parameters.Add("@SupplierName", model.SupplierName);
//                parameters.Add("@SupplierAddress", model.SupplierAddress);
//                parameters.Add("@MobileNo", model.MobileNo);
//                parameters.Add("@PhoneNo", model.PhoneNo);
//                parameters.Add("@FaxNo", model.FaxNo);
//                parameters.Add("@TinNo", model.TinNo);
//                parameters.Add("@STDCode", model.STDCode);
//                parameters.Add("@ContactPerson", model.ContactPerson);
//                parameters.Add("@ContactPersonNo", model.ContactPersonNo);
//                parameters.Add("@SuppCategId", model.SuppCategId);
//                parameters.Add("@EmailId", model.EmailId);
//                parameters.Add("@PANNo", model.PANNo);
//                parameters.Add("@GSTNo", model.GSTNo);
//                parameters.Add("@Website", model.Website);
//                parameters.Add("@UserId", model.UserId);
//                parameters.Add("@ProcID", 2); // Insert operation

//                var result = await connection.QueryAsync("[dbo].[PROC_SupplierDetails]", parameters, commandType: CommandType.StoredProcedure);
//                return Ok(new { success = true, message = "Supplier inserted successfully.", data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { success = false, message = ex.Message });
//            }
//        }

//        [HttpGet("all-suppliers")]
//        public async Task<IActionResult> GetAllSuppliers()
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@ProcID", 3); // 3 = get all

//                var result = await connection.QueryAsync("[dbo].[PROC_SupplierDetails]", parameters, commandType: CommandType.StoredProcedure);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { success = false, message = ex.Message });
//            }
//        }

//        // GET BY ID
//        [HttpGet("get-supplier/{id}")]
//        public async Task<IActionResult> GetSupplierById(int id)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@SupplierId", id);
//                parameters.Add("@ProcID", 4); // 4 = get by id

//                var result = await connection.QueryAsync("[dbo].[PROC_SupplierDetails]", parameters, commandType: CommandType.StoredProcedure);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { success = false, message = ex.Message });
//            }
//        }

//        // SOFT DELETE
//        [HttpPut("delete-supplier/{id}")]
//        public async Task<IActionResult> SoftDeleteSupplier(int id, [FromQuery] int userId)
//        {
//            try
//            {
//                using var connection = _context.Database.GetDbConnection();
//                await connection.OpenAsync();

//                var parameters = new DynamicParameters();
//                parameters.Add("@SupplierId", id);
//                parameters.Add("@UserId", userId);
//                parameters.Add("@ProcID", 5); // 5 = soft delete

//                var result = await connection.QueryAsync("[dbo].[PROC_SupplierDetails]", parameters, commandType: CommandType.StoredProcedure);
//                return Ok(new { success = true, message = "Supplier deleted (soft).", data = result });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { success = false, message = ex.Message });
//            }
//        }


//        #endregion

//    }
//}
