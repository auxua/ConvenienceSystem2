using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class AddProduct : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            Title = StringsLocal.AddProduct;
            // Get Data
            string product = (string)Request.Form["ctl00$MainContent$inputProduct"];
            string priceString = (string)Request.Form["ctl00$MainContent$inputPrice"];
            string comment = (string)Request.Form["ctl00$MainContent$inputComment"];

            if (String.IsNullOrEmpty(product) || String.IsNullOrEmpty(priceString))
            {
                // Nothing inserted - Wait for data
                return;
            }

            // Convert price to Double
            double price = 0;
            try
            {
                priceString = priceString.Replace(",", ".");
                System.Globalization.CultureInfo EnglishCulture = new System.Globalization.CultureInfo("en-EN");
                price = Double.Parse(priceString, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                StateMessage = "Please provide a valid number (e.g. 1.10)";
                return;
            }

            // Try to add new user
            CreateProductRequest request = new CreateProductRequest();
            request.comment = comment;
            request.price = price;
            request.product = product;

            // Send the request
            try
            {
                await Backend.AddProduct(request);
                StateMessage = "Prodcut was Added";
            }
            catch (Exception ex)
            {
                StateMessage = "Could not add Product: " + ex.Message;
            }
            
            
        }

        public string StateMessage { get; set; }
        
    }
}