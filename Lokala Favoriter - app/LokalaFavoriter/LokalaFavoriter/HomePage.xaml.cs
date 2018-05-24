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
        private MyData MyOperation;
        public User MyUser = new User();

        public HomePage(string Username, int user_id)
        {
            MyOperation = new MyData();
            sqls = new SqlServer();
            dt = new DataTable();
            InitializeComponent();
            MyUser = MyOperation.GetUser(user_id);

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

            HomePageVM MyVM = new HomePageVM
            {
                LoggedInUser = Username,
                Points = MyOperation.GetTotalPoints(MyUser.Id),
                PointsToday = MyOperation.HighestPoints(MyUser.Group_id,DateTime.Today, DateTime.Now),
                PointsWeek = MyOperation.HighestPoints(MyUser.Group_id, CurrentWeek, DateTime.Now),
                PointsMonth = MyOperation.HighestPoints(MyUser.Group_id, startDate, endDate),
                PointsOneCustomer = MyOperation.HighestPointsOneCustomer(MyUser.Group_id, DateTime.Today, DateTime.Now),
                MostCustomersOneDay = MyOperation.MostCostumersOneDay(MyUser.Group_id, DateTime.Today, DateTime.Now)

            };
            BindingContext = MyVM;
            
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
        async void Btn_logout(Object sender, System.EventArgs e)
        {
            var Logout = await DisplayAlert("Logga ut", "Är du säker att du vill logga ut?", "Ja","Nej");
            var page = new Login();
            if (Logout.Equals(true))
            {
                await Navigation.PushAsync(page);
            }
            else
            {
                Logout = false;
            }
            
        }


        #endregion

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

    }
}