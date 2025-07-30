using DataAccessLayer.RequestDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Implements;

namespace HotelBookingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices authServices;
        public AuthController(IAuthServices authServices)
        {
            this.authServices = authServices;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest model, [FromServices] JwtTokenGeneratorServices tokenGen)
        {
            try
            {
                var user = authServices.Login(model);
                var token = tokenGen.GenerateToken(user);
                return Ok(new
                {
                    token = token
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterReq req)
        {
            try
            {
                var user = authServices.Register(req);

                return Ok(new
                {
                    user = user,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getalluser")]
        [Authorize]
        public IActionResult GetAllUser()
        {
            try
            {
                var u = authServices.GetAllAccounts();
                return Ok(u);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("deleteuser/{id}")]
        [Authorize]
        public IActionResult DeleteAccount(int id)
        {
            try
            {
                authServices.DeleteAccount(id);
                return Ok("Account delete successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public IActionResult ResetPassword(ResetPassReq req)
        {
            try
            {
                authServices.ResetPassword(req);
                return Ok($"Your new password have already sent to {req.Email}");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
