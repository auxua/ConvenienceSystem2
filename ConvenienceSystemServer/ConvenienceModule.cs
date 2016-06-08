using Nancy;
using Nancy.Json;
using Nancy.ModelBinding;

using Newtonsoft.Json;

using ConvenienceSystemBackendServer;
using ConvenienceSystemDataModel;
using System;
using System.Text;

namespace ConvenienceSystemServer
{
    public class ConvenienceModule : NancyModule
    {
        private ConvenienceServer backend = new ConvenienceServer();
        
        private class ConsoleOutput : Logger.ILoggerOutput
        {
            void Logger.ILoggerOutput.write(string text)
            {
                Console.WriteLine(text);
            }
        }

        public ConvenienceModule()
        {
            /*
             * 
             * First Version:
             *  Does not Check the rights concerning the device code.
             *  This will be implemented later
             * 
             */

            // Configure Logging for this Application
            Logger.Output = new ConsoleOutput();
            Logger.IsActive = true;
            //Logger.IsActive = false;

            // Enable utf-8 for answers
            After += ctx =>
            {
                ctx.Response.ContentType = "application/json; charset=utf-8";
            };

            
            // Base Index - Hello World or other status indication should be set
            Get["/"] = parameters =>
            {
                Logger.Log("Server", "/ called");
                return "Hello World";
            };

            // Status indicator
            Get["/status", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server", "/status called");
                // Basic Idea: Just call somethin. If this fails, the backend server may be down/corrupted (e.g. MySQL Driver problem likely to happn..)
                try 
                {
                    await backend.GetActiveUsersAsync("Foo");
                    return "True";
                }
                catch (Exception ex)
                {
                    Logger.Log("Server", "Exception caught: " + ex.Message);
                    return "False: "+ex.Message;
                }
            };

            Get["/restart.token={token}"] = parameters =>
            {
                Logger.Log("Server", "/restart called");
                // Idea: restart the backend in case of problems

                backend = null;
                backend = new ConvenienceServer();

                return "Backend restarted";
            };

            // Just testing around
            Post["/"] = parameters =>
            {
                Logger.Log("Server (POST)", "/ called");

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    BaseResponse f = JsonConvert.DeserializeObject<BaseResponse>(b);
                    return f;
                }
                catch
                {
                    return "please provide valid JSON";
                }
                
            };

            /*Get["/viewAllUsers/{token}", runAsync:true] = async (parameters,CancelToken) =>
            {
                return parameters.token;
            };*/

            #region Basic Get/View Calls

            Get["/viewAllUsers.token={token}", runAsync:true] = async (parameters,cancelToken) =>
            {
                /*string test = parameters.token;
                return test;*/

                Logger.Log("Server", "/viewAllUsers called");

                UsersResponse response = new UsersResponse();

                try
                {
                    var users = await backend.GetAllUsersAsync((string)parameters.token);
                    response.dataSet = users;
                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.errorDescription = ex.Message;
                    response.status = false;
                    Logger.Log("Server", "Error occured: "+ex.Message);
                }

                return response;

                
                /*string test = parameters.token;
                return test;*/
            };

            Get["/viewActiveUsers.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server", "/viewActiveUsers called");

                UsersResponse response = new UsersResponse();

                try
                {
                    var users = await backend.GetActiveUsersAsync((string)parameters.token);
                    response.dataSet = users;
                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.errorDescription = ex.Message;
                    response.status = false;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;
            };

            Get["/viewAllProducts.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server", "/viewAllProducts called");

                ProductsResponse response = new ProductsResponse();

                try
                {
                    response.dataSet = await backend.GetFullProductsAsync((string)parameters.token);
                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.errorDescription = ex.Message;
                    response.status = false;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;
            };

            Get["/viewAllDebtSinceKeyDate.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server", "/viewAllDebtSinceKeyDate called");

                var response = new UsersResponse();

                try
                {
                    response.dataSet = await backend.GetDebtSinceKeyDateAsync((string)parameters.token);
                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.errorDescription = ex.Message;
                    response.status = false;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;
            };

            Get["/viewAccountingCountSince.date={date}.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server", "/viewAccountingCountSince called");

                var response = new ProductsCountResponse();

                

                try
                {
                    string c = parameters.date;
                    response.dataSet = await backend.GetAccountingCountSince((string)parameters.token, c);
                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.errorDescription = ex.Message;
                    response.status = false;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;
            };

            Get["/viewLastActivity.count={count}.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server", "/viewLastActivity called");

                var response = new AccountingElementsResponse();

                try
                {
                    string c = parameters.count;
                    int count;
                    if (int.TryParse(c,out count))
                        response.dataSet = await backend.GetLastActivityAsync((string)parameters.token,count);
                    else
                        response.dataSet = await backend.GetLastActivityAsync((string)parameters.token);
                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.errorDescription = ex.Message;
                    response.status = false;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;
            };

            Get["/viewProductsCountForUser.user={user}.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server", "/viewProductsCountForUser called");

                var response = new ProductsCountResponse();

                try
                {
                    string c = parameters.user;
                    response.dataSet = await backend.GetProductsCountForUserAsync(c, (string)parameters.token);
                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.errorDescription = ex.Message;
                    response.status = false;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }
                
                return response;
            };

            Get["/viewKeyDates.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server", "/viewKeyDates called");

                var response = new KeyDatesResponse();

                try
                {
                    response.dataSet = await backend.GetKeyDatesAsync((string)parameters.token);
                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.errorDescription = ex.Message;
                    response.status = false;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;
            };

            

