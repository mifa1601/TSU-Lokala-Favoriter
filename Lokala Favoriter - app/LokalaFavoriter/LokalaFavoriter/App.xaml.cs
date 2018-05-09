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
        
        void btn_products()
        {
            HomePage Home = new HomePage();
            Home.btn_products();

        }

    }
}
