using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class ViewKeyDates : Page
    {
        public string StateMessage { get; set; }

        protected async void Page_Load(object sender, EventArgs e)
        {
            Title = StringsLocal.ViewKeydates;
            KeyDatesResponse dates;

            try
            {
                dates = await Backend.GetKeyDatesAsync();
            }
            catch
            {
                StateMessage = "Error while loading data. Please try again";
                return;
            }

            List<KeyDate> list = dates.dataSet;

            // Convert the Users to a properties-based representation for the databinding
            List<DatesProperties> plist = list.ConvertAll<DatesProperties>((x) =>
                {
                    DatesProperties u = new DatesProperties();
                    u.keydate = x.keydate;
                    u.comment = x.comment;
                    return u;
                });

            repDates.DataSource = plist;
            repDates.DataBind();
        }

        public class DatesProperties
        {
            public string keydate { get; set; }
            public string comment { get; set; }
        }
    }
}