using System;

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
            _eventWindow.Browser.Address = _HTMLpageUrl.ToString();

        }
    }
}
