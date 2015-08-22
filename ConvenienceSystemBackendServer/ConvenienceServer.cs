using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConvenienceSystemDataModel;

using MySql.Data;
using MySql.Data.MySqlClient;

using System.Threading.Tasks;
using System.Threading;
using System.Net.Mail;
using System.Net;

namespace ConvenienceSystemBackendServer
{
    public class ConvenienceServer
    {
        /// <summary>
        /// The actual Database Connection instance
        /// </summary>
        private MySqlConnection Connection;

        public ConvenienceServer()
        {
            //do some init stuff if needed
        }

        /// <summary>
        /// Connects to the Database Server
        /// </summary>
        private void Connect()
        {
            Connection = new MySqlConnection("server=" + Settings.Server + ";database=" + Settings.DBName + ";uid=" + Settings.DBUser + ";password=" + Settings.DBPass);
            Connection.Open();

            //do sth.
            //this.Query("SELECT VERSION()");

        }

        /// <summary>
        /// Allows other parts of the server to get the promoted Port of the system
        /// </summary>
        public static int getPort()
        {
            return Settings.Port;
        }

        /// <summary>
        /// Closes the connection if it was open
        /// </summary>
        private void Close()
        {
            if (this.Connection != null)
            {
                Connection.Close();
                this.Connection = null;
            }
        }

        #region Data queries

        /// <summary>
        /// Executes the query in the Database returning and MySQLDataReader for the results.
        /// BEWARE: The Connection remains open and needs to be cloesd when finished reading!
        /// </summary>
        private MySqlDataReader Query(String stm)
        {
            this.Connect();

            MySqlCommand cmd = new MySqlCommand(stm, Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// returns a List representing the (200) active users,  and their data
        /// </summary>
        public async Task<List<User>> GetActiveUsersAsync(string deviceID)
        {
            await CheckDeviceRights(deviceID, DeviceRights.READ);

            MySqlDataReader reader = this.Query("SELECT * FROM gk_user WHERE gk_user.state='active' ORDER BY username ASC LIMIT 0,200");
            List<User> Users = new List<User>();

            while (await reader.ReadAsync())
            {
                User user = new User();
                user.username = reader.GetString("username");
                try
                {
                    user.comment = reader.GetString("comment");
                }
                catch { }
                user.debt = reader.GetDouble("debt");
                user.ID = reader.GetInt32("ID");
                user.state = reader.GetString("state");

                Users.Add(user);
            }

            reader.Close();
            this.Close();
            return Users;
        }


        public async Task<List<User>> GetAllUsersAsync(string deviceID)
        {
            await CheckDeviceRights(deviceID, DeviceRights.READ);
            return await GetAllUsersAsync();
        }


        /// <summary>
        /// Gets all the Users and their information (limited by 200)
        /// </summary>
        /// <returns></returns>
        private async Task<List<User>> GetAllUsersAsync()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_user ORDER BY username ASC LIMIT 0,200");
            List<User> Users = new List<User>();

            while (await reader.ReadAsync())
            {
                User user = new User();
                user.username = reader.GetString("username");
                try 
                {
                    user.comment = reader.GetString("comment");
                }
                catch { }
                user.debt = reader.GetDouble("debt");
                user.ID = reader.GetInt32("ID");
                user.state = reader.GetString("state");
                
                Users.Add(user);
            }

            reader.Close();
            this.Close();
            return Users;
        }
        

        /// <summary>
        /// Creates new User. In case of an error or problem, an exception is thrown
        /// </summary>
        public async Task AddUserAsync(string deviceID, string username, string comment, string state)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);

            string query = "";
            
            if (String.IsNullOrEmpty(comment))
                query = "INSERT INTO  gk_user (username,state) VALUES ('"+username+"','"+state+"')";
            else
                query = "INSERT INTO  gk_user (username,state,comment) VALUES ('" + username + "','" + state + "','"+comment+"')";

            Logger.Log("ConvenienceServer.AddUserUser", "trying to add user: "+username);

            MySqlDataReader reader = this.Query(query);
            string answer = "";
            if (await reader.ReadAsync())
            {
                answer = reader.GetString(0);
                //Console.WriteLine(answer);
            }

            Logger.Log("ConvenienceServer.AddUser", "DB returned " + answer);
            
            reader.Close();
            this.Close();
        }

