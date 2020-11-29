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
    public partial class FormMyBooks : Form
    {
        public FormMyBooks()
        {
            InitializeComponent();
            load();
        }

        public void AddBook(Book book)
        {
            Panel pal = new Panel() { Width = 350, Height = 230 };
            
            // TODO
            // user
            List<string> bookList = Database.User.BookListID;
            if (!bookList.Contains(book.Name))
            {
                bookList.Add(book.Name);
                Database.EditUser();
            }
            // form
            load();
        }

        void load()
        {
            flowLayoutPanel1.Controls.Clear();

            IEnumerable<Book> bookList = Database.Books.Where(
                item => Database.User.BookListID.Contains(item.Name)
                );
            foreach (Book book in bookList)
            {
                Panel pal = new Panel()
                {
                    Width = 350,
                    Height = 230
                };

                ApplyBookItem(pal, book);
                flowLayoutPanel1.Controls.Add(pal);
            }
        }

        public void ApplyBookItem(Panel panel, Book book)
        {
            BookItem frmBI = new BookItem(
                book, SelectedBook, null)
            { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmBI.FormBorderStyle = FormBorderStyle.None;
            panel.Controls.Add(frmBI);
            frmBI.Show();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
