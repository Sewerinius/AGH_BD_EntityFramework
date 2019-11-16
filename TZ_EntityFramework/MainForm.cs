using System;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace TZ_EntityFramework
{
    public partial class MainForm : Form
    {
        ProdContext db;
        BindingSource CurrentCustomerBinding;
        Customer noneCustomer;
        public MainForm()
        {
            InitializeComponent();
            CurrentCustomerBinding = new BindingSource { DataSource = typeof(Customer) };
            Binding labelBinding = new Binding("Text", CurrentCustomerBinding, "CompanyName");
            noneCustomer = new Customer { CompanyName = "None" };
            labelBinding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            {
                convertEventArgs.Value = "Selected company: " + convertEventArgs.Value;
            };
            label2.DataBindings.Add(labelBinding);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            db = new ProdContext();
            db.Categories.Load();
            db.Products.Load();
            db.Customers.Load();
            db.Orders.Load();
            categoryBindingSource.DataSource = db.Categories.Local.ToBindingList();
            customerBindingSource.DataSource = db.Customers.Local.ToBindingList();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
        }

        private void DbSaveChanges(object sender, EventArgs e)
        {
            db.SaveChanges();
        }

        private void categoryDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("CategorySelectionChanged");
            //Console.WriteLine(categoryDataGridView.SelectedRows.Count.ToString());
            if (categoryDataGridView.SelectedRows.Count == 1 && categoryDataGridView.SelectedRows[0].Index != categoryDataGridView.RowCount - 1)
            {
                bindingNavigatorAddNewItem1.Enabled = true;
                Category SelectedCategory = (Category)categoryDataGridView.SelectedRows[0].DataBoundItem;
                //productBindingSource.DataSource = new BindingList<Product>(db.Products.Local.Where(product => product.CategoryID == SelectedCategory.CategoryID).ToList());
                BindingList<Product> bindingList = new BindingList<Product>(SelectedCategory.Products);
                productBindingSource.DataSource = bindingList;
            }
            else
            {
                bindingNavigatorAddNewItem1.Enabled = false;
                productBindingSource.DataSource = null;
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            Category newCategory = new Category();
            using (var db2 = new ProdContext())
            {
                db2.Categories.Add(newCategory);
                db2.SaveChanges();
            }
            db.Categories.Load();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            //TODO: Generalize with Tag property
            foreach (DataGridViewRow row in categoryDataGridView.SelectedRows)
            {
                db.Categories.Remove((Category)row.DataBoundItem);
            }
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in productDataGridView.SelectedRows)
            {
                Product product = (Product)row.DataBoundItem;
                productDataGridView.Rows.Remove(row);
                db.Products.Remove(product);
            }
        }

        private void bindingNavigatorAddNewItem1_Click(object sender, EventArgs e)
        {
            Category category = (Category)categoryDataGridView.SelectedRows[0].DataBoundItem;
            Product newProduct = new Product { CategoryID = category.CategoryID };
            using (var db2 = new ProdContext())
            {
                db2.Products.Add(newProduct);
                db2.SaveChanges();
            }
            db.Products.Attach(newProduct);
            productBindingSource.ResetBindings(false);
        }

        private void productDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("ProductSelectionChanged");
            //Console.WriteLine(productDataGridView.SelectedRows.Count.ToString());
            if (productDataGridView.SelectedRows.Count == 1 && productDataGridView.SelectedRows[0].Index != productDataGridView.RowCount - 1)
            {
                toolStripButton6.Enabled = true;
                Product SelectedProduct = (Product) productDataGridView.SelectedRows[0].DataBoundItem;
                orderProductBindingSource.DataSource = new BindingList<Order>(db.Orders.Local.Where(order => order.ProductID == SelectedProduct.ProductID).ToList());
            }
            else
            {
                toolStripButton6.Enabled = false;
                orderProductBindingSource.DataSource = null;
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            Customer newCustomer = new Customer();
            db.Customers.Add(newCustomer);
        }

        private void customerDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("CustomerSelectionChanged");
            //Console.WriteLine(productDataGridView.SelectedRows.Count.ToString());
            if (customerDataGridView.SelectedRows.Count == 1 && customerDataGridView.SelectedRows[0].Index != customerDataGridView.RowCount - 1)
            {
                Customer SelectedCustomer = (Customer) customerDataGridView.SelectedRows[0].DataBoundItem;
                CurrentCustomerBinding.DataSource = SelectedCustomer;
                orderClientBindingSource.DataSource = new BindingList<Order>(SelectedCustomer.Orders);
            }
            else
            {
                //CurrentCustomerBinding.DataSource = typeof(Customer);
                CurrentCustomerBinding.DataSource = noneCustomer;
                orderClientBindingSource.DataSource = null;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Customer customer = (Customer) CurrentCustomerBinding.DataSource;
            if (customer == null || customer == noneCustomer)
            {
                MessageBox.Show("Musisz wybrać klienta przed dodaniem zamówienia.");
                return;
            }
            Product product = (Product) productDataGridView.SelectedRows[0].DataBoundItem;
            int quantity = (int) numericUpDown1.Value;
            if (product.UnitsInStock < quantity)
            {
                MessageBox.Show("Niewystarczająca liczba produktów w magazynie.");
                return;
            }
            Order newOrder = product.placeOrder(customer, quantity);
            db.Orders.Add(newOrder);
            orderProductBindingSource.DataSource = new BindingList<Order>(db.Orders.Local.Where(order => order.ProductID == product.ProductID).ToList());
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in orderProductGridView.SelectedRows)
            {
                db.Orders.Remove((Order)row.DataBoundItem);
            }
        }
    }
}
