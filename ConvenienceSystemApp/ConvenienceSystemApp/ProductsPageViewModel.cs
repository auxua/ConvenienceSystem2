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
