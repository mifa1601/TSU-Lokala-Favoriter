﻿using LokalaFavoriter.Model;
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
    public partial class HomePage : ContentPage
    {
        private SqlServer sqls;
        private DataTable dt;
        private Data MyOp;
        public User MyUser = new User();

        public HomePage(string Username, int id)
        {
            MyOp = new Data();
            sqls = new SqlServer();
            dt = new DataTable();
            InitializeComponent();
            HomePageVM MyVM = new HomePageVM
            {
                LoggedInUser = Username,
                User_id = id
            };
            BindingContext = MyVM;
            MyUser = MyOp.GetUser(id);
        }
        
        #region buttons
        void Btn_products(Object sender, System.EventArgs e)
        {
            var page = new ProductPage(MyUser.Id);
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