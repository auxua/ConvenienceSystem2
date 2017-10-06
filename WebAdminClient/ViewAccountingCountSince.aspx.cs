using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class ViewAccountingCountSince : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            Title = StringsLocal.ProductsBoughtSince;

            string mode = Request["mode"];
            if (!String.IsNullOrEmpty(inputDate.Value))
                mode = inputDate.Value;

            

            if (String.IsNullOrEmpty(mode))
            {
                AccountingTable.Visible = false;
                return;
            }

            // mode is set
            string date = "";
            DateTime dt = DateTime.Now;
            if (mode == "week")
            {
                date = dt.AddDays(-7).ToString("yyyy-MM-dd");
            }
            else if (mode=="month")
            {
                date = dt.AddMonths(-1).ToString("yyyy-MM-dd");
            }
            else if (mode == "yesterday")
            {
                date = dt.AddDays(-1).ToString("yyyy-MM-dd");
            }
            else if (mode == "keydate")
            {
                var keys = await Backend.GetKeyDatesAsync();
                var lastKD = keys.dataSet.First(); //TODO: Check SQL-Syntax for Order By clause
                                                   // Workaround - try Converting to DateTime using two common default formats

                if (!DateTime.TryParseExact(lastKD.keydate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out dt))
                {
                    if (!DateTime.TryParseExact(lastKD.keydate, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out dt))
                    {
                        DateTime.TryParseExact(lastKD.keydate, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out dt);
                    }
                }

                /*
                try
                {
                    dt = DateTime.ParseExact(lastKD.keydate, "yyyy-MM-dd HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    try
                    {
                        dt = DateTime.ParseExact(lastKD.keydate, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        dt = DateTime.ParseExact(lastKD.keydate, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    }
                }*/

                
                date = dt.ToString("yyyy-MM-dd");
            }
            else
            {
                // Fallback
                date = mode;
            }

            // Get Data
            var acc = await Backend.GetAccountingCountSinceAsync(date);

            List<AccountingCountProperties> plist = acc.dataSet.ConvertAll<AccountingCountProperties>((x) =>
            {
                AccountingCountProperties ac = new AccountingCountProperties();
                ac.amount = x.amount.ToString();
                ac.product = x.product;
                return ac;
            });

            repAccounting.DataSource = plist;
            repAccounting.DataBind();
            AccountingTable.Visible = true;
            
        }

        public class AccountingCountProperties
        {
            public string product { get; set; }
            public string amount { get; set; }
        }

        public string StateMessage { get; set; }
        
    }
}