using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SalesManagement.DAL {
    class ProductDAL {
        public static DataTable GetAllProduct() {
            string sql = "SELECT * FROM Products";
            return Database.GetDataBySQL(sql);
        }
        public static DataTable SearchProductByName(string keyword) {
            string sql = "SELECT * FROM Products where ProductName like '%"+ keyword + "%'";
            return Database.GetDataBySQL(sql);
        }
        public static int DeleteProduct(string id) {
            int count = 0;
            string sql = "delete from Products where ProductId = @id";
            SqlParameter param = new SqlParameter("@id", SqlDbType.NVarChar);
            param.Value = id;
            count = Database.ExecuteSQL(sql, param);
            return count;
        }
        public static int UpdateProduct(ArrayList arrayList) {
            int count = 0;
            string sql = "UPDATE [Products] " +
                "SET [ProductName] = @Name," +
                "[CategoryId] = @CatID," +
                "[Unit] = @Unit," +
                "[Price] = @Price," +
                "[Quantity] = @Quantity," +
                "[Discontinued] = @Discontinued," +
                "[CreateDate] = @CreateDate " +
                "WHERE [ProductId] = @Id";
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@Id", SqlDbType.Char),
                new SqlParameter("@Name", SqlDbType.NVarChar),
                new SqlParameter("@CatID", SqlDbType.Char),
                new SqlParameter("@Unit", SqlDbType.NVarChar),
                new SqlParameter("@Price", SqlDbType.Int),
                new SqlParameter("@Quantity", SqlDbType.Int),
                new SqlParameter("@Discontinued", SqlDbType.Bit),
                new SqlParameter("@CreateDate", SqlDbType.Date)
            };
            // Gán giá trị cho các tham số giả định
            for (int i = 0; i < arrayList.Count; i++) {
                param[i].Value = arrayList[i];
            }
            count = Database.ExecuteSQL(sql, param);
            return count;
        }
        public static int AddProduct(ArrayList arrayList) {
            int count = 0;
            string sql = "INSERT INTO Products VALUES (@Id, @Name, @CatID, @Unit, @Price, @Quantity, @Discontinued, @CreateDate)";
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@Id", SqlDbType.Char),
                new SqlParameter("@Name", SqlDbType.NVarChar),
                new SqlParameter("@CatID", SqlDbType.Char),
                new SqlParameter("@Unit", SqlDbType.NVarChar),
                new SqlParameter("@Price", SqlDbType.Int),
                new SqlParameter("@Quantity", SqlDbType.Int),
                new SqlParameter("@Discontinued", SqlDbType.Bit),
                new SqlParameter("@CreateDate", SqlDbType.Date)
            };
            // Gán giá trị cho các tham số giả định
            for (int i = 0; i < arrayList.Count; i++) {
                param[i].Value = arrayList[i];
            }
            count = Database.ExecuteSQL(sql, param);
            return count;
        }
    }
}
