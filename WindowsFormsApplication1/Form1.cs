using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        NorthwindDataContext db = new NorthwindDataContext();
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var customers = from c in db.Customers select c;

            var query1 = from p in db.Products
                         select p;

            //Select productID, ProductName, unitPrice from products
            var query2 = from p in db.Products
                         select new
                         {
                             p.ProductID,
                             p.ProductName,
                             p.UnitPrice
                         };

            //Select  *  from products where ProductID = 1;
            var query3 = from p in db.Products
                         where p.ProductID == 1
                         select p;

            //Select  *  from products where supplierID = 5 and unitPrice > 20;
            var query4 = from p in db.Products
                         where p.SupplierID == 5 && p.UnitPrice > 20
                         select p;

            //Select  *  from products where supplierID = 5 OR p.SupplierID = 6;
            var query5 = from p in db.Products
                         where p.SupplierID == 5 || p.SupplierID == 6
                         select p;

            //Select  *  from products order by productID dec
            var query6 = from p in db.Products
                         orderby p.ProductID descending
                         select p;

            //Select  *  from products order by categoryID , unitPrice desc
            var query7 = from p in db.Products
                         orderby p.CategoryID ascending, p.UnitPrice descending
                         select p;

            //Select  top 10  from products
            var query8 = (from p in db.Products
                          select p).Take(10);

            //Returns 1st record
            var query9 = (from p in db.Products
                          select p).First();


            //Select  top 10  from products order by pID
            var query10 = (from p in db.Products
                           orderby p.ProductID
                           select p).Take(10);

            //Select  distinct  categoryID from products 
            var query11 = (from p in db.Products
                           select p.CategoryID).Distinct();

            //Select categoryID , count(categoryID) As newfield from products 
            var query12 = from p in db.Products
                          group p by p.CategoryID into g
                          select new
                          {
                              CategoryId = g.Key,
                              NewField = g.Count()
                          };

            //Select categoryID , avg(unitprice) as newfield from products groupBy categoryID
            var query13 = from p in db.Products
                          group p by p.CategoryID into g
                          select new
                          {
                              CategoryId = g.Key,
                              NewField = g.Average(k => k.UnitPrice)
                          };

            //Select categoryID , sum(unitprice) as newfield from products groupBy categoryID
            var query14 = from p in db.Products
                          group p by p.CategoryID into g
                          select new
                          {
                              CategoryId = g.Key,
                              NewField = g.Sum(k => k.UnitPrice)
                          };

            //Select * from productswhere categoryID = 1 Union select * from products where categoryID = 2
            var query15 = (from p in db.Products
                           where p.CategoryID == 1
                           select p)
                         .Union
                        (from m in db.Products
                         where m.CategoryID == 2
                         select m);


            //Select A.ProductID, A.productName, B.CategoryID, B.CtegoryName 
            //from products A, Categories B
            // where A.categoryID = B.categoryID and supplierId = 1 
            var query16 = from p in db.Products
                          from m in db.Categories
                          where p.CategoryID == m.CategoryID
                           && p.SupplierID == 1
                          select new
                          {
                              p.ProductID,
                              p.ProductName,
                              m.CategoryID,
                              m.CategoryName
                          };

            //Select * from products where productName like 'A%'
            var query17 = from p in db.Products
                          where p.ProductName.StartsWith("A")
                          select p;

            //Select * from products where productName like 'A%' and customerName like 'P%';
            var query18 = from c in db.Customers
                          from p in db.Products
                          where c.ContactName.StartsWith("A")
                           && p.ProductName.StartsWith("P")
                          select new
                          {
                              name = c.ContactName,
                              p.ProductName
                          };

            //UPDATE 
            /*
            var cc = from c in db.Customers
                     where c.City.StartsWith("Paris")
                     select c;
            foreach (var cust in cc)
                cust.City = "PARIS";
            db.SubmitChanges();
            var cd = from c in db.Customers
                     where c.City.StartsWith("Paris")
                     select c;
            */

            //INSERT
            /*
            Product newProduct = new Product();
            newProduct.ProductName = "RC helicopter";
            db.Products.InsertOnSubmit(newProduct);
            db.SubmitChanges();
            */

            //DELETE
            /*
            var pp = from p in db.Products
                     where p.ProductName.Contains("helicopter")  //  where p.ProductName.Equals("helicopter")
                     select p;
            db.Products.DeleteAllOnSubmit(pp);
            db.SubmitChanges();
            */

            dataGridView1.DataSource = query1.ToList();
        }

    }
}
