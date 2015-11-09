using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHAstTagBoard
{
    public abstract class Event
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid ID
        {
            get
            {
                return new Guid();
            }
        }
        public List<Media> MediaFiles { get; set; }
        public abstract Dictionary<string, string> categories { get; }
    }
}
