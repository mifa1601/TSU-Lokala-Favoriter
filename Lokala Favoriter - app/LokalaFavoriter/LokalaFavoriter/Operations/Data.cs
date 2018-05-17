using LokalaFavoriter.Model;
using SqlServerConnections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LokalaFavoriter.Operations
{
    public class Data
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
    }
}
