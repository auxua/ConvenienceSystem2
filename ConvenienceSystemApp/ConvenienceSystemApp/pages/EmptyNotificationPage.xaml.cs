using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ConvenienceSystemApp.pages
{
	public partial class EmptyNotificationPage : ContentPage
	{
		public EmptyNotificationPage ()
		{
			InitializeComponent ();

            SendButton.Clicked += SendButton_Clicked;
		}

        void SendButton_Clicked(object sender, EventArgs e)
        {
            string answer = "";
            // Check if there is some text
            if (String.IsNullOrEmpty(CommentBox.Text) || String.IsNullOrWhiteSpace(CommentBox.Text))
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Fehler","Bitte Nachricht eingeben","OK");
                });
                return;
            }

            // Confirm!
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                answer = await DisplayActionSheet("Möchtest du wirklich diese Meldung abschicken?", null, null, "Ja", "Nein");
                if (answer != "Ja") return;
                // Check Cooldown for mail sending
                /*if (DataManager.LastEmptyMail.AddMinutes(DataManager.EmptyMailCooldown).CompareTo(DateTime.Now) <= 0)
                {
                    // Too early!
                    return;
                }*/
                // API-Call
                await DisplayAlert("Fehler", "API-Call noch nicht fertig - kommt bald", "OK");
                // Go Back!
                await Navigation.PopAsync();
            });

            
        }


	}
}
