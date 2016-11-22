﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CheckExistFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<string> paths = new List<string>();    // Массив путей, где будут проверяться файлы
            List<string> masks = new List<string>();    // Массив масок по которым искать файлы
            int lifetime;       // Время существования файла после которого считать что файл старый!
            string messageForTelegram = "";
            List<string> API_KEYs = new List<string>(); // Массив масок ключей для бота @ALARMER_BOT в телеграме 

            #region Чтение ini файла



            #endregion

            #region Тестовые переменные

            paths.Add(@"C:\Users\Ilya\Desktop");
            masks.Add(@"Bon_*.xml");
            lifetime = 900;
            API_KEYs.Add("285142-c99e91-e12a5a");

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
                            messageForTelegram += "Файл: \"" + dir + "\" существует " + Helper.TimeInText(ts) + "\n";
                        }

                    }
                }
            }

            foreach (string API_KEY in API_KEYs)
            {
                MyLog.Telega(messageForTelegram, API_KEY);
            }

        }
    }
}
