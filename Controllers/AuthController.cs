using AssessmentTask.ModelDTO;
using AssessmentTask.Models;
using AssessmentTask.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(DatabaseContext context, IConfiguration configuration, IUserService userService)
        {
            _context = context;
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("registerUser")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            Response response = new Response();
            try
            {
                _userService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                User user = new User();
                user.UserId = Guid.NewGuid();
                user.Username = request.Username;
                user.PasswordHash = Convert.ToBase64String(passwordHash);
                user.PasswordSalt = Convert.ToBase64String(passwordSalt);
                var result = _userService.RegisterUser(user, _context);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _userService.Log(_context, "AssessmentTask.AuthController.Register", ex.Message, Guid.Empty, ex);
                response.IsError = 'Y';
                response.Message = ex.Message;
                return Ok(response);
            }
        }


        [HttpPost("Userlogin")]
        public async Task<ActionResult<ResponseToken>> Login(UserDto request)
        {
            ResponseToken response = new ResponseToken();
            try
            {
                var user = _userService.GetUser(request, _context);
                if (user == null)
                {
                    response.IsError = 'N';
                    response.Message = "User Not Found.";
                    return Ok(response);
                }
                else
                {
                    if (!_userService.VerifyPasswordHash(request.Password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt),
                        _context))
                    {
                        response.IsError = 'N';
                        response.Message = "Wrong Password, Login Failed. Try Again.";
                        return Ok(response);
                    }
                    string tokenValue = _configuration.GetSection("AppSettings:Token").Value;
                    string token = _userService.CreateToken(user, tokenValue, _context);

                    var refreshToken = _userService.GenerateRefreshToken();
                    _userService.SetRefreshToken(user, refreshToken, _context);
                    response.IsError = 'N';
                    response.Message = "Logged in Successfully";
                    response.Token = token;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _userService.Log(_context, "AssessmentTask.AuthController.Login", ex.Message, Guid.Empty, ex);
                response.IsError = 'Y';
                response.Message = ex.Message;
                return Ok(response);
            }
        }

        [HttpPost("refresh-token"), Authorize]
        public async Task<ActionResult<ResponseToken>> RefreshToken(UserDto request)
        {
            ResponseToken response = new ResponseToken();
            try
            {
                var user = _userService.GetUser(request, _context);
                if (user == null)
                {
                    response.IsError = 'N';
                    response.Message = "User Not Found.";
                    return Ok(response);
                }
                else
                {
                    var refreshToken = Request.Cookies["refreshToken"];

                    if (!user.RefreshToken.Equals(refreshToken))
                    {
                        response.IsError = 'N';
                        response.Message = "Invalid Refresh Token.";
                        return Unauthorized(response);
                    }
                    else if (user.TokenExpires < DateTime.Now)
                    {
                        response.IsError = 'N';
                        response.Message = "Token expired.";
                        return Unauthorized(response);
                    }
                    string tokenValue = _configuration.GetSection("AppSettings:Token").Value;
                    string token = _userService.CreateToken(user, tokenValue, _context); // fresh token
                    var newRefreshToken = _userService.GenerateRefreshToken();
                    _userService.SetRefreshToken(user, newRefreshToken, _context);

                    response.IsError = 'N';
                    response.Message = "Token Refreshed Successfully";
                    response.Token = token;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _userService.Log(_context, "AssessmentTask.AuthController.RefreshToken", ex.Message, Guid.Empty, ex);
                response.IsError = 'Y';
                response.Message = ex.Message;
                return Ok(response);
            }
        }

    }
}
