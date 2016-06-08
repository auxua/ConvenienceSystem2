using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class DirectAccounting : Page
    {
        public string StateMessage { get; set; }

        //public List<ProductProperties> ReasonList;
        public List<ListItem> ReasonListItems;
        public List<ListItem> UserList;

        protected async void Page_Load(object sender, EventArgs e)
        {
            string ReasonSelected = "";
            string UserSelected = "";
            if (IsPostBack)
            {
                ReasonSelected = ReasonSelectServer.Value;
                UserSelected = UserSelect.Value;
            }

            Title = StringsLocal.Accounting;
            inputCustomReason.Attributes.Add("placeholder", StringsLocal.Reason);
            // Get Data needed from backend
            var products = await Backend.GetAllProductsAsync();
            var users = await Backend.GetAllUsersAsync();
            /*ReasonList = new List<ProductProperties>();
            ProductProperties CustomProduct = new ProductProperties();
            CustomProduct.product = "Custom";
            CustomProduct.price = "Use text field";
            ReasonList.Add(CustomProduct);

            

            

            this.repProducts.DataSource = ReasonList;
            this.repProducts.DataBind();*/

            // create List of products
            ReasonListItems = new List<ListItem>();
            ReasonListItems.AddRange(products.dataSet.ConvertAll<ListItem>((x) =>
            {
                ListItem pp = new ListItem();
                pp.Text = x.product + " - " + x.price.ToString();
                pp.Value = x.product;
                return pp;
            }));

            ListItem item = new ListItem();
            item.Text = StringsLocal.CustomUseInputField;
            item.Value = "Custom";
            item.Selected = true;

            ReasonSelectServer.Items.Clear();
            ReasonSelectServer.Items.Add(item);
            ReasonSelectServer.Items.AddRange(ReasonListItems.ToArray());

            // create list of users
            UserList = users.dataSet.ConvertAll<ListItem>((x) =>
            {
                ListItem pp = new ListItem();
                pp.Text = x.username;
                pp.Value = x.username;
                return pp;
            });
            UserSelect.Items.Clear();
            UserSelect.Items.AddRange(UserList.ToArray());

            if (IsPostBack)
            {
                double price;
                string val = ReasonSelected;
                if (val == "Custom")
                {
                    // Convert from input field
                    try
                    {
                        price = Global.String2Double(inputCustomPrice.Value);
                        if (!String.IsNullOrEmpty(inputCustomReason.Value))
                            val = inputCustomReason.Value;
                    }
                    catch
                    {
                        StateMessage = "Error: invalid number";
                        return;
                    }
                }
                else
                {
                    // Take product price
                    price = products.dataSet.First((x) => x.product == val).price;
                }
                // price is set - give data to backend
                var request = new BuyDirectlyRequest();
                request.comment = val;
                request.price = price;
                request.username = UserSelected;
                var succ = await Backend.BuyDirectlyAsync(request);
                if (succ)
                {
                    StateMessage = "Success";
                }
                else
                {
                    StateMessage = "Error: No Data updated";
                }
            }
            
            
        }

        

        public bool ShowAggregated;
    }
}