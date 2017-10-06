using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using ConvenienceSystemUWP.api;
using ConvenienceSystemUWP.UWP;
using System.Threading;
using System.IO;


[assembly: Xamarin.Forms.Dependency(typeof(Downloader_UWP))]
namespace ConvenienceSystemUWP.UWP
{
    public class Downloader_UWP : Communication.IDownloader
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
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");

            StringContent content = new StringContent(data);

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            return result;


        }

        public async Task<string> PostAsync(string url, string data)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");

            StringContent content = new StringContent(data);

            HttpResponseMessage response = await client.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();
            return result;

        }
    }
}
