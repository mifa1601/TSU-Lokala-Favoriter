﻿using LokalaFavoriter.Model;
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
	public partial class ProductPage : ContentPage
	{
        private DataTable dt;
        private SqlServer sqls;



        public List<Product> Cart = new List<Product>();
        CartPageVM Cartvm = new CartPageVM();
        

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

        public class CustomParam
        {
            public Product Parameter { get; set; }
        }

        public List<Product> Btn_add(object sender, CustomParam e)
        {
            
            var product = e.Parameter;
            Cartvm.Cartlist.Add(product);

            DisplayAlert("Kundvagn", "Du har lagt till " + product.Name, "OK");
            return Cartvm.Cartlist;
            
        }



        private void LoginButton_OnClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var classId = button.ClassId;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        
    }
}