using System;
using System.Collections.Generic;
using System.Text;

using ConvenienceSystemDataModel;
using System.Threading.Tasks;

namespace ConvenienceSystemUWP
{
    class DataManager
    {
        #region Properties

        private static List<User> ActiveUsers = null;
        private static List<User> AllUsers = null;
        private static List<Product> AllProducts = null;

        public static List<User> GetActiveUsers()
        {
            return new List<User>(ActiveUsers);
        }

        public static List<User> GetAllUsers()
        {
            return new List<User>(AllUsers);
        }

        public static List<Product> GetAllProducts()
        {
            return new List<Product>(AllProducts);
        }

        public enum DMState
        {
            INVALID, // No/invalid Data. Need to Update/Get Data
            ACTIVE, // Holding Data, you can query the data
            ERROR, // An Error occured - refer to the Error-String id you want more information
            BUSY, // There is an action going on (getting data, internal stuff, etc.)
        }

        private static DMState state = DMState.INVALID;

        /// <summary>
        /// The actual State of the DataManager
        /// </summary>
        public static DMState State
        {
            get { return state; }
        }

        private static string error = "";

        /// <summary>
        /// If there was an error, find more information in here
        /// </summary>
        public static string Error
        {
            get { return error; }
        }


        private static DateTime lastUpdate;

        /// <summary>
        /// The point in time, when the last update has ended successfully
        /// </summary>
        public static DateTime LastUpdate
        {
            get { return lastUpdate; }
        }

        #endregion

        #region Data Calls
        public async static Task<Boolean> GetAllDataAsync()
        {
            try
            {
                error = "";
                state = DMState.BUSY;
                
                var users = await api.Communication.GetActiveUsersAsync();
                if (!users.status) return false;
                ActiveUsers = users.dataSet;

                users = await api.Communication.GetAllUsersAsync();
                if (!users.status) return false;
                AllUsers = users.dataSet;

                var prod = await api.Communication.GetAllProductsAsync();
                if (!prod.status) return false;
                AllProducts = prod.dataSet;

                // Everything so far without Exceptions - just Check data
                if (ActiveUsers == null || ActiveUsers.Count <1)
                    error += "No Active Users; ";
                if (AllUsers == null || AllUsers.Count < 1)
                    error += "No Users; ";
                if (AllProducts == null || AllProducts.Count < 1)
                    error += "No Products; ";

                // Check whether there was a problem
                if (Error != "")
                {
                    state = DMState.ERROR;
                    return false;
                }


                // Everything worked fine!
                state = DMState.ACTIVE;
                lastUpdate = DateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                state = DMState.ERROR;
                return false;
            }
        }

        /// <summary>
        /// The Cooldown (in minutes) between two Empty-Mails
        /// </summary>
        public readonly static int EmptyMailCooldown = 60;

        /// <summary>
        /// 
        /// </summary>
        public static DateTime LastEmptyMail = DateTime.Now.AddMinutes(-EmptyMailCooldown);

        /// <summary>
        /// Gets the Product Counts for the specified user.
        /// In Case of error "null" is returned. In this case, more information about the error can be found in the Error-Field
        /// </summary>
        /// <param name="name">The name of the user</param>
        public static async Task<List<ProductCount>> GetProductCountForUser(string name)
        {
            try
            {
                var ans = await api.Communication.GetProductsCountAsync(name);
                // Check for errors on data/Call
                if (ans.status == false)
                {
                    error = ans.errorDescription;
                    return null;
                }
                // Everything ok!
                return ans.dataSet;
            }
            catch (Exception ex)
            {
                // Exception occured during API-Call or data access
                error = ex.Message;
                return null;
            }
        }

        #endregion

    }
}
