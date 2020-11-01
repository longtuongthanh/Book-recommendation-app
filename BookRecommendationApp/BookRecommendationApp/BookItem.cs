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
        public BookItem(Book book, EventHandler onSelectItem)
        {
            InitializeComponent();
            
            labelName.Text = book.Name;
            labelAuthor.Text = "bởi " + book.Author;
            labelDesc.Text = book.Description;

            Picture pic = new Picture(book.PictureFile);
            if (pic.GetImage() == null)
            {
                // Get image from database
                pic.Content = Firebase.Ins.LoadPicture(pic.FilePath);

                // save image to file
                pic.SaveImage();
            }

            if (pic.GetImage() != null)
                picture.Image = pic.GetImage();
            else
                ; // TODO: use default picture

            picture.MouseClick += (obj, arg) => { onSelectItem(book, arg); };
        }
    }
}
