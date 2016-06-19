using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHAstTagBoard
{
    public class EventPageController
    {
        private EventInfoWindow _eventWindow;
        private Uri _HTMLpageUrl;

        public EventPageController(EventInfoWindow window, Uri htmlPageUrl)
        {
            _eventWindow = window;
            _HTMLpageUrl = htmlPageUrl;
            _eventWindow.EventPageHTML.Source = _HTMLpageUrl;
            _eventWindow.EventPageHTML.Source = htmlPageUrl;
        }
    }
}
