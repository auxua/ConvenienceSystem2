using Nancy;
using ConvenienceSystemBackendServer;
using ConvenienceSystemDataModel;
using System;

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

            
            // Base Index - Hello World or other status indication should be set
            Get["/"] = parameters =>
            {
                Logger.Log("Server", "/ called");
                return "Hello World";
            };

            /*Get["/viewAllUsers/{token}", runAsync:true] = async (parameters,CancelToken) =>
            {
                return parameters.token;
            };*/

            Get["/viewAllUsers.token={token}", runAsync:true] = async (parameters,cancelToken) =>
            {
                /*string test = parameters.token;
                return test;*/

                Logger.Log("Server", "/viewAllUsers called");

                UsersResponse response = new UsersResponse();

                try
                {
                    var users = await backend.GetAllUsersAsync();
                    response.dataSet = users;
                    response.status = true;
                }
                catch (Exception ex)
                {
                    response.errorDescription = ex.Message;
                    response.status = false;
                }

                return response;

                
                /*string test = parameters.token;
                return test;*/
            };

            // Enable utf-8 for answers
            After += ctx =>
            {
                ctx.Response.ContentType = "application/json; charset=utf-8";
            };
        }
    }
}