using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text; 
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using MectoiApis.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UPSWCAPI.Helpers;
using UPSWCAPI.Model;
using UPSWCAPI.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static UPSWCAPI.Model.EnquiryDbContext;

namespace UPSWCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]

    public class MasterController : ControllerBase
    {
        private readonly IDapper _dapper;
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
     
        public MasterController(IConfiguration _configuration, EnquiryDbContext context, IWebHostEnvironment _environment, IDapper dapper)
        {
            Environment = _environment;
            Configuration = _configuration;
            _context = context;
            _dapper = dapper;
        }
        private readonly EnquiryDbContext _context;

        [HttpPost("ModuleMaster")]
        public async Task<ModuleMaster> ModuleMaster(ModuleMaster model)

        {

            var dbparams = new DynamicParameters();
            dbparams.Add("ProjectId", model.projectId, DbType.Int32);
            dbparams.Add("ModuleName", model.moduleName, DbType.String);
            dbparams.Add("ModuleStatus", model.moduleStatus, DbType.String);
            dbparams.Add("Sno", model.sno, DbType.Int32);
            dbparams.Add("ProcId", 1);
            var result = await Task.FromResult(_dapper.Get<ModuleMaster>("[dbo].[Proc_MenuMaster]", dbparams,
                commandType: CommandType.StoredProcedure));
            return result;

        }

        //[HttpGet(nameof(GetProjectlist))]
        [HttpGet("GetProjectlist")]

        public IApiRersponse GetProjectlist()

        {
            IApiRersponse responseObj = new IApiRersponse();
            responseObj.result = false;
            responseObj.message = string.Empty;
            responseObj.data = null;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("ProcId", 3);
                var list = _dapper.GetAll<ProjectMaster>("[dbo].[Proc_MenuMaster]", dbparams, commandType: CommandType.StoredProcedure).ToList();
                responseObj.data = list;
                responseObj.result = true;
            }
            catch(Exception ex)
            {
                
                responseObj.message = ex.Message;
            }
            return responseObj;

        }

        

        //public List<ModuleMaster> GetAllModuleList(ModuleMaster model)
        //{
        //    var dbparams = new DynamicParameters();
        //    dbparams.Add("ProjectId", model.ProjectId, DbType.Int32);
        //    dbparams.Add("ModuleName", model.ModuleName, DbType.String);
        //    dbparams.Add("ModuleStatus", model.ModuleStatus, DbType.String);
        //    dbparams.Add("Sno", model.Sno, DbType.Int32);
        //    dbparams.Add("ProcId", 4);
        //    var list = _dapper.GetAll<ModuleMaster>("[Proc_MenuMaster]", dbparams, commandType: CommandType.StoredProcedure).ToList();
        //    return list;
        //}

        [HttpPost("GetAllModuleList")]
        public async Task<IEnumerable<ModuleMaster>> GetAllModuleList([FromBody] ModuleMaster model)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ProjectId", model.projectId, DbType.Int32);
            dbparams.Add("ModuleName", model.moduleName, DbType.String);
            dbparams.Add("ModuleStatus", model.moduleStatus, DbType.String);
            dbparams.Add("Sno", model.sno, DbType.Int32);
            dbparams.Add("UsertypeId", model.UsertypeId, DbType.Int32);
            dbparams.Add("RoleTypeId", model.RoleTypeId, DbType.Int32);
            dbparams.Add("EmpId", model.EmpId, DbType.Int32);
            dbparams.Add("ProcId", 4);
            var result = await Task.FromResult(_dapper.GetAll<ModuleMaster>(
                "[dbo].[Proc_MenuMaster]", dbparams, commandType: CommandType.StoredProcedure));

            return result;
        }

        [HttpPost("GetAllMenuList")]
        public async Task<IEnumerable<ModuleMaster>> GetAllMenuList([FromBody] ModuleMaster model)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ProjectId", model.projectId, DbType.Int32);
            dbparams.Add("ModuleId", model.moduleId, DbType.Int32);
            dbparams.Add("MenuName", model.menuName, DbType.String);
            dbparams.Add("MenuId", model.menuId, DbType.Int32);
            dbparams.Add("RouterLink", model.routerLink, DbType.String);
            dbparams.Add("Sno", model.sno, DbType.Int32);
            dbparams.Add("UsertypeId", model.UsertypeId, DbType.Int32);
            dbparams.Add("RoleTypeId", model.RoleTypeId, DbType.Int32);
            dbparams.Add("EmpId", model.EmpId, DbType.Int32);
            dbparams.Add("ProcId", 7);
            var result = await Task.FromResult(_dapper.GetAll<ModuleMaster>(
                "[dbo].[Proc_MenuMaster]", dbparams, commandType: CommandType.StoredProcedure));

            return result;
        }


        [HttpPut("UpdateModule")]      
         public async Task<ModuleMaster> UpdateModule(ModuleMaster model)
         {
            var dbparams = new DynamicParameters();
            dbparams.Add("ModuleId", model.moduleId, DbType.Int32);
            dbparams.Add("ProjectId", model.projectId, DbType.Int32);
            dbparams.Add("ModuleName", model.moduleName, DbType.String);
            dbparams.Add("ModuleStatus", model.moduleStatus, DbType.String);
            dbparams.Add("Sno", model.sno, DbType.Int32);
            dbparams.Add("ProcId", 5);
            var result = await Task.FromResult(_dapper.Get<ModuleMaster>("[dbo].[Proc_MenuMaster]", dbparams,
                commandType: CommandType.StoredProcedure));
            return result;

        }


        [HttpPut("DeleteEnquiryById")]
        public async Task<ModuleMaster> DeleteEnquiryById(ModuleMaster model)        
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ModuleId", model.moduleId, DbType.Int32);           
            dbparams.Add("ProcId", 6);
            var result = await Task.FromResult(_dapper.Get<ModuleMaster>("[dbo].[Proc_MenuMaster]", dbparams,
                commandType: CommandType.StoredProcedure));
            return result;

        }


        [HttpGet("getToMasterSingle")]
        public async Task<IEnumerable<Master>> getToMasterSingle([FromQuery] int procId)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ProcId", procId);
            //dbparams.Add("ProcId", 1);
            var result = await Task.FromResult(_dapper.GetAll<Master>("[dbo].[Proc_BindMaster]", dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpGet("getToMaster")]
        public async Task<IEnumerable<Master>> getToMaster([FromQuery] int procId, [FromQuery] int id)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ProcId", procId);
            dbparams.Add("Id", id); // DepartmentId
            var result = await Task.FromResult(_dapper.GetAll<Master>(
                "[dbo].[Proc_BindMasterById]", dbparams, commandType: CommandType.StoredProcedure));
            return result;
        }


        [HttpPost("UserPermission")]
        public async Task<IEnumerable<UserPermission>> UserPermission([FromBody] UserPermission model)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Empid", model.empId, DbType.Int32);
            dbparams.Add("ProjectId", model.projectId, DbType.Int32);
            dbparams.Add("ModuleId", model.moduleId, DbType.Int32);
            dbparams.Add("MenuId", model.menuId, DbType.Int32);
            dbparams.Add("ProcId", 3);

            var result = await Task.FromResult(_dapper.GetAll<UserPermission>(
                "[dbo].[Proc_UserPermission]", dbparams, commandType: CommandType.StoredProcedure));

            return result;
        }

        #region OfficeAdmin
        [HttpPost("InsertOfficeAdmin")]
        public async Task<IActionResult> InsertOfficeAdmin([FromBody] OfficeAdmin model)
        {

            if (model == null)
                return BadRequest();


            var plainPassword = model.UserPassword;
            //check username
            int totalcount = 0;
            //totalcount = await Task.FromResult(_dapper.Get<int>($"select COUNT(*) from [ULBDemo].[Users] WHERE username like '%" + model.UserName + "%'", null, commandType: CommandType.Text));
            totalcount = await Task.FromResult(_dapper.Get<int>($"select COUNT(1) from UserLogin WHERE Lower(UserName) = Lower('" + model.UserName + "')", null, commandType: CommandType.Text));
            if (totalcount > 0)
                return BadRequest(new { Message = "Username Already Exist" });

            var passMessage = CheckPasswordStrength(model.UserPassword);
            if (!string.IsNullOrEmpty(passMessage))
                return BadRequest(new { Message = passMessage.ToString() });

            model.UserPassword = PasswordHasher.HashPassword(model.UserPassword);

            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@UserName", model.UserName);
                parameters.Add("@UserPassword", model.UserPassword);
                parameters.Add("@PlainPassword", plainPassword);
                parameters.Add("@UserRole", model.UserRole);
                parameters.Add("@RoleTypeId", model.RoleTypeId);
                parameters.Add("@UserEmail", model.UserEmail);
                parameters.Add("@UserMobileno", model.UserMobileno);
                parameters.Add("@CircleId", model.CircleId);
                parameters.Add("@UsertypeId", model.UsertypeId);
                parameters.Add("@OfficeId", model.OfficeId);
                parameters.Add("RefreshToken", model.RefreshToken, DbType.String);
                parameters.Add("RefreshTokenExpiryTime", model.RefreshTokenExpiryTime, DbType.String);

                await connection.ExecuteAsync("[dbo].[Proc_OfficeAdmin]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Office  inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        [HttpGet("GetAllOfficesAdmin")]
        public async Task<IActionResult> GetAllOfficesAdmin()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);

                var offices = await connection.QueryAsync("[dbo].[Proc_OfficeAdmin]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = offices });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetOfficeAdminById/{id}")]
        public async Task<IActionResult> GetOfficeAdminById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@UserId", id);

                var office = await connection.QueryFirstOrDefaultAsync("[dbo].[Proc_OfficeAdmin]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = office });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateOfficeAdmin")]
        public async Task<IActionResult> UpdateOfficeAdmin([FromBody] OfficeAdmin model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@UserName", model.UserName);
                parameters.Add("@UserPassword", model.UserPassword);
                parameters.Add("@UserRole", model.UserRole);
                parameters.Add("@RoleTypeId", model.RoleTypeId);
                parameters.Add("@UserEmail", model.UserEmail);
                parameters.Add("@UserMobileno", model.UserMobileno);
                parameters.Add("@CircleId", model.CircleId);
                parameters.Add("@UsertypeId", model.UsertypeId);
                parameters.Add("@OfficeId", model.OfficeId);


                await connection.ExecuteAsync("[dbo].[Proc_OfficeAdmin]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Office updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteOfficeAdmin/{id}")]
        public async Task<IActionResult> DeleteOfficeAdmin(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@UserId", id);
               
                await connection.ExecuteAsync("[dbo].[Proc_OfficeAdmin]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Office deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        #endregion

        private static string CheckPasswordStrength(string pass)
        {
            StringBuilder sb = new StringBuilder();
            if (pass.Length < 9)
                sb.Append("Minimum password length should be 8" + System.Environment.NewLine);
            if (!(Regex.IsMatch(pass, "[a-z]") && Regex.IsMatch(pass, "[A-Z]") && Regex.IsMatch(pass, "[0-9]")))
                sb.Append("Password should be AlphaNumeric" + System.Environment.NewLine);
            if (!Regex.IsMatch(pass, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                sb.Append("Password should contain special character" + System.Environment.NewLine);
            return sb.ToString();
        }

        //private string CreateJwt(UserModel user)
        //{
        //    var jwtTokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
        //    var identity = new ClaimsIdentity(new Claim[]
        //    {
        //        new Claim(ClaimTypes.Role, user.Role),
        //        new Claim(ClaimTypes.Name,$"{user.Username}")
        //    });

        //    var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = identity,
        //        Expires = DateTime.Now.AddSeconds(10),
        //        SigningCredentials = credentials
        //    };
        //    SecurityToken token = null;
        //    try
        //    {
        //        token = jwtTokenHandler.CreateToken(tokenDescriptor);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //    return jwtTokenHandler.WriteToken(token);
        //}

        //private string CreateRefreshToken()
        //{
        //    var tokenBytes = RandomNumberGenerator.GetBytes(64);
        //    var refreshToken = Convert.ToBase64String(tokenBytes);

        //    int totalcount = _dapper.Get<int>($"select COUNT(*) from [dbo].[UserLogin] WHERE RefreshToken like '%" + refreshToken + "%'", null, commandType: CommandType.Text);
        //    if (totalcount > 0)
        //    {
        //        return CreateRefreshToken();
        //    }
        //    return refreshToken;
        //}

        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("This is Invalid Token");
            return principal;

        }


        #region Role Permission

        [HttpPost("InsertRolePermission")]
        public async Task<IActionResult> InsertRolePermission([FromBody] RolePermission model)
        {
            model.RefreshToken = CreateRefreshToken();
            model.RefreshTokenExpiryTime = DateTime.Now.AddHours(1).ToString("dd-MM-yyyy HH:mm:ss");
            var DecPassword = model.UserPwd;
            try
            {

                if (model == null)
                    return BadRequest();

                int totalcount = await Task.FromResult(_dapper.Get<int>($"select COUNT(1) from UserLogin WHERE Lower(UserName) = Lower('" + model.Username + "')", null, commandType: CommandType.Text));
                if (totalcount > 0)
                    return BadRequest(new { Message = "UserName Already Exist" });

                var passMessage = CheckPasswordStrength(model.UserPwd);
                if (!string.IsNullOrEmpty(passMessage))
                    return BadRequest(new { Message = passMessage.ToString() });

                model.UserPwd = PasswordHasher.HashPassword(model.UserPwd);

                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@WTypeId", model.WTypeId);
                parameters.Add("@Userid", model.UserId);
                parameters.Add("@officeId", model.OfficeId);
                parameters.Add("@empname", model.EmpName);
                parameters.Add("@Gender", model.Gender);
                parameters.Add("@MobileNo", model.MobileNo);
                parameters.Add("@emailid", model.EmailId);
                parameters.Add("@cpfno", model.CpfNo);
                parameters.Add("@username", model.Username);
                parameters.Add("@userpwd", model.UserPwd);
                parameters.Add("@decPassword", DecPassword);
                parameters.Add("@hrmsid", model.HrmsId);
                parameters.Add("@roletypeid", model.RoleTypeId);
                parameters.Add("@Designationid", model.DesignationId);
                parameters.Add("@CreatedOn", DateTime.Now);
                parameters.Add("@Circle", model.Circle);
                parameters.Add("@UsertypeId", model.UsertypeId);
                parameters.Add("@RefreshToken", model.RefreshToken);
                parameters.Add("@RefreshTokenExpiryTime", model.RefreshTokenExpiryTime);

                await connection.ExecuteAsync("[dbo].[Proc_RoleMaster]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, message = "Role Master inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        //private static string CheckPasswordStrength(string pass)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    if (pass.Length < 9)
        //        sb.Append("Minimum password length should be 8" + Environment.NewLine);
        //    if (!(Regex.IsMatch(pass, "[a-z]") && Regex.IsMatch(pass, "[A-Z]") && Regex.IsMatch(pass, "[0-9]")))
        //        sb.Append("Password should be AlphaNumeric" + Environment.NewLine);
        //    if (!Regex.IsMatch(pass, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
        //        sb.Append("Password should contain special charcter" + Environment.NewLine);
        //    return sb.ToString();
        //}

        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            int totalcount = _dapper.Get<int>($"select COUNT(1) from [UserLogin] WHERE RefreshToken like '%" + refreshToken + "%'", null, commandType: CommandType.Text);
            if (totalcount > 0)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }

        [HttpGet("GetAllRolePermlst")]
        public async Task<IActionResult> GetAllRolePermlst()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);
                var RoleMaster = await connection.QueryAsync("[dbo].[Proc_RoleMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = RoleMaster });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetRolePermisionById/{id}")]
        public async Task<IActionResult> GetRolePermisionById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@Userid", id);

                var office = await connection.QueryFirstOrDefaultAsync("[dbo].[Proc_RoleMaster]", parameters, commandType: CommandType.StoredProcedure);

                return Ok(new { success = true, data = office });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("Updaterolepermission")]
        public async Task<IActionResult> Updaterolepermission([FromBody] RolePermission model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2); // 2 for update
                parameters.Add("@userid", model.UserId);
                parameters.Add("@officeId", model.OfficeId);
                parameters.Add("@roletypeid", model.RoleTypeId);

                await connection.ExecuteAsync("[dbo].[Proc_RoleMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Role Permission updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteRolePermission/{userid}")]
        public async Task<IActionResult> DeleteRolePermission(int userid)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 6); 
                parameters.Add("@userid", userid);

                var result = await connection.ExecuteAsync("[dbo].[Proc_RoleMaster]", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }



        #endregion Role Permission

        #region ModuleMaster

        [HttpPost("InsertModuleMaster")]
        public async Task<IActionResult> InsertModuleMaster([FromBody] ModuleMasterModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@ModuleId", 0);
                parameters.Add("@ProjectId", model.ProjectId);
                parameters.Add("@ModuleName", model.ModuleName ?? string.Empty);
                parameters.Add("@ModuleStatus", 'N');
                parameters.Add("@SNo", model.SNo);
                parameters.Add("@UserTypeId", model.UserTypeId);
                parameters.Add("@RoleTypeId", model.RoleTypeId);

                var result = await connection.QueryAsync("dbo.Proc_ModuleMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateModuleMaster")]
        public async Task<IActionResult> UpdateModuleMaster([FromBody] ModuleMasterModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@ModuleId", model.ModuleId);
                parameters.Add("@ProjectId", model.ProjectId);
                parameters.Add("@ModuleName", model.ModuleName ?? string.Empty);
                parameters.Add("@ModuleStatus", 'N');
                parameters.Add("@SNo", model.SNo);
                parameters.Add("@UserTypeId", model.UserTypeId);
                parameters.Add("@RoleTypeId", model.RoleTypeId);

                var result = await connection.QueryAsync("dbo.Proc_ModuleMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetModuleMasterById/{id}")]
        public async Task<IActionResult> GetModuleMasterById(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@ModuleId", id);
                parameters.Add("@ProjectId", 0);
                parameters.Add("@ModuleName", string.Empty);
                parameters.Add("@ModuleStatus", 'N');
                parameters.Add("@SNo", 0);
                parameters.Add("@UserTypeId", 0);
                parameters.Add("@RoleTypeId", 0);

                var data = await connection.QueryAsync("dbo.Proc_ModuleMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllModuleMasters")]
        public async Task<IActionResult> GetAllModuleMasters()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 6);
                parameters.Add("@ModuleId", 0);
                parameters.Add("@ProjectId", 0);
                parameters.Add("@ModuleName", string.Empty);
                parameters.Add("@ModuleStatus", 'N');
                parameters.Add("@SNo", 0);
                parameters.Add("@UserTypeId", 0);
                parameters.Add("@RoleTypeId", 0);

                var data = await connection.QueryAsync("dbo.Proc_ModuleMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteModuleMaster/{id}")]
        public async Task<IActionResult> DeleteModuleMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 4);
                parameters.Add("@ModuleId", id);
                parameters.Add("@ProjectId", 0);
                parameters.Add("@ModuleName", string.Empty);
                parameters.Add("@ModuleStatus", 'N');
                parameters.Add("@SNo", 0);
                parameters.Add("@UserTypeId", 0);
                parameters.Add("@RoleTypeId", 0);

                await connection.ExecuteAsync("dbo.Proc_ModuleMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Module deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetModuleMasterReport")]
        public async Task<IActionResult> GetModuleMasterReport()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 6);
                parameters.Add("@ModuleId", 0);
                parameters.Add("@ProjectId", 0);
                parameters.Add("@ModuleName", string.Empty);
                parameters.Add("@ModuleStatus", 'N');
                parameters.Add("@SNo", 0);
                parameters.Add("@UserTypeId", 0);
                parameters.Add("@RoleTypeId", 0);

                var data = await connection.QueryAsync("dbo.Proc_ModuleMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion


        #region MenuMaster

        [HttpPost("InsertMenuMaster")]
        public async Task<IActionResult> InsertMenuMaster([FromBody] MenuMasterModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 1);
                parameters.Add("@MenuId", 0);
                parameters.Add("@ProjectId", model.ProjectId);
                parameters.Add("@ModuleId", model.ModuleId);
                parameters.Add("@MenuName", model.MenuName);
                parameters.Add("@RouterLink", model.RouterLink ?? "");
                parameters.Add("@UserTypeId", model.UserTypeId);
                parameters.Add("@RoleTypeId", model.RoleTypeId);
                parameters.Add("@EmpId", 0);

                var result = await connection.QueryAsync("dbo.Proc_MenuMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateMenuMaster")]
        public async Task<IActionResult> UpdateMenuMaster([FromBody] MenuMasterModel model)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 2);
                parameters.Add("@MenuId", model.MenuId);
                parameters.Add("@ProjectId", model.ProjectId);
                parameters.Add("@ModuleId", model.ModuleId);
                parameters.Add("@MenuName", model.MenuName);
                parameters.Add("@RouterLink", model.RouterLink ?? "");
                parameters.Add("@UserTypeId", model.UserTypeId);
                parameters.Add("@RoleTypeId", model.RoleTypeId);
                parameters.Add("@EmpId", 0);

                var result = await connection.QueryAsync("dbo.Proc_MenuMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteMenuMaster/{id}")]
        public async Task<IActionResult> DeleteMenuMaster(int id)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 3);
                parameters.Add("@MenuId", id);
                parameters.Add("@ProjectId", 0);
                parameters.Add("@ModuleId", 0);
                parameters.Add("@MenuName", "");
                parameters.Add("@RouterLink", "");
                parameters.Add("@UserTypeId", 0);
                parameters.Add("@RoleTypeId", 0);
                parameters.Add("@EmpId", 0);

                await connection.ExecuteAsync("dbo.Proc_MenuMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, message = "Menu deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllMenuMasters")]
        public async Task<IActionResult> GetAllMenuMasters()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 5);
                parameters.Add("@MenuId", 0);
                parameters.Add("@ProjectId", 0);
                parameters.Add("@ModuleId", 0);
                parameters.Add("@MenuName", "");
                parameters.Add("@RouterLink", "");
                parameters.Add("@UserTypeId", 0);
                parameters.Add("@RoleTypeId", 0);
                parameters.Add("@EmpId", 0);

                var data = await connection.QueryAsync("dbo.Proc_MenuMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetMenuMastersByRole/{empId}/{moduleId}")]
        public async Task<IActionResult> GetMenuMastersByRole(int empId, int moduleId)
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ProcId", 7);
                parameters.Add("@EmpId", empId);
                parameters.Add("@ModuleId", moduleId);
                parameters.Add("@MenuId", 0);
                parameters.Add("@ProjectId", 0);
                parameters.Add("@MenuName", "");
                parameters.Add("@RouterLink", "");
                parameters.Add("@MenuStatus", "N");
                parameters.Add("@UserTypeId", 0);
                parameters.Add("@RoleTypeId", 0);

                var result = await connection.QueryAsync("dbo.Proc_MenuMaster", parameters, commandType: CommandType.StoredProcedure);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion


    }
}
