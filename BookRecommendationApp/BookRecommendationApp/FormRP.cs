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
    public partial class FormRP : Form
    {
        Book currentBook;
        public FormRP(Book book)
        {
            InitializeComponent();
            currentBook = book;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                currentBook.BaoCao.Add(new Comment { Content = checkBox1.Text, Uid = Database.User.Uid });
            if (checkBox2.Checked)
                currentBook.BaoCao.Add(new Comment { Content = checkBox2.Text, Uid = Database.User.Uid });
            if (checkBox3.Checked)
                currentBook.BaoCao.Add(new Comment { Content = checkBox3.Text, Uid = Database.User.Uid });

            if (richTextBox1.Text != null && richTextBox1.Text != "")
                currentBook.BaoCao.Add(new Comment { Content = richTextBox1.Text, Uid = Database.User.Uid });

            Database.Edit(currentBook);

            MessageBox.Show("Đã gửi báo cáo!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
