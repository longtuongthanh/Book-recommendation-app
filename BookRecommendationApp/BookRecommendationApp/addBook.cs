using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookRecommendationApp.Model;

namespace BookRecommendationApp
{
    public partial class addBook : UserControl
    {
        public Action<object, EventArgs> SizeChange { get; }

        public string PDName
        {
            get { return GetPDName(); }
            set { SetPDName(value); }
        }
        public string GetPDName() { return textBox1.Text; }
        public void SetPDName(string value)
        {
            textBox1.Text = value.Trim();
            Invalidate();
        }
        public string PDAthor
        {
            get { return GetPDAthor(); }
            set { SetPDAthor(value); }
        }
        public string GetPDAthor() { return textBox2.Text; }
        public void SetPDAthor(string value)
        {
            textBox2.Text = value.Trim();
            Invalidate();
        }
        public string PDDescription
        {
            get { return GetPDDescription(); }
            set { SetPDDescription(value); }
        }
        public string GetPDDescription() { return textBox3.Text; }
        public void SetPDDescription(string value)
        {
            textBox3.Text = value.Trim();
            Invalidate();
        }
        public addBook()
        {
            InitializeComponent();
            
        }

        private void picture_Click(object sender, EventArgs e)
        {
            string imageLocations = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg file(*.jpg)|*.jpg| PNG file(*.png)|*png| All Files(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocations = dialog.FileName;
                    picture.ImageLocation = imageLocations;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Database.PostError(er);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
