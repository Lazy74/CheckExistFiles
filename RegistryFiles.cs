using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckExistFiles
{
    class RegistryFiles
    {
        static List<string> OldRegistryFilesList = new List<string>();
        static List<string> registryFilesList = new List<string>();

        // TODO Придумать куда засунуть этот ненужный файл
        string path = Path.GetTempPath() + "RegistryFilesDB.txt";

        public RegistryFiles()
        {
            try
            {
                OldRegistryFilesList = File.ReadAllLines(path).ToList();
            }
            catch (Exception)
            {
                File.Create(path);
                return;
            }
        }

        public void Add(string dir)
        {
            registryFilesList.Add(dir);
        }

        public void Save()
        {
            //File.Create(path);
            File.WriteAllLines(path, registryFilesList);
            //File.AppendAllText
        }

        public bool Exists(string dir)
        {
            return OldRegistryFilesList.Exists(s => s == dir);
        }

        public string GetListDeletFiles()
        {
            if (registryFilesList.Count == 0 && OldRegistryFilesList.Count !=0)
            {
                return "Все файлы были удалены";
            }

            string result = "Список файлов которые были успешно удалены:\n";

            foreach (string oldDir in OldRegistryFilesList)
            {
                if (!registryFilesList.Exists(s => s == oldDir))
                {
                    result += "\n"+oldDir;
                }
            }

            if (result == "Список файлов которые были успешно удалены:\n")
            {
                result = null;
            }

            return result;
        }
    }
}
