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
    public partial class FormSignIn : Form
    {
        public FormSignIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var task = Database.Load(textBox1.Text, textBox2.Text);
            task.Wait();
            if (task.Result)
            {
                FormMain main = new FormMain();
                main.Show(this);
                main.FormClosing += (obj, args) => this.Visible = true;
                this.Visible = false;
            }
        }
    }

    public partial class FormMain : Form { }
}
