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
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
            catch (Exception)
            {
                MessageBox.Show("An Error Occured","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
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
        
    }
}
