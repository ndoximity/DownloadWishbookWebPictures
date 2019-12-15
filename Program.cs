using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;

namespace DownloadPicturesFromWishbookWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Press any key and then press ENTER to begin.");
                Console.ReadLine();

                Console.WriteLine("Starting file read... ");

                // Make sure the "AllWishbookWebCatalogPageLinks.txt" is located in the same directory as the binary (bin\Debug or bin\Release for most folks).
                IEnumerable<String> wishbookWebLinks = File.ReadAllLines("AllWishbookWebCatalogPageLinks.txt");

                // Show total number of lines in text file.
                Console.WriteLine("Read " + wishbookWebLinks.Count().ToString() + " lines. Press any key to continue.");

                Console.WriteLine("Starting download of pictures... ");

                foreach (string strLink in wishbookWebLinks)
                {
                    string[] words = strLink.Split('/');    // Split each line on the forward-slash
                    string strCatalogName = words[4].ToString().Trim();     // The 5th (index number 4) is the catalog name.

                    // words[words.Length - 1] is the last in the index of the line (of the string array).  That is the page name, which is a JPG filename.
                    Console.WriteLine("Downloading: Catalog: " + strCatalogName + ", Page: " + words[words.Length - 1].ToString());
                    using (var client = new WebClient())
                    {
                        if (!Directory.Exists(strCatalogName))  // If a directory does not exist by the catalog name
                        {
                            Directory.CreateDirectory(strCatalogName);  // Creating a directory or folder from where the binary is run.
                        }
                        client.DownloadFile(strLink, strCatalogName + "\\" + words[words.Length - 1].ToString());   // Download the file into the catalog directory.
                    }
                }

                // Wrapping up.
                Console.WriteLine("Ended download of pictures. ");

                // All done!
                Console.WriteLine("All Complete! Press any key to end this program.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message.ToString());
                Console.ReadLine();
            }            
        }
    }
}