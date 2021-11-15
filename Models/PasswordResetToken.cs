using Framework.Helpers;
using Framework.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class PasswordResetToken
    {
        public DateTime DateCreated { get; set; }
        private string Id { get; set; }
        private PasswordResetToken()
        {

        }

        public static string Generate(string secretKey)
        {
            var token = new PasswordResetToken()
            {
                DateCreated = DateTime.UtcNow,
                Id = Guid.NewGuid().ToString()
            };
            var tokenStr = JsonConvert.SerializeObject(token);
            tokenStr = EncryptionService.Encrypt(tokenStr, secretKey);
            return UrlHelper.MakeStringUrlSafe(tokenStr);
        }

        public static PasswordResetToken Parse(string token, string secretKey)
        {
            token = UrlHelper.RevertUrlSafe(token);
            token = EncryptionService.Decrypt(token, secretKey);
            return JsonConvert.DeserializeObject<PasswordResetToken>(token);
        }
    }
}
