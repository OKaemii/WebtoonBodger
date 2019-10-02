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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
			RB_isPNG.Checked = true;
			RB_isJPG.Checked = false;

            dm.createLocalDir(dm.outputFolderName);
        }

        private void BTN_refresh_Click(object sender, EventArgs e)
        {
            LBX_images.Items.Clear();

            foreach (String entry in dm.ProcessDirectory(Directory.GetCurrentDirectory()))
            {
                LBX_images.Items.Add(entry);
            }

            Console.WriteLine(dm.extension);
        }

        private void BTN_init_Click(object sender, EventArgs e)
        {
            Initialise(Convert.ToInt32(TB_maxHeight.Text), dm.extension);
            BTN_init.Enabled = false;
            MessageBox.Show("Operation est fini");
        }

        private void RB_isJPG_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_isJPG.Checked)
            {
                dm.extension = ".jpg";
                return;
            }
        }

        private void RB_isPNG_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_isPNG.Checked)
            {
                dm.extension = ".png";
                return;
            }
        }

        private void BTN_UpArrow_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        public void MoveItem(int direction)
        {
            // Checking selected item
            if (LBX_images.SelectedItem == null || LBX_images.SelectedIndex < 0)
                return; // No selected item - nothing to do

            // Calculate new index using move direction
            int newIndex = LBX_images.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= LBX_images.Items.Count)
                return; // Index out of range - nothing to do

            object selected = LBX_images.SelectedItem;

            // Removing removable element
            LBX_images.Items.Remove(selected);
            // Insert it in new position
            LBX_images.Items.Insert(newIndex, selected);
            // Restore selection
            LBX_images.SetSelected(newIndex, true);
        }

        private void BTN_DownArrow_Click(object sender, EventArgs e)
        {
            MoveItem(1);
        }

        readonly DirectoryManager dm = new DirectoryManager();

        //total images in folder
        private int TOTAL_IMAGES = 0;
        //total collected image height
        private int CANVAS_HEIGHT = 0;
        //path of where files are stored
        private readonly string PATH_LOC = System.IO.Directory.GetCurrentDirectory();


        public void Initialise(int max_height, string ext = ".png")
        {
            dm.extension = ext;
            //if no output folder, create one
            dm.createLocalDir(dm.outputFolderName);

            //KABAMAMAMAMAMMABAM
            if (dm.validateFolder(dm.outputFolderName))
            {
                FixHeight(MergeAll(PATH_LOC), max_height);
            }
        }

        private void FixHeight(Bitmap srcImg, int maxHeight)
        {
            int page = 0;
            Console.WriteLine("To do: {0}", srcImg.Height);

            //current spot to draw from
            int currentHeight = 0;

            int pageNumber = 0;

            //currently remaining is entire height of src image 
            int remainingHeight = srcImg.Height - 1;

            //if entire image already fits max
            if (remainingHeight <= maxHeight)
            {
                srcImg.Save(dm.outputFolderName + "/" + dm.outputPageName + "00" + dm.extension);
                srcImg.Dispose(); // GDI+ Encountered a generic error
                return;
            }

            //image properties, //image height is variable
            int canvas_width = srcImg.Width;

            while (remainingHeight > 0)
            {
                if (remainingHeight <= maxHeight)
                {
                    maxHeight = remainingHeight;
                }
                //current bottom line
                int cutHeight = currentHeight + maxHeight;
                Console.WriteLine("Cutting page {0}, current{1}, cutting_from{2}", page, currentHeight, cutHeight);
                if (maxHeight != remainingHeight)
                {
                    //while it's a panel
                    while (DoesLineContainDifferentColours(srcImg, cutHeight))
                    {
                        cutHeight--;
                        if (cutHeight == currentHeight + (maxHeight / 2))
                        {
                            cutHeight = currentHeight + maxHeight;
                            for (int i = cutHeight; i < srcImg.Height - 1 && DoesLineContainDifferentColours(srcImg, cutHeight); i++)
                            {
                                if ((cutHeight) == srcImg.Height)
                                {
                                    break;
                                }
                                cutHeight = i;
                            }
                            break;
                        }
                    }
                }

                //our dimensions
                Bitmap img = new Bitmap(canvas_width, cutHeight - currentHeight);

                //draw onto canvas
                using (Graphics drawTool = Graphics.FromImage(img))
                {
                    //make sure it HQ
                    drawTool.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //current spot to draw from
                    Console.WriteLine("creating image");
                    drawTool.DrawImage(srcImg, new Rectangle(0, 0, img.Width, img.Height), new Rectangle(0, currentHeight, img.Width, img.Height), GraphicsUnit.Pixel);
                    Console.WriteLine("image created");
                    currentHeight += img.Height;
                    remainingHeight -= img.Height;

                    for (int i = page; i<entriesH.Length; i++)
                    {
                        if (entriesH[page] > currentHeight)
                        {
                            break;
                        }
                        page++;
                    }

                    
                    drawTool.Flush();
                    if (dm.extension.ToLower().Equals(".png"))
                    {
                        img.Save(dm.outputFolderName + "/" + Path.GetFileName(LBX_images.Items[page].ToString()) + (pageNumber++).ToString("00") + dm.extension, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    else if (dm.extension.ToLower().Equals(".jpg") || dm.extension.ToLower().Equals(".jpeg"))
                    {
                        img.Save(dm.outputFolderName + "/" + Path.GetFileName(LBX_images.Items[page].ToString()) + (pageNumber++).ToString("00") + dm.extension, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    img.Dispose();
                    drawTool.Dispose();
                }
            }
        }

        public int[] entriesH;

        //merge all .png/.jpeg files in a directory into a super image
        //returns the super image
        private Bitmap MergeAll(string dir)
        {
            //create a new set of images, where n of set is number of .png in dir
            TOTAL_IMAGES = dm.ProcessDirectory(dir).Length;
            Image[] imgs = new Image[TOTAL_IMAGES];

            entriesH = new int[LBX_images.Items.Count];

            //store each .png into each image
            {
                int i = 0; //index
                foreach (object fileName in LBX_images.Items)    //debug
                {
                    Console.WriteLine("merging {0}", fileName);
                    imgs[i] = Image.FromFile(fileName.ToString());
                    entriesH[i] = imgs[i].Height;
                    //get the total height of all images
                    CANVAS_HEIGHT = imgs[i].Height + CANVAS_HEIGHT;
                    i++; //next img
                }
            }

            if (entriesH.Length != 0)
            {
                for (int i = 0; i < entriesH.Length - 1; i++)
                {
                    entriesH[i + 1] = entriesH[i] + entriesH[i + 1];
                }
            }

            //declare master canvas
            int canvas_width = imgs[0].Width;
            int canvas_height = CANVAS_HEIGHT;

            //our dimensions
            Bitmap canvas = new Bitmap(canvas_width, canvas_height);

            //draw onto
            Graphics drawTool = Graphics.FromImage(canvas);

            //make sure it HQ
            drawTool.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            //current spot to draw from
            int current_height = 0;
            for (int i = 0; i < TOTAL_IMAGES; i++)
            {
                drawTool.DrawImage(imgs[i], new Rectangle(0, current_height, imgs[i].Width, imgs[i].Height), new Rectangle(0, 0, imgs[i].Width, imgs[i].Height), GraphicsUnit.Pixel);
                current_height += imgs[i].Height;
            }

            drawTool.Flush();
            drawTool.Save();
            //dispose all images used
            foreach (Image img in imgs)
            {
                img.Dispose();
            }

            return canvas;
        }

        private bool DoesLineContainDifferentColours(Bitmap bmp, int lineNumber)
        {
            int threshold = 25;
            Color lineColor = bmp.GetPixel(0, lineNumber);
            for (int x = 1; x < bmp.Width; x++)
            {
                Color currentPixel = bmp.GetPixel(x, lineNumber);
                if (Math.Abs(lineColor.R - currentPixel.R) > threshold ||
                    Math.Abs(lineColor.G - currentPixel.G) > threshold ||
                    Math.Abs(lineColor.B - currentPixel.B) > threshold)
                {
                    return true;
                }
            }
            return false;
        }
    }


}
