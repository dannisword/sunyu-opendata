using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using sunyu_opendata.Infrastructure;
using sunyu_opendata.Entities;
using sunyu_opendata.Models;
namespace sunyu_opendata.Services
{
    public class OpendataService : DefaultService
    {
        public async Task GetJDocs()
        {
            try
            {
                this.delJUDoc();
                this.Success("裁決書開始轉檔");
                var token = this.getToken();
                // JList 
                var url = "https://data.judicial.gov.tw/jdg/api/JList";
                var result = this.PostRequest(url, token);
                var e = result.Contains("error");
                if (e == true)
                {
                    await this.Waring(result);
                    Console.WriteLine(result);
                    return;
                }
                var jList = JsonSerializer.Deserialize<List<JList>>(result);
                var source = jList[0].list;
                this.setJUList(jList[0]);
                // JDoc
                int index = 1;
                foreach (var item in source)
                {
                    url = "https://data.judicial.gov.tw/jdg/api/JDoc";
                    var p = new JParam()
                    {
                        Token = token.Token,
                        J = item
                    };
                    var data = this.PostRequest(url, p);
                    e = data.Contains("error");
                    if (e == true)
                    {
                        //await this.Waring(data);
                        index++;
                        Console.WriteLine(data);
                        continue;
                    }
                    var jDoc = JsonSerializer.Deserialize<JDoc>(data);
                    if (jDoc.JTITLE != "政府採購法")
                    {
                        //continue;
                    }
                    if (jDoc.JID != null)
                    {
                        using (var db = new SQLContext(this.connectName))
                        {
                            var doc = new JUDoc()
                            {
                                Attachments = jDoc.ATTACHMENTS == null ? "" : JsonSerializer.Serialize(jDoc.ATTACHMENTS),
                                JID = jDoc.JID,
                                ListDate = jList[0].date,
                                JFullX = jDoc.JFULLX.JFULLCONTENT,//JsonSerializer.Serialize(Regex.Unescape(jDoc.JFULLX.JFULLCONTENT)),
                                JYear = jDoc.JYEAR,
                                JCase = jDoc.JCASE,
                                JNO = jDoc.JNO,
                                JDate = jDoc.JDATE,
                                JTitle = jDoc.JTITLE,
                                CreateTime = DateTime.Now,
                                CreateUser = 0,
                                ModifyTime = DateTime.Now,
                                ModifyUser = 0
                            };
                            Console.WriteLine(index + ":" + doc.JID);
                            index++;
                            db.JDocs.Add(doc);
                            db.SaveChanges();
                        }
                    }
                }
                // 轉換 違反政府採購法
                this.setCourtVerdict(jList[0].date);
                this.Success("裁決書完成轉檔");
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine(ex.Message);
                await this.Waring(ex.Message);
            }
        
        }
        private OpendataToken getToken()
        {
            var url = "https://data.judicial.gov.tw/jdg/api/Auth";

            var auth = new Auth()
            {
                user = "dannis",
                password = "Qwer890@"
            };
            var result = this.PostRequest(url, auth);
            var token = JsonSerializer.Deserialize<OpendataToken>(result);
            return token;
        }
        private bool setJUDocs(IEnumerable<JUDoc> data)
        {
            using (var db = new SQLContext(this.connectName))
            {
                db.JDocs.AddRange(data);
                return db.SaveChanges() > 0 ? true : false;
            }
        }
        private bool setJUList(JList data)
        {
            using (var db = new SQLContext(this.connectName))
            {
                var jList = new List<JUList>();
                var q = from a in db.JLists
                        where a.ListDate == data.date
                        select a;
                if (q.Any())
                {
                    return false;
                }
                foreach (var item in data.list)
                {
                    JUList list = new JUList()
                    {
                        ListDate = data.date,
                        ListItem = item,
                        CreateTime = DateTime.Now,
                        CreateUser = 0,
                        ModifyTime = DateTime.Now,
                        ModifyUser = 0
                    };
                    jList.Add(list);
                }
                db.JLists.AddRange(jList);
                return db.SaveChanges() > 0 ? true : false;
            }
        }
        /// <summary>
        /// 轉檔
        /// </summary>
        /// <param name="listDate"></param>
        /// <returns></returns>
        public async Task setCourtVerdict(string listDate)
        {
            using (var db = new SQLContext(this.connectName))
            {
                var q = from a in db.JDocs
                        where a.ListDate == listDate &&
                              //"採購".Contains(a.JTitle)
                              a.JTitle.Contains("採購")
                        select a;
                var cs = new List<CourtVerdict>();

                foreach (var item in q)
                {
                    var ids = item.JID.Split(',');

                    var c = new CourtVerdict()
                    {
                        JID = item.JID,
                        JYear = item.JYear,
                        JCase = item.JCase,
                        JNo = item.JNO,
                        JDate = item.JDate,
                        JTitle = item.JTitle,
                        JFull = item.JFullX,
                        CreateTime = DateTime.Now,
                        CreateUser = 0,
                        ModifyTime = DateTime.Now,
                        ModifyUser = 0
                    };
                    cs.Add(c);
                }
                db.CourtVerdicts.AddRange(cs);
                var count =  db.SaveChanges();
                Console.WriteLine($"裁決書,寫入筆數{count}");
                await this.Success($"裁決書,寫入筆數{count}");
            }
        }

        private void delJUDoc()
        {
            using (var context = new SQLContext(this.connectName))
            {
                var dt = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                var cmd = $"DELETE FROM JUDoc WHERE ListDate <= '{dt}'";
                context.Database.ExecuteSqlCommand(cmd);
            }
        }
         
        public void Example()
        {
            var dockeys = this.DocKeys;
            using (var db = new SQLContext(this.connectName))
            {
                var q = from a in db.JDocs
                        where a.JTitle.Contains(dockeys)
                        select a;
            }
                Console.WriteLine(this.DocKeys);
        }
    }
}
