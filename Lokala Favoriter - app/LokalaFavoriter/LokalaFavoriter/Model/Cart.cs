using System;
using System.Collections.Generic;
using System.Text;

namespace LokalaFavoriter.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public int Product_id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Points { get; set; }
    }
}
