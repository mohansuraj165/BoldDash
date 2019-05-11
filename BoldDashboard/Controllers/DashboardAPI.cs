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

        /// <summary>
        /// Constructor: Gets authentication keys from API keys
        /// </summary>
        /// <param name="apiSettingsStr">API setting key as string</param>
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

        /// <summary>
        /// For generating SHA512 encoding
        /// </summary>
        /// <param name="inputString">String that requires encoding</param>
        /// <returns>Hashed string</returns>
        public static string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        /// <summary>
        /// Returns Alphanumeric string from hashed string
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        /// <summary>
        /// Gets all chat messages
        /// </summary>
        /// <returns>Object containing all chat messages</returns>
        public ChatResponse GetAllChatMessages()
        {
            try
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
            catch(Exception ex)
            {
                throw new Exception("Exception at GetAllChatMessages", ex);
            }
        }

        /// <summary>
        /// Gets a count of all chats
        /// </summary>
        /// <returns></returns>
        public int GetChatsCount()
        {
            ChatResponse allChats = GetAllChatMessages();
            if(allChats.Status.ToLower()=="success")
                return allChats.Data.GroupBy(x => x.ChatID).Count();
            else
            {
                throw new Exception("Could not fetch date for the given API Key");
            }
        }

        /// <summary>
        /// To get mobile and desktop visits
        /// </summary>
        /// <returns>Object with count of mobile and desktop visits</returns>
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
                if (chat.Status.ToLower() == "error")
                    continue;
                if (chat.Data.ChatType == 1 || chat.Data.ChatType == 2 || chat.Data.ChatType == 0)
                    ct.nonMobile++;
                else if (chat.Data.ChatType == 4 || chat.Data.ChatType == 8)
                    ct.mobile++;
            }
            return ct;
        }

        /// <summary>
        /// Gets availability status of all operstors
        /// </summary>
        /// <returns></returns>
        public List<OperatorData> GetOperatorStatus()
        {
            List<OperatorData> od = new List<OperatorData>();
            string[] serviceTypes = new string[] { "1", "3", "5", "9", "10" };
            foreach (string type in serviceTypes)
            {
                string URL = string.Format("https://api.boldchat.com/aid/{0}/data/rest/json/v1/getOperatorAvailability?auth={1}&ServiceTypeID={2}", apiSettings.accountId, apiSettings.authHash, type);
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
                var oa = JsonConvert.DeserializeObject<OperatorAvailability>(result);
                if(oa.Status == "success")
                {
                    od.AddRange(oa.Data);
                }

            }
            return od;
        }

        /// <summary>
        /// Sets new availability status of an operator
        /// </summary>
        /// <param name="ServiceTypeID"></param>
        /// <param name="OperatorID"></param>
        /// <param name="StatusType"></param>
        /// <param name="ClientID"></param>
        /// <returns>New availability list of all operators</returns>
        public Response SetOperatorStatus(string ServiceTypeID, string OperatorID, int StatusType, string ClientID)
        {
            string URL = string.Format("https://api.boldchat.com/aid/{0}/data/rest/json/v1/setOperatorAvailability?ServiceTypeID={1}&auth={2}&OperatorID={3}&StatusType={4}&ClientID={5}", apiSettings.accountId, ServiceTypeID, apiSettings.authHash, OperatorID, StatusType, ClientID);
            WebRequest request = WebRequest.Create(URL);
            request.Method = "POST";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string result = null;
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();

                sr.Close();
            }

            return JsonConvert.DeserializeObject<Response>(result);

        }
    }
}