using BookRecommendationApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRecommendationApp
{
    public partial class FormAcc : Form
    {
        public FormAcc()
        {
            InitializeComponent();
            Picture pic = Database.User.GetPicture();

            if (pic.GetImage() != null)
            {
                pictureBox2.Image = pic.GetImage();
                pictureBox1.Image = pic.GetImage();
            }
            else;
            load();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Database.User.Nickname = textBox10.Text;
            Database.User.GioiTinh = comboBox1.Text;
            Database.User.NgaySinh = dateTimePicker1.Value;


            Database.EditUser();
            //textBox1.Text = Database.User.Nickname;
            //textBox2.Text = Database.User.GioiTinh;
            //dateTimePicker2.Value = Database.User.NgaySinh;
        }
        void load()
        {
            
            textBox1.Text = Database.User.Nickname;
            textBox4.Text = Database.User.GioiTinh;
            if (Database.User.NgaySinh != dateTimePicker1.Value)
                dateTimePicker2.Value = DateTime.Now.Date;
            else
                dateTimePicker2.Value = Database.User.NgaySinh;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string imageLocations = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg file(*.jpg)|*.jpg| PNG file(*.png)|*png| All Files(*.*)|*.*";
                if(dialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
                {
                    imageLocations = dialog.FileName;
                    pictureBox2.ImageLocation = imageLocations;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An Error Occured","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                Database.PostError(er);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            User user = Database.User;
            Picture pic = new Picture(pictureBox2.ImageLocation);
            pic.LoadContent();
            pic.SetNewName();
            user.PictureFile = pic.FilePath;
            Database.Add(pic);
            Database.EditUser();
            pictureBox1.ImageLocation = pictureBox2.ImageLocation;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
