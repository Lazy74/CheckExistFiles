using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace CheckExistFiles
{
    class MyLog
    {
        static string logName = "Log.txt";
        //static string path = Directory.GetCurrentDirectory() + @"\Log";
        static string path = @"C:\Log\RebootRouter\";
        static readonly string PathCombine = Path.Combine(path, logName);

        /// <summary>
        /// Информационное сообщение
        /// </summary>
        /// <param name="str">Текст сообщения</param>
        public static void Info(string str)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string textMessage = "> " + DateTime.Now + " Info: " + str;

            try
            {
                File.AppendAllText(PathCombine, textMessage + Environment.NewLine);
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Не удалось сделать лог запись! Приложение будет закрыто\n" +
                    e.Message);

                System.Threading.Thread.CurrentThread.Abort();
            }
        }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        /// <param name="str">Текст сообщения</param>
        public static void Error(string str)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string textMessage = "> " + DateTime.Now + " ERROR: " + str;

            try
            {
                File.AppendAllText(PathCombine, textMessage + Environment.NewLine);
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Не удалось сделать лог запись! Приложение будет закрыто\n" +
                    e.Message);

                System.Threading.Thread.CurrentThread.Abort();
            }
        }

        /// <summary>
        /// Сообщение в телеграм боту: @alarmer_bot
        /// </summary>
        /// <param name="str">Текст сообщения</param>
        public static bool Telega(string str, string api_key)
        {
            string addr = @"alarmerbot.ru/?key=" + api_key + "&message=" + str;
            var webRequest = WebRequest.Create(@"http://" + addr);
            webRequest.Method = "HEAD";

            try
            {
                using (webRequest.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException ex)
            {
                return false;
            }
        }
    }
}
