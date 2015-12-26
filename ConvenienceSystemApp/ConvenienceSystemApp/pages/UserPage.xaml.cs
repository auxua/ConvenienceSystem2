using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using ConvenienceSystemDataModel;
using System.Threading;
using System.Collections.ObjectModel;

namespace ConvenienceSystemApp.pages
{
	/// <summary>
	/// Definition for Grouping in the upcoming Productspage
	/// </summary>
	public class Group : ObservableCollection<ProductsPageViewModel.ProductsViewModel>
	{
		public string Name { get; set; }
		public string ShortName { get; set; }

		public Group(string name, string shortname)
		{
			this.Name = name;
			this.ShortName = shortname;
		}
	}

    public class UserViewModel : User
    {
        public string debtString
        {
            get
            {
                // For using the Debt of the user
                //return String.Format("{0:C}", this.debt);
                // Not showing the debt;
                return " ";

            }
        }

        public string userString
        {
            get
            {
                return username;
            }
        }
    }

	public partial class UserPage : ContentPage
	{
		public UserPage ()
		{
			InitializeComponent ();

            // Get the List of usernames (In Future, use Data Binding instead!)
            //List<String> names = new List<string>(DataManager.GetActiveUsers().Select<User, string>(user => user.username));

            List<UserViewModel> users = new List<UserViewModel>(DataManager.GetActiveUsers().Select<User,UserViewModel>((x) =>
				{
					UserViewModel vm = new UserViewModel();
					vm.debt = x.debt;
					vm.username = x.username;
					return vm;
				}));

            // ConvertAll has better performance, but not supported on WinPhone
            /*List<UserViewModel> users = DataManager.GetActiveUsers().ConvertAll ((x) =>
				{
					UserViewModel vm = new UserViewModel();
					vm.debt = x.debt;
					vm.username = x.username;
				});*/

            //userListView.ItemsSource = names;
            userListView.ItemsSource = users;
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

			EmptyButton.Clicked += EmptyButton_Clicked;
		}

		async void EmptyButton_Clicked (object sender, EventArgs e)
		{
			
            
            await Navigation.PushAsync(new pages.EmptyNotificationPage());
            
            /*string answer = "";

            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    answer = await DisplayActionSheet("Möchtest du wirklich diese Meldung abschicken?", null, null, "Ja", "Nein");
                });

            if (answer != "Ja")
                return;

            if (DataManager.LastEmptyMail.AddMinutes(DataManager.EmptyMailCooldown).CompareTo(DateTime.Now) <= 0)
            {
                // Too early!
                return;
            }
            // Send Api Call...
            
            //TODO
             */
		}

        async void RefreshClicked(object sender, EventArgs e)
        {
            IsBusy = true;

            // Starting the whole system!
            bool success = await DataManager.GetAllDataAsync();
            if (!success)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Error", "Error while getting data: " + DataManager.Error, "OK");
                });
                IsBusy = false;
                return;
            }
			Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
			{
				DisplayAlert("Refresh", "Data Refreshed - Reload Page", "OK");
			});




            // Remove the Startpage from navigation and go to the UserPage, which starts the actual interaction with the users
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                /*var root = Navigation.NavigationStack[0];
                Navigation.InsertPageBefore(new NavigationPage(new pages.UserPage()), root);
                Navigation.PopToRootAsync();*/
                ((App)App.Current).SetRoot(new pages.UserPage());
            });
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
            var listItem = e.Item as UserViewModel;
            string user = listItem.userString;

            // Go to the next Page
            //Xamarin.Forms.Device.BeginInvokeOnMainThread(async () => await Navigation.PushModalAsync(new pages.ProductsPage()));
            IsBusy = false;

            // Create the Product-ViewModel for the product-Elements
            

			ObservableCollection<ProductsPageViewModel.ProductsViewModel> products = new ObservableCollection<ProductsPageViewModel.ProductsViewModel>
				(DataManager.GetAllProducts().Select<Product, ProductsPageViewModel.ProductsViewModel>(prod => 
				{
					var pvm = new ProductsPageViewModel.ProductsViewModel();
					pvm.comment = prod.comment;
					pvm.ID = prod.ID;
					pvm.price = prod.price;
					pvm.product = prod.product;
					return pvm;
				}));
			
			//Group groupAll = new Group ("All Products", "all");
			Group groupAll = new Group ("Alle Produkte", "alle");

			foreach (var item in products)
			{
				groupAll.Add (item);
			}

            // Now get Count for current user
            ProductsCountResponse response = default(ProductsCountResponse);
            try
            {
                response = await api.Communication.GetProductsCountAsync(user);
            }
            catch (Exception ex)
            {
                IsBusy = false;
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Fehler", "Ein Fehler ist aufgetreten (Internetverbindung?). Bitte später erneut versuchen", "OK");
                    return;
                });
            }
			//Group favedProducts = new Group("Popular for you","faved");
			Group favedProducts = new Group("Oft gekauft","oft");
            //List<ProductsPageViewModel.ProductsViewModel> IEproducts = new List<ProductsPageViewModel.ProductsViewModel>();
            var orderedProducts = response.dataSet.OrderBy((x) => x.amount);
			foreach (var item in orderedProducts)
			{
				if (item.amount > 1)
				{
					ProductsPageViewModel.ProductsViewModel pvm = new ProductsPageViewModel.ProductsViewModel ();
					pvm.price = item.price;
					pvm.product = item.product;
                    favedProducts.Add (pvm);
				}
			}

			// FIll the top-Group
			ObservableCollection<Group> allItems = new ObservableCollection<Group> ();
			allItems.Add (favedProducts);
			allItems.Add (groupAll);

            ProductsPageViewModel vm = new ProductsPageViewModel();
            //vm.Products = products;
			vm.Products = allItems;
            vm.Username = user;

            userListView.SelectedItem = null;

            await Navigation.PushAsync(new pages.ProductsPage(vm),true);
            
        }
	}
}
