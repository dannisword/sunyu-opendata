using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using sunyu_opendata.Infrastructure;
using sunyu_opendata.Entities;
using sunyu_opendata.Models;
namespace sunyu_opendata.Services
{
    public class OpendataService : DefaultService
    {
        public async Task GetJDocs()
        {
            var token = this.getToken();
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
            var docs = new List<JUDoc>();
            foreach (var item in source)
            {
                url = "https://data.judicial.gov.tw/jdg/api/JDoc";
                var p = new JParam()
                {
                    Token = token.Token,
                    J = item
                };
                var data = this.PostRequest(url, p);
                try
                {
                    e = data.Contains("error");
                  
                    if (e == true)
                    {
                        await this.Waring(data);
                        Console.WriteLine(data);
                        continue;
                    }
                    var jDoc = JsonSerializer.Deserialize<JDoc>(data);
                    if (jDoc.JID != null)
                    {
                        using (var db = new SQLContext(this.connectName))
                        {
                            var doc = new JUDoc()
                            {
                                Attachments = jDoc.ATTACHMENTS == null ? "" : JsonSerializer.Serialize(jDoc.ATTACHMENTS),
                                JID = jDoc.JID,
                                JFullX = JsonSerializer.Serialize(jDoc.JFULLX),
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
                            Console.WriteLine(doc.JID);
                            db.JDocs.Add(doc);
                            db.SaveChanges();
                        }
                    }
                }
                catch (System.Text.Json.JsonException ex)
                {
                    Console.WriteLine(ex.Message);
                    await this.Waring(ex.Message);
                }
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
    }
}
