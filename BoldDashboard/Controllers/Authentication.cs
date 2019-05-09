using BoldDashboard.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoldDashboard.Controllers
{
    public class Authentication
    {
        public APISettings GetAPISettings(string apiSettingsStr)
        {
            char delimiter = ':';
            APISettings apiSettings = new APISettings();
            int d1 = apiSettingsStr.IndexOf(delimiter);
            apiSettings.accountId = apiSettingsStr.Substring(0, d1);
            int d2 = apiSettingsStr.IndexOf(delimiter, d1 + 1);
            apiSettings.apiSettingId = apiSettingsStr.Substring(d1 + 1, d2 - d1 - 1);
            apiSettings.apiKey = apiSettingsStr.Substring(d2 + 1, apiSettingsStr.Length - 1 - d2);
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int date = (int)t.TotalSeconds;
            apiSettings.auth = string.Concat(apiSettings.accountId, ":", apiSettings.apiSettingId, ":", date, "000");
            string hash = GenerateSHA512String(string.Concat(apiSettings.auth, apiSettings.apiKey));
            apiSettings.authHash = string.Concat(apiSettings.auth, ":", hash);
            return apiSettings;
        }

        public static string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
