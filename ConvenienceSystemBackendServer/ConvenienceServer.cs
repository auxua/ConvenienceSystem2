using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConvenienceSystemDataModel;

using MySql.Data;
using MySql.Data.MySqlClient;

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
        public List<User> GetActiveUsers()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_user WHERE gk_user.state='active' ORDER BY username ASC LIMIT 0,200");
            List<User> Users = new List<User>();

            while (reader.Read())
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
        /// Gets all the Users and their information (limited by 200)
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_user ORDER BY username ASC LIMIT 0,200");
            List<User> Users = new List<User>();

            while (reader.Read())
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
        /// returns a List of all information about the products (limit:200)
        /// </summary>
        public List<Product> GetFullProducts()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_pricing ORDER BY product ASC LIMIT 0,200");

            List<Product> list = new List<Product>();

            while (reader.Read())
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
        public List<User> GetDebtSinceKeyDate()
        {
            MySqlDataReader reader = this.Query("SELECT *,SUM(price) FROM gk_accounting WHERE gk_accounting.date>=(SELECT MAX(keydate) FROM gk_keydates) GROUP BY user LIMIT 0,200");

            List<User> list = new List<User>();

            while (reader.Read())
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
        /// Gets the (count) last activity elements of the system.
        /// For non-positive values of count, get everything
        /// </summary>
        public List<AccountingElement> GetLastActivity(int count = 10)
        {
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

            while (reader.Read())
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
        public List<User> GetDebtSinceKeyDate(String keydate)
        {
            MySqlDataReader reader = this.Query("SELECT *,SUM(price) FROM gk_accounting WHERE gk_accounting.date>=\"" + keydate + "\" GROUP BY user LIMIT 0,200");

            List<User> list = new List<User>();

            while (reader.Read())
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
        public List<ProductCount> GetProductsCountForUser(String user)
        {
            List<ProductCount> list = new List<ProductCount>();

            MySqlDataReader reader = this.Query("SELECT *,COUNT(date) FROM `gk_accounting` WHERE user='"+user+"' GROUP BY `comment` DESC LIMIT 0,200");

            while (reader.Read())
            {
                ProductCount pc = new ProductCount();

                pc.amount = reader.GetInt32("COUNT(date)");
                pc.product = reader.GetString("comment");

                list.Add(pc);
            }
            reader.Close();
            this.Close();
            return list;
        }

        /// <summary>
        /// inserts a new keydate (form yyyy-MM-dd HH:mm:ss) into the database
        /// </summary>
        /// <param name="keydate">the keydate</param>
        /// <param name="comment">the comment that shuld be added for this keydate</param>
        public void InsertKeyDate(String keydate = "", String comment = "Added via Application without comment")
        {
            //No keydate provided? use current datetime!
            if (keydate == "")
            {
                DateTime dt = DateTime.Now;
                //String datum = String.Format ("yyyy'-'MM'-'dd HH':'mm':'ss'", dt);
                keydate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
            }
            String cmd = "INSERT INTO gk_keydates (`keydate`, `comment`) VALUES ('" + keydate + "', '" + comment + "');";

            MySqlDataReader reader = this.Query(cmd);
            if (reader.Read())
            {
                String answer = reader.GetString(0);
                //Console.WriteLine(answer);
            }

            this.Close();
        }


        /// <summary>
        /// Returns a List of Tuples representing the Keydates
        /// </summary>
        public List<KeyDate> GetKeyDates()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_keydates ORDER BY keydate DESC LIMIT 0,200");

            List<KeyDate> list = new List<KeyDate>();
            

            while (reader.Read())
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

        /// <summary>
        /// Returns a List of the users and their mailadresses
        /// </summary>
        public List<Mail> GetMails()
        {

            MySqlDataReader reader = this.Query("SELECT * FROM gk_mail");

            List<Mail> list = new List<Mail>();

            while (reader.Read())
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
        public Boolean Buy(String username, List<String> products)
        {
            //Console.WriteLine ("CS, u:" + username + ", p:" + products);
            DateTime dt = DateTime.Now;
            //String datum = String.Format ("yyyy'-'MM'-'dd HH':'mm':'ss'", dt);
            String datum = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);


            Double fullPrice = 0;
            String plist = "";

            //get Products and users
            //TODO: do this stuff inside SQL
            List<Product> prod = this.GetFullProducts();
            List<User> user = this.GetAllUsers();

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
                if (reader.Read())
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
            if (reader2.Read())
            {
                String answer = reader2.GetString(0);
                //Console.WriteLine(answer);
            }
            this.Close();
            return true;
        }

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



        ~ConvenienceServer()
        {
            if (Connection != null)
                Connection.Close();
        }
    }

}
