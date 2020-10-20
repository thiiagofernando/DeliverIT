using System.Security.Cryptography;
using System.Text;

namespace DeliverIT.Business.Services
{
    public static class PasswordService
    {
        public static string GeneratePassword(string valor)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(valor);
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X"));
            }
            return result.ToString();
        }
    }
}
