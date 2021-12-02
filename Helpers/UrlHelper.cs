namespace Framework.Helpers
{
    public class UrlHelper
    {
        /// <summary>
        /// Removes = character, replace + with - and / with _ as these characters are not url safe
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string MakeStringUrlSafe(string token)
        {
            return token.TrimEnd(new char[] { '=' }).Replace('+', '-').Replace('/', '_');
        }

        /// <summary>
        /// Replace - with + and _ with / and adds 2 or 1 = at the end if length%4 is 2 or 3
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Removes Async keyword from actions name so that it can be used with CreateAtAction function as it will not accept action name with Async if keyword is suppresed in MVC confiuration.
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static string TrimAsyncKeyword(string actionName)
        {
            return actionName.Remove(actionName.LastIndexOf("Async"));
        }
    }
}