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
    public partial class ProductPage : ContentPage
    {
        private DataTable dt;
        private SqlServer sqls;
        private MyData MyOperation;
        public User MyUser = new User();

        public ProductPage(int user_id)
        {
            MyOperation = new MyData();
            sqls = new SqlServer();
            dt = new DataTable();
            InitializeComponent();
            MyUser = MyOperation.GetUser(user_id);
            ProductPageVM MyVM = new ProductPageVM
            {
                User_id = user_id,
                LoggedInUser = MyUser.Username
            };
            BindingContext = MyVM;
            
            ProductList.ItemsSource = MyOperation.GetProductsFromGroupId(MyUser.Group_id);
        }

        public class CustomParam
        {
            public Product Parameter { get; set; }
        }

#region Buttons

        public void Btn_add(object sender, CustomParam e)
        {
            var product = e.Parameter;
            MyOperation.AddToCart(MyUser.Id, product.Id);
            DisplayAlert("Kundvagn", "Du har lagt till " + product.Name, "OK");
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