using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrary
{
    class Settings : Base<Settings>
    ////потокобезопасный singleton. доделать замену настроек при подключении к другому аккаунту с помощью костыля или неведомой благодати
    {
        private static volatile Settings instance = null;
        private static object syncRoot = new Object();
        [SaveAttribute]
        private bool windowedModeOn { get; set; }
        private Settings(bool p_windowedModeOn = false)
        {
            windowedModeOn = p_windowedModeOn;
        }
        public static Settings Instance
        {
            get
            {
                Thread.Sleep(500);
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Settings();
                    }
                }
                return instance;
            }
        }
    }
}


