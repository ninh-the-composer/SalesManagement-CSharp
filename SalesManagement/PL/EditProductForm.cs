using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SalesManagement.BL;

namespace SalesManagement.PL {
    public partial class EditProductForm : Form {


        public EditProductForm(ArrayList infomation) {
            InitializeComponent();
            txtId.Text = infomation[0].ToString();
            txtName.Text = infomation[1].ToString();
            txtCategoryId.Text = infomation[2].ToString();
            txtUnit.Text = infomation[3].ToString();
            txtPrice.Text = infomation[4].ToString();
            txtQuantity.Text = infomation[5].ToString();
            cbDiscontinued.Checked = bool.Parse(infomation[6].ToString());
            dateCreateDate.Text = infomation[7].ToString();
        }
        private bool HasExistedProduct(string id) {
            foreach (var p in Product.GetAllProduct()) {
                if (p.Id.Equals(id)) {
                    return true;
                }
            }
            return false;
        }
        private bool ValidProduct() {
            if (txtId.Text.Length == 0) {
                MessageBox.Show("Product ID cannot empty!");
                txtId.Focus();
                return false;
            }
            if (!Regex.IsMatch(txtId.Text, @"^P\d{4}$")) {
                MessageBox.Show("Product ID Invalid! Ex: P0000.");
                txtId.Focus();
                return false;
            }
            if (HasExistedProduct(txtId.Text)) {
                MessageBox.Show("Product ID Existed! Try another one.");
                txtId.Focus();
                return false;
            }
            if (txtName.Text.Length == 0) {
                MessageBox.Show("Product Name cannot empty!");
                txtName.Focus();
                return false;
            }
            // Check Category later
            if (!Regex.IsMatch(txtUnit.Text, @"^unit-\d+$")) {
                MessageBox.Show("Product ID Invalid! Ex: unit-0.");
                txtUnit.Focus();
                return false;
            }
            if (!Regex.IsMatch(txtPrice.Text, @"^\d+$") || int.Parse(txtPrice.Text) <= 0) {
                MessageBox.Show("Price must be an integer and greater zero.");
                txtPrice.Focus();
                return false;
            }
            if (!Regex.IsMatch(txtQuantity.Text, @"^\d+$") || int.Parse(txtQuantity.Text) <= 0) {
                MessageBox.Show("Quantity must be an integer and greater zero.");
                txtQuantity.Focus();
                return false;
            }
            return true;
        }
        private void btnUpdate_Click(object sender, EventArgs e) {
            string id = txtId.Text;
            string name = txtName.Text;
            string catId = txtCategoryId.Text;
            string unit = txtUnit.Text;
            string price = txtPrice.Text;
            string quantity = txtQuantity.Text;
            bool hasDiscontinued = cbDiscontinued.Checked;
            DateTime createDate = dateCreateDate.Value;
            ArrayList information = new ArrayList() { id, name, catId, unit, price, quantity, hasDiscontinued, createDate };
            if (Product.UpdateProduct(information) > 0) {
                MessageBox.Show("Update Successfuly.");
                this.Close();
            } else {
                MessageBox.Show("Update Fail.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
