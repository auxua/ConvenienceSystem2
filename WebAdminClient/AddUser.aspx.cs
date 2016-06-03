using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class AddUser : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            Title = StringsLocal.AddUser;
            // Get Data
            string username = inputUser.Value;

            /*string username = (string)Request.Form["ctl00$MainContent$inputUser"];
            string state = (string)Request.Form["ctl00$MainContent$inputState"];
            string comment = (string)Request.Form["ctl00$MainContent$inputComment"];*/

            string comment = inputComment.Value;


            bool check = this.checkState.Checked;

            if (String.IsNullOrEmpty(username))
            {
                // Nothing inserted - Wait for data
                return;
            }

            // Try to add new user
            CreateuserRequest request = new CreateuserRequest();
            request.comment = comment;
            if (check)
                request.state = "active";
            else
                request.state = "inactive";
            //request.state = state;
            request.user = username;

            // Send the request
            try
            {
                await Backend.AddUser(request);
                StateMessage = "User was Added";
            }
            catch (Exception ex)
            {
                StateMessage = "Could not add User: " + ex.Message;
            }
            
        }

        public string StateMessage { get; set; }
        
    }
}