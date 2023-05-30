using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sunyu_opendata.Services;

namespace sunyu_opendata
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("轉檔開始");
            string str = System.AppDomain.CurrentDomain.BaseDirectory;

            using (TDXService srv = new TDXService())
            {
                await srv.HandleDailyTimetable();
            }
            using (OpendataService srv = new OpendataService())
            {
                await srv.GetJDocs();
            
            }
            Console.WriteLine("轉檔結束");
           // Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
