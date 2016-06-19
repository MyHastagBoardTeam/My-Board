using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MyHAstTagBoard
{
    public class EventPageController : INotifyPropertyChanged
    {
        private EventInfoWindow _eventWindow;
        private Uri _HTMLpageUrl;
        public event PropertyChangedEventHandler PropertyChanged;
        public EventPageController(EventInfoWindow window, Uri htmlPageUrl)
        {
            _eventWindow = window;
            _HTMLpageUrl = htmlPageUrl;
            _eventWindow.Browser.Address = _HTMLpageUrl.ToString();

        }
    }
}
