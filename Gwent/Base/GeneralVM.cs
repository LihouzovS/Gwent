namespace Gwent.Base
{
    using GwentClasses.Classes;
    using ClassLibrary.Context;
    using System;
    public class GeneralVM : GwentViewModelBase
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
        private DataContext _Context;
        public DataContext Context
        {
            get
            {
                if (_Context == null)
                    _Context = new DataContext();
                return _Context;
            }
        }

    }
}
