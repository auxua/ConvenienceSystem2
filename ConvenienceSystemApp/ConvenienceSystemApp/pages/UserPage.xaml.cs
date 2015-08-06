using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using ConvenienceSystemDataModel;
using System.Threading;

namespace ConvenienceSystemApp.pages
{
	public partial class UserPage : ContentPage
	{
		public UserPage ()
		{
			InitializeComponent ();

            // Get the List of usernames (In Future, use Data Binding instead!)
            List<String> names = new List<string>(DataManager.GetActiveUsers().Select<User, string>(user => user.username));


            userListView.ItemsSource = names;
			//userListView.ItemSelected += OnUserSelected;
			//Use Tapped-Event instead of selected event to enable users to reselect themselves after going back
			userListView.ItemTapped += OnUserSelected;

            //TODO: Externalize these Strings (in best case: Localization)

            //ContactLabel.Text = "Questions? Drop a mail: " + Config.ContactMail;
            ContactLabel.Text = "Fragen? Einfach Mail schreiben: " + Config.ContactMail;
            //EmptyButton.Text = "Report Lack of Supplies";
            EmptyButton.Text = "Getränke-Notstand melden";
            //TutorialLabel.Text = " (1) Select your name \n (2) Select your product(s) \n (3) Confirm \n That's All!";
            TutorialLabel.Text = " (1) Namen auswählen \n (2) Produkt(e) auswählen \n (3) bestätigen \n Fertig!";
		}



		async void OnUserSelected(object sender, ItemTappedEventArgs e)
        {
            IsBusy = true;
            // Check Data Model
            if (DataManager.State != DataManager.DMState.ACTIVE)
            {
                // Try to restore DataModel
                bool success = await DataManager.GetAllDataAsync();
                if (!success)
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Error", "Could not get the Data of the system (please try again later): " + DataManager.Error, "OK");
                    });
                    // No success...
                    IsBusy = false;
                    return;
                }
            }

            // So, We can get the Data we want
            //TODO
            string user = e.Item.ToString();

            // Go to the next Page
            //Xamarin.Forms.Device.BeginInvokeOnMainThread(async () => await Navigation.PushModalAsync(new pages.ProductsPage()));
            IsBusy = false;
            await Navigation.PushAsync(new pages.ProductsPage(),true);
            
        }
	}
}
