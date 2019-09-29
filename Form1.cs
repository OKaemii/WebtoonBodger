using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;

namespace WebtoonBodge
{
    public partial class Form1 : Form
    {
        readonly WebtoonBodger wb = new WebtoonBodger(System.IO.Directory.GetCurrentDirectory());
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
			RB_isPNG.Checked = true;
			RB_isJPG.Checked = false;
        }

        private void BTN_refresh_Click(object sender, EventArgs e)
        {
            LBX_images.Items.Clear();

            foreach (String entry in wb.dm.ProcessDirectory(Directory.GetCurrentDirectory()))
            {
                LBX_images.Items.Add(entry);
            }

            Console.WriteLine(wb.dm.extension);
        }

        private void BTN_init_Click(object sender, EventArgs e)
        {
            wb.Initialise(Convert.ToInt32(TB_maxHeight.Text), wb.dm.extension);
            BTN_init.Enabled = false;
            MessageBox.Show("Operation est fini");
        }

        private void RB_isJPG_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_isJPG.Checked)
            {
                wb.dm.extension = ".jpg";
                return;
            }
        }

        private void RB_isPNG_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_isPNG.Checked)
            {
                wb.dm.extension = ".png";
                return;
            }
        }
    }
}
