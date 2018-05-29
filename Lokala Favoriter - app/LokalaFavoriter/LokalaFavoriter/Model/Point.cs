using System;
using System.Collections.Generic;
using System.Text;

namespace LokalaFavoriter.Model
{
    public class Point
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public string Username { get; set; }
        public int Points { get; set; }
        public int Antal { get; set; }
        public DateTime Date { get; set; }
        public int Position { get; set; }
        public string Src { get; set; }
    }
}
