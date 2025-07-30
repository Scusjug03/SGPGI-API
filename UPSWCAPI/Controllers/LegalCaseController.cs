using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using UPSWCAPI.Model;
using UPSWCAPI.Model.UPSWCAPI.Model;
using UPSWCAPI.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static UPSWCAPI.Model.EnquiryDbContext;

namespace UPSWCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class LegalCaseController : Controller
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
        public LegalCaseController(IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
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


        #region CourtMaster
        [HttpPost("InsertCourtMaster")]
        public async Task<IActionResult> InsertCourtMaster([FromBody] CourtMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procId", 1);
                parameters.Add("@CourtType", model.CourtType ?? string.Empty);
                parameters.Add("@ShortName", model.ShortName ?? string.Empty);
                parameters.Add("@Description", model.Description ?? string.Empty);
                parameters.Add("@CreatedBy", model.CreatedBy);

                var result = await connection.QueryAsync("[dbo].[sp_CourtType]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateCourtType")]
        public async Task<IActionResult> UpdateCourtType([FromBody] CourtMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procId", 2);
                parameters.Add("@CourtTypeID", model.CourtTypeID);
                parameters.Add("@CourtType", model.CourtType ?? string.Empty);
                parameters.Add("@ShortName", model.ShortName ?? string.Empty);
                parameters.Add("@Description", model.Description ?? string.Empty);
                parameters.Add("@UpdatedBy", model.UpdatedBy);

                var result = await connection.QueryAsync("[dbo].[sp_CourtType]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetCourtMasterById/{id}")]
        public async Task<IActionResult> GetCourtMasterById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procId", 3);
                parameters.Add("@CourtTypeID", id);
                parameters.Add("@CourtType", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@Description", string.Empty);
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", 0);

                var data = await connection.QueryAsync("[dbo].[sp_CourtType]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllCourtCase")]
        public async Task<IActionResult> GetAllCourtCase()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procId", 4);
                parameters.Add("@CourtType", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@Description", string.Empty);
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", 0);

                var result = await connection.QueryAsync("[dbo].[sp_CourtType]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteCourtMaster/{id}")]
        public async Task<IActionResult> DeleteCourtMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procId", 5);
                parameters.Add("@CourtTypeID", id);
                parameters.Add("@CourtType", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@Description", string.Empty);
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", 0);

                await connection.ExecuteAsync("[dbo].[sp_CourtType]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Court Type deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region CourtTypeMaster
        [HttpPost("InsertCourtTypeMaster")]
        public async Task<IActionResult> InsertCourtTypeMaster([FromBody] CourtTypeMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@CourtTypeId", model.CourtTypeId);
                parameters.Add("@CourtName", model.CourtName ?? string.Empty);
                parameters.Add("@Address", model.Address ?? string.Empty);
                parameters.Add("@ShortName", model.ShortName ?? string.Empty);
                parameters.Add("@UserId", model.CreatedBy);

                var result = await connection.QueryAsync("[dbo].[sp_CourtMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateCourtTypeMaster")]
        public async Task<IActionResult> UpdateCourtTypeMaster([FromBody] CourtTypeMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@CourtId", model.CourtId);
                parameters.Add("@CourtTypeId", model.CourtTypeId);
                parameters.Add("@CourtName", model.CourtName ?? string.Empty);
                parameters.Add("@Address", model.Address ?? string.Empty);
                parameters.Add("@ShortName", model.ShortName ?? string.Empty);
                parameters.Add("@UserId", model.UpdatedBy);

                var result = await connection.QueryAsync("[dbo].[sp_CourtMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetCourtTypeMasterById/{id}")]
        public async Task<IActionResult> GetCourtTypeMasterById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);
                parameters.Add("@CourtId", id);
                parameters.Add("@CourtTypeId", 0);
                parameters.Add("@CourtName", string.Empty);
                parameters.Add("@Address", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@UserId", 0);

                var result = await connection.QueryAsync("[dbo].[sp_CourtMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllCourtTypeMasterCase")]
        public async Task<IActionResult> GetAllCourtTypeMasterCase()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@CourtId", 0);
                parameters.Add("@CourtTypeId", 0);
                parameters.Add("@CourtName", string.Empty);
                parameters.Add("@Address", string.Empty);
                parameters.Add("@ShortName", string.Empty); 
                parameters.Add("@UserId", 0);

                var result = await connection.QueryAsync("[dbo].[sp_CourtMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteCourtTypeMaster/{id}")]
        public async Task<IActionResult> DeleteCourtTypeMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@CourtId", id);
                parameters.Add("@CourtTypeId", 0);
                parameters.Add("@CourtName", string.Empty);
                parameters.Add("@Address", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@UserId", 0);
                await connection.ExecuteAsync("[dbo].[sp_CourtMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Court deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region CaseTypeMaster

        [HttpPost("InsertCaseTypeMaster")]
        public async Task<IActionResult> InsertCaseTypeMaster([FromBody] CaseTypeModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@CaseTypeId", model.CaseTypeId);
                parameters.Add("@CourtTypeID", model.CourtTypeID);
                parameters.Add("@CaseType", model.CaseType ?? string.Empty);
                parameters.Add("@ShortName", model.ShortName ?? string.Empty);
                parameters.Add("@Remark", model.Remarks ?? string.Empty);

                var result = await connection.QueryAsync("[dbo].[sp_M_caseType]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateCaseTypeMaster")]
        public async Task<IActionResult> UpdateCaseTypeMaster([FromBody] CaseTypeModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@CaseTypeId", model.CaseTypeId);
                parameters.Add("@CourtTypeID", model.CourtTypeID);
                parameters.Add("@CaseType", model.CaseType ?? string.Empty);
                parameters.Add("@ShortName", model.ShortName ?? string.Empty);
                parameters.Add("@Remark", model.Remarks ?? string.Empty);

                var result = await connection.QueryAsync("[dbo].[sp_M_caseType]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetCaseTypeMasterById/{id}")]
        public async Task<IActionResult> GetCaseTypeMasterById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);
                parameters.Add("@CaseTypeId", id);

                // Don't add these unless they're required for ProcId=3
                // parameters.Add("@CourtTypeID", 0);
                // parameters.Add("@CaseType", string.Empty);
                // parameters.Add("@ShortName", string.Empty);
                // parameters.Add("@Remark", string.Empty);

                var result = await connection.QueryAsync("[dbo].[sp_M_caseType]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpGet("GetAllCaseTypeMaster")]
        public async Task<IActionResult> GetAllCaseTypeMaster()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@CaseTypeId", 0);
                parameters.Add("@CourtTypeID", 0);
                parameters.Add("@CaseType", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@Remark", string.Empty);

                var result = await connection.QueryAsync("[dbo].[sp_M_caseType]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteCaseTypeMaster/{id}")]
        public async Task<IActionResult> DeleteCaseTypeMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@CaseTypeId", id);
                parameters.Add("@CourtTypeID", 0);
                parameters.Add("@CaseType", string.Empty);
                parameters.Add("@ShortName", string.Empty);
                parameters.Add("@Remark", string.Empty);

                await connection.ExecuteAsync("[dbo].[sp_M_caseType]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Case Type deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region SubjectMaster
        [HttpPost("InsertSubjectMaster")]
        public async Task<IActionResult> InsertSubjectMaster([FromBody] SubjectMaster model)
        {
            try
            {
                using var _connection = _context.Database.GetDbConnection();
                await _connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@SubjectMatterID", model.SubjectMatterID);
                parameters.Add("@SubjectCode", model.SubjectCode);
                parameters.Add("@SubjectMatters", model.SubjectMatters);
                parameters.Add("@Description", model.Description);
                parameters.Add("@UserId", model.UserId);

                var result = await _connection.QueryAsync("PROC_SubjectMatter", parameters, commandType: CommandType.StoredProcedure);
                
                return Ok(new { success = true, message = "Inserted", data = result });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("SubjectMasterUpdate")]
        public async Task<IActionResult> SubjectMasterUpdates([FromBody] SubjectMaster model)
        {
            try
            {
                using var _connection = _context.Database.GetDbConnection();
                await _connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@SubjectMatterID", model.SubjectMatterID);
                parameters.Add("@SubjectCode", model.SubjectCode);
                parameters.Add("@SubjectMatters", model.SubjectMatters);
                parameters.Add("@Description", model.Description);
                parameters.Add("@UserId", model.UserId);

                var result = await _connection.QueryAsync("PROC_SubjectMatter", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Updated", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetByIdSubjectMaster/{id}")]
        public async Task<IActionResult> GetByIdSubjectMaster(int id)
        {
            try
            {
                using var _connection = _context.Database.GetDbConnection();
                await _connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);
                parameters.Add("@SubjectMatterID", id);

                var result = await _connection.QueryAsync("PROC_SubjectMatter", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllSubjectMaster")]
        public async Task<IActionResult> GetAllSubjectMaster()
        {
            try
            {
                using var _connection = _context.Database.GetDbConnection();
                await _connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);

                var result = await _connection.QueryAsync("PROC_SubjectMatter", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("SubjectMasterDelete/{id}")]
        public async Task<IActionResult> SubjectMasterDelete(int id)
        {
            try
            {
                using var _connection = _context.Database.GetDbConnection();
                await _connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@SubjectMatterID", id);

                await _connection.ExecuteAsync("PROC_SubjectMatter", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion
         
        #region SectionMaster
        [HttpPost("InsertSectionMaster")]
        public async Task<IActionResult> InsertSectionMaster([FromBody] SectionMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();
            var parameters = new DynamicParameters();
            parameters.Add("@Procid", 1);
            parameters.Add("@SectionId", 0);
            parameters.Add("@SectionName", model.SectionName);
            parameters.Add("@ShortName", model.ShortName);
            parameters.Add("@CreatedBy", model.CreatedBy);
            parameters.Add("@UpdatedBy", 0);
            var result = await connection.QueryAsync("SP_SectionMaster", parameters, commandType: CommandType.StoredProcedure);
            return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("UpdateSectionMaster")]
        public async Task<IActionResult> UpdateSectionMaster([FromBody] SectionMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();
            var parameters = new DynamicParameters();
            parameters.Add("@Procid", 2);
            parameters.Add("@SectionId", model.SectionId);
            parameters.Add("@SectionName", model.SectionName);
            parameters.Add("@ShortName", model.ShortName);
            parameters.Add("@CreatedBy", 0);
            parameters.Add("@UpdatedBy", model.UpdatedBy);
            var result = await connection.QueryAsync("SP_SectionMaster", parameters, commandType: CommandType.StoredProcedure);
            return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllSectionMaster")]

        public async Task<IActionResult> GetAllSectionMaster()
        {
            try
            {
                using var _connection = _context.Database.GetDbConnection();
                await _connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@SectionId", 0);
                parameters.Add("@SectionName", "");
                parameters.Add("@ShortName", "");
                parameters.Add("@CreatedBy", 0);
                parameters.Add("@UpdatedBy", 0);
                parameters.Add("@ProcId", 4);

                var result = await _connection.QueryAsync("SP_SectionMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        

        [HttpGet("GetByIdSectionMaster/{id}")]
        public async Task<IActionResult> GetByIdSectionMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();
            var parameters = new DynamicParameters();
            parameters.Add("@Procid", 3);
            parameters.Add("@SectionId", id);
            var result = await connection.QueryAsync("SP_SectionMaster", parameters, commandType: CommandType.StoredProcedure);
            return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteSectionMaster/{id}")]
        public async Task<IActionResult> DeleteSectionMaster(int id)
        {
            try
            {
               using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();
            var parameters = new DynamicParameters();
            parameters.Add("@Procid", 5);
            parameters.Add("@SectionId", id);
            var result = await connection.QueryAsync("SP_SectionMaster", parameters, commandType: CommandType.StoredProcedure);
            return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion
 
        #region DocumentMaster

        [HttpPost("InsertDocumentMaster")]
        public async Task<IActionResult> InsertDocumentMaster([FromBody] DocumentMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 1);
                parameters.Add("@DocumentId", 0);
                parameters.Add("@DocumentCode", model.DocumentCode);
                parameters.Add("@DocumentDetails", model.DocumentDetails);
                parameters.Add("@UserId", model.UserId);

                var result = await connection.QueryAsync("PROC_DocumentMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("UpdateDocumentMaster")]
        public async Task<IActionResult> UpdateDocumentMaster([FromBody] DocumentMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 2);
                parameters.Add("@DocumentId", model.DocumentId);
                parameters.Add("@DocumentCode", model.DocumentCode);
                parameters.Add("@DocumentDetails", model.DocumentDetails);
                parameters.Add("@UserId", model.UserId);

                var result = await connection.QueryAsync("PROC_DocumentMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllDocumentMaster")]
        public async Task<IActionResult> GetAllDocumentMaster()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@DocumentId", 0);
                parameters.Add("@DocumentCode", "");
                parameters.Add("@DocumentDetails", "");
                parameters.Add("@UserId", 0);
                parameters.Add("@Procid", 4);

                var result = await connection.QueryAsync("PROC_DocumentMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetByIdDocumentMaster/{id}")]
        public async Task<IActionResult> GetByIdDocumentMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@DocumentId", id);
                parameters.Add("@Procid", 3);

                var result = await connection.QueryAsync("PROC_DocumentMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteDocumentMaster/{id}")]
        public async Task<IActionResult> DeleteDocumentMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 5);
                parameters.Add("@DocumentId", id);

                var result = await connection.QueryAsync("PROC_DocumentMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region EvidenceMaster

        [HttpPost("InsertEvidenceMaster")]
        public async Task<IActionResult> InsertEvidenceMaster([FromBody] EvidenceMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 1);
                parameters.Add("@EvidenceId", 0);
                parameters.Add("@EvidenceCode", model.EvidenceCode);
                parameters.Add("@EvidenceDetails", model.EvidenceDetails);
                parameters.Add("@UserId", model.UserId);

                var result = await connection.QueryAsync("PROC_EvidenceMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("UpdateEvidenceMaster")]
        public async Task<IActionResult> UpdateEvidenceMaster([FromBody] EvidenceMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 2);
                parameters.Add("@EvidenceId", model.EvidenceId);
                parameters.Add("@EvidenceCode", model.EvidenceCode);
                parameters.Add("@EvidenceDetails", model.EvidenceDetails);
                parameters.Add("@UserId", model.UserId);

                var result = await connection.QueryAsync("PROC_EvidenceMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllEvidenceMaster")]
        public async Task<IActionResult> GetAllEvidenceMaster()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 4);
                parameters.Add("@EvidenceId", 0);

                var result = await connection.QueryAsync("PROC_EvidenceMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetByIdEvidenceMaster/{id}")]
        public async Task<IActionResult> GetByIdEvidenceMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 3);
                parameters.Add("@EvidenceId", id);

                var result = await connection.QueryAsync("PROC_EvidenceMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteEvidenceMaster/{id}")]
        public async Task<IActionResult> DeleteEvidenceMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 5);
                parameters.Add("@EvidenceId", id);

                var result = await connection.QueryAsync("PROC_EvidenceMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region GovDepartment

        [HttpPost("InsertGovDepartment")]
        public async Task<IActionResult> InsertGovDepartment([FromBody] GovDepartment model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 1);
                parameters.Add("@GovDeptID", 0);
                parameters.Add("@GovDepart", model.GovDepart);
                parameters.Add("@Description", model.Description);
                parameters.Add("@UserId", model.UserId);

                var result = await connection.QueryAsync("PROC_GovDepartment", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("UpdateGovDepartment")]
        public async Task<IActionResult> UpdateGovDepartment([FromBody] GovDepartment model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 2);
                parameters.Add("@GovDeptID", model.GovDeptID);
                parameters.Add("@GovDepart", model.GovDepart);
                parameters.Add("@Description", model.Description);
                parameters.Add("@UserId", model.UserId);

                var result = await connection.QueryAsync("PROC_GovDepartment", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllGovDepartment")]
        public async Task<IActionResult> GetAllGovDepartment()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 4);
                parameters.Add("@GovDeptID", 0);

                var result = await connection.QueryAsync("PROC_GovDepartment", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetByIdGovDepartment/{id}")]
        public async Task<IActionResult> GetByIdGovDepartment(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 3);
                parameters.Add("@GovDeptID", id);

                var result = await connection.QueryAsync("PROC_GovDepartment", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteGovDepartment/{id}")]
        public async Task<IActionResult> DeleteGovDepartment(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 5);
                parameters.Add("@GovDeptID", id);

                var result = await connection.QueryAsync("PROC_GovDepartment", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region IntrimMaster

        [HttpPost("InsertIntrimMaster")]
        public async Task<IActionResult> InsertIntrimMaster([FromBody] IntrimMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@InterimOrderId", 0);
                parameters.Add("@InterimOrder", model.InterimOrder);
                parameters.Add("@ShortName ", model.ShortName);
                parameters.Add("@Description", model.Description);
                parameters.Add("@Procid", 1);

                var result = await connection.QueryAsync("sp_IntrimProc", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("UpdateIntrimMaster")]
        public async Task<IActionResult> UpdateIntrimMaster([FromBody] IntrimMaster model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@InterimOrderId", model.InterimOrderId);
                parameters.Add("@InterimOrder", model.InterimOrder);
                parameters.Add("@ShortName ", model.ShortName);
                parameters.Add("@Description", model.Description);
                parameters.Add("@Procid", 2);

                var result = await connection.QueryAsync("sp_IntrimProc", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllIntrimMaster")]
        public async Task<IActionResult> GetAllIntrimMaster()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 4);
                parameters.Add("@InterimOrderId", 0);

                var result = await connection.QueryAsync("sp_IntrimProc", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetByIdIntrimMaster/{id}")]
        public async Task<IActionResult> GetByIdIntrimMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 3);
                parameters.Add("@InterimOrderId", id);

                var result = await connection.QueryAsync("sp_IntrimProc", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteIntrimMaster/{id}")]
        public async Task<IActionResult> DeleteIntrimMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Procid", 5);
                parameters.Add("@InterimOrderId", id);

                var result = await connection.QueryAsync("sp_IntrimProc", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }



        #endregion

        #region JudgementTypeMaster

        [HttpPost("InsertJudgementType")]
        public async Task<IActionResult> InsertJudgementType([FromBody] JudgementTypeModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procid", 1);
                parameters.Add("@JudgementTypeID", 0);
                parameters.Add("@JudgementType", model.JudgementType);
                parameters.Add("@ShortName", model.ShortName);
                parameters.Add("@Description", model.Description);

                var result = await connection.QueryAsync("sp_JudgementType", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("UpdateJudgementType")]
        public async Task<IActionResult> UpdateJudgementType([FromBody] JudgementTypeModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procid", 2);
                parameters.Add("@JudgementTypeID", model.JudgementTypeID);
                parameters.Add("@JudgementType", model.JudgementType);
                parameters.Add("@ShortName", model.ShortName);
                parameters.Add("@Description", model.Description);

                var result = await connection.QueryAsync("sp_JudgementType", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllJudgementType")]
        public async Task<IActionResult> GetAllJudgementType()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procid", 4);
                parameters.Add("@JudgementTypeID", 0);
                parameters.Add("@JudgementType", "");
                parameters.Add("@ShortName", "");
                parameters.Add("@Description", "");

                var result = await connection.QueryAsync("sp_JudgementType", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetByIdJudgementType/{id}")]
        public async Task<IActionResult> GetByIdJudgementType(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procid", 3);
                parameters.Add("@JudgementTypeID", id);
                parameters.Add("@JudgementType", "");  
                parameters.Add("@ShortName", "");      
                parameters.Add("@Description", "");    

                var result = await connection.QueryAsync("sp_JudgementType", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpDelete("DeleteJudgementType/{id}")]
        public async Task<IActionResult> DeleteJudgementType(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@procid", 5);
                parameters.Add("@JudgementTypeID", id);
                parameters.Add("@JudgementType", "");
                parameters.Add("@ShortName", "");
                parameters.Add("@Description", "");

                var result = await connection.QueryAsync("sp_JudgementType", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion


        #region Councel details

        [HttpPost("InsertCounsel")]
        public async Task<IActionResult> InsertCounsel()
        {
            string dbFilePath = "";
            var httpRequest = HttpContext.Request;
            var postedFile = httpRequest.Form.Files["postedFile"];
            var userData = httpRequest.Form["userData"];

            CounselDetail model = JsonConvert.DeserializeObject<CounselDetail>(userData);

            // Define the image upload path
            string contentPath = this.Environment.ContentRootPath;
            string uploadFolder = Path.Combine(contentPath, "Uploads/UserProfile");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            if (postedFile != null && postedFile.Length > 0)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await postedFile.CopyToAsync(stream);
                }

                // Store relative path in DB
                model.PhotoPath = "/Uploads/UserProfile/" + fileName;
            }

            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcID", 1);
                parameters.Add("@CounselId", 0);
                parameters.Add("@FullName", model.FullName);
                parameters.Add("@FatherName", model.FatherName);
                parameters.Add("@Gender", model.Gender);
                parameters.Add("@IsSenior", model.IsSenior);
                parameters.Add("@EmailId", model.EmailId);
                parameters.Add("@MobileNo", model.MobileNo);
                parameters.Add("@PhotoPath", model.PhotoPath);
                parameters.Add("@Address", model.Address);
                parameters.Add("@UserId", model.UserId);

                var result = await connection.QueryAsync("PROC_CounselDetails", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpPut("UpdateCounsel")]
        public async Task<IActionResult> UpdateCounsel()
        {
            string dbFilePath = "";
            var httpRequest = HttpContext.Request;
            var postedFile = httpRequest.Form.Files["postedFile"];
            var userData = httpRequest.Form["userData"];

            CounselDetail model = JsonConvert.DeserializeObject<CounselDetail>(userData);

            // Define image save location
            string contentPath = this.Environment.ContentRootPath;
            string uploadFolder = Path.Combine(contentPath, "Uploads/UserProfile");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            if (postedFile != null && postedFile.Length > 0)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await postedFile.CopyToAsync(stream);
                }

                model.PhotoPath = "/Uploads/UserProfile/" + fileName;
            }
             
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcID", 2); // Update
                parameters.Add("@CounselId", model.CounselId);
                parameters.Add("@FullName", model.FullName);
                parameters.Add("@FatherName", model.FatherName);
                parameters.Add("@Gender", model.Gender);
                parameters.Add("@IsSenior", model.IsSenior);
                parameters.Add("@EmailId", model.EmailId);
                parameters.Add("@MobileNo", model.MobileNo);
                parameters.Add("@PhotoPath", model.PhotoPath);
                parameters.Add("@Address", model.Address);
                parameters.Add("@UserId", model.UserId);

                var result = await connection.QueryAsync("PROC_CounselDetails", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllCounsel")]
        public async Task<IActionResult> GetAllCounsel()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcID", 3);

                var result = await connection.QueryAsync("PROC_CounselDetails", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetCounselById/{id}")]
        public async Task<IActionResult> GetCounselById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcID", 4);
                parameters.Add("@CounselId", id);

                var result = await connection.QueryAsync("PROC_CounselDetails", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteCounsel/{id}")]
        public async Task<IActionResult> DeleteCounsel(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcID", 5);
                parameters.Add("@CounselId", id);

                var result = await connection.QueryAsync("PROC_CounselDetails", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        #endregion


        #region Case Registration
        [HttpPost("SaveCase")]
        public async Task<IActionResult> SaveCase([FromForm] IFormFile? caseFile, [FromForm] string userData)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<CaseRegistrationVM>(userData);
                if (data == null) return BadRequest(new { message = "Invalid user data" });

                // Save file if uploaded
                if (caseFile != null && caseFile.Length > 0)
                {
                    string folderPath = Path.Combine(Environment.ContentRootPath, "Uploads/UserProfile");
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(caseFile.FileName);
                    string fullPath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                        await caseFile.CopyToAsync(stream);

                    data.CaseFile = "/Uploads/UserProfile/" + fileName;
                }

                // Convert lists to XML
                var xmlApplicants = new XElement("CaseApplicant", data.Petitioners.Select(p =>
                    new XElement("XmlCaseApplicant",
                        new XElement("EmpId", p.EmpId),
                        new XElement("Another", p.Another),
                        new XElement("DesignationId", p.DesignationId),
                        new XElement("DepartmentId", p.DepartmentId)
                    )
                ));

                var xmlRespondents = new XElement("NonCaseApplicant", data.Respondents.Select(r =>
                    new XElement("NonXmlCaseApplicant",
                        new XElement("EmpId", r.EmpId),
                        new XElement("Another", r.Another),
                        new XElement("DesignationId", r.DesignationId),
                        new XElement("DepartmentId", r.DepartmentId)
                    )
                ));

                var xmlAssign = new XElement("AssignDepartment", data.StandingCounsels.Select(s =>
                    new XElement("XmlAssignDepartment",
                        new XElement("StandingCounselId", s.StandingCounselId),
                        new XElement("AssignOn", s.AssignOn),
                        new XElement("VakalatnamaDate", s.VakalatnamaDate),
                        new XElement("ReplyDate", s.ReplyDate),
                        new XElement("Remarks", s.Remarks),
                        new XElement("Contact", s.Contact),
                        new XElement("Email", s.Email)
                    )
                ));

                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@RegistrationId", data.RegistrationId);
                parameters.Add("@CourtTypeId", data.courtTypeId);
                parameters.Add("@CourtId", data.CourtId);
                parameters.Add("@CaseTypeId", data.CaseTypeId);
                parameters.Add("@CaseNo", data.CaseNo ?? "");
                parameters.Add("@CaseRegistrationDate", data.CaseRegistrationDate ?? "");
                parameters.Add("@FileNo", data.FileNo ?? "");
                parameters.Add("@SectionId", data.SectionId);
                parameters.Add("@PreCaseNo", data.PreCaseNo ?? "");
                parameters.Add("@CaseRecDate", data.CaseRecDate ?? "");
                parameters.Add("@Title", data.Title ?? "");
                parameters.Add("@Prayer", data.Prayer ?? "");
                parameters.Add("@CaseFile", data.CaseFile ?? "");
                parameters.Add("@IsDecided", data.IsDecided);
                parameters.Add("@LCCodes", data.LCCodes);
                parameters.Add("@CaseStatusId", 1);
                parameters.Add("@XMLCaseApplicant", xmlApplicants.ToString());
                parameters.Add("@XMLCaseNonApplicant", xmlRespondents.ToString());
                parameters.Add("@XMLAssignDepartment", xmlAssign.ToString());
                parameters.Add("@UserId", 1);
                parameters.Add("@ProcID", 1);

                await connection.ExecuteAsync("PROC_CaseRegistrationDetails", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Case Registration successfully." });

                //var message = parameters.Get<string>("@Msg");
                //return Ok(new { success = true, message });
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }
      
        [HttpPost("GetCaseReport")]
        public async Task<IActionResult> GetCaseReport([FromBody] CaseReportRequest model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@LCCodes", model.LCNo ?? "");
                parameters.Add("@FileNo", model.FileNo ?? "");
                parameters.Add("@IsDecided", model.IsDecided); // 1 = Decided, 2 = Pending, 0 = All
                parameters.Add("@ProcId", 2);

                var result = await connection.QueryAsync<dynamic>(
                    "PROC_CaseRegistrationDetails",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("GetCaseDetail")]
        public async Task<IActionResult> GetCaseDetail([FromQuery] int procId, [FromQuery] int registrationId, [FromQuery] string? LCCodes = null)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var dbparams = new DynamicParameters();
                dbparams.Add("ProcId", procId);
                dbparams.Add("RegistrationId", registrationId);
                dbparams.Add("LCCodes", LCCodes ?? "");

                using var multi = await connection.QueryMultipleAsync(
                    "PROC_CaseRegistrationDetails", dbparams, commandType: CommandType.StoredProcedure);

                
                var caseDetail = await multi.ReadFirstOrDefaultAsync<CaseRegistrationVM>();

                if (caseDetail == null)
                {
                    return NotFound(new { success = false, message = "Case not found" });
                }

                
                var petitioners = (await multi.ReadAsync<PetitionerVM>()).ToList();
                caseDetail.Petitioners = petitioners;

                
                var respondents = (await multi.ReadAsync<RespondentVM>()).ToList();
                caseDetail.Respondents = respondents;

                
                var counsels = (await multi.ReadAsync<StandingCounselVM>()).ToList();
                caseDetail.StandingCounsels = counsels;

                return Ok(new { success = true, data = caseDetail });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error retrieving case detail",
                    error = ex.Message
                });
            }
        }

        [HttpPost("UpdateCase")]
        public async Task<IActionResult> UpdateCase([FromForm] IFormFile? caseFile, [FromForm] string userData)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<CaseRegistrationVM>(userData);
                if (data == null) return BadRequest(new { message = "Invalid user data" });

                if (caseFile != null && caseFile.Length > 0)
                {
                    string folderPath = Path.Combine(Environment.ContentRootPath, "Uploads/UserProfile");
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(caseFile.FileName);
                    string fullPath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                        await caseFile.CopyToAsync(stream);

                    data.CaseFile = "/Uploads/UserProfile/" + fileName;
                }

                var xmlApplicants = new XElement("CaseApplicant", data.Petitioners.Select(p =>
                    new XElement("XmlCaseApplicant",
                        new XElement("EmpId", p.EmpId),
                        new XElement("Another", p.Another),
                        new XElement("DesignationId", p.DesignationId),
                        new XElement("DepartmentId", p.DepartmentId)
                    )
                ));

                var xmlRespondents = new XElement("NonCaseApplicant", data.Respondents.Select(r =>
                    new XElement("NonXmlCaseApplicant",
                        new XElement("EmpId", r.EmpId),
                        new XElement("Another", r.Another),
                        new XElement("DesignationId", r.DesignationId),
                        new XElement("DepartmentId", r.DepartmentId)
                    )
                ));

                var xmlAssign = new XElement("AssignDepartment", data.StandingCounsels.Select(s =>
                    new XElement("XmlAssignDepartment",
                        new XElement("StandingCounselId", s.StandingCounselId),
                        new XElement("AssignOn", s.AssignOn),
                        new XElement("VakalatnamaDate", s.VakalatnamaDate),
                        new XElement("ReplyDate", s.ReplyDate),
                        new XElement("Remarks", s.Remarks),
                        new XElement("Contact", s.Contact),
                        new XElement("Email", s.Email)
                    )
                ));

                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@RegistrationId", data.RegistrationId);
                parameters.Add("@CourtTypeId", data.courtTypeId);
                parameters.Add("@CourtId", data.CourtId);
                parameters.Add("@CaseTypeId", data.CaseTypeId);
                parameters.Add("@CaseNo", data.CaseNo ?? "");
                parameters.Add("@CaseRegistrationDate", data.CaseRegistrationDate ?? "");
                parameters.Add("@FileNo", data.FileNo ?? "");
                parameters.Add("@SectionId", data.SectionId);
                parameters.Add("@PreCaseNo", data.PreCaseNo ?? "");
                parameters.Add("@CaseRecDate", data.CaseRecDate ?? "");
                parameters.Add("@Title", data.Title ?? "");
                parameters.Add("@Prayer", data.Prayer ?? "");
                parameters.Add("@CaseFile", data.CaseFile ?? "");
                parameters.Add("@IsDecided", data.IsDecided);
                parameters.Add("@LCCodes", data.LCCodes);
                parameters.Add("@CaseStatusId", 1);  
                parameters.Add("@XMLCaseApplicant", xmlApplicants.ToString());
                parameters.Add("@XMLCaseNonApplicant", xmlRespondents.ToString());
                parameters.Add("@XMLAssignDepartment", xmlAssign.ToString());
                parameters.Add("@UserId", 1);
                parameters.Add("@ProcID", 4); 

                await connection.ExecuteAsync("PROC_CaseRegistrationDetails", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Case updated successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteCase")]
        public async Task<IActionResult> DeleteCase(
        [FromQuery] int registrationId,
        [FromQuery] int procId,
        [FromQuery] string LCCodes = "")
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@RegistrationId", registrationId);
                parameters.Add("@ProcId", procId);
                parameters.Add("@LCCodes", LCCodes ?? "");

                var result = await connection.QueryAsync("PROC_CaseRegistrationDetails", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Deletion failed", error = ex.Message });
            }
        }

        #endregion

        #region CaseHearing
        [HttpPost("GetCaseHearingReport")]
        public async Task<IActionResult> GetCaseHearingReport([FromBody] CaseHearingRequest model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@FromDate", model.FromDate ?? "");
                parameters.Add("@ToDate", model.ToDate ?? "");
                parameters.Add("@ProcId", model.ProcId); 

                var result = await connection.QueryAsync<dynamic>(
                    "PROC_CaseHearingDetails", 
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpPost("SaveCaseHearingWithFile")]
        public async Task<IActionResult> SaveCaseHearingWithFile([FromForm] CaseHearingFormModel model)
        {
            try
            {
                string? savedFilePath = null;

                if (model.HearingFile != null && model.HearingFile.Length > 0)
                {
                    string folderPath = Path.Combine(Environment.ContentRootPath, "Uploads/UserProfile");
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.HearingFile.FileName);
                    string fullPath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                        await model.HearingFile.CopyToAsync(stream);

                    savedFilePath = "/Uploads/UserProfile/" + fileName;
                }

                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", 0);
                parameters.Add("@RegistrationId", model.RegistrationId);
                parameters.Add("@PreviousHearing", model.PreviousHearing ?? "");
                parameters.Add("@HearingDate", model.HearingDate ?? "");
                parameters.Add("@NextHearingRemark", model.NextHearingRemark ?? "");
                parameters.Add("@HearingFile", savedFilePath ?? "");
                parameters.Add("@Procid", 1);
                parameters.Add("@UserId", model.UserId); 

                await connection.ExecuteAsync("PROC_CaseHearingDetails", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        #endregion

    }
}
