using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using ConvenienceSystemDataModel;

namespace ConvenienceSystemApp.pages
{
	public partial class UserPage : ContentPage
	{
		public UserPage ()
		{
			InitializeComponent ();

            List<string> list = new List<string>();
            list.Add("aaaa");
            list.Add("bbb");

            // Create some Fake-Data
            List<User> users = new List<User>();

            User user1 = new User { username = "Testuser 1" };
            User user2 = new User { username = "Testuser 2" };
            User user3 = new User { username = "Testuser 3" };
            User user4 = new User { username = "Testuser 4" };
            users.Add(user1);
            users.Add(user2);
            users.Add(user3);
            users.Add(user4);

            userListView.ItemsSource = list;
		}
	}
}
