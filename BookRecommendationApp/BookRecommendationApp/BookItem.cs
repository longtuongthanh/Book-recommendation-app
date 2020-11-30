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
    public partial class BookItem : Form
    {
        public BookItem(Book book, EventHandler onSelectItem, EventHandler onAction)
        {
            InitializeComponent();
            
            labelName.Text = book.Name;
            labelAuthor.Text = "bởi " + book.Author;
            labelDesc.Text = book.Description;

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
                button1.MouseClick += (obj, arg) => Database.User.RemoveFromBookList(book);
                button1.BackColor = Color.Crimson;
            }
            else
            {
                button1.Text = "Thêm";
                button1.MouseClick += (obj, arg) => Database.User.AddToBookList(book);
                button1.BackColor = Color.RoyalBlue;
            }
            #endregion

            picture.MouseClick += (obj, arg) => { onSelectItem?.Invoke(book, arg); };
            labelName.MouseClick += (obj, arg) => { onSelectItem?.Invoke(book, arg); };
            button1.MouseClick += (obj, arg) => { onAction?.Invoke(book, arg); };
        }
    }
}
