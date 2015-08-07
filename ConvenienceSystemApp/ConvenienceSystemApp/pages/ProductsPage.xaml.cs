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

        private string username;

		private List<String> products = new List<string> ();

        ObservableCollection<Product> productCollection = new ObservableCollection<Product>();

		public ProductsPage (string username)
		{
			InitializeComponent ();

            this.username = username;

			List<String> names = new List<string>(DataManager.GetAllProducts().Select<Product, string>(prod => prod.product+" ("+String.Format("{0:C}",prod.price)+")"));

            productCollection = new ObservableCollection<Product>(DataManager.GetAllProducts());

			//productListView.ItemsSource = names;
            //productListView.ItemsSource = new ObservableCollection<Product>(DataManager.GetAllProducts());

            productListView.ItemTapped += productListView_ItemTapped;

			AbortButton.Clicked += (object sender, EventArgs e) => Navigation.PopAsync();
			BuyButton.Clicked += OnBuyClicked;

            BindingContext = this;
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
        }

		async void OnBuyClicked(object sender, EventArgs e)
		{
			// wait
            
		}
	}
}
