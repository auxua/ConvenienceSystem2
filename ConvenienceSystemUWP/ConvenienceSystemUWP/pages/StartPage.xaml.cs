using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ConvenienceSystemUWP.pages
{
	public partial class StartPage : ContentPage
	{
		public StartPage ()
		{
			InitializeComponent ();

            BindingContext = this;

            StartButton.Clicked += ButtonClicked;
			IsBusy = false;
		}

        public async void ButtonClicked(object sender, EventArgs e)
        {
            //string test = DependencyService.Get<api.Communication.IDownloader>().Get("http://www.google.com");
            //string test = await DependencyService.Get<api.Communication.IDownloader>().GetAsync("http://www.google.com");

            IsBusy = true;

            // Starting the whole system!
            bool success = await DataManager.GetAllDataAsync();
            if (!success)
            {
                Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Error", "Error while getting data: " + DataManager.Error, "OK");
                    });
                IsBusy = false;
                return;
            }

            


            // Remove the Startpage from navigation and go to the UserPage, which starts the actual interaction with the users
            Device.BeginInvokeOnMainThread(() =>
                {
                    /*var root = Navigation.NavigationStack[0];
                    Navigation.InsertPageBefore(new NavigationPage(new pages.UserPage()), root);
                    Navigation.PopToRootAsync();*/
                    ((App)App.Current).SetRoot(new pages.UserPage());
                });
        }
	}
}
