using SalesManagement.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesManagement.PL {
    public partial class ProductGUI : Form {
        private bool HasSearched;
        public ProductGUI() {
            InitializeComponent();
        }

        private void RefreshTable(List<Product> productList) {
            dgvProduct.Rows.Clear();
            foreach (var p in productList) {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvProduct);
                row.Cells[1].Value = p.Id;
                row.Cells[2].Value = p.Name;
                row.Cells[3].Value = p.CatId;
                row.Cells[4].Value = p.Unit;
                row.Cells[5].Value = p.Price;
                row.Cells[6].Value = p.Quantity;
                row.Cells[7].Value = p.HasDiscontinued;
                row.Cells[8].Value = p.CreateDate.ToString("M/dd/yyyy", CultureInfo.InvariantCulture);
                row.Cells[9].Value = "Edit";
                row.Cells[10].Value = "Delete";
                dgvProduct.Rows.Add(row);
            }
        }
        private void ProductGUI_Load(object sender, EventArgs e) {
            RefreshTable(Product.GetAllProduct());
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            HasSearched = txtSearch.Text.Trim().Length != 0;
            RefreshTable(Product.SearchProductByName(txtSearch.Text.Trim()));
        }
        private void btnAdd_Click(object sender, EventArgs e) {
            AddProductForm addProductForm = new AddProductForm();
            addProductForm.ShowDialog();
            RefreshTable(Product.GetAllProduct());
        }
        private void btnDelete_Click(object sender, EventArgs e) {
            int count = 0;
            string message = "Delete ";
            bool hasDeleted = false;
            foreach (DataGridViewRow row in dgvProduct.Rows) {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true) {
                    count++;
                    string pId = row.Cells["ProductId"].Value.ToString();
                    hasDeleted = Product.DeleteProduct(pId) > 0;
                    if (hasDeleted) {
                        message += pId + " ";
                    }
                }
            }

            if (hasDeleted) {
                MessageBox.Show(message + " Successful.");
                RefreshTable(Product.GetAllProduct());
            } else {
                MessageBox.Show(message + " Fail.");
            }
        }
        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e) {
            
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) {
                switch (e.ColumnIndex) {
                    case 9:
                        ArrayList information = new ArrayList();
                        for(int i = 0; i < dgvProduct.Columns.Count - 2; i++) {
                            information.Add(dgvProduct.Rows[e.RowIndex].Cells[i].Value.ToString());
                        }

                        EditProductForm editProductForm = new EditProductForm(information);
                        editProductForm.ShowDialog();
                        if (HasSearched) {
                            RefreshTable(Product.SearchProductByName(txtSearch.Text.Trim()));
                        } else {
                            RefreshTable(Product.GetAllProduct());
                        }
                        break;
                    case 10:
                        DialogResult result = MessageBox.Show("Are you sure want to Delete " + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if(result == DialogResult.OK) {
                            if(Product.DeleteProduct(dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString()) > 0) {
                                MessageBox.Show("Delete Successfuly!");
                                if (HasSearched) {
                                    RefreshTable(Product.SearchProductByName(txtSearch.Text.Trim()));
                                } else {
                                    RefreshTable(Product.GetAllProduct());
                                }
                            } else {
                                MessageBox.Show("Delete Fail!");
                            }
                        }
                        break;
                }
            }
        }

    }
}