            Get["/viewMails.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server", "/viewMails called");

                var response = new MailsResponse();

                try
                {
                    response.dataSet = await backend.GetMailsAsync((string)parameters.token);
                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.errorDescription = ex.Message;
                    response.status = false;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;
            };


            #endregion

            Post["/verifyWebUser.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (Post)", "/verifyWebUser called");

                var response = new BaseResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    WebLoginRequest request = JsonConvert.DeserializeObject<WebLoginRequest>(b);

                    response.status = await backend.VerifyWebUser(request.username,request.password,(string)parameters.token);
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;
            };

            Post["/emptyMail.token={token}"] = parameters =>
            {
                Logger.Log("Server (Post)", "/emptyMail called");

                var response = new BaseResponse();
                
                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    EmptyMailRequest request = JsonConvert.DeserializeObject<EmptyMailRequest>(b);

                    backend.SendEmptyMail(request.message);

                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;
            };


            Post["/insertKeyDate.token={token}", runAsync:true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (POST)", "/insertKeyDate called");

                BaseResponse response = new BaseResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    InsertKeyDateRequest request = JsonConvert.DeserializeObject<InsertKeyDateRequest>(b);
                    // do stuff
                    await backend.InsertKeyDateAsync((string)parameters.token,comment: request.comment);

                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;

            };


            Post["/addUser.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (POST)", "/addUser called");

                BaseResponse response = new BaseResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    CreateuserRequest request = JsonConvert.DeserializeObject<CreateuserRequest>(b);
                    // do stuff
                    await backend.AddUserAsync((string)parameters.token, request.user, request.comment, request.state);
                    

                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;

            };


            Post["/updateUser.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (POST)", "/updateUser called");

                UpdateResponse response = new UpdateResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    UpdateUsersRequest request = JsonConvert.DeserializeObject<UpdateUsersRequest>(b);
                    // do stuff
                    response.dataset = await backend.UpdateUsersAsync((string)parameters.token, request.dataSet);
                    
                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;

            };

            Post["/deleteUser.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (POST)", "/deleteUser called");

                UpdateResponse response = new UpdateResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    DeleteRequest request = JsonConvert.DeserializeObject<DeleteRequest>(b);
                    // do stuff
                    response.dataset = await backend.DeleteUsersAsync((string)parameters.token, request.dataSet);

                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;

            };


            Post["/updateMails.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (POST)", "/updateMails called");

                UpdateResponse response = new UpdateResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    UpdateMailRequest request = JsonConvert.DeserializeObject<UpdateMailRequest>(b);
                    // do stuff
                    await backend.UpdateMailsAsync((string)parameters.token, request.dataSet);

                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;

            };

            Post["/deleteMails.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (POST)", "/deleteMails called");

                UpdateResponse response = new UpdateResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    DeleteMailRequest request = JsonConvert.DeserializeObject<DeleteMailRequest>(b);
                    // do stuff
                    await backend.DeleteMailsAsync((string)parameters.token, request.dataSet);

                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;

            };

            Post["/addMail.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (POST)", "/addMail called");

                BaseResponse response = new BaseResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    CreateMailRequest request = JsonConvert.DeserializeObject<CreateMailRequest>(b);
                    // do stuff
                    await backend.AddMailAsync((string)parameters.token, request.username, request.adress);


                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;

            };


            Post["/updateProduct.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (POST)", "/updateProduct called");

                UpdateResponse response = new UpdateResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    UpdateProductsRequest request = JsonConvert.DeserializeObject<UpdateProductsRequest>(b);
                    // do stuff
                    response.dataset = await backend.UpdateProductsAsync((string)parameters.token, request.dataSet);

                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;

            };

            Post["/deleteProduct.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (POST)", "/deleteProduct called");

                UpdateResponse response = new UpdateResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    DeleteRequest request = JsonConvert.DeserializeObject<DeleteRequest>(b);
                    // do stuff
                    response.dataset = await backend.DeleteProductsAsync((string)parameters.token, request.dataSet);

                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;

            };

            Post["/addProduct.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (POST)", "/addProduct called");

                BaseResponse response = new BaseResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    CreateProductRequest request = JsonConvert.DeserializeObject<CreateProductRequest>(b);
                    // do stuff
                    await backend.AddProductAsync((string)parameters.token, request.product, request.comment, request.price);
                    
                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;

            };


            Post["/buyProducts.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (POST)", "/buyProducts called");

                BaseResponse response = new BaseResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    BuyProductsRequest request = JsonConvert.DeserializeObject<BuyProductsRequest>(b);
                    // do stuff
                    await backend.BuyAsync((string)parameters.token, request.username, request.products);

                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;

            };

            Post["/buyDirectly.token={token}", runAsync: true] = async (parameters, cancelToken) =>
            {
                Logger.Log("Server (POST)", "/buyDirectly called");

                BaseResponse response = new BaseResponse();

                try
                {

                    byte[] array = new byte[Request.Body.Length];
                    var a = Request.Body.Read(array, 0, array.Length);
                    //return parameters;
                    var b = Encoding.UTF8.GetString(array);

                    BuyDirectlyRequest request = JsonConvert.DeserializeObject<BuyDirectlyRequest>(b);
                    // do stuff
                    await backend.BuyDirectlyAsync((string)parameters.token, request.username, request.comment, request.price);
                    //await backend.BuyAsync((string)parameters.token, request.username, request.products);

                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.errorDescription = ex.Message;
                    Logger.Log("Server", "Error occured: " + ex.Message);
                }

                return response;

            };

        }
    }
}