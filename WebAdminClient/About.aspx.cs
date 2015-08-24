using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdminClient
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = StringsLocal.About;
        }
    }
}