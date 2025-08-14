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
                return BadRequest(new { Message = "Invalid Request" });

      
            if (model.RoleTypeId == 16)
            {
                var peruser = await Task.FromResult(_dapper.Get<UserModel>(
     $@"SELECT  
        A.*,
        CONVERT(VARCHAR(10), A.DOB, 103) AS password,
        A.EmpCode AS UserName,
        ISNULL(MR.DashboardPage, 'PersonalDashboard') AS DashboardPage
    FROM EmpDetail A
    LEFT JOIN UserLogin D ON D.Empid = A.EmpId
    LEFT JOIN M_RoleType MR ON MR.RoleTypeId = D.RoleTypeId
    WHERE A.EmpCode = '{model.userName}'",
     null, commandType: CommandType.Text));
            

                if (peruser == null)
                    return NotFound(new { Message = "User not found!" });

             
                if (peruser.password.Trim() != model.password.Trim())
                {
                    return BadRequest(new { Message = "Password is Incorrect" });
                }

                peruser.Token = CreateJwt(peruser);

                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = new
                    {
                        AccessToken = peruser.Token,
                        RefreshToken = "" // optional agar nahi chahiye
                    },
                    userid = peruser.UserId,
                    usertypeId = peruser.UsertypeId,
                    roleTypeId = peruser.RoleTypeId,
                    officeId = peruser.OfficeId,
                    regionId = peruser.RegionId,
                    circleId = peruser.CircleId,
                    userName = peruser.userName,
                    empId = peruser.EmpId,
                    isFirstLogin = peruser.IsFirstLogin,
                    DashboardPage = peruser.DashboardPage
                });
            }

            // बाकी users के लिए logic
            var user = await Task.FromResult(_dapper.Get<UserModel>(
                $"SELECT *, UserPassword as password, ISNULL((SELECT DashboardPage FROM M_RoleType WHERE Roletypeid = UserLogin.roletypeid),'samplePage') DashboardPage FROM [dbo].[UserLogin] WHERE RoleTypeId = {model.RoleTypeId} AND UserName = '{model.userName}'",
                null, commandType: CommandType.Text));

            if (user == null)
                return NotFound(new { Message = "User not found!" });

            // hashed password verify
            if (!Helpers.PasswordHasher.VerifyPassword(model.password, user.password))
                return BadRequest(new { Message = "Password is Incorrect" });

            // AccessToken & RefreshToken generate
            user.Token = CreateJwt(user);
            var newAccessToken = user.Token;
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddHours(1).ToString("dd-MM-yyyy HH:mm:ss");

            // RefreshToken को database में update
            await Task.FromResult(_dapper.Update<int>($@"
        UPDATE [dbo].[UserLogin] 
        SET RefreshToken='{user.RefreshToken}', 
            RefreshTokenExpiryTime=Convert(datetime,'{user.RefreshTokenExpiryTime}',103) 
        WHERE UserName = '{user.userName}'",
                null, commandType: CommandType.Text));

            return Ok(new
            {
                success = true,
                message = "Success",
                data = new { AccessToken = newAccessToken, RefreshToken = newRefreshToken },
                userid = user.UserId,
                AccessToken = user.RefreshToken,
                usertypeId = user.UsertypeId,
                roleTypeId = user.RoleTypeId,
                officeId = user.OfficeId,
                regionId = user.RegionId,
                circleId = user.CircleId,
                UserName = user.userName,
                empId = user.EmpId,
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
