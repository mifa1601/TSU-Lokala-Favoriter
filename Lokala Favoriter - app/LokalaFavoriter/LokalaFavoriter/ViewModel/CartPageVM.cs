﻿using LokalaFavoriter.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LokalaFavoriter.ViewModel
{
    public class CartPageVM
    {
        public int TotalPrice { get; set; }
        public string LoggedInUser { get; set; }
        public int Points { get; set; }
    }
}
