using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyu_opendata.Infrastructure
{
    public class ExceptionFilter : Exception
    {
        public string MethName { get; set; }

        public override string Message { get; }

        public ExceptionFilter(string methName, Exception ex)
        {
            this.MethName = methName;
            this.Message = this.getExceptionMessage(ex);
        }

        private string getExceptionMessage(Exception ex)
        {
            var type = ex.GetType().ToString();
            switch (type)
            {
                case "System.InvalidOperationException":
                    return "無效操作";
                case "System.Net.Http.HttpRequestException":
                    return "HTTP 訊息處理例外狀況";
                case "System.Threading.Tasks.TaskCanceledException":
                    return "工作取消的例外狀況";
                case "System.ArgumentNullException":
                    return "傳遞的引數例外狀況";
                case "System.Text.Json.JsonException":
                    return "無效JSON";
                case "System.NotSupportedException":
                    return "方法不受支援";
                //case Microsoft.Data.SqlClient.SqlException:
                //    return "對資料庫操作產生的例外情況";
                default:
                    return "未攔截例外狀況";
            }
        }
    }
}
