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
            foreach (string bookname in Database.User.BookListID)
            {
                Book b = Database.Books.Find((tbook) => { return tbook.Name == bookname; });
            }
        }

        public void AddBook(Book book)
        {
            Panel pal = new Panel() { Width = 350, Height = 230 };



            // TODO
            // user
            Database.User.BookListID.Add(book.Name);
            // form
        }
        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
