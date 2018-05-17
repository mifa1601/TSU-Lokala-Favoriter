using LokalaFavoriter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LokalaFavoriter
{
	public partial class App : Application
	{
        
        public App ()
		{
            InitializeComponent();
            //MainPage = new LokalaFavoriter.Login();
            MainPage = new NavigationPage(new Login()); 
        }
        
        void Btn_products(Object sender, System.EventArgs e)
        {
            var page = new ProductPage();
            MainPage.Navigation.PushAsync(page);
        }
        void Btn_cart(Object sender, System.EventArgs e)
        {
            CartPage page = new CartPage();
            
            MainPage.Navigation.PushAsync(new CartPage());
        }
        void Btn_profile(Object sender, System.EventArgs e)
        {
            var page = new ProfilePage();
            MainPage.Navigation.PushAsync(page);
        }
        void Btn_toplist(Object sender, System.EventArgs e)
        {
            var page = new ToplistPage();
            MainPage.Navigation.PushAsync(page);
        }
        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
        
        

    }
}
