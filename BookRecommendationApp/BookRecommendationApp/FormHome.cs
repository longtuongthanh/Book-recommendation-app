using BookRecommendationApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRecommendationApp
{
    public partial class FormHome : Form
    {
        public FormHome()
        {
            InitializeComponent();

            BookItem frmBI1 = new BookItem(
                Database.Books.FirstOrDefault(), SelectedBook, AddBook)
                { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmBI1.FormBorderStyle = FormBorderStyle.None;
            panel1.Controls.Add(frmBI1);
            frmBI1.Show();

            BookItem frmBI2 = new BookItem(
                Database.Books.FirstOrDefault(), SelectedBook, AddBook)
                { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmBI2.FormBorderStyle = FormBorderStyle.None;
            panel2.Controls.Add(frmBI2);
            frmBI2.Show();
            
            BookItem frmBI3 = new BookItem(
                Database.Books.FirstOrDefault(), SelectedBook, AddBook)
            { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmBI3.FormBorderStyle = FormBorderStyle.None;
            panel3.Controls.Add(frmBI3);
            frmBI3.Show();

            BookItem frmBI4 = new BookItem(
                Database.Books.FirstOrDefault(), SelectedBook, AddBook)
                { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmBI4.FormBorderStyle = FormBorderStyle.None;
            panel4.Controls.Add(frmBI4);
            frmBI4.Show();

            ApplyBookItem(panel5, Database.Books.FirstOrDefault());
        }

        private void ApplyBookItem(Panel panel, Book book)
        {
            BookItem frmBI = new BookItem(
                book, SelectedBook, AddBook)
            { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmBI.FormBorderStyle = FormBorderStyle.None;
            panel.Controls.Add(frmBI);
            frmBI.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SelectedBook(object sender, EventArgs e)
        {
            Panel panelLoad = (this.Parent as Panel);

            foreach (Control item in panelLoad.Controls)
                item.Dispose();

            panelLoad.Controls.Clear();

            BookInfo frmBI = new BookInfo(sender as Book) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmBI.FormBorderStyle = FormBorderStyle.None;
            panelLoad.Controls.Add(frmBI);
            frmBI.Show();
        }
        private void AddBook(object sender, EventArgs e)
        {
            Panel panelLoad = (this.Parent as Panel);

            foreach (Control item in panelLoad.Controls)
                item.Dispose();

            panelLoad.Controls.Clear();

            FormMyBooks frmBI = new FormMyBooks() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmBI.AddBook(sender as Book);
            frmBI.FormBorderStyle = FormBorderStyle.None;
            panelLoad.Controls.Add(frmBI);
            frmBI.Show();
        }
    }
}
