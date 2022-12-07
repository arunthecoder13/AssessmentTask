using AssessmentTask.ModelDTO;
using AssessmentTask.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AssessmentTask.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateToken(User user, string tokenValue, DatabaseContext _context)
        {
            try
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "Customer")
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenValue));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            catch (Exception ex)
            {
                Log(_context, "AssessmentTask.UserService.CreateToken", ex.Message, Guid.Empty, ex);
                return string.Empty;
            }
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }
        public User GetUser(UserDto user, DatabaseContext _context)
        {
            try
            {
                var User = _context.User.Where(x => x.Username == user.Username && x.isActive == 1).FirstOrDefault();
                if (User != null)
                {
                    return User;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log(_context, "AssessmentTask.UserService.GetUser", ex.Message, Guid.Empty, ex);
                return null;
            }
        }

        public void Log(DatabaseContext _context, string originator, string message, Guid UserId, Exception ex = null)
        {
            try
            {

                ApplicationLog logEntry = new ApplicationLog()
                {
                    ApplicationLogID = Guid.NewGuid(),
                    Exception = (ex != null) ? ex.ToString() : "",
                    LogDate = DateTime.Now,
                    LogOriginator = originator,
                    Message = message,
                    UserID = UserId
                };

                _context.ApplicationLog.Add(logEntry);
                _context.SaveChanges();
            }
            catch
            {
                //Log to some other location
            }
        }

        public void SetRefreshToken(User user, RefreshToken newRefreshToken, DatabaseContext _context)
        {
            try
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = newRefreshToken.Expires
                };
                _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
                user.RefreshToken = newRefreshToken.Token;
                user.TokenCreated = newRefreshToken.Created;
                user.TokenExpires = newRefreshToken.Expires;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Log(_context, "AssessmentTask.UserService.SetRefreshToken", ex.Message, Guid.Empty, ex);
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt, DatabaseContext _context)
        {
            try
            {
                using (var hmac = new HMACSHA512(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    return computedHash.SequenceEqual(passwordHash);
                }
            }
            catch (Exception ex)
            {
                Log(_context, "AssessmentTask.UserService.VerifyPasswordHash", ex.Message, Guid.Empty, ex);
                return false;
            }
        }

        public Response RegisterUser(User user, DatabaseContext _context)
        {
            Response response = new Response();
            try
            {
                var checkUserAlreadyExist = _context.User.Where(x => x.Username == user.Username && x.isActive == 1).FirstOrDefault();
                if (checkUserAlreadyExist == null)
                {
                    user.isActive = 1;
                    _context.User.Add(user);
                    var isSuccess = _context.SaveChanges();
                    if (isSuccess > 0)
                    {
                        response.IsError = 'N';
                        response.Message = "Successfully Registered.";
                        return response;
                    }
                    else
                    {
                        response.IsError = 'Y';
                        response.Message = "Something went wrong. Please Contact Administrator";
                        return response;
                    }
                }
                else
                {
                    response.IsError = 'N';
                    response.Message = "Username Already Exist.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                Log(_context, "AssessmentTask.UserService.RegisterUser", ex.Message, Guid.Empty, ex);
                response.IsError = 'Y';
                response.Message = "Something went wrong. Please Contact Administrator : " + ex.Message;
                return response;
            }
        }
    }
}
