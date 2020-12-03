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
    public partial class AddBooks : Form
    {
        List<string> tagList = new List<string>();
        public AddBooks()
        {
            InitializeComponent();

            comboBox1.DataSource = Database.Tags;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book.Name = textBox1.Text;
            book.Author = textBox2.Text;
            book.Link = textBox3.Text;
            book.Description = richTextBox1.Text;
            book.Tags = tagList;
            Picture pic = new Picture(picture.ImageLocation);
            pic.LoadContent();
            pic.SetNewName();
            book.PictureFile = pic.FilePath;

           

            Database.Add(book);
            Database.Add(pic);

            MessageBox.Show("Thêm thành công");
            clear();
        }
        void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            richTextBox1.Text = "";
            this.picture.Image = global::BookRecommendationApp.Properties.Resources._130304;
        }

        private void picture_Click_1(object sender, EventArgs e)
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
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddBooks_Load(object sender, EventArgs e)
        {

        }

        Task toResumeFlowPanelLayout = null;
        private async Task ResumeFlowPanelLayout(TagItem tagItem)
        {
            await Task.Delay(100);
            flowLayoutPanel1.Controls.Add(tagItem);
            toAdd.Remove(tagItem);
        }
        HashSet<TagItem> toAdd = new HashSet<TagItem>();
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TagItem tagItem = new TagItem(comboBox1.SelectedItem.ToString())
            { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            
            tagItem.FormBorderStyle = FormBorderStyle.None;
            toAdd.Add(tagItem);
            tagItem.Show();
            toResumeFlowPanelLayout?.Dispose();
            toResumeFlowPanelLayout = ResumeFlowPanelLayout(tagItem);

            tagList.Add(comboBox1.SelectedItem.ToString());
            tagItem.SizeChanged += (obj, arg) => flowLayoutPanel1.Invalidate();
        }
    }
}
