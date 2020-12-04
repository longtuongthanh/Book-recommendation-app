using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRecommendationApp
{
    public partial class cmt : UserControl
    {
        
        
        public cmt()
        {          
            InitializeComponent();
        }
        public void hienthi(string abc)
        {
            FormAcc n = new FormAcc();
            label1.Text = abc;
            pictureBox1.ImageLocation = n.pictureBox1.ImageLocation;
        }
    }
}
