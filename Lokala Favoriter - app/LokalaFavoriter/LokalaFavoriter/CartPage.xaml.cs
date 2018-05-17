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
        
        public CartPage(List<Product> MyTempCart)
        {
            InitializeComponent();

            CartPageVM CartVM = new CartPageVM
            {
                Cartlist = MyTempCart
            };
            BindingContext = CartVM;

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

        //public List<Product> GetValues()
        //{
        //    //List<Product> MyCart = new List<Product>();
        //    //CartPageVM Cart = new CartPageVM();
        //    //foreach (Product item in Cart.Cartlist)
        //    //{
        //    //    MyCart.Add(item);
        //    //}
        //    //return MyCart;
        //}

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