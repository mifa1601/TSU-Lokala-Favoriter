using LokalaFavoriter.Model;
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

        public ProductPage ()
		{
			InitializeComponent ();
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public void btn_products()
        {
            Navigation.PushAsync(new ProductPage());
        }
    }
}