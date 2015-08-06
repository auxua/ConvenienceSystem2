using System;
using System.Collections.Generic;
using System.Text;

using ConvenienceSystemDataModel;
using System.Threading.Tasks;

namespace ConvenienceSystemApp
{
    class DataManager
    {
        private static List<User> ActiveUsers;
        private static List<User> Allusers;
        private static List<Product> AllProducts;

        public async static Task<Boolean> getAllDataAsync()
        {
            try
            {
                var users = await api.Communication.GetActiveUsersAsync();
                if (!users.status) return false;
                ActiveUsers = users.dataSet;

                users = await api.Communication.GetAllUsersAsync();
                if (!users.status) return false;
                Allusers = users.dataSet;

                var prod = await api.Communication.GetAllProductsAsync();
                if (!prod.status) return false;
                AllProducts = prod.dataSet;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
