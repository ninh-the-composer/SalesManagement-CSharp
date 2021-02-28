using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Khai bao cac namspace cho phep lam viec voi CSDL
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SalesManagement.DAL
{
    class Database
    {
        public static SqlConnection GetConnection()
        {
            string strCon = ConfigurationManager.ConnectionStrings["ProductDB"].ToString();
            return new SqlConnection(strCon);
        }

        /// <summary>
        /// Phuong thuc lay du lieu tu CSDL
        /// </summary>
        /// <param name="sql">Truy van can lay du lieu</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataBySQL(string sql)
        {
            // Tao doi tuong SqlCommand de thuc thi truy van
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            // Tao doi tuong cau noi voi CSDL
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            // Tao doi tuong DataSet de chua du lieu duoc tra ve
            DataSet ds = new DataSet(); // Data cache
            da.Fill(ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// Phuong thuc cho thuc thi: Insert, update, delete
        /// </summary>
        /// <param name="sql">Truy van can thuc hien</param>
        /// <param name="para">Danh sach tham so truyen cho truy van</param>
        /// <returns>int</returns>
        public static int ExecuteSQL(string sql, params SqlParameter[] para)
        {
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            cmd.Parameters.AddRange(para);
            cmd.Connection.Open();
            int result = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return result;
        }
    }
}
