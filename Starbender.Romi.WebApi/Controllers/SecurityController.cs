// using WebApi.Helpers;
// using AutoMapper;

namespace Starbender.Romi.Web.Service.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;

    using Starbender.Core.Data;
    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;
    using Starbender.Romi.Web.Service.Models;

    [Authorize]
    [ApiController]
    [Route("api/security")]
    public class SecurityController : ControllerBase
    {
        // todo: Move this to AppSettings?
        private const string secret = "JSDNM^&%#bn^&#@NB<^&TSDMNdsf6@LK8";

        private readonly SignInManager<RomiUser> _signInManager;

        private readonly UserManager<RomiUser> _userManager;

        public SecurityController(SignInManager<RomiUser> signInManager, UserManager<RomiUser> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            string x = "";
        }

        [Route("roles/{roleId}/users/{userid}")]
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(string roleId, string userId)
        {
            RomiRole role = null;
            RomiUser user = null;
            using (var uow = new UnitOfWork<RomiRole>(new RomiDbContext()))
            {
                role = await uow.Repository().SingleOrDefaultAsync(t => t.Id == roleId);
                if (role == null)
                    return this.BadRequest(new { Message = $"RoleId '{roleId}' not found" });
            }

            using (var uow = new UnitOfWork<RomiUser>(new RomiDbContext()))
            {
                user = await uow.Repository().SingleOrDefaultAsync(t => t.Id == userId);
                if (user == null)
                    return this.BadRequest(new { Message = $"UserId '{userId}' not found" });
            }

            await this._userManager.AddToRoleAsync(user, role.Name);
            return this.Ok();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] RomiUser user)
        {
            var authenticatedUser = this._signInManager.SignInAsync(user, false);

            if (authenticatedUser == null)
                return this.BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
                                      {
                                          Subject =
                                              new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, user.Id) }),
                                          Expires = DateTime.UtcNow.AddDays(7),
                                          SigningCredentials = new SigningCredentials(
                                              new SymmetricSecurityKey(key),
                                              SecurityAlgorithms.HmacSha256Signature)
                                      };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return this.Ok(
                new UserInfo()
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Token = tokenString
                    });
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = this._userManager.Users.SingleOrDefault(t => t.Id == id);
            if (user != null)
            {
                await this._userManager.DeleteAsync(user);
                return this.Ok();
            }

            return this.BadRequest(new { message = $"UserId '{id}' not found" });
        }

        [Route("users")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<UserInfo> result = new List<UserInfo>();
            using (var uow = new UnitOfWork<RomiUser>(new RomiDbContext()))
            {
                var users = await uow.Repository().AllAsync();
                result = users.Select(
                    t => new UserInfo()
                             {
                                 Id = t.Id, Username = t.UserName, FirstName = t.FirstName, LastName = t.LastName,
                             }).ToList();
            }

            return this.Ok(result);
        }

        [Route("roles")]
        [HttpGet]
        public async Task<IEnumerable<RomiRole>> GetRoles()
        {
            IEnumerable<RomiRole> result = null;
            using (var uow = new UnitOfWork<RomiRole>(new RomiDbContext()))
            {
                result = await uow.Repository().AllAsync();
            }

            return result;
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            UserInfo result = new UserInfo();
            RomiUser user;

            using (var uow = new UnitOfWork<RomiUser>(new RomiDbContext()))
            {
                user = await uow.Repository().SingleOrDefaultAsync(t => t.Id == id);
                result = new UserInfo()
                             {
                                 Id = user.Id,
                                 Username = user.UserName,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                             };
            }

            return this.Ok(result);
        }

        [Route("roles/{roleId}/users")]
        [HttpGet]
        public async Task<IEnumerable<UserInfo>> GetUsers(string roleId)
        {
            RomiRole role = null;
            using (var uow = new UnitOfWork<RomiRole>(new RomiDbContext()))
            {
                role = await uow.Repository().SingleOrDefaultAsync(t => t.Id == roleId || t.Name == roleId);
                if (role == null)
                    throw new Exception($"Role '{roleId}' not found");
            }

            var users = await this._userManager.GetUsersInRoleAsync(role.Name);
            return users.Select(
                t => new UserInfo()
                         {
                             Id = t.Id, Username = t.UserName, FirstName = t.FirstName, LastName = t.LastName,
                         }).ToList();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RomiUser user)
        {
            try
            {
                // save 
                this._userManager.CreateAsync(user);
                return this.Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return this.BadRequest(new { message = ex.Message });
            }
        }

        [Route("roles/{roleId}/users/{userid}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveUserfromRole(string roleId, string userId)
        {
            RomiRole role = null;
            RomiUser user = null;
            using (var uow = new UnitOfWork<RomiRole>(new RomiDbContext()))
            {
                role = await uow.Repository().SingleOrDefaultAsync(t => t.Id == roleId);
                if (role == null)
                    return this.BadRequest(new { Message = $"RoleId '{roleId}' not found" });
            }

            using (var uow = new UnitOfWork<RomiUser>(new RomiDbContext()))
            {
                user = await uow.Repository().SingleOrDefaultAsync(t => t.Id == userId);
                if (user == null)
                    return this.BadRequest(new { Message = $"UserId '{userId}' not found" });
            }

            await this._userManager.RemoveFromRoleAsync(user, role.Name);
            return this.Ok();
        }

        [AllowAnonymous]
        [HttpPost("signout")]
        public async Task<IActionResult> Signout()
        {
            await this._signInManager.SignOutAsync();
            foreach (var cookieKey in this.Request.Cookies.Keys)
            {
                if (cookieKey == ".AspNetCore.Identity.Application" || cookieKey.StartsWith(".AspNetCore.Antiforgery."))
                {
                    this.Response.Cookies.Delete(cookieKey);
                }
            }

            return this.Redirect("/Identity/Account/Logout");
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserInfo userDto)
        {
            RomiUser user;

            try
            {
                using (var uow = new UnitOfWork<RomiUser>(new RomiDbContext()))
                {
                    user = await uow.Repository().SingleOrDefaultAsync(t => t.Id == id);
                    user.FirstName = userDto.FirstName;
                    user.LastName = userDto.LastName;
                    user.UserName = userDto.Username;
                }

                // save 
                await this._userManager.UpdateAsync(user);
                return this.Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return this.BadRequest(new { message = ex.Message });
            }
        }
    }
}