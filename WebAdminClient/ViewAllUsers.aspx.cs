using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class ViewAllUsers : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            var Users = await Backend.GetAllUsersAsync();
            List<User> list = Users.dataSet;

            // Convert the Users to a properties-based representation for the databinding
            List<UserProperties> plist = list.ConvertAll<UserProperties>((x) =>
                {
                    UserProperties u = new UserProperties();
                    u.comment = x.comment;
                    u.status = x.state;
                    u.username = x.username;
                    u.id = x.ID;
                    return u;
                });

            repUsers.DataSource = plist;
            repUsers.DataBind();
        }

        public class UserProperties
        {
            public int id { get; set; }
            public string username { get; set; }
            public string status { get; set; }
            public string comment { get; set; }
        }
    }
}