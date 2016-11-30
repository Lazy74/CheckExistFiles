using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckExistFiles
{
    class Helper
    {
        public static IniFile _iniFile = new IniFile("CheckExistFiles.ini");

        /// <summary>
        /// Конвертировать разницу во времени в строковый формат Xд Xч Xм Xс
        /// </summary>
        /// <param name="timeSpan">Разница во времени</param>
        /// <returns></returns>
        public static string TimeInText(TimeSpan timeSpan)
        {
            string result = "";

            if (timeSpan.Days > 0)
            {
                result += timeSpan.Days + "д ";
            }
            if (timeSpan.Hours > 0)
            {
                result += timeSpan.Hours + "ч ";
            }
            if (timeSpan.Minutes > 0)
            {
                result += timeSpan.Minutes + "м ";
            }
            if (timeSpan.Seconds > 0)
            {
                result += timeSpan.Seconds + "с ";
            }

            return result;
        }

        /// <summary>
        /// Считывание списка путей к файлам
        /// </summary>
        /// <returns></returns>
        public static List<string> ReadIniPathList()
        {
            int i = 1;
            List<string> pathList = new List<string>();

            bool flag = true;

            string readPath = _iniFile.ReadINI("Path", i.ToString());
            while (readPath.Length > 0)
            {
                flag = false;

                pathList.Add(readPath);
                i++;
                readPath = _iniFile.ReadINI("Path", i.ToString());
            }

            if (flag)
            {
                MyLog.Error("Ошибка чтения файла настроек! Не найдена секция \"Path\" или нет ниодного ключа. Принято значение по умолчанию");
                _iniFile.Write("Path", i.ToString(), "C:\\dir");
                return null;
            }

            return pathList;
        }

        /// <summary>
        /// Считывание масок для поиска файла
        /// </summary>
        /// <returns></returns>
        public static List<string> ReadIniMasksList()
        {
            int i = 1;
            List<string> maskList = new List<string>();

            bool flag = true;

            string readMask = _iniFile.ReadINI("Mask", i.ToString());
            while (readMask.Length > 0)
            {
                flag = false;

                maskList.Add(readMask);
                i++;
                readMask = _iniFile.ReadINI("Mask", i.ToString());
            }

            if (flag)
            {
                MyLog.Error("Ошибка чтения файла настроек! Не найдена секция \"Mask\" или нет ниодного ключа. Принято значение по умолчанию");
                _iniFile.Write("Mask", i.ToString(), "Test*.txt");
                return null;
            }

            return maskList;
        }

        /// <summary>
        /// Считывание списка api ключей для отправки сообщений в телеграм
        /// </summary>
        /// <returns></returns>
        public static List<string> ReadIniApiKeyList()
        {
            int i = 1;
            List<string> ApiKeyList = new List<string>();

            bool flag = true;

            string readApiKey = _iniFile.ReadINI("API_KEY", i.ToString());
            while (readApiKey.Length > 0)
            {
                flag = false;

                ApiKeyList.Add(readApiKey);
                i++;
                readApiKey = _iniFile.ReadINI("API_KEY", i.ToString());
            }

            if (flag)
            {
                MyLog.Error("Ошибка чтения файла настроек! Не найдена секция \"API_KEY\" или нет ниодного ключа. Принято значение по умолчанию");
                _iniFile.Write("API_KEY", i.ToString(), "000000-000000-000000");
                return null;
            }

            return ApiKeyList;
        }

        /// <summary>
        /// Считать префикс для сообщения в телеграме
        /// </summary>
        /// <returns></returns>
        public static string ReadIniMessageForTelegram()
        {
            // TODO Проверить что будет если нет такой секции или ключа! 
            string result = _iniFile.ReadINI("Message", "message");

            if (result.Length == 0)
            {
                MyLog.Error("Ошибка чтения файла настроек! Не найден ключ \"Message\". Принято значение по умолчанию");
                _iniFile.Write("Message", "message", "*");
                result = "*";
            }

            return result + "\n";
        }

        /// <summary>
        /// Считать время жизни файлов.
        /// </summary>
        /// <returns></returns>
        public static int ReadIniLifetime()
        {
            try
            {
                return Convert.ToInt32(_iniFile.ReadINI("Time", "lifetime"));
            }
            catch (Exception)
            {
                MyLog.Error("Ошибка чтения файла настроек! Не удалось считать lifetime. Принято значение по умолчанию.");
                _iniFile.Write("Time", "lifetime", "0");
                return 0;
            }
        }
    }
}
