using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;
using System.Threading;
using Xamarin.Forms;

using ConvenienceSystemDataModel;


namespace ConvenienceSystemApp.api
{
    class Communication
    {
        #region API-Calls

        /*
         * 
         * Backend-API-Calls.
         *  They all work the same way. Just call them and they try to get information from backend
         *  In case of error, default(.) is returned.
         * 
         * 
         */


        /// <summary>
        /// Gets all Users from the Backend-API.
        /// In case of an error, default(UserResponse) is returned.
        /// </summary>
        public async static Task<UsersResponse> GetAllUsersAsync()
        {
            string callURL = Config.APIBaseUrl + "/viewAllUsers.token=" + Config.Token;
            var answer = await RestCallAsync<UsersResponse>("", callURL, false);
            return answer;
        }

        public async static Task<UsersResponse> GetActiveUsersAsync()
        {
            string callURL = Config.APIBaseUrl + "/viewActiveUsers.token=" + Config.Token;
            //string callURL = Config.APIBaseUrl + "/";
            //string callURL = "http://www.google.com";
            //var answer = await RestCallAsync<UsersResponse>("", callURL, false);
            //var answer = RestCallSync<UsersResponse>("", callURL, false);
            var answer = await RestCallAsync<UsersResponse>("", callURL, false);
            return answer;
        }

        public async static Task<ProductsResponse> GetAllProductsAsync()
        {
            string callURL = Config.APIBaseUrl + "/viewAllProducts.token=" + Config.Token;
            var answer = await RestCallAsync<ProductsResponse>("", callURL, false);
            return answer;
        }

        public async static Task<UsersResponse> GetAllDebtSinceKeyDateAsync()
        {
            string callURL = Config.APIBaseUrl + "/viewAllDebtSinceKeyDate.token=" + Config.Token;
            var answer = await RestCallAsync<UsersResponse>("", callURL, false);
            return answer;
        }

        public async static Task<AccountingElementsResponse> GetLastActivityAsync(int count)
        {
            //if (count == null) count = 10;
            string callURL = Config.APIBaseUrl + "/viewLastActivity.count=" + count.ToString() + ".token=" + Config.Token;
            var answer = await RestCallAsync<AccountingElementsResponse>("", callURL, false);
            return answer;
        }


        public async static Task<KeyDatesResponse> GetKeyDatesAsync()
        {
            string callURL = Config.APIBaseUrl + "/viewKeyDates.token=" + Config.Token;
            var answer = await RestCallAsync<KeyDatesResponse>("", callURL, false);
            return answer;
        }

        public async static Task<ProductsCountResponse> GetProductsCountAsync(string username)
        {
            if (String.IsNullOrEmpty(username)) return default(ProductsCountResponse);
            string callURL = Config.APIBaseUrl + "/viewProductsCountForUser.user="+username+".token=" + Config.Token;
            var answer = await RestCallAsync<ProductsCountResponse>("", callURL, false);
            return answer;
        }

        public async static Task<Boolean> BuyProductsCountAsync(BuyProductsRequest request)
        {
            string callURL = Config.APIBaseUrl + "/buyProducts.token=" + Config.Token;
            var answer = await RestCallAsync<BaseResponse>(JsonConvert.SerializeObject(request), callURL, true);
            return answer.status;
        }

        #endregion

        #region generic calls

        public interface IDownloader
        {
            string Get(string url);

            Task<string> GetAsync(string url);

            string Post(string url, string data);

            Task<string> PostAsync(string url, string data);
        }

        public class APIException : Exception 
        {
            public APIException(string msg) : base(msg) { }
        }

        /// <summary>
        /// A generic REST-Call to an endpoint using GET or POST method
        /// 
        /// Uses a WebRequest for POST, a httpClient for GET calls
        /// 
        /// Throws a APIException if it fails
        /// </summary>
        /// <typeparam name="T1">The Datatype to await for response</typeparam>
        /// <param name="input">the data as string (ignored, if using GET)</param>
        /// <param name="endpoint">The REST-Endpoint to call</param>
        /// <param name="post">A flag indicating whether to use POST or GET</param>
        /// <returns>The datatype that has been awaited for the call</returns>
        public async static Task<T1> RestCallAsync<T1>(string input, string endpoint, bool post)
        {
            try
            {
                if (!post)
                {
                    // GET-Request
                    string answer = await DependencyService.Get<IDownloader>().GetAsync(endpoint);
                    // Deserialize
                    return JsonConvert.DeserializeObject<T1>(answer);
                }
                else
                {
                    // POST Request
                    string answer = await DependencyService.Get<IDownloader>().PostAsync(endpoint, input);
                    // Deserialize
                    return JsonConvert.DeserializeObject<T1>(answer);
                }
            }
            catch (Exception ex)
            {
                // trivial error handling...
                throw new APIException(ex.Message);
                //return default(T1);
            }
        }

        public static T1 RestCallSync<T1>(string input, string endpoint, bool post)
        {
            try
            {
                if (!post)
                {
                    // GET-Request
                    string answer = DependencyService.Get<IDownloader>().Get(endpoint);
                    // Deserialize
                    return JsonConvert.DeserializeObject<T1>(answer);
                }
                else
                {
                    // POST Request
                    string answer = DependencyService.Get<IDownloader>().Post(endpoint, input);
                    // Deserialize
                    return JsonConvert.DeserializeObject<T1>(answer);
                }
            }
            catch (Exception ex)
            {
                // trivial error handling...
                throw new APIException(ex.Message);
                //return default(T1);
            }
        }

        #endregion
    }

}
