using SalesManagement.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SalesManagement.BL {
    class Product {
        private string id;
        private string name;
        private string catId;
        private string unit;
        private int price;
        private int quantity;
        private bool hasDiscontinued;
        private DateTime createDate;

        public Product() { }
        public Product(string id, string name, string catId, string unit, int price, int quantity, bool hasDiscontinued, DateTime createDate) {
            this.Id = id;
            this.Name = name;
            this.CatId = catId;
            this.Unit = unit;
            this.Price = price;
            this.Quantity = quantity;
            this.HasDiscontinued = hasDiscontinued;
            this.CreateDate = createDate;
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string CatId { get => catId; set => catId = value; }
        public string Unit { get => unit; set => unit = value; }
        public int Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public bool HasDiscontinued { get => hasDiscontinued; set => hasDiscontinued = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        
        public static List<Product> GetAllProduct() {
            List<Product> products = new List<Product>();
            DataTable dataTable = ProductDAL.GetAllProduct();
            foreach (DataRow dr in dataTable.Rows) {
                string id = dr["ProductId"].ToString();
                string name = dr["ProductName"].ToString();
                string catId = dr["CategoryId"].ToString();
                string unit = dr["Unit"].ToString();
                int price = int.Parse(dr["Price"].ToString());
                int quantity = int.Parse(dr["Quantity"].ToString());
                bool hasDiscontinued = bool.Parse(dr["Discontinued"].ToString());
                DateTime createDate = DateTime.Parse(dr["CreateDate"].ToString());
                Product product = new Product(id, name, catId, unit, price, quantity, hasDiscontinued, createDate);
                products.Add(product);
            }
            return products;
        }
        public static int DeleteProduct(string id) {
            return ProductDAL.DeleteProduct(id);
        }
        public static int UpdateProduct(ArrayList information) {
            return ProductDAL.UpdateProduct(information);
        }
        public static int AddProduct(ArrayList information) {
            return ProductDAL.AddProduct(information);
        }
    }
}
