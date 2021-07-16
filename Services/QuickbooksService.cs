using Intuit.Ipp.Core;
using Intuit.Ipp.DataService;
using Intuit.Ipp.OAuth2PlatformClient;
using Intuit.Ipp.Security;

namespace Framework.Services
{
    public class QuickbooksService
    {
        protected string ConsumerKey { get; }
        protected string ConsumerSecret { get; }
        private string RealmId { get; }
        public string RefreshToken { get; set; }
        protected DataService DataService { get; }
        protected Environments Environment { get; }
        public enum Environments
        {
            sandbox, production
        }

        private string BaseUrl { get; }

        public QuickbooksService(string consumerKey, string consumerSecret, string realmId, string refreshToken)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            RealmId = realmId;
            RefreshToken = refreshToken;
            DataService = CreateDataService();
        }
        private DataService CreateDataService()
        {
            var tokenResponse = RefreshAccessToken();
            var requestValidator = new OAuth2RequestValidator(tokenResponse.AccessToken);
            var serviceContext = new ServiceContext(RealmId, IntuitServicesType.QBO, requestValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = BaseUrl;
            return new DataService(serviceContext);
        }

        private TokenResponse RefreshAccessToken()
        {
            var oauth2Client = new OAuth2Client(ConsumerKey, ConsumerSecret, string.Empty, Environment.GetStringValue());
            return oauth2Client.RefreshTokenAsync(RefreshToken).GetAwaiter().GetResult();
        }
    }
}