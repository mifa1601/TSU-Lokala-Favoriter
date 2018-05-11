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
    public partial class Login : ContentPage
    {
        private SqlServer sqls;
        private DataTable dt;

        public Login()
        {
            InitializeComponent();
        }
        private void btn_login(object sender, EventArgs e)
        {
            string name = username.Text;
            string pass = password.Text;

            UserLogin(name, pass);
        }
        public void UserLogin(string username, string password)
        {
            sqls = new SqlServer();
            User u = new User();
            u.Username = username;
            u.Password = password;
            string query = "SELECT * FROM users WHERE username = '" + u.Username + "' AND password = '" + u.Password + "'";
            dt = sqls.QueryRead(query);
            if (dt.Rows.Count == 1)
            {
                Navigation.PushAsync(new HomePage(username));
            }
            else
            {
                DisplayAlert("Error", "Felaktigt användarnamn eller lösenord", "Stäng");
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}