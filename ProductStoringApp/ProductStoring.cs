using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductStoringApp.BLL;
using ProductStoringApp.DAL.DAO;

namespace ProductStoringApp
{
    public partial class ProductStoringUI : Form
    {
        private ProductBll aProductBll;
        private Product aProduct;

        public ProductStoringUI()
        {
            InitializeComponent();
            aProductBll=new ProductBll();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            aProduct = new Product(codeTextBox.Text,nameTextBox.Text,Convert.ToInt32(quantityTextBox.Text));
            string msg = aProductBll.Save(aProduct);
            MessageBox.Show(msg);
        }

        private void viewAllButton_Click(object sender, EventArgs e)
        {
            aProduct = new Product();
            List<Product> products = aProductBll.GetAllProduct(aProduct);
            productListView.Items.Clear();
            foreach (Product product in products)
            {
                ListViewItem lvi = new ListViewItem(product.Code);
                lvi.SubItems.Add(product.Name);
                lvi.SubItems.Add(product.Quantity.ToString());
                productListView.Items.Add(lvi);
            }
            totalQuantityTextBox.Text = aProductBll.GetTotalQuantity().ToString();

        }
    }
}
