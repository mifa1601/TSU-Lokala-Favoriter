using LokalaFavoriter.Model;
using LokalaFavoriter.ViewModel;
using SqlServerConnections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LokalaFavoriter
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        private SqlServer sqls;
        private DataTable dt;

        public HomePage (string Username, int id)
		{
			InitializeComponent ();
            HomePageVM MyVM = new HomePageVM
            {
                LoggedInUser = Username,
                User_id = id
            };
            BindingContext = MyVM;

            sqls = new SqlServer();

            groupname.Text = group();


        }
        //public HomePage(string username)
        //{


        //}

        public string group()
        {
            string name = username.Text;

            string group;

            string query = "SELECT * FROM users WHERE Username = '" + name + "'";
            dt = sqls.QueryRead(query);
            User u = new User();
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow item in dt.Rows)
                {
                    u = new User()
                    {
                        Id = (int)item["Id"],
                        Username = (string)item["Username"],
                        Group_id = (int)item["Group_id"]
                    };

                }

            }

            string groupquery = "SELECT * FROM Groups WHERE id = '" + u.Group_id + "'";
            dt = sqls.QueryRead(groupquery);
            Groups g = new Groups();
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow item in dt.Rows)
                {
                    g = new Groups()
                    {
                        Groupname = (string)item["Groupname"],
                        Id = (int)item["id"]
                    };
                }
            }

            group = g.Groupname;

            return group;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        
    }
}