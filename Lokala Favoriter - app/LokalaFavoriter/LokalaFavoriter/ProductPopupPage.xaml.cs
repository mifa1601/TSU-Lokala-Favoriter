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
	public partial class ProductPopupPage : ContentPage
	{
        private DataTable dt;
        private SqlServer sqls;
        private MyData MyOperation;
        public Product p = new Product();

        public ProductPopupPage (int product_id)
		{
            MyOperation = new MyData();
            sqls = new SqlServer();
            dt = new DataTable();

            InitializeComponent ();

            p = MyOperation.GetProductInfo(product_id);

            ProductPageVM MyVM = new ProductPageVM
            {
                Src = p.Src,
                Info = p.Info,
                Name = p.Name,
                Price = p.Price,
                Points = p.Points
                
            };
            BindingContext = MyVM;

        }
	}
}