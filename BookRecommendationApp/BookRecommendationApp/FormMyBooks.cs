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
        private Dictionary<Book, Panel> mapBookPanel = new Dictionary<Book, Panel>();
        public FormMyBooks()
        {
            InitializeComponent();
            load();
        }

        void load()
        {
            flowLayoutPanel1.Controls.Clear();
            mapBookPanel.Clear();

            // TODO: reload Database

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
            mapBookPanel[book] = panel;

            BookItem frmBI = new BookItem(
                book, SelectedBook, RemoveBook)
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
        private void RemoveBook(object sender, EventArgs e)
        {
            Book currentBook = sender as Book;
            Panel currentBookPanel = mapBookPanel[currentBook];

            flowLayoutPanel1.Controls.Remove(currentBookPanel);
            mapBookPanel.Remove(currentBook);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
