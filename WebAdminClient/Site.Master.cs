using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Threading;
using System.Globalization;
using System.Web.UI.WebControls;

namespace WebAdminClient
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Localization
            if (Session != null && !String.IsNullOrEmpty((string)Session["lang"]))
            {
                try
                {
                    Page.UICulture = (string)Session["lang"];
                    Page.Culture = (string)Session["lang"];
                }
                catch { }
            }
#if !DEBUG
            // When Debugging, to not force Login
            if (Session == null || String.IsNullOrEmpty((string)Session["Logged In"]) || (string)Session["Logged In"] != bool.TrueString)
            {
                // Not logged In!
                Response.Redirect("~/Login.aspx",false);
            }
#endif
        }
    }
}