using System;
using System.Collections.Generic;
using System.IO;

namespace WpfHost
{
    public static class Search
    {
        private static string[] drives = Environment.GetLogicalDrives();
        private static List<string> mediaExtensions = new List<string> { ".cbr" };
        private static List<string> filesFound = new List<string>();

        public static void DirSearch(string sDir)
        {
            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetFiles(d, "*.*"))
                {
                    if (mediaExtensions.Contains(Path.GetExtension(f).ToLower()))
                        filesFound.Add(f);
                }
                DirSearch(d);
            }
        }
    }
}