using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Globalization;

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

        /// <summary>
        /// Helper function
        ///     tries to convert a string to double independently from localization
        /// </summary>
        public static double String2Double(string s)
        {
            

            double d;
            s = s.Replace(",", ".");
            if (Double.TryParse(s, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture,  out d))
            {
                return d;
            }
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
            throw new Exception("Could not convert string to double");
        }
    }

}