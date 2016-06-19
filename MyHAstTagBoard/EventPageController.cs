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

        public EventPageController(EventInfoWindow window)
        {
            _eventWindow = window;
        }
    }
}
