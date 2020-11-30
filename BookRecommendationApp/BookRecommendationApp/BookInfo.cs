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
    public partial class BookInfo : Form
    {
        public BookInfo(Book book)
        {
            InitializeComponent();

            labelName.Text = book.Name;
            labelAuthor.Text = "bởi " + book.Author;
            labelDesc.Text = book.Description;
            foreach (var item in book.Tags)
            {
                Label tag = new Label();
                tag.Text = item;
                tag.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                tag.AutoSize = true;
                flowLayoutPanel1.Controls.Add(tag);
                tag.Show();
            }

            Picture pic = book.GetPicture();

            if (pic.GetImage() != null)
                picture.Image = pic.GetImage();
            else
                ; // TODO: use default picture

            #region Action based on bookList.
            // Action is Remove if book is in booklist
            // Action is Add if book is not in booklist
            List<string> bookList = Database.User.BookListID;
            if (bookList.Contains(book.Name))
            {
                button1.Text = "Xóa";
                button1.MouseClick += (obj, arg) => Database.User.RemoveBook(book);
                button1.BackColor = Color.Crimson;
            }
            else
            {
                button1.Text = "Thêm";
                button1.MouseClick += (obj, arg) => Database.User.AddBook(book);
                button1.BackColor = Color.RoyalBlue;
            }
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Panel panelLoad = (this.Parent as Panel);

            foreach (Control item in panelLoad.Controls)
                item.Dispose();

            panelLoad.Controls.Clear();

            FormMyBooks frmBI = new FormMyBooks() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Book book = sender as Book;
            frmBI.FormBorderStyle = FormBorderStyle.None;
            panelLoad.Controls.Add(frmBI);
            frmBI.Show();
        }
    }
}
