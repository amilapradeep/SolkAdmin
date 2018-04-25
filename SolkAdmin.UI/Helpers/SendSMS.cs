// Decompiled with JetBrains decompiler
// Type: SolkAdmin.UI.Helpers.SendSMS
// Assembly: SolkAdmin.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 08258DEC-670D-4CE6-92B4-269F5A1CC95F
// Assembly location: C:\Users\amila\OneDrive\Desktop\SolkAdmin.UI.dll

using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SolkAdmin.UI.Helpers
{
    public class SendSMS
    {
        public static string SMSGatewayUrl { get; set; }

        public static string SMSGatewayAuthCode { get; set; }

        public static string LogFilePath { get; set; }

        public static async Task SendMessage(string phoneNumber, string messageText)
        {
            SendSMS.LogFilePath = ConfigurationManager.AppSettings["LogFilePath"];
            SendSMS.SMSGatewayUrl = ConfigurationManager.AppSettings["SMS_GatewayURL"];
            SendSMS.SMSGatewayAuthCode = ConfigurationManager.AppSettings["SMS_Gateway_AuthCode"];
            await SendSMS.SendMessageAsync(phoneNumber, messageText);
        }

        private static async Task SendMessageAsync(string Phone, string Message)
        {
            StringContent stringContent = new StringContent("destination=" + Phone + "&q=" + SendSMS.SMSGatewayAuthCode + "&message=" + Message, Encoding.UTF8, "application/x-www-form-urlencoded");
            try
            {
                HttpClient client = new HttpClient();
                try
                {
                    string str = await (await client.PostAsync(SendSMS.SMSGatewayUrl, (HttpContent)stringContent)).Content.ReadAsStringAsync();
                    if (str == "0")
                        SendSMS.LogInfo(Phone + " Sent at " + DateTime.Now.ToString("yyyy-dd-M HH-mm-ss"));
                    else
                        SendSMS.LogInfo(Phone + " Failed at " + DateTime.Now.ToString("yyyy-dd-M HH-mm-ss") + " " + str);
                }
                catch (Exception ex)
                {
                    SendSMS.LogInfo(Phone + " Failed at " + DateTime.Now.ToString("yyyy-dd-M HH-mm-ss") + " - Exception " + ex.Message);
                }
                finally
                {
                    if (client != null)
                        client.Dispose();
                }
                client = (HttpClient)null;
            }
            finally
            {
                if (stringContent != null)
                    stringContent.Dispose();
            }
            stringContent = (StringContent)null;
        }

        private static void LogInfo(string logText)
        {
            string dirPath = Path.GetDirectoryName(LogFilePath);

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            using (StreamWriter streamWriter = new StreamWriter(SendSMS.LogFilePath, true))
                streamWriter.WriteLine(logText);
        }
    }
}
