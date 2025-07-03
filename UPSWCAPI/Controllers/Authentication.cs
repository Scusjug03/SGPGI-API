using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure.Core;
using Dapper;
using MectoiApis.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UPSWCAPI.Model;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.HttpResults;
using UPSWCAPI.Services;
//using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static UPSWCAPI.Model.EnquiryDbContext;



namespace UPSWCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class Authentication : ControllerBase
    {        
        private readonly IDapper _dapper;
        public Authentication(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CheckUserLogin))]

        public async Task<IActionResult> CheckUserLogin(UserModel model)
        {
            if (model == null)
                return BadRequest();

            var user = await Task.FromResult(_dapper.Get<UserModel>($"Select *, UserPassword as password ,isnull((select DashboardPage from M_RoleType where Roletypeid=[UserLogin].roletypeid) ,'samplePage')  DashboardPage from [dbo].[UserLogin] where RoleTypeId = " + model.RoleTypeId + " AND UserName = '" + model.userName + "'", null, commandType: CommandType.Text)); if (user == null)
                return NotFound(new { Message = "User not found!" });

            if (string.IsNullOrWhiteSpace(user.password) || user.password.Length < 20) // adjust if you know your hash format
            {
                return BadRequest(new { Message = "Password hash is invalid or corrupted" });
            }
            

            if (!Helpers.PasswordHasher.VerifyPassword(model.password, user.password))
            {
                return BadRequest(new { Message = "Password is Incorrect" });
            }

            user.Token = CreateJwt(user);
            var newAccessToken = user.Token;
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddHours(1).ToString("dd-MM-yyyy HH:mm:ss");
            var updateUser = Task.FromResult(_dapper.Update<int>($"Update [dbo].[UserLogin] set RefreshToken='" + user.RefreshToken + "', RefreshTokenExpiryTime=Convert(datetime,'" + user.RefreshTokenExpiryTime + "',103) where UserName = '" + user.userName + "'",
            null,
            commandType: CommandType.Text));

            //var dbparams = new DynamicParameters();
            //dbparams.Add("UserId", model.UserId, DbType.String);
            //dbparams.Add("UserName", model.userName, DbType.String);
            //dbparams.Add("UserPassword", model.password, DbType.String);
            //dbparams.Add("UsertypeId", model.UsertypeId, DbType.Int32);
            //dbparams.Add("OfficeId", model.OfficeId, DbType.Int32);
            //dbparams.Add("RoleTypeId", model.RoleTypeId, DbType.Int32);
            //dbparams.Add("IsFirstLogin", model.IsFirstLogin, DbType.String);
            //if (model.UsertypeId == 1 || model.UsertypeId == 2 || model.UsertypeId == 3 || model.UsertypeId == 4 || model.UsertypeId == 5)
            //{
            //    model.ProcId = 1;
            //}
            //else if (model.UsertypeId == 6)
            //{
            //    model.ProcId = 2;
            //}
            //dbparams.Add("ProcId", model.ProcId, DbType.Int32);
            //var result = await Task.FromResult(_dapper.Get<UserModel>("[dbo].[Proc_UserLogin]", dbparams,
            //    commandType: CommandType.StoredProcedure));

            //return Ok(new UserToken()
            //{
            //    AccessToken = newAccessToken,
            //    RefreshToken = newRefreshToken
            //});

            return Ok(new
            {
                
                success = true,
                message = "Success",
                data = new UserToken()
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                },
                userid=user.UserId,
                AccessToken = user.RefreshToken,
                usertypeId = user.UsertypeId,
                roleTypeId = user.RoleTypeId,
                officeId = user.OfficeId,
                UserName = user.userName,
                empId   = user.EmpId,
                isFirstLogin = user.IsFirstLogin,
               DashboardPage = user.DashboardPage




            });

        }


        [HttpPost("FirstChangePassword")] 
        public async Task<IActionResult> FirstChangePassword([FromBody] DepartmentLogin model)
        {
            if (model == null || model.UserId <= 0 || string.IsNullOrEmpty(model.OldPassword) || string.IsNullOrEmpty(model.NewPassword))
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                // Determine ProcId based on UserType
                switch (model.UserType)
                {
                    case 1: model.ProcId = 1; break;
                    case 2: model.ProcId = 2; break;
                    case 3: model.ProcId = 3; break;
                    case 4: model.ProcId = 4; break;
                    case 5: model.ProcId = 5; break;
                    case 6: model.ProcId = 6; break;
                    default: return BadRequest("Invalid user type.");
                }

                var dbparams = new DynamicParameters();
                dbparams.Add("ProcId", model.ProcId, DbType.Int32);
                dbparams.Add("UserId", model.UserId, DbType.Int32);
                dbparams.Add("OldPassword", model.OldPassword, DbType.String);
                dbparams.Add("NewPassword", model.NewPassword, DbType.String);

                // Assuming you're using a standard Dapper service
                var result = await Task.FromResult(_dapper.Get<DepartmentLogin>(
                    "[dbo].[Proc_ChangeFirstPassword]", dbparams, commandType: CommandType.StoredProcedure));

                if (result != null && result.msg == "success")
                {
                    return Ok(new { status = "success", message = "Password changed successfully." });
                }
                else
                {
                    return Ok(new { status = "error", message = "Old password is incorrect." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while changing password.");
            }
        }

        private static string CheckPasswordStrength(string pass)
        {
            StringBuilder sb = new StringBuilder();
            if (pass.Length < 9)
                sb.Append("Minimum password length should be 8" + Environment.NewLine);
            if (!(Regex.IsMatch(pass, "[a-z]") && Regex.IsMatch(pass, "[A-Z]") && Regex.IsMatch(pass, "[0-9]")))
                sb.Append("Password should be AlphaNumeric" + Environment.NewLine);
            if (!Regex.IsMatch(pass, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                sb.Append("Password should contain special charcter" + Environment.NewLine);
            return sb.ToString();
        }

        private string CreateJwt(UserModel user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, $"{user.RoleTypeId}"),
                new Claim(ClaimTypes.Name,$"{user.userName}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials
            };
            SecurityToken token = null;
            try
            {
                token = jwtTokenHandler.CreateToken(tokenDescriptor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return jwtTokenHandler.WriteToken(token);
        }

        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            int totalcount = _dapper.Get<int>($"select COUNT(*) from [dbo].[UserLogin] WHERE RefreshToken like '%" + refreshToken + "%'", null, commandType: CommandType.Text);
            if (totalcount > 0)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }

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

    }
}
