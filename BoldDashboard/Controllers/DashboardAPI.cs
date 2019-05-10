using BoldDashboard.Modals;
using BoldDashboard.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BoldDashboard.Controllers
{
    public class DashboardAPI
    {
        private APISettings apiSettings = new APISettings();
        private ChatResponse allChatMessages = new ChatResponse();
        public DashboardAPI(string apiSettingsStr)
        {
            
            char delimiter = ':';
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

        public ChatResponse GetAllChatMessages()
        {

            string URL = string.Format("https://api.boldchat.com/aid/{0}/data/rest/json/v1/getAllChatMessages?auth={1}", apiSettings.accountId, apiSettings.authHash);
            WebRequest request = WebRequest.Create(URL);
            request.Method = "GET";
            HttpWebResponse response = null;
            response = (HttpWebResponse)request.GetResponse();

            string result = null;
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();

                sr.Close();
            }
            allChatMessages = JsonConvert.DeserializeObject<ChatResponse>(result);
            return allChatMessages;
        }

        public int GetChatsCount()
        {
            ChatResponse allChats = GetAllChatMessages();
            int count = allChats.Data.GroupBy(x => x.ChatID).Count();
            return count;
        }

        public ClientType GetVisitType()
        {
            ClientType ct = new ClientType();
            IEnumerable<string> allChatIds = allChatMessages.Data.Select(e => e.ChatID).Distinct();
            foreach (string chatId in allChatIds)
            {
                string URL = string.Format("https://api.boldchat.com/aid/{0}/data/rest/json/v1/getChat?auth={1}&ChatID={2}", apiSettings.accountId, apiSettings.authHash, chatId);
                WebRequest request = WebRequest.Create(URL);
                request.Method = "GET";
                HttpWebResponse response = null;
                response = (HttpWebResponse)request.GetResponse();

                string result = null;
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();

                    sr.Close();
                }
                Chat chat = JsonConvert.DeserializeObject<Chat>(result);
                if (chat.Data.ChatType == 1 || chat.Data.ChatType == 2 || chat.Data.ChatType == 0)
                    ct.nonMobile++;
                else if (chat.Data.ChatType == 4 || chat.Data.ChatType == 8)
                    ct.mobile++;
            }
            return ct;
        }
    }
}