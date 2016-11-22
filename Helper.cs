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

        public static List<string> ReadIniPathList()
        {
            int i = 1;
            List<string> pathList = new List<string>();

            string readPath = _iniFile.ReadINI("Path", i.ToString());
            while (readPath.Length > 0)
            {
                pathList.Add(readPath);
                i++;
                readPath = _iniFile.ReadINI("Path", i.ToString());
            }
            return pathList;
        }

        public static List<string> ReadIniMasksList()
        {
            int i = 1;
            List<string> maskList = new List<string>();

            string readMask = _iniFile.ReadINI("Mask", i.ToString());
            while (readMask.Length > 0)
            {
                maskList.Add(readMask);
                i++;
                readMask = _iniFile.ReadINI("Mask", i.ToString());
            }
            return maskList;
        }

        public static List<string> ReadIniApiKeyList()
        {
            int i = 1;
            List<string> ApiKeyList = new List<string>();

            string readApiKey = _iniFile.ReadINI("API_KEY", i.ToString());
            while (readApiKey.Length > 0)
            {
                ApiKeyList.Add(readApiKey);
                i++;
                readApiKey = _iniFile.ReadINI("API_KEY", i.ToString());
            }
            return ApiKeyList;
        }

        public static string ReadIniMessageForTelegram()
        {
            // TODO Проверить что будет если нет такой секции или ключа! 
            return _iniFile.ReadINI("Message", "message");
        }

        public static int ReadIniLifetime()
        {
            try
            {
                return Convert.ToInt32(_iniFile.ReadINI("Time", "lifetime"));
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
