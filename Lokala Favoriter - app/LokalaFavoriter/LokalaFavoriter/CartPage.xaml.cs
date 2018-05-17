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
	public partial class CartPage : ContentPage
	{
        ProductPage p = new ProductPage();


        public CartPage()
        {
            InitializeComponent();
            
            MyCart.ItemsSource = p.Cart;
            
        }

  //      public CartPage (List<Product> CustomerCart)
		//{

            
            

  //          //CartPageVM Cart = new CartPageVM()
  //          //{
  //          //    Cartlist = MyCart
  //          //};
  //      }


        public class CustomParam
        {
            public Product Parameter { get; set; }
        }

        public List<Product> GetValues()
        {
            List<Product> MyCart = new List<Product>();
            ProductPage p = new ProductPage();
            foreach (Product item in p.Cart)
            {
                MyCart.Add(item);
            }
            return MyCart;
        }

        //private void btn_remove(object sender, CustomParam e)
        //{
        //    var product = e.Parameter;
        //    MyCart.(product);
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}