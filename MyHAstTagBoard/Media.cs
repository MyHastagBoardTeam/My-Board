using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHAstTagBoard
{
    public class MediaContent
    {
        public Guid ID
        {
            set { value = new Guid(); }
        }
        public string Description { get; set; }
        public Bitmap[] Pictures { get; set; }
        public Uri Movie { get; set; }
    }
}
