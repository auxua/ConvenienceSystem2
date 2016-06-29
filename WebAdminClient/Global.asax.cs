using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Globalization;
using System.Web.UI;

namespace WebAdminClient
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code, der beim Anwendungsstart ausgeführt wird
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_Error(object sender,EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is HttpException && ex.InnerException is ViewStateException)
            {
                Response.Redirect(Request.Url.AbsoluteUri);
                return;
           }
        }

        /// <summary>
        /// Helper function
        ///     tries to convert a string to double independently from localization
        /// </summary>
        public static double String2Double(string s)
        {
            

            double d;
            s = s.Replace(",", ".");

            var fmt = new NumberFormatInfo();
            fmt.NegativeSign = "-";
            fmt.NumberDecimalSeparator = ".";

            try
            {
                d = Double.Parse(s, fmt);
                return d;
            }
            catch
            {
                throw new Exception("Could not convert string to double");
            }
            
            /*if (Double.TryParse(s, fmt, CultureInfo.InvariantCulture,  out d))
            {
                return d;
            }*/
            /*
            // did not work, try replacement , -> .
            s = s.Replace(",", ".");
            
            
            if (Double.TryParse(s, NumberStyles.AllowDecimalPoint, out d))
            {
                return d;
            }
            // did not work, try replacement . -> ,
            s = s.Replace(".", ",");
            if (Double.TryParse(s, NumberStyles.AllowDecimalPoint, out d))
            {
                return d;
            }*/
            // nothing did work
            
        }
    }

}