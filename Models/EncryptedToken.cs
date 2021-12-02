using Framework.Helpers;
using Framework.Services;
using Newtonsoft.Json;
using System;

namespace Framework.Models
{
    public class EncryptedToken
    {
        public DateTime Expiry { get; set; }
        private string Id { get; set; }

        private EncryptedToken()
        {
        }

        public static string Generate(string secretKey, TimeSpan timeSpan)
        {
            var token = new EncryptedToken()
            {
                Expiry = DateTime.UtcNow.Add(timeSpan),
                Id = Guid.NewGuid().ToString()
            };
            var tokenStr = JsonConvert.SerializeObject(token);
            tokenStr = EncryptionService.Encrypt(tokenStr, secretKey);
            return UrlHelper.MakeStringUrlSafe(tokenStr);
        }

        public static EncryptedToken Parse(string token, string secretKey)
        {
            token = UrlHelper.RevertUrlSafe(token);
            token = EncryptionService.Decrypt(token, secretKey);
            return JsonConvert.DeserializeObject<EncryptedToken>(token);
        }
    }
}