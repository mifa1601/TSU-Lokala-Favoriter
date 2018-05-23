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
	public partial class ProfilePage : ContentPage
	{
        private DataTable dt;
        private SqlServer sqls;
        private MyData MyOperation;
        public User MyUser = new User();
        public Groups MyGroup = new Groups();

        public ProfilePage (int user_id)
		{
			InitializeComponent ();
            MyOperation = new MyData();
            sqls = new SqlServer();
            dt = new DataTable();
            MyUser = MyOperation.GetUser(user_id);
            MyGroup = MyOperation.GetGroup(MyUser.Group_id);

            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = now.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            DateTime CurrentWeek = now.AddDays(-diff);


            ProfilePageVM MyVm = new ProfilePageVM
            {
                Username = MyUser.Username,
                Password = MyUser.Password,
                PointsToday = MyOperation.GetPoints(MyUser.Id, DateTime.Today, DateTime.Now),
                PointsWeek = MyOperation.GetPoints(MyUser.Id, CurrentWeek, DateTime.Now),
                PointsMonth = MyOperation.GetPoints(MyUser.Id, startDate, endDate),
                PointsTotal = MyOperation.GetTotalPoints(MyUser.Id),
                Groupname = MyGroup.Groupname,
                Month = MyOperation.GetMonthName(Convert.ToInt32(DateTime.Now.Month))
                
            };
            BindingContext = MyVm;
        }
        

#region Navbar
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
        void Btn_Passwordpage(Object sender, System.EventArgs e)
        {
            var page = new PasswordPage(MyUser.Id);
            Navigation.PushAsync(page);
        }
        void Btn_home(Object sender, System.EventArgs e)
        {
            var page = new HomePage(MyUser.Username, MyUser.Id);
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