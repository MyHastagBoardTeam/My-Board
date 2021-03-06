﻿using System;
using System.Collections.Generic;

namespace MyHAstTagBoard
{
    public abstract class EventList
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid ID
        {
            set
            {
                value = new Guid();
            }
        }
        public List<MediaContent> MediaFiles { get; set; }
        public abstract Dictionary<string, string> categories { get; }
    }
}
