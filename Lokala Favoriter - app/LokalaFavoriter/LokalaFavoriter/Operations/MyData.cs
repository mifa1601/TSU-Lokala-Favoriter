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
                        Points = (int)item["Points"]
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

        #endregion

#region Toplist

        public List<User> GetTopList()
        {
            List<User> Toplist = new List<User>();
            sqls = new SqlServer();
            User u;
            
            string Myquery = "SELECT TOP 5 * FROM Users ORDER BY Points desc";
            dt = sqls.QueryRead(Myquery);
            foreach (DataRow item in dt.Rows)
            {
                u = new User()
                {
                    Id = (int)item["Id"],
                    Username = (string)item["Username"],
                    Points = (int)item["Points"]
                };
                Toplist.Add(u);
            }
            return Toplist;
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

#endregion

    }
}
