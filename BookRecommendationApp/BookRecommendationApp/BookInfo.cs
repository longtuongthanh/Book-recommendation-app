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
        private Book currentBook;
        public BookInfo(Book book)
        {
            InitializeComponent();

            currentBook = book;

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
                button1.MouseClick += (obj, arg) =>
                {
                    Database.User.RemoveFromBookList(book);
                    button1_Click(book, arg); ;
                };
                button1.BackColor = Color.Crimson;
            }
            else
            {
                button1.Text = "Thêm";
                button1.MouseClick += (obj, arg) =>
                {
                    Database.User.AddToBookList(book);
                    button1_Click(book, arg);
                };
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
            Book book = currentBook;
            frmBI.FormBorderStyle = FormBorderStyle.None;
            panelLoad.Controls.Add(frmBI);
            frmBI.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!Database.User.LikeListID.Contains(currentBook.Name))
            {
                Database.User.AddToLikeList(currentBook);

                this.pictureBox2.Image = Properties.Resources.Dislike_disabled_;
                this.pictureBox1.Image = Properties.Resources.Like;
            }
            else
            {
                Database.User.RemoveFromLikeAndDislikeList(currentBook);

                this.pictureBox1.Image = Properties.Resources.Like_disabled_;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!Database.User.DislikeListID.Contains(currentBook.Name))
            {
                Database.User.AddToLikeList(currentBook);

                this.pictureBox2.Image = Properties.Resources.Dislike;
                this.pictureBox1.Image = Properties.Resources.Like_disabled_;
            }
            else
            {
                Database.User.RemoveFromLikeAndDislikeList(currentBook);

                this.pictureBox2.Image = Properties.Resources.Dislike_disabled_;
            }
        }

        private void panel3_SizeChanged(object sender, EventArgs e)
        {
            labelDesc.MaximumSize = new Size(panel3.Size.Width - 24, 0);
        }


        private void panel2_Resize(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmt c = new cmt();
            c.hienthi(textBox1.Text);
            flowLayoutPanel2.Controls.Add(c);
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = System.Drawing.SystemColors.InfoText;
        }
    }
}
