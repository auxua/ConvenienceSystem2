using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdminClient
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request != null && !String.IsNullOrEmpty((string)Request["lang"]))
            {
                Session["lang"] = (string)Request["lang"];
                try
                {
                    UICulture = (string)Session["lang"];
                    Culture = (string)Session["lang"];
                }
                catch { }
            }
        }
    }
}