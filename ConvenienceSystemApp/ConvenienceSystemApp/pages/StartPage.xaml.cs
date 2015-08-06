using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ConvenienceSystemApp.pages
{
	public partial class StartPage : ContentPage
	{
		public StartPage ()
		{
			InitializeComponent ();

            StartButton.Clicked += ButtonClicked;
		}

        public async void ButtonClicked(object sender, EventArgs e)
        {
            //string test = DependencyService.Get<api.Communication.IDownloader>().Get("http://www.google.com");
            //string test = await DependencyService.Get<api.Communication.IDownloader>().GetAsync("http://www.google.com");
            
            // Starting the whole system!
            await DataManager.getAllDataAsync();


            // Remove the Startpage from navigation and go to the UserPage, which starts the actual interaction with the users
            Device.BeginInvokeOnMainThread(() =>
                {
                    var root = Navigation.NavigationStack[0];
                    Navigation.InsertPageBefore(new pages.UserPage(), root);
                    Navigation.PopToRootAsync();
                });
        }
	}
}
