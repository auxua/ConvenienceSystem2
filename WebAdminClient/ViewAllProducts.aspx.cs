using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class ViewAllProducts : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            var Users = await Backend.GetAllProductsAsync();
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
            repUsers.DataBind();
        }

        public class ProductProperties
        {
            public int id { get; set; }
            public string product { get; set; }
            public double price { get; set; }
            public string comment { get; set; }
        }
    }
}