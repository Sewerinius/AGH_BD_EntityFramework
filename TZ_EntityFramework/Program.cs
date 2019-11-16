using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TZ_EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            using (var db = new ProdContext())
            {
                //Console.WriteLine("Category name: ");
                //string name = Console.ReadLine();
                //db.Categories.Add(new Category { Name = name });
                //db.SaveChanges();

                var categories = from c in db.Categories
                                 orderby c.Name descending
                                 select c;
                foreach (var category in categories)
                {
                    Console.WriteLine(category.Name);
                    //db.Products.Add(new Product { Name = "Temp", Category = category });
                }
                //db.SaveChanges();

                //CategoryForm form = new CategoryForm();
                MainForm form = new MainForm();
                form.ShowDialog();
            }
        }
    }
}
