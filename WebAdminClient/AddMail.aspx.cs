using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class AddMail : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            // Get Data
            string username = (string)Request.Form["ctl00$MainContent$inputUser"];
            string adress = (string)Request.Form["ctl00$MainContent$inputAdress"];
            

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(adress))
            {
                // Nothing inserted - Wait for data
                return;
            }

            // Try to add new user
            CreateMailRequest request = new CreateMailRequest();
            request.adress = adress;
            request.username = username;
            
            // Send the request
            try
            {
                await Backend.AddMail(request);
                StateMessage = "Mail was Added";
            }
            catch (Exception ex)
            {
                StateMessage = "Could not add Mail: " + ex.Message;
            }
            
        }

        public string StateMessage { get; set; }
        
    }
}