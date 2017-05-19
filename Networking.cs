using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FFBENews.Network;
using Newtonsoft.Json;

namespace FFBENews
{
    public class Networking
    {
        public static void SendRequest(INetworkRequest request)
        {
            try
            {
                Console.WriteLine("Starting request for {1}:{0}.", request.GetRequestId(), request.GetType().Name);
                var webRequest =
                    (HttpWebRequest)
                        WebRequest.Create(
                            $"http://v4.android.game.exvius.com/lapis/app/php/gme/actionSymbol/{request.GetUrl()}.php");
                webRequest.Headers.Clear();
                webRequest.Method = "POST";
                webRequest.Timeout = 30000;
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Accept = "*/*";
                webRequest.UserAgent = "android";
                webRequest.AutomaticDecompression = DecompressionMethods.GZip;
                webRequest.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
                webRequest.Headers[HttpRequestHeader.AcceptLanguage] = "en";

                var data = Encoding.UTF8.GetBytes(GenerateData(request));
                Console.WriteLine("Sending POST for {1}:{0}.", request.GetRequestId(), request.GetType().Name);
                using (var stream = webRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                }
                Console.WriteLine("Getting response.");
                var responseStream = webRequest.GetResponse();
                var responseStr = "";
                using (var stream = responseStream.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseStr = reader.ReadToEnd();
                    }
                }
                dynamic responseJson = JsonConvert.DeserializeObject(responseStr);
                request.HandleResponse(
                    JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(Encryption.DecryptBase64String(
                        (string) responseJson.t7n6cVWf.qrVcDe48, request.GetEncodeKey())));
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void SendRequestGlobal(INetworkRequest request)
        {
            try
            {
                Console.WriteLine("Starting request for {1}:{0}.", request.GetRequestId(), request.GetType().Name);
                var webRequest =
                    (HttpWebRequest)
                        WebRequest.Create(
                            $"https://lapisv200.gumi.sg/lapisProd/app/php/gme/actionSymbol/{request.GetUrl()}.php");
                webRequest.Headers.Clear();
                webRequest.Method = "POST";
                webRequest.Timeout = 30000;
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Accept = "*/*";
                webRequest.UserAgent = "android";
                webRequest.AutomaticDecompression = DecompressionMethods.GZip;
                webRequest.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
                webRequest.Headers[HttpRequestHeader.AcceptLanguage] = "en";

                var data = Encoding.UTF8.GetBytes(GenerateData(request));
                Console.WriteLine("Sending Global POST for {1}:{0}.", request.GetRequestId(), request.GetType().Name);
                using (var stream = webRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                }
                Console.WriteLine("Getting response.");
                var responseStream = webRequest.GetResponse();
                var responseStr = "";
                using (var stream = responseStream.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseStr = reader.ReadToEnd();
                    }
                }
                dynamic responseJson = JsonConvert.DeserializeObject(responseStr);
                request.HandleResponse(
                    JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(Encryption.DecryptBase64String(
                        (string)responseJson.t7n6cVWf.qrVcDe48, request.GetEncodeKey())));
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static string GenerateData(INetworkRequest request)
        {
            var dict = new Dictionary<string, Dictionary<string, string>>();
            var metaDict = new Dictionary<string, string>
            {
                ["z5hB3P01"] = request.GetRequestId(),
                ["ytHoz4E2"] = "0"
            };
            dict["TEAYk6R1"] = metaDict;
            var body = request.CreateBody();
            var encrypted = Encryption.EncryptBase64String(body, request.GetEncodeKey());
            var bodyDict = new Dictionary<string, string> {["qrVcDe48"] = encrypted};
            dict["t7n6cVWf"] = bodyDict;
            return JsonConvert.SerializeObject(dict, Formatting.None);
        }
    }
}
