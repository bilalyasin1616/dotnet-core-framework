using System;

namespace Framework.Models
{
    public class QuickbooksConfig
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string RealmId { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public long AccessTokenExpiresIn { get; set; }
        public long RefreshTokenExpiresIn { get; set; }
        public DateTime TokenUpdated { get; set; }
        public DateTime TokenCreated { get; set; }
    }
}
