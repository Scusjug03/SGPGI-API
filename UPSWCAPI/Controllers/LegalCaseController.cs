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


        #region Councel deatails

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
            string uploadFolder = Path.Combine(contentPath, "Uploads/CounselPhotos");

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
                model.PhotoPath = "/Uploads/CounselPhotos/" + fileName;
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
            string uploadFolder = Path.Combine(contentPath, "Uploads/CounselPhotos");

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

                model.PhotoPath = "/Uploads/CounselPhotos/" + fileName;
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
    }
}
