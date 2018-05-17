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
	public partial class ProductPage : ContentPage
	{
        private DataTable dt;
        private SqlServer sqls;

        public ProductPage(int user_id)
        {
            InitializeComponent();
            ProductPageVM MyVM = new ProductPageVM
            {
                User_id = user_id
            };
            BindingContext = MyVM;

            ProductList.ItemsSource = GetValues();
        }
        
        
        public List<Product> GetValues()
        {
            List<Product> ProductList = new List<Product>();
            sqls = new SqlServer();
            Product p;
            string Myquery = "SELECT * FROM Products ORDER BY id ASC";
            dt = sqls.QueryRead(Myquery);
            foreach (DataRow item in dt.Rows)
            {
                p = new Product()
                {
                    Id = (int)item["Id"],
                    Name = (string)item["Name"],
                    Price = (int)item["Price"]
                };
                ProductList.Add(p);
            }
            return ProductList;
        }

        public class CustomParam
        {
            public Product Parameter { get; set; }
        }

        public void Btn_add(object sender, CustomParam e)
        {
            var product = e.Parameter;

            sqls = new SqlServer();
            
            string Myquery = "SELECT * FROM Products ORDER BY id ASC";
            dt = sqls.QueryRead(Myquery);

            DisplayAlert("Kundvagn", "Du har lagt till " + product.Name, "OK"); 
        }



        private void LoginButton_OnClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var classId = button.ClassId;
        }

        #region buttons
        void Btn_products(Object sender, System.EventArgs e)
        {
            int id = Convert.ToInt32(Testknapp.Text);
            var page = new ProductPage(id);
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