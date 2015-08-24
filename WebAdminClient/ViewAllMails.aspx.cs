using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class ViewAllMails : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            Title = StringsLocal.ViewMails;
            MailsResponse Mails;

            try
            {
                Mails = await Backend.GetAllMailsAsync();
            }
            catch
            {
                StateMessage = "Error while loading data. Please try again";
                return;
            }

            List<Mail> list = Mails.dataSet;

            // Convert the Users to a properties-based representation for the databinding
            List<MailProperties> plist = list.ConvertAll<MailProperties>((x) =>
                {
                    MailProperties u = new MailProperties();
                    u.active = x.active.ToString();
                    u.adress = x.adress;
                    u.username = x.username;
                    return u;
                });

            repUsers.DataSource = plist;
            repUsers.DataBind();
        }

        public class MailProperties
        {
            //public int id { get; set; }
            public string username { get; set; }
            public string adress { get; set; }
            public string active { get; set; }
        }

        public string StateMessage { get; set; }
    }
}