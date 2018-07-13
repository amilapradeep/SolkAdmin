using Newtonsoft.Json.Linq;
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

        public static bool SendUsingBell { get; set; }

        public static async Task SendMessage(string phoneNumber, string messageText)
        {
            SendSMS.LogFilePath = ConfigurationManager.AppSettings["LogFilePath"];
            SendSMS.SMSGatewayUrl = ConfigurationManager.AppSettings["SMS_GatewayURL"];
            SendSMS.SMSGatewayAuthCode = ConfigurationManager.AppSettings["SMS_Gateway_AuthCode"];
            SendSMS.SendUsingBell = Convert.ToBoolean(ConfigurationManager.AppSettings["SendUsingBell"]);

            if (SendUsingBell)
            {
                await SendMessageBellAsync(phoneNumber, messageText);
            }
            else
            {
                await SendMessageDialogAsync(phoneNumber, messageText);
            }
        }

        [Obsolete ("Don't use this, Instead use bell or dialog methods")]
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

        private static async Task SendMessageDialogAsync(string Phone, string Message)
        {
            //dialog service
            using (var stringContent = new StringContent("destination=" + Phone + "&q=" + SMSGatewayAuthCode + "&message=" + Message,
                                                            Encoding.UTF8, "application/x-www-form-urlencoded"))
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = await client.PostAsync(SMSGatewayUrl, stringContent);
                        var result = await response.Content.ReadAsStringAsync();

                        if (result == "0")
                        {
                            LogInfo(Phone + " Sent at " + DateTime.Now.ToString("yyyy-dd-M HH-mm-ss"));
                        }
                        else
                        {
                            LogInfo(Phone + " Failed at " + DateTime.Now.ToString("yyyy-dd-M HH-mm-ss") + " " + result);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogInfo(Phone + " Failed at " + DateTime.Now.ToString("yyyy-dd-M HH-mm-ss") + " - Exception " + ex.Message);
                    }
                }
            }
        }

        private static async Task SendMessageBellAsync(string Phone, string Message)
        {
            var BellSMSURL = ConfigurationManager.AppSettings["BellSMSURL"];
            var BellSMSCompanyId = ConfigurationManager.AppSettings["BellSMSCompanyId"];
            var BellSMSPassword = ConfigurationManager.AppSettings["BellSMSPassword"];

            //http://119.235.1.63:4050/Sms.svc/SendSms?phoneNumber=[phoneNumber]&smsMessage=[smsMessage]&companyId=[companyId]&pword=[pword]
            var smsCommand = string.Concat(BellSMSURL, "?phoneNumber=", Phone, "&smsMessage=", Message, "&companyId=", BellSMSCompanyId, "&pword=", BellSMSPassword);

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(smsCommand);
                    var result = await response.Content.ReadAsStringAsync();

                    JObject joResponse = JObject.Parse(result);

                    string responseCode = joResponse["Status"].ToString();
                    string responseData = joResponse["Data"].ToString();
                    string responseId = joResponse["ID"].ToString();

                    if (responseCode == "200")
                    {
                        LogInfo(Phone + " Sent at " + DateTime.Now.ToString("yyyy-dd-M HH-mm-ss") + " ID : " + responseId);
                    }
                    else
                    {
                        LogInfo(Phone + " Failed at " + DateTime.Now.ToString("yyyy-dd-M HH-mm-ss") + " " + responseData + " ID : " + responseId);
                    }
                }
                catch (Exception ex)
                {
                    LogInfo(Phone + " Failed at " + DateTime.Now.ToString("yyyy-dd-M HH-mm-ss") + " - Exception " + ex.Message);
                }
            }
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
