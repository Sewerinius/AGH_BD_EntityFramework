using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace TZ_EntityFramework
{
    public partial class CategoryForm : Form
    {
        ProdContext db;
        public CategoryForm()
        {
            
            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e)
        {
            db = new ProdContext();
            db.Categories.Load();
            this.categoryBindingSource.DataSource = db.Categories.Local.ToBindingList();
        }

        private void saveCategories(object sender, EventArgs e)
        {
            db.SaveChanges();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            db.Categories.Add(new Category());
            db.SaveChanges();
            categoryDataGridView.Update();
            categoryDataGridView.Refresh();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            foreach(var Category in categoryDataGridView.SelectedRows)
            {
                Console.WriteLine(Category.ToString());
            }
        }

        private void CategoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
        }
    }
}
