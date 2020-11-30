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
    public partial class TagItem : Form
    {
        public string tagName;
        public TagItem(string tagName)
        {
            this.tagName = tagName;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }

        public void label1_Resize(object sender, EventArgs e)
        {
            button1.Location = new Point(label1.Width + label1.Location.X + 20, button1.Location.Y);
            Size = new Size(button1.Location.X + button1.Width + 20 , button1.Height + 12);
        }
    }
}
