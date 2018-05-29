using LokalaFavoriter.Model;
using SqlServerConnections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LokalaFavoriter.Operations
{
    public class MyData
    {
        private SqlServer sqls;
        private DataTable dt;
        private Cart c;

#region user

        public User GetUser(int id)
        {
            sqls = new SqlServer();
            User MyUser;
            string query = "SELECT * FROM users WHERE Id = '" + id + "'";
            dt = sqls.QueryRead(query);
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MyUser = new User()
                    {
                        Id = (int)item["Id"],
                        Username = (string)item["Username"],
                        Password = (string)item["Password"],
                        Group_id = (int)item["Group_id"],
                        
                    };
                    return MyUser;
                };
            }
            return null;
        }
        public Groups GetGroup(int id)
        {
            sqls = new SqlServer();
            Groups MyGroup;
            string query = "SELECT * FROM groups WHERE Id = '" + id + "'";
            dt = sqls.QueryRead(query);
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MyGroup = new Groups()
                    {
                        Id = (int)item["Id"],
                        Groupname = (string)item["Groupname"]
                    };
                    return MyGroup;
                }
            }
            return null;
        }
        public int GetPoints(int user_id, DateTime MyStartTime, DateTime MyEndTime)
        {
            sqls = new SqlServer();
            Point MyPoint;
            int Points = 0;
            string query = "SELECT * FROM Points WHERE User_id = '"+user_id+ "' AND Date between '" + MyStartTime + "' and '" + MyEndTime + "'";
            dt = sqls.QueryRead(query);
           
                foreach (DataRow item in dt.Rows)
                {
                    MyPoint = new Point()
                    {
                        Id = (int)item["Id"],
                        User_id = (int)item["User_id"],
                        Points = (int)item["Points"],
                        Date = (DateTime)item["Date"]
                    };
                Points = Points + MyPoint.Points;
                };
            

            return Points;
        }
        public int GetTotalPoints(int user_id)
        {
            sqls = new SqlServer();
            Point MyPoint;
            int Points = 0;
            string query = "SELECT * FROM Points WHERE User_id = '" + user_id + "'";
            dt = sqls.QueryRead(query);

            foreach (DataRow item in dt.Rows)
            {
                MyPoint = new Point()
                {
                    Id = (int)item["Id"],
                    User_id = (int)item["User_id"],
                    Points = (int)item["Points"],
                };
                Points = Points + MyPoint.Points;
            };


            return Points;
        }


        #endregion

        #region Toplist

        public string GetMonthName(int monthNumber)
        {
            List<string> Months = new List<string>
            {
                "Januari","Februari","Mars","April","Maj","Juni","Juli","Augusti","September","Oktober","November","December"
            };
            return Months[monthNumber - 1];
        }
        public List<Point> GetTopList(int group_id)
        {
            List<Point> Toplist = new List<Point>();
            sqls = new SqlServer();
            Point p;
            int position = -1;
            string Myquery = "SELECT top 5 sum(Points.Points) as points, Users.Username From Points left join Users on Points.User_id = Users.Id WHERE Users.Group_id = '"+ group_id+"' group by Users.Username ORDER BY Points DESC";
            dt = sqls.QueryRead(Myquery);
            string first = "first.png";
            string second = "second.png";
            string third = "third.png";
            string src = "";
            foreach (DataRow item in dt.Rows)
            {
                position = position + 1;

                if (position == 0)
                {
                    src = first;
                }
                if (position == 1)
                {
                    src = second;
                }
                if (position == 2)
                {
                    src = third;
                }
                if (position >= 3)
                {
                    src = "";
                }
                p = new Point()
                {
                    Points = (int)item["Points"],
                    Username = (string)item["Username"],
                    Position = position,
                    Src = src
                };
                Toplist.Add(p);
            }
            position = -1;
            return Toplist;
        }
        public Point HighestPoints(int group_id, DateTime start, DateTime end)
        {
            Point p;
            string Myquery = "SELECT top 1 SUM(Points.Points) as Points, Users.Username FROM Points LEFT JOIN Users on points.User_id = Users.Id WHERE Users.Group_id = '" + group_id + "' AND DATE between '" + start + "' and '" + end + "' GROUP BY Username ORDER BY Points desc";
            dt = sqls.QueryRead(Myquery);
            foreach (DataRow item in dt.Rows)
            {
                p = new Point()
                {
                    Points = (int)item["Points"],
                    Username = (string)item["Username"],
                };
                return p;
            }
            return null;
        }
        public Point HighestPointsOneCustomer(int group_id, DateTime start, DateTime end)
        {
            Point p;
            string Myquery = "SELECT top 1 Points.Points, Users.Username, Points.Date FROM Points LEFT JOIN Users on points.User_id = Users.Id WHERE Users.Group_id = '" + group_id + "' AND DATE between '" + start + "' and '" + end + "' GROUP BY Username, Date, Points ORDER BY Points desc";
            dt = sqls.QueryRead(Myquery);
            foreach (DataRow item in dt.Rows)
            {
                p = new Point()
                {
                    Points = (int)item["Points"],
                    Username = (string)item["Username"],
                };
                return p;
            }
            return null;
        }
        public Point MostCostumersOneDay(int group_id, DateTime start, DateTime end)
        {
            Point p;
            
            string Myquery = "SELECT top 1 COUNT(*) as Antal, Users.Username FROM Points LEFT JOIN Users on points.User_id = Users.Id WHERE Users.Group_id = '" + group_id + "' AND DATE between '" + start + "' and '" + end + "' GROUP BY Username ORDER BY Antal desc";
            dt = sqls.QueryRead(Myquery);
            foreach (DataRow item in dt.Rows)
            {
                p = new Point()
                {
                    Username = (string)item["Username"],
                    Antal = (int)item["Antal"]
                };
                return p;
            }
            return null;
        }


