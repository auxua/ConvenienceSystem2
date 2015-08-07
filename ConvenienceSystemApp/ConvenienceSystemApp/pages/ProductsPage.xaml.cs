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
            // Add the element to the internal list
            /*products.Add(e.Item.ToString());

            // Update the Label representing the Products
            string productString = "";
            foreach (string s in products)
            {
                productString += s + ", ";
            }*/

            // Just Add to the ViewModel
            ((ProductsPageViewModel)this.BindingContext).AddProductSelection(((ProductsPageViewModel.ProductsViewModel)e.Item));
        }

		async void OnBuyClicked(object sender, EventArgs e)
		{
			// wait
            
		}
	}
}
