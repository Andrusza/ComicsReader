using System.IO;
using System.Windows;
using SharpCompress.Reader;
using System.Collections.Generic;
using System;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string[] drives = Environment.GetLogicalDrives();
        List<string> mediaExtensions = new List<string> { ".cbr" };
        List<string> filesFound = new List<string>();

        void DirSearch(string sDir)
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

        public void ReadFromRar()
        {
            using (Stream stream = File.OpenRead(@"C:\a.cbr"))
            {
                var reader = ReaderFactory.Open(stream, SharpCompress.Common.Options.KeepStreamsOpen);

                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        int lenght = (int)reader.Entry.Size;
                        using (var entryStream = reader.OpenEntryStream())
                        {
                            byte[] b;
                            using (BinaryReader br = new BinaryReader(entryStream))
                            {
                                b = br.ReadBytes(lenght);
                                entryStream.SkipEntry();
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DirSearch("D:\\Batman (Contempory, Chronological & Compiled)");
        }
    }
}