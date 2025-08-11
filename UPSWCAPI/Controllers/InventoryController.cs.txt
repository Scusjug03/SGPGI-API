

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

namespace UPSWCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class InventoryController : Controller
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
        public InventoryController(IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
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
            return Ok("Inventory API is running.");
        }
        #region Item Grp

        // POST: Insert ItemGroup
        [HttpPost("insert-itemgroup")]
        public async Task<IActionResult> InsertItemGroup([FromBody] ItemGroupModel model)
        {
            try
            {

                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Itemgrpid", 0);
                parameters.Add("@Itemgrpname", model.Itemgrpname);
                parameters.Add("@CreatedBy", model.CreatedBy);
                parameters.Add("@UpdatedBy", 0);
                parameters.Add("@Procid", 1); // Insert
                await connection.ExecuteAsync("[dbo].[SP_ItemGroupMaster]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Item Group inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        // PUT: Update ItemGroup
        [HttpPut("update-itemgroup")]
        public async Task<IActionResult> UpdateItemGroup([FromBody] ItemGroupModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Itemgrpid", model.Itemgrpid);
                parameters.Add("@Itemgrpname", model.Itemgrpname);
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", model.UpdatedBy);
                parameters.Add("@Procid", 2); // Update

                await connection.ExecuteAsync("[dbo].[SP_ItemGroupMaster]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Item Group updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("get-all-itemgroups")]
        public async Task<IActionResult> GetAllItemGroups()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Itemgrpid", 0);          // ✅ Required
                parameters.Add("@Itemgrpname", null);     // ✅ Optional
                parameters.Add("@CreatedBy", 0);          // ✅ Optional
                parameters.Add("@UpdatedBy", 0);          // ✅ Optional
                parameters.Add("@Procid", 4);             // ✅ Required

                var result = await connection.QueryAsync("[dbo].[SP_ItemGroupMaster]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        // DELETE: Delete ItemGroup

        [HttpDelete("delete-itemgroup/{itemgrpid}")]
        public async Task<IActionResult> DeleteItemGroup(int itemgrpid)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@Itemgrpid", itemgrpid);

                await connection.ExecuteAsync("[dbo].[SP_ItemGroupMaster]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Item Group deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("search-Itemgrp")]
        public async Task<IActionResult> SearchItemgrp([FromQuery] string name)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Itemgrpid", 0);
                parameters.Add("@Itemgrpname", string.IsNullOrWhiteSpace(name) ? null : name);
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", 0);
                parameters.Add("@Procid", 6); // Search

                var result = await connection.QueryAsync("[dbo].[SP_ItemGroupMaster]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        #endregion

        #region Make

        // POST: Insert Make
        [HttpPost("insert-make")]
        public async Task<IActionResult> InsertMake([FromBody] MakeModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Makeid", 0);
                parameters.Add("@Makename", model.Makename);
                parameters.Add("@CreatedBy", model.CreatedBy);
                parameters.Add("@UpdatedBy", 0);
                parameters.Add("@Procid", 1); // Insert

                var result = await connection.QueryAsync("[dbo].[SP_MakeMaster]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Make inserted successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        // PUT: Update Make
        [HttpPut("update-make")]
        public async Task<IActionResult> UpdateMake([FromBody] MakeModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Makeid", model.Makeid);
                parameters.Add("@Makename", model.Makename);
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", model.UpdatedBy);
                parameters.Add("@Procid", 2); // Update

                var result = await connection.QueryAsync("[dbo].[SP_MakeMaster]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Make updated successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        // GET: Get All Makes
        [HttpGet("get-all-makes")]
        public async Task<IActionResult> GetAllMakes()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Makeid", 0);
                parameters.Add("@Makename", null);
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", 0);
                parameters.Add("@Procid", 4); // Get All

                var result = await connection.QueryAsync("[dbo].[SP_MakeMaster]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        // DELETE: Soft Delete Make
        [HttpDelete("delete-make/{makeid}")]
        public async Task<IActionResult> DeleteMake(int makeid)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Makeid", makeid);
                parameters.Add("@Makename", null);
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", 0);
                parameters.Add("@Procid", 5); // Soft Delete

                await connection.ExecuteAsync("[dbo].[SP_MakeMaster]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Make deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("search-make")]
        public async Task<IActionResult> SearchMake([FromQuery] string name)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Makeid", 0);
                parameters.Add("@Makename", name);
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", 0);
                parameters.Add("@Procid", 6); // Search

                var result = await connection.QueryAsync("[dbo].[SP_MakeMaster]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        #endregion

        #region Unit Details
        [HttpPost("insert-unit")]
        public async Task<IActionResult> InsertUnit([FromBody] UnitModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UnitId", 0);
                parameters.Add("@UnitName", model.UnitName);
                parameters.Add("@Remark", model.Remark);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@Procid", 1); // Insert

                await connection.ExecuteAsync("[dbo].[SP_Unit]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Unit inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // PUT: Update Unit
        [HttpPut("update-unit")]
        public async Task<IActionResult> UpdateUnit([FromBody] UnitModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UnitId", model.UnitId);
                parameters.Add("@UnitName", model.UnitName);
                parameters.Add("@Remark", model.Remark);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@Procid", 2); // Update

                await connection.ExecuteAsync("[dbo].[SP_Unit]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Unit updated successfully."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: Get Unit by ID
        [HttpGet("get-unit/{id}")]
        public async Task<IActionResult> GetUnitById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UnitId", id);
                parameters.Add("@UnitName", null);
                parameters.Add("@Remark", null);
                parameters.Add("@UserId", 0);
                parameters.Add("@Procid", 3); // Get by ID

                await connection.ExecuteAsync("[dbo].[SP_Unit]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: Get All Units
        [HttpGet("get-all-units")]
        public async Task<IActionResult> GetAllUnits()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UnitId", 0);
                parameters.Add("@UnitName", null);
                parameters.Add("@Remark", null);
                parameters.Add("@UserId", 0);
                parameters.Add("@Procid", 4); // Get all

                var result = await connection.QueryAsync("[dbo].[SP_Unit]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // DELETE: Soft Delete Unit
        [HttpDelete("delete-unit/{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UnitId", id);
                parameters.Add("@UnitName", null);
                parameters.Add("@Remark", null);
                parameters.Add("@UserId", 0);
                parameters.Add("@Procid", 5); // Soft delete

                await connection.ExecuteAsync("[dbo].[SP_Unit]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Unit deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: Search Unit
        [HttpGet("search-unit")]
        public async Task<IActionResult> SearchUnit([FromQuery] string name)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UnitId", 0);
                parameters.Add("@UnitName", name);
                parameters.Add("@Remark", null);
                parameters.Add("@UserId", 0);
                parameters.Add("@Procid", 6); // Search

                var result = await connection.QueryAsync("[dbo].[SP_Unit]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region Supplier Category

            [HttpPost("insert-supplier-category")]
            public async Task<IActionResult> InsertSupplierCategory([FromBody] SupplierCategoryModel model)
            {
                try
                {
                    using var connection = _context.Database.GetDbConnection();
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@SuppCategId", 0);
                    parameters.Add("@SuppCategNm", model.SuppCategNm);
                    parameters.Add("@Remark", model.Remark);
                    parameters.Add("@UserId", model.UserId);
                    parameters.Add("@Procid", 1); // Insert

                    var result = await connection.QueryAsync("[dbo].[SP_SupplierCategory]", parameters, commandType: CommandType.StoredProcedure);

                    return Ok(new { success = true, message = "Supplier Category inserted successfully.", data = result });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { success = false, message = ex.Message });
                }
            }

            [HttpPut("update-supplier-category")]
            public async Task<IActionResult> UpdateSupplierCategory([FromBody] SupplierCategoryModel model)
            {
                try
                {
                    using var connection = _context.Database.GetDbConnection();
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@SuppCategId", model.SuppCategId);
                    parameters.Add("@SuppCategNm", model.SuppCategNm);
                    parameters.Add("@Remark", model.Remark);
                    parameters.Add("@UserId", model.UserId); // UpdatedBy
                    parameters.Add("@Procid", 2); // Update

                    var result = await connection.QueryAsync("[dbo].[SP_SupplierCategory]", parameters, commandType: CommandType.StoredProcedure);

                    return Ok(new { success = true, message = "Supplier Category updated successfully.", data = result });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { success = false, message = ex.Message });
                }
            }

            [HttpGet("get-supplier-category/{id}")]
            public async Task<IActionResult> GetSupplierCategoryById(int id)
            {
                try
                {
                    using var connection = _context.Database.GetDbConnection();
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@SuppCategId", id);
                    parameters.Add("@SuppCategNm", null);
                    parameters.Add("@Remark", null);
                    parameters.Add("@UserId", 0);
                    parameters.Add("@Procid", 3); // Get by ID

                    var result = await connection.QueryAsync("[dbo].[SP_SupplierCategory]", parameters, commandType: CommandType.StoredProcedure);

                    return Ok(new { success = true, data = result });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { success = false, message = ex.Message });
                }
            }

            [HttpGet("get-all-supplier-categories")]
            public async Task<IActionResult> GetAllSupplierCategories()
            {
                try
                {
                    using var connection = _context.Database.GetDbConnection();
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@SuppCategId", 0);
                    parameters.Add("@SuppCategNm", null);
                    parameters.Add("@Remark", null);
                    parameters.Add("@UserId", 0);
                    parameters.Add("@Procid", 4); // Get all

                    var result = await connection.QueryAsync("[dbo].[SP_SupplierCategory]", parameters, commandType: CommandType.StoredProcedure);

                    return Ok(new { success = true, data = result });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { success = false, message = ex.Message });
                }
            }

            [HttpDelete("delete-supplier-category/{id}")]
            public async Task<IActionResult> DeleteSupplierCategory(int id)
            {
                try
                {
                    using var connection = _context.Database.GetDbConnection();
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@SuppCategId", id);
                    parameters.Add("@SuppCategNm", null);
                    parameters.Add("@Remark", null);
                    parameters.Add("@UserId", 0);
                    parameters.Add("@Procid", 5); // Soft delete

                    await connection.ExecuteAsync("[dbo].[SP_SupplierCategory]", parameters, commandType: CommandType.StoredProcedure);

                    return Ok(new { success = true, message = "Supplier Category deleted successfully." });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { success = false, message = ex.Message });
                }
            }
        

        [HttpGet("search-supplier-category")]
        public async Task<IActionResult> SearchSupplierCategory([FromQuery] string name)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@SuppCategId", 0); // Not needed for search
                parameters.Add("@SuppCategNm", string.IsNullOrWhiteSpace(name) ? null : name);
                parameters.Add("@Remark", null);
                parameters.Add("@UserId", 0);
                parameters.Add("@Procid", 6); // Search

                var result = await connection.QueryAsync("[dbo].[SP_SupplierCategory]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }



        #endregion

        #region Item Details


        [HttpPost("insert-item")]
        public async Task<IActionResult> InsertItem([FromBody] ItemDetailsModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Itemid", 0);
                parameters.Add("@Rackno", model.Rackno);
                parameters.Add("@ItemName", model.ItemName);
                parameters.Add("@ManufactureID", model.ManufactureID);
                parameters.Add("@ModelId", model.ModelId);
                parameters.Add("@SetID", model.SetID);
                parameters.Add("@SetQty", model.SetQty);
                parameters.Add("@UnitId", model.UnitId);
                parameters.Add("@Manufatureno", model.Manufatureno);
                parameters.Add("@Itemgrpid", model.Itemgrpid);
                parameters.Add("@ItemTypeId", model.ItemTypeId);
                parameters.Add("@ItemCategoryId", model.ItemCategoryId);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@ProcID", 1); // Insert

                var result = await connection.QueryAsync("[dbo].[PROC_ItemDetails]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Item inserted successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update-item")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemDetailsModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Itemid", model.Itemid);
                parameters.Add("@Rackno", model.Rackno);
                parameters.Add("@ItemName", model.ItemName);
                parameters.Add("@ManufactureID", model.ManufactureID);
                parameters.Add("@ModelId", model.ModelId);
                parameters.Add("@SetID", model.SetID);
                parameters.Add("@SetQty", model.SetQty);
                parameters.Add("@UnitId", model.UnitId);
                parameters.Add("@Manufatureno", model.Manufatureno);
                parameters.Add("@Itemgrpid", model.Itemgrpid);
                parameters.Add("@ItemTypeId", model.ItemTypeId);
                parameters.Add("@ItemCategoryId", model.ItemCategoryId);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@ProcID", 2); // Update

                var result = await connection.QueryAsync("[dbo].[PROC_ItemDetails]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Item updated successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("get-item/{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Itemid", id);
                parameters.Add("@Rackno", null);
                parameters.Add("@ItemName", null);
                parameters.Add("@ManufactureID", 0);
                parameters.Add("@ModelId", 0);
                parameters.Add("@SetID", 0);
                parameters.Add("@SetQty", 0); 
                parameters.Add("@UnitId", 0);
                parameters.Add("@Manufatureno", null);
                parameters.Add("@Itemgrpid", 0);
                parameters.Add("@ItemTypeId", 0);
                parameters.Add("@ItemCategoryId", 0);
                parameters.Add("@UserId", 0);
                parameters.Add("@ProcID", 3); // Get by ID

                var result = await connection.QueryAsync("[dbo].[PROC_ItemDetails]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpGet("get-all-items")]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                //parameters.Add("@Itemid", id);
                parameters.Add("@Rackno", null);
                parameters.Add("@ItemName", null);
                parameters.Add("@ManufactureID", 0);
                parameters.Add("@ModelId", 0);
                parameters.Add("@SetID", 0);
                parameters.Add("@SetQty", 0);
                parameters.Add("@UnitId", 0);
                parameters.Add("@Manufatureno", null);
                parameters.Add("@Itemgrpid", 0);
                parameters.Add("@ItemTypeId", 0);
                parameters.Add("@ItemCategoryId", 0);
                parameters.Add("@UserId", 0);
                parameters.Add("@ProcID", 4); // Get all

                var result = await connection.QueryAsync("[dbo].[PROC_ItemDetails]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("delete-item/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Itemid", id);
                parameters.Add("@Rackno", null);
                parameters.Add("@ItemName", null);
                parameters.Add("@ManufactureID", 0);
                parameters.Add("@ModelId", 0);
                parameters.Add("@SetID", 0);
                parameters.Add("@SetQty", 0);
                parameters.Add("@UnitId", 0);
                parameters.Add("@Manufatureno", null);
                parameters.Add("@Itemgrpid", 0);
                parameters.Add("@ItemTypeId", 0);
                parameters.Add("@ItemCategoryId", 0);
                parameters.Add("@UserId", 0);
                parameters.Add("@ProcID", 5); // Soft delete

                await connection.ExecuteAsync("[dbo].[PROC_ItemDetails]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Item deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("search-item")]
        public async Task<IActionResult> SearchItem([FromQuery] string name)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ItemName", string.IsNullOrWhiteSpace(name) ? null : name);
                parameters.Add("@ProcID", 6); // Search

                var result = await connection.QueryAsync("[dbo].[PROC_ItemDetails]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Supplier Details

        [HttpPost("insert-supplier")]
        public async Task<IActionResult> InsertSupplier([FromBody] SupplierDetailsModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@SupplierId", 0);
                parameters.Add("@SupplierName", model.SupplierName);
                parameters.Add("@SupplierAddress", model.SupplierAddress);
                parameters.Add("@MobileNo", model.MobileNo);
                parameters.Add("@PhoneNo", model.PhoneNo);
                parameters.Add("@FaxNo", model.FaxNo);
                parameters.Add("@TinNo", model.TinNo);
                parameters.Add("@STDCode", model.STDCode);
                parameters.Add("@ContactPerson", model.ContactPerson);
                parameters.Add("@ContactPersonNo", model.ContactPersonNo);
                parameters.Add("@SuppCategId", model.SuppCategId);
                parameters.Add("@EmailId", model.EmailId);
                parameters.Add("@PANNo", model.PANNo);
                parameters.Add("@GSTNo", model.GSTNo);
                parameters.Add("@Website", model.Website);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@ProcID", 1); // Insert operation

                var result = await connection.QueryAsync("[dbo].[PROC_SupplierDetails]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Supplier inserted successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

    }
}
