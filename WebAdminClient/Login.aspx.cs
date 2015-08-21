using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class Login : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            string username = (string)Request.Form["inputUser"];
            string password = (string)Request.Form["inputPassword"];
            string remember = (string)Request.Form["remember"];
            

            if (username != null && password != null)
            {
                // Provided Data, check it
                var Crypto = System.Security.Cryptography.SHA512.Create();

                byte[] passwordBytes = GetBytes(password);
                var passwordHash = Crypto.ComputeHash(passwordBytes);
                string passwordHash64 = Convert.ToBase64String(passwordHash);

                var answer = await Backend.VerifyWebUserAsync(username, passwordHash64);

                //BaseResponse answer = new BaseResponse();
                //answer.status = true;
                
                if (!(answer.status))
                {
                    // Not successful
                    LoginMessage = "Please provide (valid) Credentials";
                    return;
                }
                

                Session["Logged In"] = bool.TrueString;
                // Set the Timeout - for now, use 60min as default, 7 days for extended Session support
                if (String.IsNullOrEmpty(remember) || remember != "remember-me")
                    Session.Timeout = 60;
                else
                    Session.Timeout = 10080;
                //LoginMessage = (string)Session["Logged In"];
                Response.Redirect("Default.aspx",false);
                //Response.Redirect("Default.aspx");
            }
        }

        public string LoginMessage { get; set; }

        static byte[] GetBytes(string str)
        {
            /*byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;*/
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        static string GetString(byte[] bytes)
        {
            /*char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);*/
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}