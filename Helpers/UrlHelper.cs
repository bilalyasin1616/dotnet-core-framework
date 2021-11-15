namespace Framework.Helpers
{
    public class UrlHelper
    {
        public static string MakeStringUrlSafe(string token)
        {
            return token.TrimEnd(new char[] { '=' }).Replace('+', '-').Replace('/', '_');
        }

        public static string RevertUrlSafe(string token)
        {
            if (token != null)
            {
                token = token.Replace('_', '/').Replace('-', '+');
                switch (token.Length % 4)
                {
                    case 2: token += "=="; break;
                    case 3: token += "="; break;
                };
            }
            return token;
        }
    }
}
