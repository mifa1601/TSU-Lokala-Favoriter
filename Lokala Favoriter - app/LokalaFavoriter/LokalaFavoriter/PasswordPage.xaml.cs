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
    public partial class PasswordPage : ContentPage
    {
        private DataTable dt;
        private SqlServer sqls;
        private MyData MyOperation;
        public User MyUser = new User();


        public PasswordPage(int user_id)
        {
            InitializeComponent();
            MyOperation = new MyData();
            sqls = new SqlServer();
            dt = new DataTable();
            MyUser = MyOperation.GetUser(user_id);

            ProfilePageVM MyVm = new ProfilePageVM
            {
                Username = MyUser.Username,
                LoggedInUser = MyUser.Username,
                Password = MyUser.Password,
                Points = MyOperation.GetTotalPoints(MyUser.Id),
            };
            BindingContext = MyVm;

        }
        private void Btn_SavePassword(object sender, EventArgs e)
        {
            string oldpw = OldPassword.Text;
            string newpw = NewPassword.Text;
            string reppw = RepeatNewPassword.Text;
            SaveNewPassword(oldpw, newpw, reppw);
        }
        public void SaveNewPassword(string oldpassword, string newpassword, string repeatnewpassword)
        {
            sqls = new SqlServer();


            string query = "UPDATE users SET Password = '" + newpassword + "' WHERE Id = '" + MyUser.Id + "'";

            if (oldpassword != MyUser.Password)
            {
                DisplayAlert("Misslyckades", "Det gamla lösenordet stämmer inte", "Stäng");
                OldPassword.Text = String.Empty;


            }
            else if (newpassword != repeatnewpassword)
            {
                DisplayAlert("Misslyckades", "De nya lösenordet du upprepade stämmer inte", "Stäng");
                NewPassword.Text = String.Empty;
                RepeatNewPassword.Text = String.Empty;
            }
            else
            {
                DisplayAlert("Lyckades!", "Ditt lösenord är nu bytt till " + newpassword, "Stäng");
                ProfilePage page = new ProfilePage(MyUser.Id);
                Navigation.PushAsync(page);

                dt = sqls.QueryRead(query);
                if (dt.Rows.Count == 1)
                {
                    foreach (DataRow item in dt.Rows)
                    {

                        MyUser = new User()
                        {
                            Id = (int)item["Id"],

                        };

                    }

                }

            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        #region buttons
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
            var page = new ToplistPage(MyUser.Id);
            Navigation.PushAsync(page);
        }
        #endregion
    }


}