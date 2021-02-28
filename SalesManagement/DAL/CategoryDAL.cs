using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SalesManagement.DAL
{
    class CategoryDAL
    {
        public static DataTable GetAllCategory() {
            string sql = "SELECT * FROM Categories";
            return Database.GetDataBySQL(sql);
        }
        public static DataTable SearchCategoryByName(string keyword) {
            string sql = "SELECT * FROM Categories where CategoryName like '%"+keyword+"%'";
            return Database.GetDataBySQL(sql);
        }
        public static int AddCategory(ArrayList arrayList) {
            int count = 0;
            string sql = "insert into Categories values(@catId, @catName, @desc)";
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@catId", SqlDbType.Char),
                new SqlParameter("@catName", SqlDbType.NVarChar),
                new SqlParameter("@desc", SqlDbType.NText)
            };
            // Gán giá trị cho các tham số giả định
            for(int i = 0; i < arrayList.Count; i++) {
                param[i].Value = arrayList[i];
            }
            count = Database.ExecuteSQL(sql, param);
            return count;
        }
        public static int UpdateCategory(ArrayList arrayList) {
            int count = 0;
            string sql = "update Categories set CategoryName = @catName, [Description] = @desc where CategoryId = @catId";
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@catId", SqlDbType.Char),
                new SqlParameter("@catName", SqlDbType.NVarChar),
                new SqlParameter("@desc", SqlDbType.NText)
            };
            // Gán giá trị cho các tham số giả định
            for (int i = 0; i < arrayList.Count; i++) {
                param[i].Value = arrayList[i];
            }
            count = Database.ExecuteSQL(sql, param);
            return count;
        }
        public static int DeleteCategory(string id) {
            int count = 0;
            string sql = "delete from Categories where CategoryId = @id";
            SqlParameter param = new SqlParameter("@id", SqlDbType.NVarChar);
            param.Value = id;
            count = Database.ExecuteSQL(sql, param);
            return count;
        }

        // 2. Hoàn thiện chức năng update và delete
    }
}
