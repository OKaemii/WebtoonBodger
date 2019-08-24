using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebtoonBodge
{
    class WebtoonBodger
    {
        public DirectoryManager dm = new DirectoryManager();

        //total images in folder
        private int TOTAL_IMAGES = 0;
        //total collected image height
        private int CANVAS_HEIGHT = 0;
        //path of where files are stored
        private string PATH_LOC = "";

        //constructor
        public WebtoonBodger(string dir, string output = "")
        {
            this.PATH_LOC = dir;
            if (!output.Equals(""))
            {
                dm.outputFolderName = output;
            }
            dm.createLocalDir(dm.outputFolderName);
        }

        public void initialise(int max_height, string ext = ".png")
        {
            dm.extension = ext;
            //if no output folder, create one
            dm.createLocalDir(dm.outputFolderName);

            //KABAMAMAMAMAMMABAM
            if (dm.validateFolder(dm.outputFolderName))
            {
                FixHeight(mergeAll(PATH_LOC), max_height);
            }
        }

        private void FixHeight(Bitmap srcImg, int maxHeight)
        {
            Console.WriteLine("To do: {0}", srcImg.Height);
            //page counter
            int pageNumber = 0;
            //current spot to draw from
            int currentHeight = 0;

            //currently remaining is entire height of src image 
            int remainingHeight = srcImg.Height -1;

            //if entire image already fits max
            if (remainingHeight <= maxHeight)
            {
                srcImg.Save(dm.outputFolderName + "/" + dm.outputPageName + "00" + dm.extension);
                srcImg.Dispose(); // GDI+ Encountered a generic error
                return;
            }

            //image properties, //image height is variable
            int canvas_width = srcImg.Width;

            ////create new image slots for collection
            //double calc = (double) remainingHeight / maxHeight;
            //int chunks = Convert.ToInt32(Math.Ceiling(calc));
            //Image[] imgs = new Image[chunks];
            while (remainingHeight > 0)
            {
                if (remainingHeight <= maxHeight)
                {
                    maxHeight = remainingHeight;
                }
                //current bottom line
                int cutHeight = currentHeight + maxHeight;
                Console.WriteLine("Cutting page {0}, current{1}, cutting_from{2}", pageNumber, currentHeight, cutHeight);
                //while it's a panel
                while (!isLineOneColour(srcImg, cutHeight))
                {
                    cutHeight--;
                    if (cutHeight == currentHeight + (maxHeight/2))
                    {
                        cutHeight = currentHeight + maxHeight;
                        for (int i = cutHeight; i < srcImg.Height - 1 && !isLineOneColour(srcImg, cutHeight) ; i++)
                        {
                            cutHeight = i;
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

                    drawTool.Flush();
                    if (dm.extension.ToLower().Equals(".png"))
                    {
                        img.Save(dm.outputFolderName + "/page_" + pageNumber++ + dm.extension, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    else if (dm.extension.ToLower().Equals(".jpg") || dm.extension.ToLower().Equals(".jpeg"))
                    {
                        img.Save(dm.outputFolderName + "/page_" + pageNumber++ + dm.extension, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    img.Dispose();
                    drawTool.Dispose();
                }
            }
        }

        //merge all .png/.jpeg files in a directory into a super image
        //returns the super image
        private Bitmap mergeAll(string dir)
        {
            //create a new set of images, where n of set is number of .png in dir
            TOTAL_IMAGES = dm.ProcessDirectory(dir).Length;
            Image[] imgs = new Image[TOTAL_IMAGES];
            //store each .png into each image
            {
                int i = 0; //index
                foreach (string fileName in dm.ProcessDirectory(dir))    //debug
                {
                    Console.WriteLine("merging {0}", fileName);
                    imgs[i] = Image.FromFile(fileName);

                    //get the total height of all images
                    CANVAS_HEIGHT = imgs[i].Height + CANVAS_HEIGHT;
                    i++; //next img
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

        private bool isLineOneColour(Bitmap bmp, int lineNumber)
        {
            //int threshold = 2;
            //Color lineColor = bmp.GetPixel(0, lineNumber);
            //for (int x = 1; x < bmp.Width; x++)
            //{
            //    Color currentPixel = bmp.GetPixel(x, lineNumber);
            //    if (Math.Abs(lineColor.R - currentPixel.R) < threshold && Math.Abs(lineColor.G - currentPixel.G) <threshold && Math.Abs(lineColor.B - currentPixel.B) < threshold)
            //    {
            //        return false;
            //    }
            //}
            //return true;
            Color lineColor = bmp.GetPixel(0, lineNumber);
            for (int x = 1; x < bmp.Width; x++)
            {
                Color currentPixel = bmp.GetPixel(x, lineNumber);
                if (lineColor != currentPixel)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
