namespace Gwent.Classes
{
    //почему оно не работает?
    using ClassLibrary;
    using Gwent.Classes;
    using System;
    using System.Collections.Generic;
    

    class GeneralVM : ViewModelBase
    {
        // запилить сюда сеттингс
        private static volatile GeneralVM instance = null;
        private static object syncRoot = new Object();
        private GeneralVM(bool p_windowedModeOn = false)
        {
            
        }
        public static GeneralVM Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GeneralVM();
                    }
                }
                return instance;
            }
        }
        private Gamer _activeGamer;
        public Gamer ActiveGamer
        {
            get { return _activeGamer; }
            set
            {
                _activeGamer = value;
                OnPropertyChanged("ActiveGamer");
            }
        }
    }
}
