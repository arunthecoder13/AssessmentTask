using AssessmentTask.ModelDTO;
using AssessmentTask.Models;

namespace AssessmentTask.Services.UserService
{
    public interface IUserService
    {
        Response RegisterUser(User user, DatabaseContext _context);
        User GetUser(UserDto user, DatabaseContext _context);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt, DatabaseContext _context);
        string CreateToken(User user, string tokenValue, DatabaseContext _context);
        RefreshToken GenerateRefreshToken();
        void SetRefreshToken(User user, RefreshToken newRefreshToken, DatabaseContext _context);

        void Log(DatabaseContext _context, string originator, string message, Guid UserId, Exception ex = null);
    }
}
