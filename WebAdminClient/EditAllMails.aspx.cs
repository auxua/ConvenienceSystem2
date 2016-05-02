using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ConvenienceSystemDataModel;

namespace WebAdminClient
{
    public partial class EditAllMails : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            Title = StringsLocal.EditMails;
            try
            {
                if (this.IsPostBack)
                {
                    // First, handle updates!
                    var dirty = CheckDirtyElements();
                    UpdateMailRequest request = new UpdateMailRequest();
                    request.dataSet = dirty;
                    try
                    {
                        if (request.dataSet.Count < 1)
                        {
                            throw new Exception("No Data updated");
                        }
                        var answer = await Backend.UpdateMails(request);
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
                        DeleteMailRequest delRequest = new DeleteMailRequest();
                        delRequest.dataSet = deletions;
                        StateMessage += " - ";
                        var answer = await Backend.DeleteMails(delRequest);
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

            MailsResponse Mails;

            try
            {
                Mails = await Backend.GetAllMailsAsync();
            }
            catch
            {
                StateMessage = "Error while loading data. Please try again";
                return;
            }

            List<Mail> list = Mails.dataSet;

            // Convert the Users to a properties-based representation for the databinding
            List<MailProperties> plist = list.ConvertAll<MailProperties>((x) =>
            {
                MailProperties u = new MailProperties();
                u.active = x.active.ToString();
                u.adress = x.adress;
                u.username = x.username;
                return u;
            });

            EntryCount = plist.Count.ToString();
            entryCount.Value = EntryCount;

            repMails.DataSource = plist;
            repMails.DataBind();
        }

        /// <summary>
        /// Basing on the submitted Form, check which elements are dirty (changes)
        /// </summary>
        public List<Mail> CheckDirtyElements()
        {
            // Get number of Elements
            int amount = int.Parse((string)Request.Form["ctl00$MainContent$entryCount"]);

            List<Mail> list = new List<Mail>();

            // Check one by one...
            for (int i=0;i<amount;i++)
            {
                if ((string)Request.Form["ctl00$MainContent$repMails$ctl"+i.ToString("D2")+"$textAdress"] != (string)Request.Form["ctl00$MainContent$repMails$ctl" + i.ToString("D2") + "$textAdressOld"])
                {
                    // Found a dirty item, Create a User for this and store data

                    // TODO: Check for syntactically correct mail adress

                    string adress = (string)Request.Form["ctl00$MainContent$repMails$ctl" + i.ToString("D2") + "$textAdress"];
                    string name = (string)Request.Form["ctl00$MainContent$repMails$ctl" + i.ToString("D2") + "$textName"];
                    Mail mail = new Mail();
                    mail.adress = adress;
                    mail.username = name;
                    
                    list.Add(mail);
                }

                // Todo: Deletions
            }

            return list;
        }

        /// <summary>
        /// Basing on the submitted Form, check which elements are to be deleted
        /// </summary>
        public List<string> CheckDeleteElements()
        {
            // Get number of Elements
            int amount = int.Parse((string)Request.Form["ctl00$MainContent$entryCount"]);

            List<string> list = new List<string>();

            // Check one by one...
            for (int i = 0; i < amount; i++)
            {
                string name;
                try
                {
                    // Try parsng the element. If it is not existing, just go on
                    name = (string)(Request.Form["ctl00$MainContent$repMails$ctl" + i.ToString("D2") + "$checkDelete"]);
                }
                catch { continue; }

                // Check whether it should be deleted
                if (!String.IsNullOrEmpty(name))
                    list.Add(name);
                
            }

            return list;
        }

        public class MailProperties
        {
            //public int id { get; set; }
            public string username { get; set; }
            public string adress { get; set; }
            public string active { get; set; }
        }

        public string StateMessage { get; set; }

        public string EntryCount { get; set; }
    }
}