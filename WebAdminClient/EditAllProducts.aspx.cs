using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class EditAllProducts : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            Title = StringsLocal.EditAllProducts;
            try
            {
                if (this.IsPostBack)
                {
                    // First, handle updates!
                    var dirty = CheckDirtyElements();
                    UpdateProductsRequest request = new UpdateProductsRequest();
                    request.dataSet = dirty;
                    try
                    {
                        if (request.dataSet.Count < 1)
                        {
                            throw new Exception("No Data updated");
                        }
                        var answer = await Backend.UpdateProducts(request);
                        if (answer.status)
                            StateMessage = "Data was updated";
                        else
                            throw new Exception(answer.errorDescription);
                    }
                    catch (Exception ex)
                    {
                        StateMessage = "Error while updating data: " + ex.Message;
                    }


                    // Second, handle deletions
                    var deletions = CheckDeleteElements();
                    if (!(deletions.Count < 1))
                    {
                        // There are Deletions
                        DeleteRequest delRequest = new DeleteRequest();
                        delRequest.dataSet = deletions;
                        StateMessage += " - ";
                        var answer = await Backend.DeleteProducts(delRequest);
                        if (answer.status)
                            StateMessage += "Data was deleted";
                        else
                            StateMessage += "Error while deleting items: " + answer.errorDescription;
                    }
                }
            }
            catch
            {
                StateMessage = "Internal Error - maybe the Database did not answer";
            }


            ProductsResponse Products;

            try
            {
                Products = await Backend.GetAllProductsAsync();
            }
            catch
            {
                StateMessage = "An Error occured while loading the data. Please try again";
                return;
            }

            List<Product> list = Products.dataSet;

            // Convert the Users to a properties-based representation for the databinding
            List<ProductProperties> plist = list.ConvertAll<ProductProperties>((x) =>
                {
                    ProductProperties u = new ProductProperties();
                    u.comment = x.comment;
                    u.id = x.ID;
                    u.price = x.price;
                    u.product = x.product;
                    return u;
                });
            

            EntryCount = plist.Count.ToString("D2");
            entryCount.Attributes.Add("value", EntryCount);
            
            repProd.DataSource = plist;
            repProd.DataBind();
        }

        /// <summary>
        /// Basing on the submitted Form, check which elements are dirty (changes)
        /// </summary>
        public List<Product> CheckDirtyElements()
        {
            // Get number of Elements
            int amount = int.Parse((string)Request.Form["ctl00$MainContent$entryCount"]);

            List<Product> list = new List<Product>();

            // Check one by one...
            for (int i=0;i<amount;i++)
            {
                if ((string)Request.Form["ctl00$MainContent$repProd$ctl"+i.ToString("D2")+"$textName"] != (string)Request.Form["ctl00$MainContent$repProd$ctl" + i.ToString("D2") + "$textNameOld"]
                    || (string)Request.Form["ctl00$MainContent$repProd$ctl" + i.ToString("D2") + "$textComment"] != (string)Request.Form["ctl00$MainContent$repProd$ctl" + i.ToString("D2") + "$textCommentOld"]
                    || (string)Request.Form["ctl00$MainContent$repProd$ctl" + i.ToString("D2") + "$textPrice"] != (string)Request.Form["ctl00$MainContent$repProd$ctl" + i.ToString("D2") + "$textPriceOld"])
                {
                    //Response.Write("Test");
                    // Found a dirty item, Create a User for this and store data
                    int id = (int.Parse((string)Request.Form["ctl00$MainContent$repProd$ctl" + i.ToString("D2") + "$textID"]));
                    Product product = new Product();
                    product.comment = (string)Request.Form["ctl00$MainContent$repProd$ctl" + i.ToString("D2") + "$textComment"];
                    try
                    {
                        
                        string debtString = (string)Request.Form["ctl00$MainContent$repProd$ctl" + i.ToString("D2") + "$textPrice"];
                        debtString = debtString.Replace(",", ".");
                        product.price = Double.Parse(debtString, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
                        //Response.Write(product.price);
                        /*System.Globalization.CultureInfo EnglishCulture = new System.Globalization.CultureInfo("en-EN");
                        product.price = Double.Parse(debtString, System.Globalization.NumberStyles.Float, EnglishCulture);*/
                    }
                    catch
                    {
                        // not valid double value
                        StateMessage = "Please provide valid values for price! (Aborting)";
                        return new List<Product>();
                    }
                    product.ID = id;
                    
                    product.product = (string)Request.Form["ctl00$MainContent$repProd$ctl" + i.ToString("D2") + "$textName"];
                    if (product.comment == null) product.comment = String.Empty;

                    list.Add(product);
                }

                // Todo: Deletions
            }

            return list;
        }

        /// <summary>
        /// Basing on the submitted Form, check which elements are to be deleted
        /// </summary>
        public List<int> CheckDeleteElements()
        {
            // Get number of Elements
            int amount = int.Parse((string)Request.Form["ctl00$MainContent$entryCount"]);

            List<int> list = new List<int>();

            // Check one by one...
            for (int i = 0; i < amount; i++)
            {
                int id;
                try
                {
                    // Try parsng the element. If it is not existing, just go on
                    id = int.Parse(Request.Form["ctl00$MainContent$repProd$ctl" + i.ToString("D2") + "$checkDelete"]);
                }
                catch { continue; }

                // Found the id - add it!
                list.Add(id);
                
            }

            return list;
        }

        public class ProductProperties
        {
            public int id { get; set; }
            public string product { get; set; }
            public double price { get; set; }
            public string comment { get; set; }
        }

        public string StateMessage { get; set; }

        public string EntryCount { get; set; }
    }
}