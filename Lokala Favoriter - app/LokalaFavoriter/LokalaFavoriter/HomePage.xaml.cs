using LokalaFavoriter.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LokalaFavoriter
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage (int user_id)
		{
			InitializeComponent ();
            HomePageVM MyVM = new HomePageVM
            {
                LoggedInUser = user_id.ToString()
            };
            BindingContext = MyVM;
        }
        //public HomePage(string username)
        //{
            

        //}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        
    }
}