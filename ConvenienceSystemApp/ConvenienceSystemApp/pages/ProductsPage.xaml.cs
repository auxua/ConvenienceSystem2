using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ConvenienceSystemDataModel;
using System.Collections.ObjectModel;

namespace ConvenienceSystemApp.pages
{
	public partial class ProductsPage : ContentPage
	{

		private List<String> products = new List<string> ();

        ObservableCollection<Product> productCollection = new ObservableCollection<Product>();

		public ProductsPage (ProductsPageViewModel vm)
		{
			InitializeComponent ();

            BindingContext = vm;

            productListView.ItemTapped += productListView_ItemTapped;

			AbortButton.Clicked += (object sender, EventArgs e) => Navigation.PopAsync();
			BuyButton.Clicked += OnBuyClicked;

		}

        async void productListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Just Add to the ViewModel
            ((ProductsPageViewModel)this.BindingContext).IsBusy = true;
            ((ProductsPageViewModel)this.BindingContext).AddProductSelection(((ProductsPageViewModel.ProductsViewModel)e.Item));
            ((ProductsPageViewModel)this.BindingContext).IsBusy = false;
        }

		async void OnBuyClicked(object sender, EventArgs e)
		{            
            // Execute on ViewModel
            ((ProductsPageViewModel)this.BindingContext).IsBusy = true;

            // Only buy if there are items in the cart..
            if (String.IsNullOrEmpty(((ProductsPageViewModel)this.BindingContext).SelectedProductsString))
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Error", "Bitte zuerst Produkte auswählen!", "OK");
                });

            bool success = await ((ProductsPageViewModel)this.BindingContext).BuyProductsAsync();

            // On error, inform to user
            if (!success)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Error", "Kauf konnte nciht getätigt werden. Bitte später erneut versuchen. Falls das problem länger besteht, bitte melden!", "OK");
                    });
                ((ProductsPageViewModel)this.BindingContext).IsBusy = false;
                return;
            }

            // No error, Inform User and go back
            ((ProductsPageViewModel)this.BindingContext).IsBusy = false;
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Success", "Kauf erfolgreich!", "OK");
                    Navigation.PopAsync(true);
                });
            
            
		}
	}
}
