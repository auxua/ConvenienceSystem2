using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using ConvenienceSystemApp.api;
using ConvenienceSystemApp.iOS;
using System.Threading;
using System.IO;


[assembly: Xamarin.Forms.Dependency(typeof(Downloader_iOS))]
namespace ConvenienceSystemApp.iOS
{
    public class Downloader_iOS : Communication.IDownloader
    {
        public string Get(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
            string result = client.GetStringAsync(url).Result;
            return result;
            
        }

        public async Task<string> GetAsync(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
            string result = await client.GetStringAsync(url);
            return result;
        }

        public string Post(string url, string data)
        {
            WebClient wc = new WebClient();
            Uri uri = new Uri(url);
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Cache-Control"] = "no-cache";
            wc.Headers["Content-Length"] = data.Length.ToString();
            wc.UploadStringCompleted += wc_UploadStringCompleted;
            wc.UploadStringAsync(uri, "POST", data);


            var t = getPOSTResponse();

            //wc.CancelAsync();
            wc = null;
            return t;
            
            
        }

        public async Task<string> PostAsync(string url, string data)
        {
            var http = (HttpWebRequest)WebRequest.Create(new Uri(url));
            //http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            Byte[] bytes = Encoding.UTF8.GetBytes(data);

            using (var stream = await Task.Factory.FromAsync<Stream>(http.BeginGetRequestStream,
                http.EndGetRequestStream, null))
            {
                // Write the bytes to the stream
                await stream.WriteAsync(bytes, 0, bytes.Length);
            }

            using (var response = await Task.Factory.FromAsync<WebResponse>(http.BeginGetResponse,
                http.EndGetResponse, null))
            {
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();

                
                //http.Abort();
                http = null;
                return content;
            }
        }

        static void wc_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            try
            {
                Uploadresult = e.Result;
            }
            catch (Exception ex)
            {
                // fail!
            }
        }

        private static string Uploadresult = null;

        private static string getPOSTResponse()
        {
            while (Uploadresult == null)
            {
                Thread.Sleep(500);
            }
            return Uploadresult;
        }
    }
}
