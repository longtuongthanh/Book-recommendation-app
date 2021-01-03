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
        List<Comment> cmtList = new List<Comment>();

        Action<Book> refreshOnUpdate;
        public BookInfo(Book book)
        {
            InitializeComponent();

            deltaDistanceP5_F3 = flowLayoutPanel3.Bottom - panel5.Top;

            currentBook = book;
            LoadCurrentBookInfo();
        }
        private void Form_Closing(object sender, EventArgs e)
        {
            Firebase.Ins.onBookUpdate -= refreshOnUpdate;
        }
        private void ClearCurrentBookInfo()
        {
            button1.Click -= onButton1Click;
            button2.Click -= onButton2Click;
            picture.Image = BookRecommendationApp.Properties.Resources.book;
        }
        EventHandler onButton1Click;
        EventHandler onButton2Click;
        private void LoadCurrentBookInfo()
        {
            labelName.Text = currentBook.Name;
            labelAuthor.Text = "bởi " + currentBook.Author;
            labelDesc.Text = currentBook.Description;
            foreach (var item in currentBook.Tags)
            {
                Label tag = new Label();
                tag.Text = item;
                tag.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                tag.AutoSize = true;
                flowLayoutPanel1.Controls.Add(tag);
                tag.Show();
            }
            cmtList = currentBook.Comment;
            foreach (var item in currentBook.Comment)
            {
                cmt c = new cmt();
                c.hienthi(item);
                flowLayoutPanel2.Controls.Add(c);


            }
            Picture pic = currentBook.GetPicture();

            if (pic.GetImage() != null)
                picture.Image = pic.GetImage();
            else
                ; // TODO: use default picture

            #region Action based on bookList.
            // Action is Remove if book is in booklist
            // Action is Add if book is not in booklist
            List<string> bookList = Database.User.BookListID;
            if (bookList.Contains(currentBook.Name))
            {
                button1.Text = "Xóa";
                onButton1Click = (obj, arg) =>
                {
                    Database.User.RemoveFromBookList(currentBook);
                    button1_Click(currentBook, arg); ;
                };
                button1.Click += onButton1Click;
                button1.BackColor = Color.Crimson;
            }
            else
            {
                button1.Text = "Thêm";
                onButton2Click = (obj, arg) =>
                {
                    Database.User.AddToBookList(currentBook);
                    button1_Click(currentBook, arg);
                };
                button1.Click += onButton2Click;
                button1.BackColor = Color.RoyalBlue;
            }
            #endregion

            #region Like / Dislike initial type
            if (Database.User.LikeListID.Contains(currentBook.Name))
            {
                this.pictureBox2.Image = Properties.Resources.Like;
            }
            else
            {
                this.pictureBox2.Image = Properties.Resources.Like_disabled_;
            }

            if (Database.User.DislikeListID.Contains(currentBook.Name))
            {
                this.pictureBox1.Image = Properties.Resources.Dislike;
            }
            else
            {
                this.pictureBox1.Image = Properties.Resources.Dislike_disabled_;
            }
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainMenu owner = Parent.Parent.Parent as MainMenu;
            owner.ClearPanelLoad();

            FormMyBooks frmBI = new FormMyBooks() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Book book = currentBook;
            frmBI.FormBorderStyle = FormBorderStyle.None;
            owner.panelLoad.Controls.Add(frmBI);
            frmBI.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!Database.User.LikeListID.Contains(currentBook.Name))
            {
                Database.User.AddToLikeList(currentBook);

                this.pictureBox1.Image = Properties.Resources.Dislike_disabled_;
                this.pictureBox2.Image = Properties.Resources.Like;
            }
            else
            {
                Database.User.RemoveFromLikeAndDislikeList(currentBook);

                this.pictureBox2.Image = Properties.Resources.Like_disabled_;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!Database.User.DislikeListID.Contains(currentBook.Name))
            {
                Database.User.AddToDislikeList(currentBook);

                this.pictureBox1.Image = Properties.Resources.Dislike;
                this.pictureBox2.Image = Properties.Resources.Like_disabled_;
            }
            else
            {
                Database.User.RemoveFromLikeAndDislikeList(currentBook);

                this.pictureBox1.Image = Properties.Resources.Dislike_disabled_;
            }
        }

        private void panel3_SizeChanged(object sender, EventArgs e)
        {
            labelDesc.MaximumSize = new Size(panel3.Size.Width - 24, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Merge conflict

            FormRP frmRP = new FormRP(currentBook);
            frmRP.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Comment cmt = new Comment { Content = textBox1.Text, Uid = Database.User.Uid };
            cmtList.Add( cmt);

            cmt c = new cmt();
            c.hienthi(cmt);
            flowLayoutPanel2.Controls.Add(c);

            textBox1.Text = "";
            textBox1.ForeColor = System.Drawing.SystemColors.InfoText;

            currentBook.Comment = cmtList;
            Database.Edit(currentBook);
        }
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = System.Drawing.SystemColors.InfoText;
        }
        int? deltaDistanceP5_F3 = null;
        void MaintainDistanceOfPanel5AndFlowPanel3()
        {
            if (deltaDistanceP5_F3 != null)
            panel5.Top = flowLayoutPanel3.Bottom - deltaDistanceP5_F3.Value;
        }

        private void BookInfo_Load(object sender, EventArgs e)
        {
            refreshOnUpdate = (item) =>
            {
                Action<Book> action = (temp) =>
                {
                    if (temp.Name == currentBook.Name)
                    {
                        currentBook = temp;
                        ClearCurrentBookInfo();
                        LoadCurrentBookInfo();
                    }
                };
                // Cross-thread action
                this.Invoke(action, item);
            };
            Firebase.Ins.onBookUpdate += refreshOnUpdate;
        }
        private void flowLayoutPanel3_Resize(object sender, EventArgs e)
        {
            MaintainDistanceOfPanel5AndFlowPanel3();
            labelDesc.MaximumSize = new System.Drawing.Size(flowLayoutPanel3.Width, 0);
        }
    }
}