        /// <summary>
        /// Updates User. In case of an error or problem, an exception is thrown
        /// </summary>
        public async Task<List<int>> UpdateUsersAsync(string deviceID, List<User> users)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);

            string query = "";
            List<int> list = new List<int>();

            // Create a query sequence
            foreach (User user in users)
            {
                query += "UPDATE gk_user SET username='" + user.username + "', "
                    + "state='" + user.state + "', debt='" + user.debt.ToString() + "', comment='" + user.comment + "' "
                    + "WHERE ID=" + user.ID.ToString() + "; ";
                list.Add(user.ID);
            }
            

            Logger.Log("ConvenienceServer.UpdateUser", "trying to update users ");

            MySqlDataReader reader = this.Query(query);
            string answer = "";
            if (await reader.ReadAsync())
            {
                answer = reader.GetString(0);
                //Console.WriteLine(answer);
            }

            Logger.Log("ConvenienceServer.UpdateUser", "DB returned " + answer);

            reader.Close();
            this.Close();
            return list;
        }

        /// <summary>
        /// Deletes User. In case of an error or problem, an exception is thrown
        /// </summary>
        public async Task<List<int>> DeleteUsersAsync(string deviceID, List<int> users)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);

            string query = "";
            List<int> list = new List<int>();

            // Create a query sequence
            foreach (int user in users)
            {
                query += "DELETE FROM gk_user WHERE ID=" + user.ToString() + "; ";
                list.Add(user);
            }


            Logger.Log("ConvenienceServer.Deleteusers", "trying to delete users ");

            MySqlDataReader reader = this.Query(query);
            string answer = "";
            if (await reader.ReadAsync())
            {
                answer = reader.GetString(0);
                //Console.WriteLine(answer);
            }

            Logger.Log("ConvenienceServer.DeleteUsers", "DB returned " + answer);

            reader.Close();
            this.Close();
            return list;
        }


        
        /// <summary>
        /// Updates Product. In case of an error or problem, an exception is thrown
        /// </summary>
        public async Task<List<int>> UpdateProductsAsync(string deviceID, List<Product> Products)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);

            string query = "";
            List<int> list = new List<int>();

            // Create a query sequence
            foreach (Product product in Products)
            {
                query += "UPDATE gk_pricing SET product='" + product.product + "', "
                    + "comment='" + product.comment + "', price='" + product.price.ToString() + "' WHERE ID=" + product.ID.ToString() + "; ";
                list.Add(product.ID);
            }


            Logger.Log("ConvenienceServer.UpdateProduct", "trying to update products ");

            MySqlDataReader reader = this.Query(query);
            string answer = "";
            if (await reader.ReadAsync())
            {
                answer = reader.GetString(0);
                //Console.WriteLine(answer);
            }

            Logger.Log("ConvenienceServer.UpdateProduct", "DB returned " + answer);

            reader.Close();
            this.Close();
            return list;
        }

        /// <summary>
        /// Deletes Products. In case of an error or problem, an exception is thrown
        /// </summary>
        public async Task<List<int>> DeleteProductsAsync(string deviceID, List<int> Products)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);

            string query = "";
            List<int> list = new List<int>();

            // Create a query sequence
            foreach (int product in Products)
            {
                query += "DELETE FROM gk_pricing WHERE ID=" + product.ToString() + "; ";
                list.Add(product);
            }


            Logger.Log("ConvenienceServer.DeleteProducts", "trying to delete Products ");

            MySqlDataReader reader = this.Query(query);
            string answer = "";
            if (await reader.ReadAsync())
            {
                answer = reader.GetString(0);
                //Console.WriteLine(answer);
            }

            Logger.Log("ConvenienceServer.DeleteProducts", "DB returned " + answer);

            reader.Close();
            this.Close();
            return list;
        }

        /// <summary>
        /// Creates new Product. In case of an error or problem, an exception is thrown
        /// </summary>
        public async Task AddProductAsync(string deviceID, string product, string comment, double price)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);
            string query;
            if (String.IsNullOrEmpty(comment))
                query = "INSERT INTO  gk_pricing (product,price) VALUES ('" + product + "','" + price.ToString() + "')";
            else
                query = "INSERT INTO  gk_pricing (product,price,comment) VALUES ('" + product + "','" + price.ToString() + "','" + comment + "')";

            Logger.Log("ConvenienceServer.AddProduct", "trying to add product: " + product);

            MySqlDataReader reader = this.Query(query);
            string answer = "";
            if (await reader.ReadAsync())
            {
                answer = reader.GetString(0);
                //Console.WriteLine(answer);
            }

            Logger.Log("ConvenienceServer.AddProduct", "DB returned " + answer);

            reader.Close();
            this.Close();
        }

        public async Task<List<Product>> GetFullProductsAsync(string deviceID)
        {
            await CheckDeviceRights(deviceID, DeviceRights.READ);
            return await GetFullProductsAsync();
        }

        /// <summary>
        /// returns a List of all information about the products (limit:200)
        /// </summary>
        private async Task<List<Product>> GetFullProductsAsync()
        {
            
            MySqlDataReader reader = this.Query("SELECT * FROM gk_pricing ORDER BY product ASC LIMIT 0,200");

            List<Product> list = new List<Product>();

            while (await reader.ReadAsync())
            {
                Product product = new Product();

                try
                {
                    product.comment = reader.GetString("comment");
                }
                catch { }

                product.ID = reader.GetInt32("ID");
                product.price = reader.GetDouble("price");
                product.product = reader.GetString("product");

                list.Add(product);
            }

            reader.Close();
            this.Close();
            return list;
        }


        

        /// <summary>
        /// Gets the Sum of products bought in the system since the last Keydate
        ///     Uses the User-DataType, but this is only partially populated!
        /// </summary>
        public async Task<List<User>> GetDebtSinceKeyDateAsync(string deviceID)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);

            MySqlDataReader reader = this.Query("SELECT *,SUM(price) FROM gk_accounting WHERE gk_accounting.date>=(SELECT MAX(keydate) FROM gk_keydates) GROUP BY user LIMIT 0,200");

            List<User> list = new List<User>();

            while (await reader.ReadAsync())
            {
                User user = new User();

                user.username = reader.GetString("user");
                user.debt = reader.GetDouble("SUM(price)");

                list.Add(user);
            }
            reader.Close();
            this.Close();
            return list;
        }

        /// <summary>
        /// Gets all registered Devices of the system
        /// </summary>
        public async Task<List<Device>> GetDevicesAsync(string deviceID)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);
            return await this.GetDevicesAsync();
        }

        /// <summary>
        /// Gets the actual Devices registered in the system
        /// </summary>
        private async Task<List<Device>> GetDevicesAsync()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_devices");

            List<Device> list = new List<Device>();

            while (await reader.ReadAsync())
            {
                Device device = new Device();

                device.code = reader.GetString("code");
                device.comment = reader.GetString("comment");
                device.rights = reader.GetString("rights");

                list.Add(device);
            }
            reader.Close();
            this.Close();
            return list;
        }

        /// <summary>
        /// Gets the (count) last activity elements of the system.
        /// For non-positive values of count, get everything
        /// </summary>
        public async Task<List<AccountingElement>> GetLastActivityAsync(string deviceID, int count = 10)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);

            MySqlDataReader reader;

            //Allow getting all activities by havin non-positive count-parameter
            if (count < 1)
            {
                reader = this.Query("SELECT * FROM gk_accounting ORDER BY gk_accounting.date DESC");
            }
            else
            {
                reader = this.Query("SELECT * FROM gk_accounting ORDER BY gk_accounting.date DESC LIMIT 0," + count);
            }


            List<AccountingElement> list = new List<AccountingElement>();

            while (await reader.ReadAsync())
            {
                AccountingElement item = new AccountingElement();

                try 
                {
                    item.comment = reader.GetString("comment");
                }
                catch { }

                try
                {
                    item.device = reader.GetString("device");
                }
                catch { }

                item.date = reader.GetString("date");
                item.ID = reader.GetInt32("ID");
                item.price = reader.GetDouble("price");
                item.user = reader.GetString("user");

                list.Add(item);

            }
            reader.Close();
            this.Close();
            return list;
        }

        /// <summary>
        /// Gets the Sum of products bought in the system since the provided Keydate (form: yyyy-mm-dd)
        /// </summary>
        public async Task<List<User>> GetDebtSinceKeyDateAsync(String keydate, string deviceID)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);

            MySqlDataReader reader = this.Query("SELECT *,SUM(price) FROM gk_accounting WHERE gk_accounting.date>=\"" + keydate + "\" GROUP BY user LIMIT 0,200");

            List<User> list = new List<User>();

            while (await reader.ReadAsync())
            {
                User user = new User();

                user.debt = reader.GetDouble("SUM(price)");
                user.username = reader.GetString("user");

                list.Add(user);

            }
            reader.Close();
            this.Close();
            return list;
        }

        /// <summary>
        /// Returns what product was bought how often by the user.
        /// Beware! uses the "comment" column of the DB - on-product-comments are possible!
        /// </summary>
        /// <param name="user">The user</param>
        public async Task<List<ProductCount>> GetProductsCountForUserAsync(String user, string deviceID)
        {
            await CheckDeviceRights(deviceID, DeviceRights.READ);

            List<ProductCount> list = new List<ProductCount>();

            MySqlDataReader reader = this.Query("SELECT *,COUNT(date) FROM `gk_accounting` WHERE user='"+user+"' GROUP BY `comment` DESC LIMIT 0,200");

            while (await reader.ReadAsync())
            {
                ProductCount pc = new ProductCount();

                pc.amount = reader.GetInt32("COUNT(date)");
                pc.product = reader.GetString("comment");
                pc.price = reader.GetDouble("price");

                list.Add(pc);
            }
            reader.Close();
            this.Close();
            return list;
        }

        /// <summary>
        /// Verifies the user for Web CLients
        /// </summary>
        /// <param name="user">the name of the user</param>
        /// <param name="password">the (hashed) password the user provided</param>
        public async Task<bool> VerifyWebUser(String user, string password, string deviceID)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);
            
            MySqlDataReader reader = this.Query("SELECT * FROM `gk_webusers` WHERE username='" + user + "'");

            if (!reader.HasRows)
            {
                // No such user!
                reader.Close();
                return false;
            }

            await reader.ReadAsync();
            string dbPass = reader.GetString("password");
            // QUick and dirty workaround for now - the escaping of "+" can result in whitespaces
            password = password.Replace(" ", "+");
            bool answer = (dbPass == password);
            
            reader.Close();
            this.Close();
            return answer;
        }

        /// <summary>
        /// inserts a new keydate (form yyyy-MM-dd HH:mm:ss) into the database
        /// </summary>
        /// <param name="keydate">the keydate</param>
        /// <param name="comment">the comment that shuld be added for this keydate</param>
        public async Task InsertKeyDateAsync(string deviceID, String keydate = "", String comment = "Added via Application without comment")
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);

            //No keydate provided? use current datetime!
            if (keydate == "")
            {
                DateTime dt = DateTime.Now;
                //String datum = String.Format ("yyyy'-'MM'-'dd HH':'mm':'ss'", dt);
                keydate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
            }
            String cmd = "INSERT INTO gk_keydates (`keydate`, `comment`) VALUES ('" + keydate + "', '" + comment + "');";

            Logger.Log("ConvenienceServer.InsertKeyDate", "trying to add keydate for" + keydate + " with " + comment);

            MySqlDataReader reader = this.Query(cmd);
            string answer = "";
            if (await reader.ReadAsync())
            {
                answer = reader.GetString(0);
                //Console.WriteLine(answer);
            }

            Logger.Log("ConvenienceServer.InsertKeyDate", "DB returned " + answer);

            this.Close();
        }


        /// <summary>
        /// Returns a List of Tuples representing the Keydates
        /// </summary>
        public async Task<List<KeyDate>> GetKeyDatesAsync(string deviceID)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);

            MySqlDataReader reader = this.Query("SELECT * FROM gk_keydates ORDER BY keydate DESC LIMIT 0,200");

            List<KeyDate> list = new List<KeyDate>();
            

            while (await reader.ReadAsync())
            {
                KeyDate key = new KeyDate();

                key.comment = reader.GetString("comment");
                key.keydate = reader.GetString("keydate");

                list.Add(key);
            }

            reader.Close();
            this.Close();
            return list;
        }

        public async Task<List<Mail>> GetMailsAsync(string deviceID)
        {
            await CheckDeviceRights(deviceID, DeviceRights.FULL);
            return await GetMailsAsync();
        }

        /// <summary>
        /// Returns a List of the users and their mailadresses
        /// </summary>
        private async Task<List<Mail>> GetMailsAsync()
        {

            MySqlDataReader reader = this.Query("SELECT * FROM gk_mail");

            List<Mail> list = new List<Mail>();

            while (await reader.ReadAsync())
            {
                Mail mail = new Mail();

                mail.adress = reader.GetString("adress");
                mail.username = reader.GetString("username");
                try
                {
                    mail.active = Boolean.Parse(reader.GetString("active"));
                }
                catch
                {
                    mail.active = false;
                }

                list.Add(mail);
            }

            reader.Close();
            this.Close();
            return list;
        }

        /// <summary>
        /// perform the buy action for a user
        /// </summary>
        /// <param name="username">the buying user</param>
        /// <param name="products">A List of the products</param>
        public async Task<Boolean> BuyAsync(string deviceID, String username, List<String> products)
        {
            await CheckDeviceRights(deviceID, DeviceRights.READ);

            //Console.WriteLine ("CS, u:" + username + ", p:" + products);
            DateTime dt = DateTime.Now;
            //String datum = String.Format ("yyyy'-'MM'-'dd HH':'mm':'ss'", dt);
            String datum = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);


            Double fullPrice = 0;
            String plist = "";

            //get Products and users
            //TODO: do this stuff inside SQL
            List<Product> prod = await this.GetFullProductsAsync();
            List<User> user = await this.GetAllUsersAsync();

            foreach (String s in products)
            {
                Double p = 0;
                // Search for the product and store the price of it
                Boolean b = prod.Any((x) => { 
                    if (x.product == s)
                    {
                        p = x.price;
                        return true;
                    }
                    return false; 
                });
                //prod.Where((x) => return (x.product ==)
                
                fullPrice = fullPrice + p;
                plist = plist + s + ", ";
                String pString = p.ToString().Replace(',', '.');
                if (!b) break;
                String cmd = "INSERT INTO `gk_accounting` (`ID`, `date`, `user`, `price`, `comment`) VALUES (NULL, '" + datum + "', '" + username + "', '" + pString + "', '" + s + "');";
                //Console.WriteLine ("CMD: " + cmd);
                MySqlDataReader reader = this.Query(cmd);
                if (await reader.ReadAsync())
                {
                    String answer = reader.GetString(0);
                    //Console.WriteLine(answer);
                }
            }

            //Console.WriteLine ("["+datum+"] "+ username + " bought " + plist);
             Logger.Log("ConvenienceServer.Buy", username + " bought " + plist);

            //Update user's debt
            Double nDebt = 0;
            // Search the user and store the actual debt
            user.Any((x) =>
                {
                    if (x.username == username)
                    {
                        nDebt = x.debt;
                        return true;
                    }
                    return false;
                });

            
            nDebt = nDebt + fullPrice;
            String newDebt = nDebt.ToString().Replace(',', '.');
            String cmd2 = "UPDATE `gk_user` SET `debt` = '" + newDebt + "' WHERE `gk_user`.`username` = '" + username + "';";
            //Console.WriteLine (cmd2);
            MySqlDataReader reader2 = this.Query(cmd2);
            if (await reader2.ReadAsync())
            {
                String answer = reader2.GetString(0);
                //Console.WriteLine(answer);
            }
            this.Close();
            
            // Check, whether the users wants to get mails
            var mails = await this.GetMailsAsync();
            string adress = "";
            bool wantMail = mails.Any((x) =>
                {
                    if (x.username == username && x.active)
                    {
                        adress = x.adress;
                        return true;
                    }
                    return false;
                });

            if (wantMail)
            {
                // User wants a mail
                this.BuyMailThread(username, products);
            }
            
            return true;
        }

        #endregion

        #region Mail handling

        /// <summary>
        /// Just a wrapper for executing the sending mail method as thread
        /// </summary>
        private void BuyMailThread(string user, List<String> list)
        {
            Logger.Log("ConvenienceServer.BuyMailThread", "Send (thread) mail to " + user);
            Thread thread = new Thread(async delegate() { await this.BuyMailAsync(user, list); });
            thread.Start();
        }

        public void SendEmptyMail(string message)
        {
            this.EmptyMailThread(message);
        }

        /// <summary>
        /// Wrapper for sending mails about lack of stuff (e.g. Mate or coffee ;) )
        /// </summary>
        private void EmptyMailThread(string message)
        {
            Logger.Log("ConvenienceServer.EmptyMailThread", "Send (thread) mail for Empty Notification");
            string s = "Es wurde eine Meldung über einen Getränke-Notstand eingereicht mit der Nachricht: "+Environment.NewLine+message;
            Thread thread = new Thread(delegate()
            {
                this.SendMail(Settings.Contactmail, s, "Getränke-Notstand");
            });
            thread.Start();
        }

        /// <summary>
        /// Send a Mail to the user for products they bought
        /// Should be used in a seperate thread!
        /// </summary>
        /// <param name="p">username</param>
        /// <param name="list">list of products</param>
        private async Task BuyMailAsync(string p, List<string> list)
        {
            //TODO: extract Strings for mail and make it generic/english at least...
            //get mail for user
            String mail = String.Empty;
            List<Mail> mails = await GetMailsAsync();

            try
            {
                Boolean a = mails.Any((x) =>
                    {
                        if (x.username == p)
                        {
                            mail = x.adress;
                            return true;
                        }
                        return false;
                    });
                if (!a) return;
            }
            catch (Exception e)
            {
                //no mail adress known for this person -> return
                Logger.Log("ConvenienceServer.BuyMail", "BuyMail-Fail: " + e.Message);
                return;
            }


            String msg = "Hallo " + p + ", " + System.Environment.NewLine + System.Environment.NewLine;
            msg += "Du hast gerade Prdukte gekauft: " + System.Environment.NewLine;
            List<Product> products = await GetFullProductsAsync();
            
            foreach (String s in list)
            {
                Double prod;
                products.ForEach((x) =>
                    {
                        if (x.product == s)
                        {
                            prod = x.price;
                            msg += s + " fuer " + x.price.ToString("C") + System.Environment.NewLine;
                        }

                    });
            }
            msg += "Bitte beachte, dass die Daten nur sporadisch aktualisiert werden. Bei Fragen wende dich einfach an: " + Settings.Contactmail + System.Environment.NewLine;
            msg += "Vielen Dank und guten Durst/Appetit, " + System.Environment.NewLine + "Deine Getraenkekasse";


            bool success = this.SendMail(mail, msg);

            if (success)
            {
                Logger.Log("ConvenienceServer.BuyMail", "Mail was sent");
            }
            else
            {
                Logger.Log("ConvenienceServer.BuyMail", "Mail was NOT sent");
            }
        }

        /// <summary>
        /// Send a mail using the Credentials in the Settings.cs
        /// </summary>
        /// <param name="to">the target mailadress</param>
        /// <param name="message">the message body of the mail</param>
        private Boolean SendMail(String to, String message, String subject = "Kauf im Getränkekassen-System")
        {
            //Console.WriteLine("Now, Sending Mail!");
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            MailMessage mail = new MailMessage(Settings.MailFrom, to);
            SmtpClient client = new SmtpClient();
            client.Port = Settings.MailPort;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = Settings.MailSMTPHost;
            //client.Credentials = new System.Net.NetworkCredential(Settings.MailUser, Settings.MailPass);
            client.Credentials = new System.Net.NetworkCredential(Settings.MailUser, Settings.MailPass);
            client.EnableSsl = true;
            mail.Subject = subject;
            mail.Body = message;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            try
            {
                //Console.WriteLine(client.Credentials.ToString());
                client.Send(mail);
            }
            catch (Exception e)
            {
                Logger.Log("ConvenienceServer.SendMail", "Mail-Fail: " + e.Message);
                return false;
            }

            return true;
        }

        #endregion

        internal String GenerateRandomString()
        {
            String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            String result = new string(
                Enumerable.Repeat(chars, 64)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public enum DeviceRights
        {
            READ = 0, // Default: Can read Data, buy products
            FULL = 1, // Full Access - can see keydates, Accounting, change Data, etc.
            NONE = 2, // Blocked - Cannot do anything
        }

        /// <summary>
        /// Checks the DeviceID having the expected rights
        /// 
        /// usage: provide the Device ID and the Rights you want the Device to have for calling the query. An Exception gets thrown, if the reight are not met
        /// </summary>
        /// <param name="deviceID">The string representation of the DeviceID</param>
        /// <param name="rights">The rights that are needed for the calling query</param>
        private async Task CheckDeviceRights(string deviceID, DeviceRights rightsNeeded)
        {
            DeviceRights rights = default(DeviceRights);

            // Get List of Devices from backend
            List<Device> devices = await this.GetDevicesAsync();

            // Get the Device for the ID
            try
            {
                Device device = devices.First<Device>((x) => x.code == deviceID);
                // A valid one was found, 
                rights = (DeviceRights)Enum.Parse(typeof(DeviceRights), device.rights);
            }
            catch (InvalidOperationException)
            {
                // Device ID cannot be found - stay with default
            }

            // now check that!
            if (rightsNeeded == rights || rights == DeviceRights.FULL)
                return;

            // If we get here, this is not allowed!
            throw new Exception("This Device does not have enough rights for this operation");


        }

        ~ConvenienceServer()
        {
            if (Connection != null)
                Connection.Close();
        }
    }

}
