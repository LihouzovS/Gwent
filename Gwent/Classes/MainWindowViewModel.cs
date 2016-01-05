namespace Gwent.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ClassLibrary;

    class MainWindowViewModel : ViewModelBase
    {
        private string login { get; set; }
        private string pass { get; set; }
        private bool rememberMe { get; set; }
        private bool showPassword { get; set; }
        private bool admin { get; set; }
        
    }
}
