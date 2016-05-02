using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class AddKeyDate : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            //Title = StringsLocal.AddKeyDate;
            // Get Data
            string comment = (string)Request.Form["ctl00$MainContent$inputComment"];

            if (String.IsNullOrEmpty(comment))
            {
                // Nothing inserted - Wait for data
                return;
            }

            // Try to add new user
            InsertKeyDateRequest request = new InsertKeyDateRequest();
            request.comment = comment;

            // Send the request
            try
            {
                await Backend.AddKeyDate(request);
                StateMessage = "Keydate was Added";
            }
            catch (Exception ex)
            {
                StateMessage = "Could not add User: " + ex.Message;
            }
            
        }

        public string StateMessage { get; set; }
        
    }
}