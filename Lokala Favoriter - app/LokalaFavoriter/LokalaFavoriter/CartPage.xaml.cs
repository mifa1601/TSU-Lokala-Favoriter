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
        
        public CartPage()
        {
            InitializeComponent();

            //CartPageVM CartVM = new CartPageVM
            //{
            //    Cartlist = MyTempCart
            //};
            //BindingContext = CartVM;

        }



        public class CustomParam
        {
            public Product Parameter { get; set; }
        }

 

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}