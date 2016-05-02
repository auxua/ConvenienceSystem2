using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdminClient
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Just destroy the session/Login status
            Session.Clear();
            Response.Redirect("~/Login.aspx", false);
        }

    }
}