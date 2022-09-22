using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using sunyu_opendata.Infrastructure;
using sunyu_opendata.Models;
namespace sunyu_opendata.Services
{
    public class DefaultService: IDisposable
    {
        protected string connectName = "EQC_NEW";

        public string DocKeys
        {
            get
            {
                return ConfigurationManager.AppSettings["DocKeys"];
            }
        }

        public DefaultService()
        {
            string strCov = Regex.Unescape("\u6700\u9AD8\u7406");
            //Console.WriteLine(strCov);
        }

        public DefaultService(string connectName)
        {
            this.connectName = connectName; 
        }
        ~DefaultService()
        {
            // Finalizer calls Dispose(false)
            Dispose();
        }
        public async Task<T> GetAsync<T>(string url, string token)
        {
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    if (string.IsNullOrEmpty(token) == false)
                    {
                        client.DefaultRequestHeaders.Add("authorization", $"Bearer {token}");
                    }
                    var response = await client.GetStringAsync(url);
                    return JsonSerializer.Deserialize<T>(response);
                }
                catch (Exception ex)
                {
                    throw new ExceptionFilter("GetAsync", ex);
                }
            }
        }
        public async Task<IEnumerable<T>> GetAsyncs<T>(string url, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(token) == false)
                    {
                        client.DefaultRequestHeaders.Add("authorization", $"Bearer {token}");
                    }
                    var response = await client.GetStringAsync(url);
                    return JsonSerializer.Deserialize<IEnumerable<T>>(response);
                }
                catch (Exception ex)
                {
                    throw new ExceptionFilter("GetAsyncs", ex);
                }
            }
        }
        public string PostRequest(string url, object obj)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var buffer = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));
                    var content = new ByteArrayContent(buffer);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = client.PostAsync(url, content).Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public async Task Success(string content)
        {
            await this.WriteLineAsync(LogType.Success, content);
        }

        public async Task Waring(string content)
        {
            await this.WriteLineAsync(LogType.Waring, content);
        }

        private async Task WriteLineAsync(LogType logType, string content)
        {
            var fileName = string.Format("{0}.log", DateTime.Now.ToString("yyyyMMdd"));
            var typeName = "訊息";
            if (logType == LogType.Waring)
            {
                typeName = "警告";
            }
            var msg = string.Format("[{0}] - {1} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), typeName, content);

            using (StreamWriter file = new StreamWriter(fileName, append: true))
            {
                await file.WriteLineAsync(msg);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
