using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class EditAllUsers : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.IsPostBack)
                {
                    // First, handle updates!
                    var dirty = CheckDirtyElements();
                    UpdateUsersRequest request = new UpdateUsersRequest();
                    request.dataSet = dirty;
                    try
                    {
                        if (request.dataSet.Count < 1)
                        {
                            throw new Exception("No Data updated");
                        }
                        var answer = await Backend.UpdateUsers(request);
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
                        var answer = await Backend.DeleteUsers(delRequest);
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

            UsersResponse Users;

            try
            {
                Users = await Backend.GetAllUsersAsync();
            }
            catch
            {
                StateMessage = "Error while getting the data. Pleasy try again";
                return;
            }
            List<User> list = Users.dataSet;

            // Convert the Users to a properties-based representation for the databinding
            List<UserProperties> plist = list.ConvertAll<UserProperties>((x) =>
                {
                    UserProperties u = new UserProperties();
                    u.comment = x.comment;
                    u.status = x.state;
                    u.username = x.username;
                    u.debt = x.debt.ToString();
                    u.id = x.ID;
                    return u;
                });
            

            EntryCount = plist.Count.ToString("D2");
            entryCount.Attributes.Add("value", EntryCount);

            

            repUsers.DataSource = plist;
            repUsers.DataBind();
        }

        /// <summary>
        /// Basing on the submitted Form, check which elements are dirty (changes)
        /// </summary>
        public List<User> CheckDirtyElements()
        {
            // Get number of Elements
            int amount = int.Parse((string)Request.Form["ctl00$MainContent$entryCount"]);

            List<User> list = new List<User>();

            // Check one by one...
            for (int i=0;i<amount;i++)
            {
                if ((string)Request.Form["ctl00$MainContent$repUsers$ctl"+i.ToString("D2")+"$textName"] != (string)Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$textNameOld"]
                    || (string)Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$textState"] != (string)Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$textStateOld"]
                    || (string)Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$textComment"] != (string)Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$textCommentOld"]
                    || (string)Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$textDebt"] != (string)Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$textDebtOld"])
                {
                    // Found a dirty item, Create a User for this and store data
                    int id = (int.Parse((string)Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$textID"]));
                    User user = new User();
                    user.comment = (string)Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$textComment"];
                    try
                    {
                        string debtString = (string)Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$textDebt"];
                        debtString = debtString.Replace(",", ".");
                        System.Globalization.CultureInfo EnglishCulture = new System.Globalization.CultureInfo("en-EN");
                        user.debt = Double.Parse(debtString, System.Globalization.NumberStyles.Float, EnglishCulture);
                    }
                    catch
                    {
                        // not valid double value
                        StateMessage = "Please provide valid values for debt! (Aborting)";
                        return new List<User>();
                    }
                    user.ID = id;
                    user.state = (string)Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$textState"];
                    user.username = (string)Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$textName"];
                    if (user.username == null) user.username = String.Empty;

                    list.Add(user);
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
                    id = int.Parse(Request.Form["ctl00$MainContent$repUsers$ctl" + i.ToString("D2") + "$checkDelete"]);
                }
                catch { continue; }

                // Found the id - add it!
                list.Add(id);
                
            }

            return list;
        }

        public class UserProperties
        {
            public int id { get; set; }
            public string username { get; set; }
            public string debt { get; set; }
            public string status { get; set; }
            public string comment { get; set; }
        }

        public string StateMessage { get; set; }

        public string EntryCount { get; set; }
    }
}