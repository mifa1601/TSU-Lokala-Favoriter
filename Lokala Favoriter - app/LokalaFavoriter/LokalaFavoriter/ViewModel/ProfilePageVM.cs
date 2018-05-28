using LokalaFavoriter.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LokalaFavoriter.ViewModel
{
    public class ProfilePageVM
    {
        public string Username { get; set; }
        public string LoggedInUser { get; set; }
        public string Password { get; set; }
        public int PointsToday { get; set; }
        public int PointsWeek { get; set; }
        public int PointsMonth { get; set; }
        public int PointsTotal { get; set; }
        public int Points { get; set; }
        public string Groupname { get; set; }
        public string Month { get; set; }
    }
}
