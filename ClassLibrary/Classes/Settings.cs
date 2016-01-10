namespace GwentClasses.Classes
{
    using System;
    public class Settings : Base<Settings>
    ////потокобезопасный singleton. доделать замену настроек при подключении к другому аккаунту с помощью костыля или небесной благодати
    {
        private static volatile Settings instance = null;
        private static object syncRoot = new Object();
        [SaveAttribute]
        private bool windowedModeOn { get; set; }
        private Settings(bool p_windowedModeOn = false)
        {
            this.windowedModeOn = p_windowedModeOn;
        }
        public static Settings Instance
        {
            get
            {
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
