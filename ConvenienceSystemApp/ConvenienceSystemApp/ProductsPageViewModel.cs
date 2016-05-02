using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;

using ConvenienceSystemDataModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ConvenienceSystemApp
{
    public class ProductsPageViewModel : INotifyPropertyChanged
    {
        #region Further definitions

        public class ProductsViewModel : Product
        {
            public string ProductName
            {
                get { return this.product; }
            }

            public string PriceString
            {
                get { return String.Format("{0:C}", this.price); }
            }

            public int amount = 0;
        }

        #endregion

        #region Properties

        private bool isBusy;

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                this.isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        private string username;

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
                RaisePropertyChanged("Username");
            }
        }

        /*private ObservableCollection<ProductsViewModel> products;

        public ObservableCollection<ProductsViewModel> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
                RaisePropertyChanged("Products");
            }
        }*/

		private ObservableCollection<pages.Group> products;

		public ObservableCollection<pages.Group> Products
		{
			get
			{
				return this.products;
			}
			set
			{
				this.products = value;
				RaisePropertyChanged("Products");
			}
		}

        /// <summary>
        /// Manages the List of the actual selection of products to buy
        /// </summary>
        private List<ProductsViewModel> selectedProducts = new List<ProductsViewModel>();

        /// <summary>
        /// Gets the String representation of the 
        /// </summary>
        public string SelectedProductsString
        {
            get
            {
                string s = "";
                foreach (ProductsViewModel prod in selectedProducts)
                {
                    s += prod.product+ " ("+prod.amount+"), ";
                }
                return s;
            }
        }

        public void AddProductSelection(ProductsViewModel prod)
        {
            // Search for the product already being in the list
            ProductsViewModel existing = selectedProducts.Find((x) => x.product == prod.product);

            prod.amount++;

            // Not existing yet, add it!
            if (existing == default(ProductsViewModel))
            {
                selectedProducts.Add(prod);
            }

            // Inform Binding objects about changes
            RaisePropertyChanged("SelectedProductsString");
        }


        #endregion

        public ProductsPageViewModel()
        {

        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public string ErrorMessage = "";

        private bool handled = false;

        public async Task<bool> BuyProductsAsync()
        {
            if (handled)
                return true;

            // Workaround for Windows Phones - Buttons sometimes get fired twice...
            handled = true;

            if (this.selectedProducts.Count < 1)
            {
                // No products selected - just create error
                //this.ErrorMessage = "Bitte erst Produkte auswählen";
                this.ErrorMessage = "Please Select Products first";
                return false;
            }

            // So, there are products. Try to Buy...
            try
            {
                // Create the request
                BuyProductsRequest request = new BuyProductsRequest();
                request.username = this.username;
                request.products = new List<string>();

                selectedProducts.ForEach((x) =>
                    {
                        // Add the product as often as the amount tells us
                        for (int i = 0; i<x.amount;i++)
                        {
                            request.products.Add(x.product);
                        }
                    });

                //request.products = new List<string>(selectedProducts.Select<ProductsViewModel,string>((x) => x.product));
                // try executing
                bool answer = await api.Communication.BuyProductsCountAsync(request);
                // Check for error without Exceptions
                if (!answer)
                {
                    ErrorMessage = "Unknown Error. Buying did not succeed!";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // Workaround: The Buy-Call is working fine with the server, but the Web Client receives a 500 internal Server Error on iOS
                /*if (Xamarin.Forms.Device.OS == Xamarin.Forms.TargetPlatform.iOS)
                {
                    if (ex.Message.Contains("The remote server returned an error: (500) Internal Server Error."))
                        return true;
                }*/
                ErrorMessage = "Error: " + ex.Message;
                
                return false;
            }
        }


    }
}
