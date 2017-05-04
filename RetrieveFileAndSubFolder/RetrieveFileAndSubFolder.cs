using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrieveFileAndSubFolder
{
    class RetrieveFileAndSubFolder
    {
        string basePath;
        string logPath;

        public RetrieveFileAndSubFolder()
        {
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["BasePath"]))
                    basePath = ConfigurationManager.AppSettings["BasePath"];

            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["LogPath"]))
                logPath = ConfigurationManager.AppSettings["LogPath"];
        }

        public void Run()
        {
            List<string> files = RetrieveFiles(basePath);

            if (files != null)
            {
                foreach (var item in files)
                {
                    string logFile = Path.Combine(logPath, String.Format("File_{0:yyyy-MM-dd}.txt", DateTime.Now));

                    using (StreamWriter writer = new StreamWriter(logFile, true))
                    {
                        writer.WriteLine(item);
                    }
                }
            }
        }

        private List<String> RetrieveFiles(string basePath)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string file in Directory.GetFiles(basePath))
                {
                    files.Add(file);
                }
                foreach (string directory in Directory.GetDirectories(basePath))
                {
                    files.AddRange(RetrieveFiles(directory));
                }
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }

            return files;
        }
    }
}
