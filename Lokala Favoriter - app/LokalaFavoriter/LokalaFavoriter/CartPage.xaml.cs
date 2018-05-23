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
            CartList.ItemsSource = MyOperation.GetCartFromUserId(user_id);
            int TotalPrice = MyOperation.TotalPrice(MyOperation.GetCartFromUserId(user_id));
            MyUser = MyOperation.GetUser(user_id);
            CartPageVM CartVM = new CartPageVM
            {
                TotalPrice = TotalPrice,
                LoggedInUser = MyUser.Username,
                UserPoints = MyOperation.GetTotalPoints(MyUser.Id),
            };
            BindingContext = CartVM;

            
            
        }

        public class CustomParam
        {
            public Cart Parameter { get; set; }
        }

#region Buttons
        public void Btn_remove(object sender, CustomParam e)
        {
            var Cart = e.Parameter;
            MyOperation.RemoveFromCart(Cart.Id);
           

            DisplayAlert("Kundvagn", "Du har tagit bort " + Cart.Name + " Från kundvagnen", "OK");

            CartList.ItemsSource = MyOperation.GetCartFromUserId(MyUser.Id);
            int TotalPrice = MyOperation.TotalPrice(MyOperation.GetCartFromUserId(MyUser.Id));
            CartPageVM CartVM = new CartPageVM
            {
                TotalPrice = TotalPrice
            };
            BindingContext = CartVM;

        }

        public void Btn_EmptyCart(object sender, CustomParam e)
        {
            
            MyOperation.EmptyUserCart(MyUser.Id);
            

            DisplayAlert("Kundvagn", "Du har tömt kundvagnen", "OK");

            CartList.ItemsSource = MyOperation.GetCartFromUserId(MyUser.Id);
            int TotalPrice = MyOperation.TotalPrice(MyOperation.GetCartFromUserId(MyUser.Id));
            CartPageVM CartVM = new CartPageVM
            {
                TotalPrice = TotalPrice
            };
            BindingContext = CartVM;

        }

        #endregion

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


        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}