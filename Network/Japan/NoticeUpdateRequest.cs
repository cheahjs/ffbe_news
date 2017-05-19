using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FFBENews.Network.Japan
{
    public class NoticeUpdateRequest : INetworkRequest
    {
        private readonly bool _past;

        public NoticeUpdateRequest(bool past)
        {
            _past = past;
        }

        public void HandleResponse(Dictionary<string, dynamic> response)
        {
            var file = _past ? "../news_past.json" : "../news.json";
            var newsList = ((JArray) response[_past ? "R17unt2e" : "tN8f0PSB"]).ToObject<List<Dictionary<string, string>>>();
            var parsedList = newsList.Select(news => new NewsItem
            {
                Id = news["td06MKEX"],
                Type = news["5GNraZM0"],//(NewsType) int.Parse(news["5GNraZM0"]),
                DisplayDate = news["1d8R5ajV"],
                Caption = news["15Y3fBmF"],
                Url = news["1X65WPLU"],
                OpenDescription = news["DSJpb89M"],
                OpenStartDate = news["5r8HSq1N"],
                OpenEndDate = news["M9ZKJY3w"]
            }).ToList();
            Console.WriteLine($"Writing {parsedList.Count} news items to {file}.");
            File.WriteAllText(file,
                JsonConvert.SerializeObject(new NewsFile {LastUpdated = DateTime.UtcNow, NewsList = parsedList}));
            Console.WriteLine($"Wrote to {file} at {DateTime.UtcNow}.");
        }

        public string CreateBody()
        {
            var body = new Dictionary<string, dynamic>();
            BaseRequestUtils.AddUserInfo(body);

            var jsonObject = new List<Dictionary<string, string>>();
            var dict = new Dictionary<string, string> {["8mD2v6IX"] = _past ? "2" : "1"}; //1=news, 2=old, 3=events
            jsonObject.Add(dict);
            body["gM1w8m4y"] = jsonObject;

            return JsonConvert.SerializeObject(body, Formatting.None);
        }

        public string GetEncodeKey()
        {
            return "9t68YyjT";
        }

        public string GetRequestId()
        {
            return "CQ4jTm2F";
        }

        public string GetUrl()
        {
            return "TqtzK84R";
        }

        private class NewsItem
        {
            public string Id;
            //public NewsType Type;
            public string Type;
            public string Caption;
            public string Url;
            public string DisplayDate;
            public string OpenDescription;
            public string OpenStartDate;
            public string OpenEndDate;
        }

        private enum NewsType
        {
            Notice = 1,
            Event = 2,
            Important = 3,
            Apology = 4,
            ClientVersion = 5,
        }

        private class NewsFile
        {
            public List<NewsItem> NewsList;
            public DateTime LastUpdated;
        }
    }
}
