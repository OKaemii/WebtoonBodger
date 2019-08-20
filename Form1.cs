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

namespace WebtoonBodge
{
    public partial class Form1 : Form
    {
        WebtoonBodger wb = new WebtoonBodger(System.IO.Directory.GetCurrentDirectory());
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "refresh";
            button2.Text = "CONVERT!";
            label1.Text = "max height";
            textBox1.Text = "1200";
            radioButton1.Checked = true;

            radioButton1.Text = ".png";
            radioButton2.Text = ".jpeg";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            foreach (String entry in wb.getQueue())
            {
                listBox1.Items.Add(entry);
            }

            Console.WriteLine(wb.getExtension());
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            wb.initialise(Convert.ToInt32(textBox1.Text), wb.getExtension());
            button2.Enabled = false;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                wb.setExtension(".jpg");
                return;
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                wb.setExtension(".png");
                return;
            }
        }
    }
}
