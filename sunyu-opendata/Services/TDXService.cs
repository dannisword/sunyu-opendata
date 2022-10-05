using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using sunyu_opendata.Infrastructure;
using sunyu_opendata.Entities;
using sunyu_opendata.Models;

namespace sunyu_opendata.Services
{
   public class TDXService :DefaultService
    {
        public TDXService()
        {

        }
        public TDXService(string connectName): base(connectName)
        {

        }

        public async Task HandleDailyTimetable()
        {
            try
            {
                var sb = new StringBuilder();
                var token = await getTDXToken();
                var url = "https://tdx.transportdata.tw/api/basic/v2/Rail/THSR/DailyTimetable/TrainDates";
                var timetables = await this.GetAsync<Timetables>(url, token.access_token);
                foreach (var item in timetables.TrainDates)
                {
                    var tmpDt = item.ToDateTime("yyyy-MM-dd");
                    Console.WriteLine($"轉檔日期{tmpDt}");
                    if (tmpDt == null)
                    {
                        continue;
                    }
                    if (tmpDt < DateTime.Now.Min())
                    {
                        continue;
                    }

                    var dailys = await this.getDailyTimetable(token.access_token, item);
                    // 寫入資料庫
                    var isSet = this.setTHSRs(dailys, item);
                    // 失敗
                    if (isSet == true)
                    {

                        sb.AppendLine($"轉檔日期：{item}, 車次筆數：{dailys.Count()}");
                    }
                    else
                    {
                        sb.AppendLine($"轉檔日期：{item}, 不需轉檔");
                    }
                }
                await this.Success(sb.ToString());

                Console.WriteLine("高鐵轉檔成功");
            }
            catch (Exception e)
            {
                //getTDXToken
                await this.Waring(e.Message);
            }
        }

        private async Task<AccessToken> getTDXToken()
        {
            string baseUrl = "https://tdx.transportdata.tw/auth/realms/TDXConnect/protocol/openid-connect/token";
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpContent formData = new FormUrlEncodedContent(
                        new List<KeyValuePair<string, string>>
                            {
                            new KeyValuePair<string, string>("grant_type", "client_credentials"),
                            new KeyValuePair<string, string>("client_id", "dannis.word-4a3a5894-c2a5-4ce0"),
                            new KeyValuePair<string, string>("client_secret", "5058c0b6-32a1-4f92-9072-e17eb5bb1fee"),
                            }
                        );
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    var response = await httpClient.PostAsync(baseUrl, formData);
                    var responseStr = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<AccessToken>(responseStr);
                }
                catch (Exception ex)
                {
                    throw new ExceptionFilter("GetAsync", ex);
                    //throw new Exception();
                }
            }
        }
        
        private async Task<IEnumerable<THSR>> getDailyTimetable(string token, string trainDate)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = "https://tdx.transportdata.tw/api/basic/v2/Rail/THSR/DailyTimetable/TrainDate/" + trainDate;
                var dailys = await this.GetAsyncs<DailyTimetable>(url, token);
                List<THSR> ts = new List<THSR>();
                foreach(var daily in dailys)
                {
                    int length = daily.StopTimes.Count;
                    foreach (var stop in daily.StopTimes)
                    {
                        var ends = daily.StopTimes.Where(p => p.StopSequence > stop.StopSequence);
                        if (ends.Any())
                        {
                            foreach(var end in ends)
                            {
                                var t = new THSR()
                                {
                                    Direction = daily.DailyTrainInfo.Direction == 0 ? (byte)2 : daily.DailyTrainInfo.Direction,
                                    CarNo = daily.DailyTrainInfo.TrainNo,
                                    StartStationName = stop.StationName.Zh_tw,
                                    DepartureTime = stop.DepartureTime,
                                    EndingStationName = end.StationName.Zh_tw,
                                    ArrivalTime = end.ArrivalTime,
                                    Memo = daily.TrainDate,
                                    CreateTime = DateTime.Now,
                                    CreateUser = 0,
                                    ModifyTime = DateTime.Now,
                                    ModifyUser = 0,
                                };
                                ts.Add(t);
                            }
                        }
                    }
                }
                return ts;
                /*
                return dailys.Select(x => new THSR()
                {
                    Direction = x.DailyTrainInfo.Direction == 0 ? (byte)2 : x.DailyTrainInfo.Direction,
                    CarNo = x.DailyTrainInfo.TrainNo,
                    StartStationName = x.DailyTrainInfo.StartingStationName.Zh_tw,
                    DepartureTime = x.StopTimes.First().DepartureTime,
                    EndingStationName = x.DailyTrainInfo.EndingStationName.Zh_tw,
                    ArrivalTime = x.StopTimes.Last().ArrivalTime,
                    Memo = x.TrainDate,
                    CreateTime = DateTime.Now,
                    CreateUser = 0,
                    ModifyTime = DateTime.Now,
                    ModifyUser = 0,
                });
                */
            }
        }

        private bool setTHSRs(IEnumerable<THSR> records, string trainDate)
        {
            using (var context = new SQLContext(this.connectName))
            {
                context.Database.ExecuteSqlCommand("DELETE FROM THSR");
                int eCode = 0;
                var q = from a in context.THSRs
                        where a.Memo == trainDate
                        select a;
                if (q.Any() == false)
                {
                    context.THSRs.AddRange(records);
                    eCode = context.SaveChanges();
                }
                return eCode > 0 ? true : false;
            }
        }

        public void GetName()
        {
            using (var db = new SQLContext("EQC_NEW"))
            {
                var q = from a in db.THSRs
                        select a;
                var ds = q.ToList();
            }
        }
    }
}
