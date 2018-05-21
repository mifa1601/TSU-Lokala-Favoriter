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
    public partial class CartPage : ContentPage
    {
        private DataTable dt;
        private SqlServer sqls;
        private MyData MyOperation;
        public User MyUser = new User();

        public CartPage(int user_id)
        {
            InitializeComponent();
            MyOperation = new MyData();
            sqls = new SqlServer();
            dt = new DataTable();
            CartPageVM CartVM = new CartPageVM
            {
                
            };
            BindingContext = CartVM;
            CartList.ItemsSource = MyOperation.GetCartFromUserId(user_id);
            MyUser = MyOperation.GetUser(user_id);
        }

        public class CustomParam
        {
            public Product Parameter { get; set; }
        }
        #region buttons
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