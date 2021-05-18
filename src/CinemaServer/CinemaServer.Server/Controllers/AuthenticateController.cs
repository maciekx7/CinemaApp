using CinemaServer.Model.cinemadb;
using CinemaServer.Rest.Logic.APILogic;
using CinemaServer.Rest.Model.APIModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using CinemaServer.DBConnector.DBInfo;
using Microsoft.Extensions.Logging;

namespace CinemaServer.Server.Controllers
{
    [AllowAnonymous]
    public class AuthenticateController : Controller
    {
        private UserManager<Client> userManager;
        private readonly ICinemaQueriesHandler cinemaQueriesHandler;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(UserManager<Client> userManager, ICinemaQueriesHandler cinemaQueriesHandler, ILogger<AuthenticateController> logger)
        {
            this.userManager = userManager;
            this.cinemaQueriesHandler = cinemaQueriesHandler;
            this._logger = logger;
        }

        

        [HttpPost]
        [Route("register")]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] LoginData model)
        {
            _logger.LogInformation("[POST] [register] Request");
            try
            {
                var result = userManager.CreateAsync(new Client
                {
                    Name = model.Name,
                    Lastname = model.LastName,
                    Email = model.Mail,
                    UserName = model.Mail, //ATTENCION! because we don't use username - we override it by email
                    Phone = model.Phone
                }, model.Password).Result;

                var users = userManager.Users;
                _logger.LogInformation($"[POST] [register] {model.Mail} registered");

            } catch(Exception e)
            {
                _logger.LogInformation("[POST] [register] ERROR");
                Console.WriteLine(e);
                return StatusCode(404);
            }
            return new StatusCodeResult(200);
        }

        
        [HttpPost]
        [Route("update")]
        [Authorize(Policy = "ValidAccessToken")]
        [ValidateModel]
        public async Task<IActionResult> UpdateClient([FromBody] ClientData clientData)
        {
           
            try
            {
                var email = Request.Headers["email"];
                _logger.LogInformation($"[POST] [update] {email} Request");

                var user = await userManager.FindByEmailAsync(email);

                user.Name = clientData.Name;
                user.Lastname = clientData.Lastname;
                user.Phone = clientData.Phone;

                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var result1 = await userManager.ResetPasswordAsync(user, token, clientData.Password);

                var res = userManager.UpdateAsync(user);

                var result = cinemaQueriesHandler.UpdateClient();

                _logger.LogInformation($"[POST] [update] {email} updated");

            }
            catch (Exception e)
            {
                _logger.LogInformation($"[POST] [update] ERROR");
                Console.WriteLine(e);
                return StatusCode(404);

            }
            return StatusCode(200);

        }



        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] NewLoginData model)
        {
            try
            {
                DBInfoData dB = DBInfoData.GetInstance();
                var user = await userManager.FindByEmailAsync(model.Mail);
                _logger.LogInformation($"[POST] [login] {model.Mail} Request");

                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    
                    var authClaims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7S79jvOkEdwoRqHx"));
                    var token = new JwtSecurityToken(
                        issuer: dB.UserName,
                        audience: "javaapp",
                        expires: DateTime.Now.AddDays(5),
                        claims: authClaims,
                        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                    _logger.LogInformation($"[POST] [login] {model.Mail} logged");
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });

                }
                return Unauthorized();
                //var users = userManager.Users;
            } catch(Exception e)
            {
                _logger.LogInformation($"[POST] [login] ERROR");
                Console.WriteLine(e);
                return StatusCode(404);
            }
            
        }

        
        [HttpGet]
        [Route("info/{email}")]
        [Authorize(Policy = "ValidAccessToken")]
        public async Task<IActionResult> ClientInfo()
        {
            try
            {
                string email = RouteData.Values["email"].ToString();
                _logger.LogInformation($"[GET] [info] {email} Request");

                var result = cinemaQueriesHandler.GetClientInfo(email);
                if(result == null) {
                    _logger.LogInformation($"[GET] [info] {email} EROR");
                    return new StatusCodeResult(404); }

                return new JsonResult(result);
            } catch(Exception e)
            {
                _logger.LogInformation($"[GET] [info] ERROR");
                Console.WriteLine(e);
                return new StatusCodeResult(404);
            }
        }



        public class ValidateModelAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                if (!context.ModelState.IsValid)
                {
                    context.Result = new BadRequestObjectResult(context.ModelState);
                }
            }
        }

    }
}
