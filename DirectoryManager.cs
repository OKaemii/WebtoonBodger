using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebtoonBodge
{
    class DirectoryManager
    {
        private String extension = ".png";
        private String outputFolderName = "_output";
        private String outputPageName = "page_";
        public Boolean createLocalDir(String nameOfFolder)
        {
            // Specify the directory you want to manipulate.
            //path = @"c:\MyDir";
            String path = nameOfFolder + "/";

            try
            {
                if (!validateFolder(path))
                {
                    // Try to create the directory.
                    System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                    //Console.WriteLine("The directory was created successfully at {0}.", System.IO.Directory.GetCreationTime(path));
                    return true;

                    #region validateFolderDeleteFeature
                    // Delete the directory.
                    //di.Delete();
                    //Console.WriteLine("The directory was deleted successfully.");
                    #endregion
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return false;
            }
            finally { }

            return false;
        }

        // Process all .png files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.
        public string[] ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = System.IO.Directory.GetFiles(targetDirectory, "*" + extension);
            return fileEntries;
            #region ProcessDirectorySubDir
            // Recurse into subdirectories of this directory.    //currently not needed
            //string[] subdirectoryEntries = System.IO.Directory.GetDirectories(targetDirectory);
            //foreach (string subdirectory in subdirectoryEntries)
            //    ProcessDirectory(subdirectory);
            #endregion
        }

        //make sure given path is a valid folder
        public Boolean validateFolder(String path)
        {
            if (System.IO.File.Exists(path))
            {
                // This path is a file
                //Console.WriteLine(path);
                return false;
            }
            else if (System.IO.Directory.Exists(path))
            {
                // This path is a directory
                //Console.WriteLine(path);
                return true;
            }
            else
            {
                Console.WriteLine("{0} is not a valid path.", path);
                return false;
            }
        }

        public String getOutputFolderName()
        {
            return outputFolderName;
        }

        //changes the folder for output. If (name,OPTIONAL true), renames the previous folder to new
        public void setOutputFolderName(String name, Boolean rename = false)
        {
            if (rename)
            {
                System.IO.Directory.Move(outputFolderName, name);
            }
            outputFolderName = name;
        }

        public String getOutputPageName()
        {
            return outputPageName;
        }

        public void setOutputPageName(String name)
        {
            outputPageName = name;
        }

        public String getExtension()
        {
            return extension;
        }

        public Boolean setExtension(String ext)
        {
            if (ext.StartsWith("."))
            {
                Console.WriteLine("{0} ->> {1}", this.extension, ext);
                this.extension = ext;
                return true;
            }
            Console.WriteLine("error occured, not correct format");
            return false;
        }
    }
}
