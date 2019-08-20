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
        private DirectoryManager dm = new DirectoryManager();

        //total images in folder
        private int TOTAL_IMAGES = 0;
        //total collected image height
        private int CANVAS_HEIGHT = 0;
        //path of where files are stored
        private String PATH_LOC = "";

        //constructor
        public WebtoonBodger(String dir, String output = "")
        {
            this.PATH_LOC = dir;
            if (!output.Equals(""))
            {
                dm.setOutputFolderName(output);
            }
            dm.createLocalDir(dm.getOutputFolderName());
        }

        public void initialise(int max_height = 200, String ext = ".png")
        {
            dm.setExtension(ext);
            //if no output folder, create one
            dm.createLocalDir(dm.getOutputFolderName());

            //KABAMAMAMAMAMMABAM
            if (dm.validateFolder(dm.getOutputFolderName()))
            {
                FixHeight(mergeAll(PATH_LOC), max_height);
            }
        }

        private Boolean FixHeight(Image srcImg, int maxHeight)
        {
            //current spot to draw from
            int current_height = 0;

            //currently remaining is entire height of src image 
            int remainingHeight = srcImg.Height;

            //if entire image already fits max
            if (remainingHeight <= maxHeight)
            {
                srcImg.Save(dm.getOutputFolderName() + "/" + dm.getOutputPageName() + "00" + dm.getExtension());
                return true;
            }

            //image properties, //image height is variable
            int canvas_width = srcImg.Width;

            //create new image slots for collection
            double calc = (double) remainingHeight / maxHeight;
            int chunks = Convert.ToInt32(Math.Ceiling(calc));
            Image[] imgs = new Image[chunks];

            for (int i = 0; i < chunks; i++)
            {
                if (remainingHeight <= maxHeight)
                {
                    maxHeight = remainingHeight;
                }

                //our dimensions
                imgs[i] = new Bitmap(canvas_width, maxHeight);

                //draw onto canvas
                using (Graphics drawTool = Graphics.FromImage(imgs[i]))
                {
                    //make sure it HQ
                    drawTool.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //current spot to draw from
                    drawTool.DrawImage(srcImg, new Rectangle(0, 0, imgs[i].Width, imgs[i].Height), new Rectangle(0, current_height, imgs[i].Width, imgs[i].Height), GraphicsUnit.Pixel);
                    current_height = maxHeight + current_height;
                    remainingHeight = remainingHeight - imgs[i].Height;

                    drawTool.Flush();
                    if (dm.getExtension().ToLower().Equals(".png"))
                    {
                        imgs[i].Save(dm.getOutputFolderName() + "/page_" + i + dm.getExtension(), System.Drawing.Imaging.ImageFormat.Png);
                    }else if (dm.getExtension().ToLower().Equals(".jpg") || dm.getExtension().ToLower().Equals(".jpeg"))
                    {
                        imgs[i].Save(dm.getOutputFolderName() + "/page_" + i + dm.getExtension(), System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    
                    imgs[i].Dispose();
                    drawTool.Dispose();
                }
            }
            return true;
        }

        //merge all .png/.jpeg files in a directory into a super image
        //returns the super image
        private Image mergeAll(String dir)
        {
            //create a new set of images, where n of set is number of .png in dir
            TOTAL_IMAGES = dm.ProcessDirectory(dir).Length;
            Image[] imgs = new Image[TOTAL_IMAGES];
            //store each .png into each image
            {
                int i = 0; //index
                foreach (string fileName in dm.ProcessDirectory(dir))    //debug
                {
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
                current_height = imgs[i].Height + current_height;
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

        public String[] getQueue()
        {
            String[] q = dm.ProcessDirectory(PATH_LOC);
            return q;
        }

        public String getPathLoc()
        {
            return this.PATH_LOC;
        }

        public void setPathLoc(String loc)
        {
            this.PATH_LOC = loc;
        }
        public void setPageName(String name)
        {
            dm.setOutputPageName(name);
        }

        public void setOutPutFolderName(String name)
        {
            dm.setOutputFolderName(name);
        }

        public String getExtension()
        {
            return dm.getExtension();
        }

        public void setExtension(String ext)
        {
            dm.setExtension(ext);
        }
    }
}
