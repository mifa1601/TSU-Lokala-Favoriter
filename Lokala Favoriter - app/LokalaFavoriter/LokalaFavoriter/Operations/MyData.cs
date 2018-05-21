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
                        Group_id = (int)item["Group_id"]
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
        public List<Product> GetCartFromUserId(int user_id)
        {
            List<Product> ProductList = new List<Product>();
            sqls = new SqlServer();
            Product p;
            string Myquery = "SELECT Products.Id, Products.Name, Products.Price, Products.Points FROM ((Cart RIGHT JOIN Users ON Cart.User_id = Users.Id) RIGHT JOIN Products ON Cart.Product_id = Products.Id) WHERE Users.id= '"+user_id+"'";
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

        public void AddToCart(int user_id, int product_id)
        {
            sqls = new SqlServer();
            string Myquery = "INSERT INTO Cart (Product_id, User_id) VALUES ('"+ product_id + "','"+user_id+"')";
            dt = sqls.QueryRead(Myquery);
        }

       
        
    }
}
