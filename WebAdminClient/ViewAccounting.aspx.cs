using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class ViewAccounting : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            string mode = (string)Request["mode"];

            ShowAggregated = (!String.IsNullOrEmpty(mode) && mode == "keydate");

            // Depending on the aggretation decide which table to show and bind the data to it
            if (ShowAggregated)
            {
                AccountingTable.Visible = false;
                AggregatedAccountingTable.Visible = true;

                var Users = await Backend.GetAllDebtSinceKeyDateAsync();
                List<User> list = Users.dataSet;

                // Convert to a properties-based representation for the databinding
                List<UserDebtProperties> plist = list.ConvertAll<UserDebtProperties>((x) =>
                {
                    UserDebtProperties u = new UserDebtProperties();
                    u.debt = x.debt;
                    u.username = x.username;
                    u.id = x.ID;
                    return u;
                });

                repUsers.DataSource = plist;
                repUsers.DataBind();

            }
            else
            {
                AccountingTable.Visible = true;
                AggregatedAccountingTable.Visible = false;

                var Accounting = await Backend.GetLastActivityAsync();
                List<AccountingElement> list = Accounting.dataSet;

                // Convert to a properties-based representation for the databinding
                List<AccountingProperties> plist = list.ConvertAll<AccountingProperties>((x) =>
                {
                    AccountingProperties u = new AccountingProperties();
                    u.comment = x.comment;
                    u.date = x.date;
                    u.device = x.device;
                    u.price = x.price;
                    u.user = x.user;
                    u.id = x.ID;
                    return u;
                });

                repAccounting.DataSource = plist;
                repAccounting.DataBind();
                
            }

            

            /*var Users = await Backend.GetAllProductsAsync();
            List<Product> list = Users.dataSet;

            // Convert the Users to a properties-based representation for the databinding
            List<ProductProperties> plist = list.ConvertAll<ProductProperties>((x) =>
                {
                    ProductProperties u = new ProductProperties();
                    u.comment = x.comment;
                    u.price = x.price;
                    u.product = x.product;
                    u.id = x.ID;
                    return u;
                });

            repUsers.DataSource = plist;
            repUsers.DataBind();*/
        }

        public class UserDebtProperties
        {
            public int id { get; set; }
            public string username { get; set; }
            public double debt { get; set; }
        }

        public class AccountingProperties
        {
            public int id { get; set; }
            public string date { get; set; }
            public string user { get; set; }
            public double price { get; set; }
            public string comment { get; set; }
            public string device { get; set; }
        }

        public bool ShowAggregated;
    }
}