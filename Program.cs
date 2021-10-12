using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashaBuildSystem
{
    class Program
    {

        static void Main(string[] args)
        {
            var exeName = "run.exe";
            try
            {
                Directory.Delete("./build", true);
            }
            catch
            { }
            Directory.CreateDirectory("./build");

            File.Copy("./test.exe", "./build/" + exeName);

            var files = Directory.GetFiles("./src/");

            foreach (var item in files)
            {
                if (item.EndsWith(".py"))
                {
                    using (FileStream zipToOpen = new FileStream(@"./build/" + exeName, FileMode.Open))
                    {
                        using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                        {
                            ZipArchiveEntry readmeEntry = archive.CreateEntry(Path.GetFileName(item));
                            Stream stream = readmeEntry.Open();

                            stream.Write(File.ReadAllBytes(item), 0, File.ReadAllBytes(item).Length);
                        }
                    }
                }
            }


        }



    }
}