#endregion

#region product

        public List<Product> GetProductsFromGroupId(int id)
        {
            List<Product> ProductList = new List<Product>();
            sqls = new SqlServer();
            Product p;
            string Myquery = "SELECT Products.Id, Products.Name, Products.Price, Products.Points FROM Groups_Products RIGHT JOIN Products on Groups_Products.Products_id = Products.Id WHERE Groups_id= '" + id + "' GROUP BY Name, Price, Points, Id";
            dt = sqls.QueryRead(Myquery);
            foreach (DataRow item in dt.Rows)
            {
                p = new Product()
                {
                    Id = (int)item["Id"],
                    Name = (string)item["Name"],
                    Price = (int)item["Price"],
                    Points = (int)item["Points"]
                };
                ProductList.Add(p);
            }
            return ProductList;
        }

        public Product GetProductInfo(int product_id)
        {
            sqls = new SqlServer();
            Product p;
            string Myquery = "SELECT * FROM Products WHERE id = '" + product_id + "'";
            dt = sqls.QueryRead(Myquery);

            foreach (DataRow item in dt.Rows)
            {
                p = new Product()
                {
                    Id = (int)item["Id"],
                    Name = (string)item["Name"],
                    Src = (string)item["Src"],
                    Info = (string)item["Info"],
                    Points = (int)item["Points"],
                    Price = (int)item["Price"]
                };

                return p;
            }
            return null;
        }

#endregion

#region Cart

        public List<Cart> GetCartFromUserId(int user_id)
        {
            List<Cart> CartList = new List<Cart>();
            sqls = new SqlServer();
            Cart c;
            string Myquery = "SELECT Cart.Id, Products.Id AS Product_id, Products.Name, Products.Price, Products.Points FROM ((Cart RIGHT JOIN Users ON Cart.User_id = Users.Id) RIGHT JOIN Products ON Cart.Product_id = Products.Id) WHERE Users.id= '" + user_id+"'";
            dt = sqls.QueryRead(Myquery);
            foreach (DataRow item in dt.Rows)
            {
                c = new Cart()
                {
                    Id = (int)item["Id"],
                    Product_id = (int)item["Product_id"],
                    Name = (string)item["Name"],
                    Price = (int)item["Price"],
                    Points = (int)item["Points"]
                };
                CartList.Add(c);
            }
            return CartList;
        }

        public void AddToCart(int user_id, int product_id)
        {
            sqls = new SqlServer();
            string Myquery = "INSERT INTO Cart (Product_id, User_id) VALUES ('"+ product_id + "','"+user_id+"')";
            dt = sqls.QueryRead(Myquery);
        }

        public Cart RemoveFromCart(int cart_id)
        {
            sqls = new SqlServer();
            
            string Myquery =  "DELETE FROM Cart WHERE Id = '" + cart_id + "'";
            sqls.QueryRead(Myquery);
            foreach (DataRow dr in dt.Rows)
            {
                c = new Cart()
                {
                    Id = (int)dr["Id"],
                };
            }
            return c;
        }

        public Cart EmptyUserCart(int user_id)
        {
            sqls = new SqlServer();

            string Myquery = "DELETE FROM Cart WHERE User_id = '" + user_id + "'";
            sqls.QueryRead(Myquery);
            foreach (DataRow dr in dt.Rows)
            {
                c = new Cart()
                {
                    Id = (int)dr["Id"],
                };
            }
            return c;
        }

        public int TotalPrice(List<Cart> cart)
        {
            int total = 0;
            foreach (var c in cart)
            {
                total = total + c.Price;
            }
            return total;
        }

        public void Sell(int points, int user_id)
        {
            sqls = new SqlServer();
            string date = DateTime.Now.ToString("yyyy-MM-dd");


            string Myquery = "INSERT INTO Points (Points, User_id, Date) Values ('"+ points + "', '" + user_id + "', '" + date + "')";
            dt = sqls.QueryRead(Myquery);
        }

#endregion

    }
}
