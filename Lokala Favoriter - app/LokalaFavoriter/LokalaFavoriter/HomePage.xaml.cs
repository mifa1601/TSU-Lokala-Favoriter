using LokalaFavoriter.Model;
using LokalaFavoriter.Operations;
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
        //public Data MyOp;
        public User MyUser = new User();

        public HomePage(string Username, int id)
        {
            InitializeComponent();
            HomePageVM MyVM = new HomePageVM
            {
                LoggedInUser = Username,
                User_id = id
            };
            BindingContext = MyVM;
            MyUser = GetUser(id);
            
        }
        
        public User GetUser(int id)
        {
            User MyUser;
            string query = "SELECT * FROM users WHERE Id = '" + id + "'";
            dt = sqls.QueryRead(query);
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MyUser = new User()
                    {
                        Id = (int)item["Id"],
                        Username = (string)item["Username"],
                        Group_id = (int)item["Group_id"]
                    };
                    return MyUser;
                };
            }
            return null;
        }


        //public int GetId()
        //{
        //    string name = username.Text;
        //    int MyId;
        //    string query = "SELECT * FROM users WHERE Username = '" + name + "'";
        //    dt = sqls.QueryRead(query);
        //    User u = new User();
        //    if (dt.Rows.Count == 1)
        //    {
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            u = new User()
        //            {
        //                Id = (int)item["Id"],
        //                Username = (string)item["Username"],
        //                Group_id = (int)item["Group_id"]
        //            };

        //        }

        //    }
        //    return MyId = u.Id;
        //}


        //public string group()
        //{
        //    string name = username.Text;

        //    string group;

        //    string query = "SELECT * FROM users WHERE Username = '" + name + "'";
        //    dt = sqls.QueryRead(query);
        //    User u = new User();
        //    if (dt.Rows.Count == 1)
        //    {
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            u = new User()
        //            {
        //                Id = (int)item["Id"],
        //                Username = (string)item["Username"],
        //                Group_id = (int)item["Group_id"]
        //            };

        //        }

        //    }

        //    string groupquery = "SELECT * FROM Groups WHERE id = '" + u.Group_id + "'";
        //    dt = sqls.QueryRead(groupquery);
        //    Groups g = new Groups();
        //    if (dt.Rows.Count == 1)
        //    {
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            g = new Groups()
        //            {
        //                Groupname = (string)item["Groupname"],
        //                Id = (int)item["id"]
        //            };
        //        }
        //    }

        //    group = g.Groupname;

        //    return group;
        //}
        #region buttons
        void Btn_products(Object sender, System.EventArgs e)
        {
            var page = new ProductPage(MyUser.Id);
            Navigation.PushAsync(page);
        }
        void Btn_cart(Object sender, System.EventArgs e)
        {
            var page = new CartPage();
            Navigation.PushAsync(page);
        }
        void Btn_profile(Object sender, System.EventArgs e)
        {
            var page = new ProfilePage();
            Navigation.PushAsync(page);
        }
        void Btn_toplist(Object sender, System.EventArgs e)
        {
            var page = new ToplistPage();
            Navigation.PushAsync(page);
        }
        #endregion
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

    }
}