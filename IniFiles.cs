using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace OnlineTTsevr
{
    class IniFile
    {
        string Path; //Имя файла.

        [DllImport("kernel32")] // Подключаем kernel32.dll и описываем его функцию WritePrivateProfilesString
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32")] // Еще раз подключаем kernel32.dll, а теперь описываем функцию GetPrivateProfileString
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        /// <summary>
        /// С помощью конструктора записываем его имя.
        /// </summary>
        /// <param name="IniPath">Имя файла</param>
        public IniFile(string IniPath)
        {
            Path = new FileInfo(IniPath).FullName.ToString();
        }

        /// <summary>
        /// Читаем ini-файл и возвращаем значение указного ключа из заданной секции.
        /// </summary>
        /// <param name="Section">Секция</param>
        /// <param name="Key">Ключ</param>
        /// <returns></returns>
        public string ReadINI(string Section, string Key)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        /// <summary>
        /// Записываем в ini-файл. Запись происходит в выбранную секцию в выбранный ключ.
        /// </summary>
        /// <param name="Section">Секция ключей</param>
        /// <param name="Key">Ключ</param>
        /// <param name="Value">Значение</param>
        public void Write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, Path);
        }

        /// <summary>
        /// Удаляем ключ из выбранной секции.
        /// </summary>
        /// <param name="Key">Ключ</param>
        /// <param name="Section">Секция ключей</param>
        public void DeleteKey(string Key, string Section = null)
        {
            Write(Section, Key, null);
        }

        /// <summary>
        /// Удаляем выбранную секцию
        /// </summary>
        /// <param name="Section">Секция</param>
        public void DeleteSection(string Section = null)
        {
            Write(Section, null, null);
        }

        /// <summary>
        /// Проверяем, есть ли такой ключ, в этой секции
        /// </summary>
        /// <param name="Key">Ключ</param>
        /// <param name="Section">Секция</param>
        /// <returns></returns>
        public bool KeyExists(string Key, string Section = null)
        {
            return ReadINI(Section, Key).Length > 0;
        }
    }
}