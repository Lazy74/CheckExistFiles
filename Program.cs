using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckExistFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistryFiles registryFiles = new RegistryFiles();

            List<string> paths = new List<string>();    // Массив путей, где будут проверяться файлы
            List<string> masks = new List<string>();    // Массив масок по которым искать файлы
            int lifetime = 0;                           // Время существования файла после которого считать что файл старый! Время в СЕКУНДАХ
            string messageForTelegram = "";             // Префикс к сообщению в телеграме
            List<string> API_KEYs = new List<string>(); // Массив масок ключей для бота @ALARMER_BOT в телеграме 

            #region Чтение ini файла


            API_KEYs = Helper.ReadIniApiKeyList();
            if (API_KEYs == null)
            {
                MyLog.Info("Программа будет завершена досрочно!");
                return;
            }

            paths = Helper.ReadIniPathList();
            if (paths == null)
            {
                foreach (var apiKeY in API_KEYs)
                {
                    MyLog.Telega("Не найден список путей к файлам! Программа будет завершена", apiKeY);
                }
                return;
            }

            masks = Helper.ReadIniMasksList();
            if (masks == null)
            {
                foreach (var apiKeY in API_KEYs)
                {
                    MyLog.Telega("Не найден список масок к файлам! Программа будет завершена", apiKeY);
                }
                return;
            }

            lifetime = Helper.ReadIniLifetime();
            if (lifetime == 0)
            {
                foreach (var apiKeY in API_KEYs)
                {
                    MyLog.Telega("Ошибка чтения времени. Для выполнения программы будет принято значение lifetime=300", apiKeY);
                }
                lifetime = 300;
            }

            #endregion

            #region Тестовые переменные

            //paths.Add(@"C:\Users\Ilya\Desktop");
            //masks.Add(@"Bon_*.xml");
            //masks.Add(@"");
            //lifetime = 900;
            //API_KEYs.Add("285142-c99e91-e12a5a");

            #endregion

            foreach (string path in paths)
            {
                foreach (string mask in masks)
                {
                    List<string> dirs = Directory.GetFiles(path, mask).ToList();

                    foreach (string dir in dirs)
                    {
                        DateTime timeCreateFile = File.GetCreationTime(dir);
                        TimeSpan ts = DateTime.Now - timeCreateFile;
                        if (ts.TotalSeconds > lifetime)
                        {
                            if (!registryFiles.Exists(dir))
                            {
                                messageForTelegram += "***\nФайл: \"" + dir + "\" существует " + Helper.TimeInText(ts) + "\n\n";
                            }
                            registryFiles.Add(dir);
                        }
                    }
                }
            }

            registryFiles.Save();

            foreach (string API_KEY in API_KEYs)
            {
                MyLog.Telega(messageForTelegram, API_KEY);
            }

            messageForTelegram = registryFiles.GetListDeletFiles();

            if (messageForTelegram != null)
            {
                foreach (string API_KEY in API_KEYs)
                {
                    MyLog.Telega(messageForTelegram, API_KEY);
                }
            }

        }
    }
}
