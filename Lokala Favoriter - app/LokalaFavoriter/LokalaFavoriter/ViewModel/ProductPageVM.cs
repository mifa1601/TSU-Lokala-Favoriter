using System;
using System.Collections.Generic;
using System.Text;

namespace LokalaFavoriter.ViewModel
{
    public class ProductPageVM
    {
        public int User_id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Points { get; set; }
        public string LoggedInUser { get; set; }
        public int UserPoints { get; set; }
        public string Info { get; set; }
        public string Src { get; set; }
    }
}
