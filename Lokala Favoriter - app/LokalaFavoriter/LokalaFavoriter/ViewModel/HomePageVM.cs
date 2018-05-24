using LokalaFavoriter.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LokalaFavoriter.ViewModel
{
    public class HomePageVM
    {
        public string LoggedInUser { get; set; }
        public int Points { get; set; }
        public Point PointsToday { get; set; }
        public Point PointsWeek { get; set; }
        public Point PointsMonth { get; set; }
        public Point PointsOneCustomer { get; set; }
        public Point MostCustomersOneDay { get; set; }


    }
}
