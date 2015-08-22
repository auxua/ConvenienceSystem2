using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using ConvenienceSystemDataModel;
using Newtonsoft.Json;

namespace WebAdminClient
{
    class Backend
    {
        #region generic REST Calls

        public static async Task<string> GetAsync(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
            string result = await client.GetStringAsync(url);
            return result;
        }
        

        public static async Task<string> PostAsync(string url, string data)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");

            StringContent content = new StringContent(data);

            HttpResponseMessage response = await client.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();
            return result;

        }
        
        
        public class APIException : Exception
        {
            public APIException(string msg) : base(msg) { }
        }

        /// <summary>
        /// A generic REST-Call to an endpoint using GET or POST method
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
                    string answer = await GetAsync(endpoint);
                    // Deserialize
                    return JsonConvert.DeserializeObject<T1>(answer);
                }
                else
                {
                    // POST Request
                    string answer = await PostAsync(endpoint, input);
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
            string callURL = Settings.APIBaseUrl + "/viewAllUsers.token=" + Settings.Token;
            var answer = await RestCallAsync<UsersResponse>("", callURL, false);
            return answer;
        }

        public async static Task<UsersResponse> GetActiveUsersAsync()
        {
            string callURL = Settings.APIBaseUrl + "/viewActiveUsers.token=" + Settings.Token;
            //string callURL = Config.APIBaseUrl + "/";
            //string callURL = "http://www.google.com";
            //var answer = await RestCallAsync<UsersResponse>("", callURL, false);
            //var answer = RestCallSync<UsersResponse>("", callURL, false);
            var answer = await RestCallAsync<UsersResponse>("", callURL, false);
            return answer;
        }

        public async static Task<ProductsResponse> GetAllProductsAsync()
        {
            string callURL = Settings.APIBaseUrl + "/viewAllProducts.token=" + Settings.Token;
            var answer = await RestCallAsync<ProductsResponse>("", callURL, false);
            return answer;
        }

        public async static Task<BaseResponse> VerifyWebUserAsync(string user, string pass)
        {
            string callURL = Settings.APIBaseUrl + "/verifyWebUser.token=" + Settings.Token;
            var request = new WebLoginRequest();
            request.password = pass;
            request.username = user;
            var answer = await RestCallAsync<BaseResponse>(request.ToString(), callURL, true);
            return answer;
        }

        public async static Task<UsersResponse> GetAllDebtSinceKeyDateAsync()
        {
            string callURL = Settings.APIBaseUrl + "/viewAllDebtSinceKeyDate.token=" + Settings.Token;
            var answer = await RestCallAsync<UsersResponse>("", callURL, false);
            return answer;
        }

        public async static Task<AccountingElementsResponse> GetLastActivityAsync(int count = 10)
        {
            //if (count == null) count = 10;
            string callURL = Settings.APIBaseUrl + "/viewLastActivity.count=" + count.ToString() + ".token=" + Settings.Token;
            var answer = await RestCallAsync<AccountingElementsResponse>("", callURL, false);
            return answer;
        }


        public async static Task<KeyDatesResponse> GetKeyDatesAsync()
        {
            string callURL = Settings.APIBaseUrl + "/viewKeyDates.token=" + Settings.Token;
            var answer = await RestCallAsync<KeyDatesResponse>("", callURL, false);
            return answer;
        }

        public async static Task<ProductsCountResponse> GetProductsCountAsync(string username)
        {
            if (String.IsNullOrEmpty(username)) return default(ProductsCountResponse);
            string callURL = Settings.APIBaseUrl + "/viewProductsCountForUser.user=" + username + ".token=" + Settings.Token;
            var answer = await RestCallAsync<ProductsCountResponse>("", callURL, false);
            return answer;
        }

        public async static Task<Boolean> BuyProductsCountAsync(BuyProductsRequest request)
        {
            string callURL = Settings.APIBaseUrl + "/buyProducts.token=" + Settings.Token;
            //var answer = await RestCallAsync<BaseResponse>(JsonConvert.SerializeObject(request), callURL, true);
            var answer = await RestCallAsync<BaseResponse>(request.ToString(), callURL, true);
            return answer.status;
        }

        public async static Task AddUser(CreateuserRequest request)
        {
            string callURL = Settings.APIBaseUrl + "/addUser.token=" + Settings.Token;
            //var answer = await RestCallAsync<BaseResponse>(JsonConvert.SerializeObject(request), callURL, true);
            var answer = await RestCallAsync<BaseResponse>(request.ToString(), callURL, true);
        }

        public async static Task AddProduct(CreateProductRequest request)
        {
            string callURL = Settings.APIBaseUrl + "/addProduct.token=" + Settings.Token;
            //var answer = await RestCallAsync<BaseResponse>(JsonConvert.SerializeObject(request), callURL, true);
            var answer = await RestCallAsync<BaseResponse>(request.ToString(), callURL, true);
        }

        public async static Task<UpdateResponse> UpdateUsers(UpdateUsersRequest request)
        {
            string callURL = Settings.APIBaseUrl + "/updateUser.token=" + Settings.Token;
            //var answer = await RestCallAsync<BaseResponse>(JsonConvert.SerializeObject(request), callURL, true);
            var answer = await RestCallAsync<UpdateResponse>(request.ToString(), callURL, true);
            return answer;
        }

        public async static Task<UpdateResponse> DeleteUsers(DeleteRequest request)
        {
            string callURL = Settings.APIBaseUrl + "/deleteUser.token=" + Settings.Token;
            //var answer = await RestCallAsync<BaseResponse>(JsonConvert.SerializeObject(request), callURL, true);
            var answer = await RestCallAsync<UpdateResponse>(request.ToString(), callURL, true);
            return answer;
        }

        public async static Task<Boolean> EmptyMailAsync(string message)
        {
            string callURL = Settings.APIBaseUrl + "/emptyMail.token=" + Settings.Token;
            var request = new EmptyMailRequest();
            request.message = message;
            //var answer = await RestCallAsync<BaseResponse>(JsonConvert.SerializeObject(request), callURL, true);
            var answer = await RestCallAsync<BaseResponse>(request.ToString(), callURL, true);
            return answer.status;
        }

        #endregion
    }
}
