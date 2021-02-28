using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SalesManagement.DAL;
using System.Collections;

namespace SalesManagement.BL
{
    class Category
    {
        private string categoryId;
        private string categoryName;
        private string description;

        public Category(string categoryId, string categoryName, string description)
        {
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.Description = description;
        }

        public string CategoryId { get => categoryId; set => categoryId = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public string Description { get => description; set => description = value; }

        public static List<Category> GetAllCategory()
        {
            List<Category> categories = new List<Category>();
            DataTable dataTable = CategoryDAL.GetAllCategory();
            foreach(DataRow dr in dataTable.Rows)
            {
                string catId = dr["CategoryId"].ToString();
                string catName = dr["CategoryName"].ToString();
                string description = dr["Description"].ToString();
                Category category = new Category(catId, catName, description);
                categories.Add(category);
            }
            return categories;
        }
        public static List<Category> SearchCategoryByName(string keyword) {
            List<Category> categories = new List<Category>();
            DataTable dataTable = CategoryDAL.SearchCategoryByName(keyword);
            foreach (DataRow dr in dataTable.Rows) {
                string catId = dr["CategoryId"].ToString();
                string catName = dr["CategoryName"].ToString();
                string description = dr["Description"].ToString();
                Category category = new Category(catId, catName, description);
                categories.Add(category);
            }
            return categories;
        }

        public static int AddCategory(ArrayList arrayList) {
            return CategoryDAL.AddCategory(arrayList);
        }
        public static int UpdateCategory(ArrayList arrayList) {
            return CategoryDAL.UpdateCategory(arrayList);
        }
        public static int DeleteCategory(string id) {
            return CategoryDAL.DeleteCategory(id);
        }
    }
    
}
