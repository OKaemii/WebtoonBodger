using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebtoonBodge
{
    class DirectoryManager
    {
		public bool willRename = false;


        private string Extension = ".png";
		public string extension
		{
			get
			{
				return Extension;
			}
			set
			{
				if(!value.StartsWith(".")) return;
				Extension = value;
			}
		}

		private string OutputPageName = "page_";
		public string outputPageName
		{
			get
			{
				return OutputPageName;
			}
			set
			{
				OutputPageName = value;
			}
		}

		private string OutputFolderName = "_cropped";
		public string outputFolderName
		{
			get
			{
				return OutputFolderName;
			}
			set
			{
				if (willRename)
				{
					System.IO.Directory.Move(OutputFolderName, value);
				}
				OutputFolderName = value;
			}
		}

        public bool createLocalDir(string nameOfFolder)
        {
            // Specify the directory you want to manipulate.
            // path = @"c:\MyDir";
            String path = nameOfFolder + "/";

            try
            {
                if (!validateFolder(path))
                {
                    // Try to create the directory.
                    System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                    // Console.WriteLine("The directory was created successfully at {0}.", System.IO.Directory.GetCreationTime(path));
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

        // make sure given path is a valid folder
        public bool validateFolder(string path)
        {
            if (System.IO.File.Exists(path))
            {
                // This path is a file
                // Console.WriteLine(path);
                return false;
            }
            else if (System.IO.Directory.Exists(path))
            {
                // This path is a directory
                // Console.WriteLine(path);
                return true;
            }
            else
            {
                Console.WriteLine("{0} is not a valid path.", path);
                return false;
            }
        }
    }
}
