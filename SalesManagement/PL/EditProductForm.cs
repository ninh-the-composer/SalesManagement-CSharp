using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
