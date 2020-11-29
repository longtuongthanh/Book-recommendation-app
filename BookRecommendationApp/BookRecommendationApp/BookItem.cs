﻿using BookRecommendationApp.Model;
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
        public BookItem(Book book, EventHandler onSelectItem, EventHandler onAddToList)
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

            picture.MouseClick += (obj, arg) => { onSelectItem?.Invoke(book, arg); };
            labelName.MouseClick += (obj, arg) => { onSelectItem?.Invoke(book, arg); };
            button1.MouseClick += (obj, arg) => { onAddToList?.Invoke(book, arg); };
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
