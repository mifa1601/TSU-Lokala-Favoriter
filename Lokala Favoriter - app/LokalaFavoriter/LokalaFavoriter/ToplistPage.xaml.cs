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
	public partial class ToplistPage : ContentPage
	{
        private DataTable dt;
        private SqlServer sqls;
        private MyData MyOperation;
        public User MyUser = new User();

        public ToplistPage (int user_id)
		{
            InitializeComponent();
            MyOperation = new MyData();
            sqls = new SqlServer();
            dt = new DataTable();
            MyUser = MyOperation.GetUser(user_id);

            ProfilePageVM MyVm = new ProfilePageVM
            {
                LoggedInUser = MyUser.Username,
                Points = MyOperation.GetTotalPoints(MyUser.Id)

            };
            BindingContext = MyVm;

            Toplist.ItemsSource = MyOperation.GetTopList(MyUser.Group_id);
        }

        public List<string> medals = new List<string>
        {
            "first.png",
            "second.png",
            "third.png"

        };

        

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

#region Navbar
        void Btn_home(Object sender, System.EventArgs e)
        {
            var page = new HomePage(MyUser.Username, MyUser.Id);
            Navigation.PushAsync(page);
        }

        void Btn_products(Object sender, System.EventArgs e)
        {
            var page = new ProductPage(MyUser.Id);
            Navigation.PushAsync(page);
        }
        void Btn_cart(Object sender, System.EventArgs e)
        {
            var page = new CartPage(MyUser.Id);
            Navigation.PushAsync(page);
        }
        void Btn_profile(Object sender, System.EventArgs e)
        {
            var page = new ProfilePage(MyUser.Id);
            Navigation.PushAsync(page);
        }
        void Btn_toplist(Object sender, System.EventArgs e)
        {
            var page = new ToplistPage(MyUser.Id);
            Navigation.PushAsync(page);
        }
        #endregion

    }
}