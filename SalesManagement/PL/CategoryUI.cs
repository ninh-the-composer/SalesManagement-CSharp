using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SalesManagement.BL;
using System.Text.RegularExpressions;
using System.Collections;

namespace SalesManagement {
    public partial class Form1 : Form {
        bool addNew;
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            // Load du lieu vao dgvCategory
            dgvCategory.DataSource = Category.GetAllCategory();
            // Disable các TextBox trên Form
            txtId.Enabled = false;
            txtName.Enabled = false;
            txtDescription.Enabled = false;
        }
        private void RefreshCategory() {
            dgvCategory.DataSource = null;
            dgvCategory.DataSource = Category.GetAllCategory();
        }
        // Sự kiện khi người dùng kích chuột vào 1 dòng trong dgvCategory
        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e) {
            if(dgvCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null) {
                dgvCategory.CurrentRow.Selected = true;
                txtId.Text = dgvCategory.Rows[e.RowIndex].Cells["CategoryId"].Value.ToString();
                txtName.Text = dgvCategory.Rows[e.RowIndex].Cells["CategoryName"].Value.ToString();
                txtDescription.Text = dgvCategory.Rows[e.RowIndex].Cells["Description"].Value.ToString();

                // Enable 2 TextBox: Name và Description
                txtId.Enabled = false;
                txtName.Enabled = true;
                txtDescription.Enabled = true;
                addNew = false;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e) {
            addNew = true;
            txtId.Text = "";
            txtId.Focus();
            txtId.Enabled = true;
            txtName.Text = "";
            txtDescription.Text = "";
        }
        private bool HasExistedCategory(string id) {
            foreach(Category c in Category.GetAllCategory()) {
                if (c.CategoryId.Equals(id)) {
                    return true;
                }
            }
            return false;
        }
        private bool ValidCategory() {
            if(txtId.Text.Length == 0) {
                MessageBox.Show("Category ID cannot empty!");
                txtId.Focus();
                return false;
            }
            if (!Regex.IsMatch(txtId.Text, @"^C\d{4}$")) {
                MessageBox.Show("Category ID Invalid! Ex: C0000.");
                txtId.Focus();
                return false;
            }
            if (HasExistedCategory(txtId.Text)) {
                MessageBox.Show("Category ID Existed! Try another one.");
                txtId.Focus();
                return false;
            }
            return true;
        }
        
        private void btnSave_Click(object sender, EventArgs e) {
            ArrayList arrayList = new ArrayList() { txtId.Text, txtName.Text, txtDescription.Text };
            if (addNew) {
                // Nếu có lỗi thì cảnh báo
                if (!ValidCategory()) return;

                // Thực hiện thêm mới dữ liệu
                if(Category.AddCategory(arrayList) > 0) {
                    MessageBox.Show("A Category Added!");
                }
            } else { // Cập nhật dữ liệu của Category
                if (Category.UpdateCategory(arrayList) > 0) {
                    MessageBox.Show("A Category Updated!");
                }
            }
            // Refresh lại dgvCategory
            RefreshCategory();
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if(txtId.Text.Trim() == "") {
                MessageBox.Show("You must choose an Category.");
                txtId.Focus();
                return;
            }
            if (!HasExistedCategory(txtId.Text)) {
                MessageBox.Show("Category doesn't exist! Try another one.");
                txtId.Focus();
                return;
            }

            DialogResult res = MessageBox.Show("Are you sure you want to Delete", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            switch (res) {
                case DialogResult.OK:
                    if (Category.DeleteCategory(txtId.Text) > 0) {
                        MessageBox.Show("A Category Deleted!");
                    }
                    break;
                case DialogResult.Cancel:
                    return;
            }
            // Refresh lại dgvCategory
            RefreshCategory();
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            dgvCategory.DataSource = null;
            dgvCategory.DataSource = Category.SearchCategoryByName(txtSearch.Text);
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }
    }
}
