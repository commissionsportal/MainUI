using MainUI.ConnectedServices.AuthService;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MainUI.Authentication
{
    public class PasswordHasher : IPasswordHasher<ApplicationUser>
    {
        private readonly IAuthClient _restClient;

        public PasswordHasher(IAuthClient restClient)
        {
            _restClient = restClient;
        }

        public string HashPassword(ApplicationUser user, string password)
        {
            return CreateMD5(password);
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            return VerifyHashedPasswordAsync(user, hashedPassword, providedPassword).GetAwaiter().GetResult();
        }

        public async Task<PasswordVerificationResult> VerifyHashedPasswordAsync(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            try
            {
                var auth = await _restClient.GetValue<AuthToken>($"/Authentication?username={user.UserName}&password={providedPassword}");

                user.ApiToken = auth.Token;
                return PasswordVerificationResult.Success;
            }
            catch
            {
                return PasswordVerificationResult.Failed;
            }

        }
    }
}
