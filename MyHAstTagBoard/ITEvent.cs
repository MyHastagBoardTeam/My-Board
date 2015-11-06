using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHAstTagBoard
{

    public class ITEvent : Event
    {
        public List<Uri> Source = new List<Uri>();

        public enum Categories
        {
            All, NET, Agile, FrontEnd, GameDev, Hardware, HR, Java, JAvaScript, Mobile, Python, QA, Ruby, UX, Hakaton
        }

        public override Dictionary<string, string> categories
        {
            get
            {
                 return new Dictionary<string, string>()
                {
                    {"http://dou.ua/calendar/feed/", "На любую тему"},
                    {"http://dou.ua/calendar/feed/.NET/", ".NET"},
                    {"http://dou.ua/calendar/feed/agile/", "Agile"},
                    {"http://dou.ua/calendar/feed/front-end/", "Front-End"},
                    {"http://dou.ua/calendar/feed/gamedev/", "GameDev"},
                    {"http://dou.ua/calendar/feed/hardware/", "Hardware"},
                    {"http://dou.ua/calendar/feed/HR/", "HR"},
                    {"http://dou.ua/calendar/feed/Java/", "Java"},
                    {"http://dou.ua/calendar/feed/JavaScript/", "JavaScript"},
                    {"http://dou.ua/calendar/feed/mobile/", "Mobile"},
                    {"http://dou.ua/calendar/feed/Python/", "Python"},
                    {"http://dou.ua/calendar/feed/QA/", "QA"},
                    {"http://dou.ua/calendar/feed/Ruby/, ", "Ruby"},
                    {"http://dou.ua/calendar/feed/UX/", "UX"},
                    {"http://dou.ua/calendar/feed/%D0%A5%D0%B0%D0%BA%D0%B0%D1%82%D0%BE%D0%BD/", "Хакатон"}
                };
                
            }
        }
    }
}
