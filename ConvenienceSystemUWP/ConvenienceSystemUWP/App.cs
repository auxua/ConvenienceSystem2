using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ConvenienceSystemUWP
{
	public enum LastBuyState { SUCCESS, FAILED, NONE, MSG_SUCC }

	public class App : Application
	{
		public static LastBuyState LastBuy;

		public App ()
		{
			// The root page of your application
			LastBuy = LastBuyState.NONE;
			//LastBuy = LastBuyState.SUCCESS;
			MainPage = new pages.StartPage();
		}

        public void SetRoot(Page p)
        {
            MainPage = new NavigationPage(p);
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}


	}
}
