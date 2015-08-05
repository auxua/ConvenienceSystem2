using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using ConvenienceSystemDataModel;
using System.Threading;

namespace ConvenienceSystemApp.pages
{
	public partial class UserPage : ContentPage
	{
		public UserPage ()
		{
			InitializeComponent ();

            List<string> list = new List<string>();
            /*list.Add("aaaa");
            list.Add("bbb");*/

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

            UsersResponse ans = new UsersResponse();

            /*Thread th = new Thread(new ParameterizedThreadStart((x) =>
            {
                ans = api.Communication.GetActiveUsersAsync().Result;
                var i = 5;
            }));
            th.Start();
            th.Join();*/
            //ans = api.Communication.GetActiveUsersAsync().Result;


            //string test = DependencyService.Get<api.Communication.IDownloader>().Get("http://www.google.com");

            /*foreach (User u in ans.dataSet)
            {
                list.Add(u.username);
            }*/

            userListView.ItemsSource = users;
		}
	}
}
