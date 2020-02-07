using System;
using System.Collections.Generic;
using System.IO;

namespace SSMWarehouseManagement.Helpers
{
    /// <summary>
    /// Clase para lanzar errores al log
    /// </summary>
    public static class DirectLog
    {
        static int SAVE_PERIOD = 5 * 1000;// period=10 seconds
        static int SAVE_COUNTER = 10;// save after 1000 messages
        static int MIN_IMPORTANCE = 0;// log only messages with importance value >=MIN_IMPORTANCE
        static string DIR_LOG_FILES = Path.GetTempPath() + "SSMWarehouseManagement\\";
        static string _filename = DIR_LOG_FILES + @"Log." + DateTime.Now.ToString("yyMMdd.HHmm") + @".txt";
        static List<string> _list_log = new List<string>();
        static object _locker = new object();
        static int _counter;
        static DateTime _lastSave = DateTime.Now;

        public static void NewFile()
        {//new file is created because filename changed
            SaveToFile();
            lock (_locker)
            {

                bool exists = Directory.Exists(DIR_LOG_FILES);

                if (!exists)
                    Directory.CreateDirectory(DIR_LOG_FILES);

                _filename = DIR_LOG_FILES + @"Log." + DateTime.Now.ToString("yyMMdd.HHmm") + @".txt";

                _counter = 0;
            }
        }
        public static void Log(string logMessage, int importance)
        {
            if (importance < MIN_IMPORTANCE) return;
            lock (_locker)
            {
                _list_log.Add(String.Format("{0:HH:mm:ss.ffff},{1},{2}", DateTime.Now, logMessage, importance));
                _counter++;
            }
            TimeSpan timeDiff = DateTime.Now - _lastSave;

            if (_counter > SAVE_COUNTER || timeDiff.TotalMilliseconds > SAVE_PERIOD)
                SaveToFile();
        }

        public static void SaveToFile()
        {
            lock (_locker)
                if (_list_log.Count == 0)
                {
                    _lastSave = DateTime.Now;
                    return;
                }
            lock (_locker)
            {
                using (StreamWriter logfile = File.AppendText(_filename))
                {

                    foreach (string s in _list_log) logfile.WriteLine(s);
                    logfile.Flush();
                }

                _list_log.Clear();
                _counter = 0;
                _lastSave = DateTime.Now;
            }
        }
    }
}