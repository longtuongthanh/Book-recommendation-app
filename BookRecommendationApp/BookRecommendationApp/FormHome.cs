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
    public partial class FormHome : Form
    {
        Action<Book> refreshOnUpdate;
        public FormHome()
        {
            InitializeComponent();
            LoadBooks();
        }
        public void LoadBooks ()
        {
            for (int i = 0; i<Database.Books.Count; i++)
            {


                Panel pal = new Panel()
                {
                    Width = 350,
                    Height = 230
                };


                ApplyBookItem(pal, Database.Books[i]);
                flowLayoutPanel1.Controls.Add(pal);
                
            }    
        }
        public void ClearBooks()
        {
            flowLayoutPanel1.Controls.Clear();
        }
        public void ApplyBookItem(Panel panel, Book book)
        {
            BookItem frmBI = new BookItem(
                book, SelectedBook, AddBook)
            { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmBI.FormBorderStyle = FormBorderStyle.None;
            panel.Controls.Add(frmBI);
            frmBI.Show();
        }

        private void SelectedBook(object sender, EventArgs e)
        {
            MainMenu owner = Parent.Parent.Parent as MainMenu;
            owner.ClearPanelLoad();

            BookInfo frmBI = new BookInfo(sender as Book) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmBI.FormBorderStyle = FormBorderStyle.None;
            owner.panelLoad.Controls.Add(frmBI);
            frmBI.Show();
        }
        private void AddBook(object sender, EventArgs e)
        {
            MainMenu owner = Parent.Parent.Parent as MainMenu;
            owner.ClearPanelLoad();

            FormMyBooks frmBI = new FormMyBooks() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Book book = sender as Book;
            frmBI.FormBorderStyle = FormBorderStyle.None;
            owner.panelLoad.Controls.Add(frmBI);
            frmBI.Show();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            Firebase.Ins.onBookUpdate -= refreshOnUpdate;
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            refreshOnUpdate = (book) =>
            {
                Action action = () =>
                {
                    ClearBooks();
                    LoadBooks();
                };
                // Cross-thread action
                this.Invoke(action);
            };
            Firebase.Ins.onBookUpdate += refreshOnUpdate;
        }
    }
}
