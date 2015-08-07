using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using ConvenienceSystemDataModel;
using System.Collections.ObjectModel;

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

        private ObservableCollection<ProductsViewModel> products;

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


    }
}
